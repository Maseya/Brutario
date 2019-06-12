namespace Brutario
{
    using System;

    public class RomIO
    {
        public const int DefaultHeaderSize = 0x200;

        public const int BankSize = 0x8000;

        public const int BankMask = BankSize - 1;

        public const int LoRomBankLimit = 0x80;

        public const int LoRomSizeLimit = LoRomBankLimit * BankSize;

        public const int ExLoRomBankLimit = 0xFE;

        public const int ExLoRomSizeLimit = ExLoRomBankLimit * BankSize;

        public RomIO(byte[] data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            if (HeaderSize != 0 && HeaderSize != DefaultHeaderSize)
            {
                throw new ArgumentException(
                    "Rom does not have valid header size (0 or 512 bytes).");
            }

            if (HeaderlessSize >= ExLoRomSizeLimit)
            {
                throw new ArgumentOutOfRangeException(
                    "Rom exceeds maximum allowable size.");
            }
        }

        public int Size
        {
            get
            {
                return Data.Length;
            }
        }

        public bool HasHeader
        {
            get
            {
                return HeaderSize == DefaultHeaderSize;
            }
        }

        public int HeaderSize
        {
            get
            {
                return Data.Length & BankMask;
            }
        }

        public int HeaderlessSize
        {
            get
            {
                return Size - HeaderSize;
            }
        }

        public bool IsExLoRom
        {
            get
            {
                return HeaderlessSize > LoRomSizeLimit;
            }
        }

        public byte[] Data
        {
            get;
        }

        public int SnesToPc(int snesAddress)
        {
            if (snesAddress < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(snesAddress),
                    "SNES address cannot be less than zero.");
            }

            if ((snesAddress & BankSize) == 0)
            {
                throw new ArgumentException(
                    "Invalid SNES address.");
            }

            var word = snesAddress & BankMask;

            var bank = IsExLoRom
                ? snesAddress >= 0x800000
                    ? (snesAddress & ExLoRomSizeLimit) >> 1
                    : LoRomSizeLimit | ((snesAddress & ExLoRomSizeLimit) >> 1)
                : (snesAddress & ExLoRomSizeLimit) >> 1;

            return (bank | word) + HeaderSize;
        }

        public int PcToSnes(int pcAddress)
        {
            pcAddress -= HeaderSize;
            if (pcAddress < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pcAddress),
                    "PC Address cannot be less than zero.");
            }

            var word = BankSize | (pcAddress & BankMask);

            var bank = IsExLoRom
                ? pcAddress > LoRomSizeLimit
                    ? (pcAddress & 0x3F8000) << 1
                    : 0x800000 | ((pcAddress & 0x3F8000) << 1)
                : pcAddress >= 0x3F8000
                    ? 0x800000 | ((pcAddress & 0x3F8000) << 1)
                    : (pcAddress & 0x3F8000) << 1;

            return (bank | word);
        }

        public byte ReadByte(int snesAddress)
        {
            var pcAddress = SnesToPc(snesAddress);
            return Data[pcAddress];
        }

        public int ReadInt16(int snesAddress, bool crossBanks = true)
        {
            return ReadInt16(
                snesAddress,
                IncrementSnesAddress(snesAddress, 1, crossBanks));
        }

        public int ReadInt16(int snesAddressLow, int snesAddressHigh)
        {
            var low = ReadByte(snesAddressLow);
            var high = ReadByte(snesAddressHigh);
            return low | (high << 8);
        }

        public int ReadInt24(int snesAddress, bool crossBanks = true)
        {
            return ReadInt24(
                snesAddress,
                IncrementSnesAddress(snesAddress, 1, crossBanks),
                IncrementSnesAddress(snesAddress, 2, crossBanks));
        }

        public int ReadInt24(
            int snesAddressLow,
            int snesAddressHigh,
            int snesAddressBank)
        {
            var low = ReadByte(snesAddressLow);
            var high = ReadByte(snesAddressHigh);
            var bank = ReadByte(snesAddressBank);
            return low | (high << 8) | (bank << 16);
        }

        public byte[] ReadBytes(
            int snesAddress,
            int count,
            bool crossBanks = true)
        {
            var result = new byte[count];
            var pcAddress = SnesToPc(snesAddress);
            if (crossBanks || IsArrayInBank(count, snesAddress))
            {
                Array.Copy(
                    sourceArray: Data,
                    sourceIndex: pcAddress,
                    destinationArray: result,
                    destinationIndex: 0,
                    length: count);
            }
            else
            {
                var subCount = BankSize - (snesAddress & BankMask);
                var index = 0;
                do
                {
                    Array.Copy(
                        sourceArray: Data,
                        sourceIndex: pcAddress,
                        destinationArray: result,
                        destinationIndex: index,
                        length: subCount);

                    index += subCount;
                    count -= subCount;
                    subCount = Math.Min(count, BankSize);
                    snesAddress &= ~BankMask;
                } while (count > 0);
            }

            return result;
        }

        public int IncrementSnesAddress(
            int snesAddress,
            int amount,
            bool crossBanks)
        {
            if (crossBanks)
            {
                return snesAddress + amount;
            }

            var bank = snesAddress & ~BankMask;
            var word = snesAddress & BankMask;
            return bank | ((word + amount) & BankMask);
        }

        public bool IsArrayInBank(int count, int snesAddress)
        {
            return count + (snesAddress & BankMask) <= BankSize;
        }

        public void WriteByte(int snesAddress, int value)
        {
            var pcAddress = SnesToPc(snesAddress);
            Data[pcAddress] = (byte)value;
        }

        public void WriteInt16(
            int snesAddress,
            int value,
            bool crossBanks = true)
        {
            WriteInt16(
                snesAddress,
                IncrementSnesAddress(snesAddress, 1, crossBanks),
                value);
        }

        public void WriteInt16(
            int snesAddressLow,
            int snesAddressHigh,
            int value)
        {
            WriteByte(snesAddressLow, value);
            WriteByte(snesAddressHigh, value >> 8);
        }

        public void WriteInt24(
            int snesAddress,
            int value,
            bool crossBanks = true)
        {
            WriteInt24(
                snesAddress,
                IncrementSnesAddress(snesAddress, 1, crossBanks),
                IncrementSnesAddress(snesAddress, 2, crossBanks),
                value);
        }

        public void WriteInt24(
            int snesAddressLow,
            int snesAddressHigh,
            int snesAddressBank,
            int value)
        {
            WriteByte(snesAddressLow, value);
            WriteByte(snesAddressHigh, value >> 8);
            WriteByte(snesAddressBank, value >> 16);
        }

        public void WriteBytes(
            byte[] array,
            int snesAddress,
            bool crossBanks = true)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            WriteBytes(
                array,
                0,
                array.Length,
                snesAddress,
                crossBanks);
        }

        public void WriteBytes(
            byte[] array,
            int arrayIndex,
            int count,
            int snesAddress,
            bool crossBanks = true)
        {
            var pcAddress = SnesToPc(snesAddress);
            if (crossBanks || IsArrayInBank(count, snesAddress))
            {
                Array.Copy(
                    sourceArray: array,
                    sourceIndex: arrayIndex,
                    destinationArray: Data,
                    destinationIndex: pcAddress,
                    length: count);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
