// <copyright file="RomIO.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed under GNU
//     Affero General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RomIO
    {
        public const int DefaultHeaderSize = 0x200;

        public const int BankSize = 0x8000;

        public const int WordMask = BankSize - 1;

        public const int BankMask = 0xFF0000;

        public const int LoRomBankLimit = 0x80;

        public const int LoRomSizeLimit = LoRomBankLimit * BankSize;

        public const int ExLoRomBankLimit = 0xFE;

        public const int ExLoRomSizeLimit = ExLoRomBankLimit * BankSize;

        public const int NameAddress = 0xFFC0;
        public const int NameSize = 0x16;

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
                return Data.Length & WordMask;
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

        public string Name
        {
            get
            {
                var result = ReadBytes(NameAddress, NameSize);
                return Encoding.ASCII.GetString(result).TrimEnd();
            }

            set
            {
                WriteBytes(NameAddress, Encoding.ASCII.GetBytes(value));
            }
        }

        public static bool IsArrayInBank(int count, int snesAddress)
        {
            return count + (snesAddress & WordMask) <= BankSize;
        }

        public static int SnesToPc(int snesAddress, bool hasHeader, bool isExLoRom)
        {
            if (snesAddress < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(snesAddress),
                    "SNES address cannot be less than zero.");
            }

            if ((snesAddress & BankSize) == 0)
            {
                throw new ArgumentException("Invalid LOROM SNES address.");
            }

            var word = snesAddress & WordMask;

            var bank = isExLoRom
                ? snesAddress >= 0x800000
                    ? (snesAddress & ExLoRomSizeLimit) >> 1
                    : LoRomSizeLimit | ((snesAddress & ExLoRomSizeLimit) >> 1)
                : (snesAddress & ExLoRomSizeLimit) >> 1;

            return (bank | word) + (hasHeader ? DefaultHeaderSize : 0);
        }

        public static int PcToSnes(int pcAddress, bool hasHeader, bool isExLoRom)
        {
            pcAddress -= hasHeader ? DefaultHeaderSize : 0;
            if (pcAddress < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pcAddress),
                    "PC Address cannot be less than zero.");
            }

            var word = BankSize | (pcAddress & WordMask);

            var bank = isExLoRom
                ? pcAddress > LoRomSizeLimit
                    ? (pcAddress & 0x3F8000) << 1
                    : 0x800000 | ((pcAddress & 0x3F8000) << 1)
                : pcAddress >= 0x3F8000
                    ? 0x800000 | ((pcAddress & 0x3F8000) << 1)
                    : (pcAddress & 0x3F8000) << 1;

            return bank | word;
        }

        public byte ReadByte(int snesAddress)
        {
            var pcAddress = SnesToPc(snesAddress, HasHeader, IsExLoRom);
            return Data[pcAddress];
        }

        public byte ReadByteIndirect(int snesAddress)
        {
            return ReadByteIndirectIndexed(snesAddress, 0, false);
        }

        public byte ReadByteIndirectIndexed(
            int snesAddress,
            int index,
            bool crossBanks = true)
        {
            var bank = snesAddress & BankMask;
            var word = ReadInt16(snesAddress);
            var address = IncrementSnesAddress(bank | word, index, crossBanks);
            return ReadByte(address);
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

        public int ReadInt16Indirect(int snesAddress)
        {
            return ReadInt16IndirectIndexed(snesAddress, 0);
        }

        public int ReadInt16Indirect(int snesAddressLow, int snesAddressHigh)
        {
            return ReadByteIndirect(snesAddressLow)
                | (ReadByteIndirect(snesAddressHigh) << 8);
        }

        public int ReadInt16IndirectIndexed(
            int snesAddress,
            int index,
            bool crossBanks = true)
        {
            var bank = snesAddress & BankMask;
            var word = ReadInt16(snesAddress);
            var address = IncrementSnesAddress(bank | word, index << 1, crossBanks);
            return ReadInt16(address, crossBanks);
        }

        public int ReadInt16IndirectIndexed(
            int snesAddressLow,
            int snesAddressHigh,
            int index,
            bool crossBanks = true)
        {
            return ReadByteIndirectIndexed(snesAddressLow, index, crossBanks)
                | (ReadByteIndirectIndexed(snesAddressHigh, index, crossBanks) << 8);
        }

        public void ReadBytes(
            int snesAddress,
            int count,
            Span<byte> dest,
            bool crossBanks = true)
        {
            ReadBytes(snesAddress, dest.Slice(0, count), crossBanks);
        }

        public void ReadBytes(
            int snesAddress,
            Span<byte> dest,
            bool crossBanks = true)
        {
            if (crossBanks || IsArrayInBank(dest.Length, snesAddress))
            {
                var pcAddress = SnesToPc(snesAddress, HasHeader, IsExLoRom);
                new Span<byte>(Data, pcAddress, dest.Length).CopyTo(dest);
            }
            else
            {
                var count = dest.Length;
                var subCount = BankSize - (snesAddress & WordMask);
                var index = 0;
                do
                {
                    var pcAddress = SnesToPc(snesAddress, HasHeader, IsExLoRom);
                    new Span<byte>(Data, pcAddress, subCount).CopyTo(dest.Slice(index));

                    index += subCount;
                    count -= subCount;
                    subCount = Math.Min(count, BankSize);
                    snesAddress &= BankMask;
                } while (count > 0);
            }
        }

        public byte[] ReadBytes(
            int snesAddress,
            int count,
            bool crossBanks = true)
        {
            var result = new byte[count];
            ReadBytes(snesAddress, result, crossBanks);
            return result;
        }

        public IEnumerable<byte> EnumerateBytes(
            int snesAddress,
            bool crossBanks = true)
        {
            var pcAddress = SnesToPc(snesAddress, HasHeader, IsExLoRom);
            if (crossBanks)
            {
                while (true)
                {
                    yield return Data[pcAddress];
                    if (++pcAddress == Data.Length)
                    {
                        pcAddress = HeaderSize;
                    }
                }
            }
            else
            {
                var subCount = BankSize - (snesAddress & WordMask);
                do
                {
                    for (var i = 0; i < subCount; i++)
                    {
                        yield return Data[pcAddress + i];
                    }
                    subCount = BankMask;
                    pcAddress &= ~WordMask;
                } while (true);
            }
        }

        public void ReadBytesIndirect(
            int snesAddress,
            int count,
            Span<byte> dest,
            bool crossBanks = true)
        {
            ReadBytesIndirect(snesAddress, dest.Slice(0, count), crossBanks);
        }

        public void ReadBytesIndirect(
            int snesAddress,
            Span<byte> dest,
            bool crossBanks = true)
        {
            ReadBytesIndirectIndexed(snesAddress, 0, dest, crossBanks);
        }

        public byte[] ReadBytesIndirect(
            int snesAddress,
            int count,
            bool crossBanks = true)
        {
            var result = new byte[count];
            ReadBytesIndirect(snesAddress, result, crossBanks);
            return result;
        }

        public void ReadBytesIndirectIndexed(
            int snesAddress,
            int index,
            int count,
            Span<byte> dest,
            bool crossBanks = true)
        {
            ReadBytesIndirectIndexed(
                snesAddress,
                index,
                dest.Slice(0, count),
                crossBanks);
        }

        public void ReadBytesIndirectIndexed(
            int snesAddress,
            int index,
            Span<byte> dest,
            bool crossBanks = true)
        {
            var bank = snesAddress & BankMask;
            var word = ReadInt16(snesAddress);
            var address = IncrementSnesAddress(bank | word, index, crossBanks);
            ReadBytes(address, dest, crossBanks);
        }

        public byte[] ReadBytesIndirectIndexed(
            int snesAddress,
            int index,
            int count,
            bool crossBanks = true)
        {
            var result = new byte[count];
            ReadBytesIndirectIndexed(snesAddress, index, result, crossBanks);
            return result;
        }

        public void ReadInt16Array(
            int snesAddress,
            int count,
            Span<short> dest,
            bool crossBanks = true)
        {
            ReadInt16Array(snesAddress, dest.Slice(0, count), crossBanks);
        }

        public void ReadInt16Array(
            int snesAddress,
            Span<short> dest,
            bool crossBanks = true)
        {
            unsafe
            {
                fixed (short* ptr = dest)
                {
                    ReadBytes(
                        snesAddress,
                        dest.Length * sizeof(short),
                        new Span<byte>((byte*)ptr, dest.Length * sizeof(short)),
                        crossBanks);
                }
            }
        }

        public short[] ReadInt16Array(
            int snesAddress,
            int count,
            bool crossBanks = true)
        {
            var result = new short[count];
            ReadInt16Array(snesAddress, result, crossBanks);
            return result;
        }

        public void ReadInt16ArrayIndirect(
            int snesAddress,
            int count,
            Span<short> dest,
            bool crossBanks = true)
        {
            ReadInt16ArrayIndirect(snesAddress, dest.Slice(0, count), crossBanks);
        }

        public void ReadInt16ArrayIndirect(
            int snesAddress,
            Span<short> dest,
            bool crossBanks = true)
        {
            var bank = snesAddress & BankMask;
            var word = ReadInt16(snesAddress);
            ReadInt16Array(bank | word, dest, crossBanks);
        }

        public short[] ReadInt16ArrayIndirect(
            int snesAddress,
            int count,
            bool crossBanks = true)
        {
            var result = new short[count];
            ReadInt16ArrayIndirect(snesAddress, result, crossBanks);
            return result;
        }

        public void ReadInt16ArrayIndirectIndexed(
            int snesAddress,
            int index,
            int count,
            Span<short> dest,
            bool crossBanks = true)
        {
            ReadInt16ArrayIndirectIndexed(
                snesAddress,
                index,
                dest.Slice(0, count),
                crossBanks);
        }

        public void ReadInt16ArrayIndirectIndexed(
            int snesAddress,
            int index,
            Span<short> dest,
            bool crossBanks = true)
        {
            var bank = snesAddress & BankMask;
            var word = ReadInt16(snesAddress);
            var address = IncrementSnesAddress(bank | word, index, crossBanks);
            ReadInt16Array(address, dest, crossBanks);
        }

        public short[] ReadInt16ArrayIndirectIndexed(
            int snesAddress,
            int index,
            int count,
            bool crossBanks = true)
        {
            var result = new short[count];
            ReadInt16ArrayIndirectIndexed(snesAddress, index, result, crossBanks);
            return result;
        }

        public void ReadInt16ArrayAs<T>(
            int snesAddress,
            int count,
            Span<T> dest,
            Func<short, T> func,
            bool crossBanks = true)
        {
            ReadInt16ArrayAs(
                snesAddress,
                dest.Slice(0, count),
                func,
                crossBanks);
        }

        public void ReadInt16ArrayAs<T>(
            int snesAddress,
            Span<T> dest,
            Func<short, T> func,
            bool crossBanks = true)
        {
            var source = ReadInt16Array(snesAddress, dest.Length, crossBanks);
            for (var i = 0; i < dest.Length; i++)
            {
                dest[i] = func(source[i]);
            }
        }

        public T[] ReadInt16ArrayAs<T>(
            int snesAddress,
            int count,
            Func<short, T> func,
            bool crossBanks = true)
        {
            var result = new T[count];
            ReadInt16ArrayAs(snesAddress, count, result, func, crossBanks);
            return result;
        }

        public void ReadInt16ArrayIndirectAs<T>(
            int snesAddress,
            int count,
            Span<T> dest,
            Func<short, T> func,
            bool crossBanks = true)
        {
            ReadInt16ArrayIndirectAs(
                snesAddress,
                dest.Slice(0, count),
                func,
                crossBanks);
        }

        public void ReadInt16ArrayIndirectAs<T>(
            int snesAddress,
            Span<T> dest,
            Func<short, T> func,
            bool crossBanks = true)
        {
            var bank = snesAddress & BankMask;
            var word = ReadInt16(snesAddress);
            ReadInt16ArrayAs(bank | word, dest, func, crossBanks);
        }

        public T[] ReadInt16ArrayIndirectAs<T>(
            int snesAddress,
            int count,
            Func<short, T> func,
            bool crossBanks = true)
        {
            var result = new T[count];
            ReadInt16ArrayIndirectAs(snesAddress, result, func, crossBanks);
            return result;
        }

        public void WriteByte(int snesAddress, int value)
        {
            var pcAddress = SnesToPc(snesAddress, HasHeader, IsExLoRom);
            Data[pcAddress] = (byte)value;
        }

        public void WriteByteIndirect(int snesAddress, int value)
        {
            var bank = snesAddress & BankMask;
            var word = ReadInt16(snesAddress);
            WriteByte(bank | word, value);
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

        public void WriteInt16Indirect(
            int snesAddress,
            int value,
            bool crossBanks = true)
        {
            var bank = snesAddress & BankMask;
            var word = ReadInt16(snesAddress);
            WriteInt16(bank | word, value, crossBanks);
        }

        public void WriteInt16(
            int snesAddressLow,
            int snesAddressHigh,
            int value)
        {
            WriteByte(snesAddressLow, value);
            WriteByte(snesAddressHigh, value >> 8);
        }

        public void WriteInt16Indirect(
            int snesAddressLow,
            int snesAddressHigh,
            int value)
        {
            WriteByteIndirect(snesAddressLow, value);
            WriteByteIndirect(snesAddressHigh, value >> 8);
        }

        public void WriteBytes(
            int snesAddress,
            Span<byte> bytes,
            bool crossBanks = true)
        {
            var pcAddress = SnesToPc(snesAddress, HasHeader, IsExLoRom);
            if (crossBanks || IsArrayInBank(bytes.Length, snesAddress))
            {
                bytes.CopyTo(new Span<byte>(Data, pcAddress, bytes.Length));
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void WriteBytesIndirect(
            int snesAddress,
            Span<byte> bytes,
            bool crossBanks = true)
        {
            WriteBytesIndirectIndexed(snesAddress, 0, bytes, crossBanks);
        }

        public void WriteBytesIndirectIndexed(
            int snesAddress,
            int index,
            Span<byte> bytes,
            bool crossBanks = true)
        {
            var bank = snesAddress & BankMask;
            var word = ReadInt16(snesAddress);
            var address = IncrementSnesAddress(bank | word, index, crossBanks);
            WriteBytes(address, bytes, crossBanks);
        }

        public void WriteInt16Array(
            int snesAddress,
            Span<short> words,
            bool crossBanks = true)
        {
            unsafe
            {
                fixed (short* ptr = words)
                {
                    WriteBytes(
                        snesAddress,
                        new Span<byte>((byte*)ptr, words.Length * sizeof(short)),
                        crossBanks);
                }
            }
        }

        public void WriteInt16ArrayIndirect(
            int snesAddress,
            Span<short> words,
            bool crossBanks = true)
        {
            WriteInt16ArrayIndirectIndexed(snesAddress, 0, words, crossBanks);
        }

        public void WriteInt16ArrayIndirectIndexed(
            int snesAddress,
            int index,
            Span<short> words,
            bool crossBanks = true)
        {
            var bank = snesAddress & BankMask;
            var word = ReadInt16(snesAddress);
            var address = IncrementSnesAddress(bank | word, index, crossBanks);
            WriteInt16Array(address, words, crossBanks);
        }

        public void WriteArrayAsInt16<T>(
            int snesAddress,
            Span<T> data,
            Func<T, short> func,
            bool crossBanks = true)
        {
            var source = new short[data.Length];
            for (var i = 0; i < data.Length; i++)
            {
                source[i] = func(data[i]);
            }

            WriteInt16Array(snesAddress, source, crossBanks);
        }

        public void WriteArrayAsInt16Indirect<T>(
            int snesAddress,
            Span<T> data,
            Func<T, short> func,
            bool crossBanks = true)
        {
            var bank = snesAddress & BankMask;
            var word = ReadInt16(snesAddress);
            WriteArrayAsInt16(bank | word, data, func, crossBanks);
        }

        public int IncrementSnesAddress(
            int snesAddress,
            int amount,
            bool crossBanks = true)
        {
            if (crossBanks)
            {
                return snesAddress + amount;
            }

            var bank = snesAddress & BankMask;
            var word = snesAddress & WordMask;
            return bank | 0x8000 | ((word + amount) & WordMask);
        }
    }
}
