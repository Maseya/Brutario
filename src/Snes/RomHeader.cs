namespace Maseya.Snes;

using System;
using System.Linq;
using System.Text;

internal readonly ref struct RomHeader
{
    public RomHeader(
        int fullRomSize,
        ReadOnlySpan<byte> data,
        AddressMode addressMode)
    {
        FullRomSize = fullRomSize;
        Data = data;
        AddressMode = addressMode;
    }

    public string MakerCode
    {
        get
        {
            var result = Data[..Rom.MakerCodeSize];
            return Encoding.ASCII.GetString(result);
        }
    }

    public RamSize ExpansionRAMSize
    {
        get
        {
            return (RamSize)Data[Rom.ExpansionRAMSizeAddress - 0xFFB0];
        }
    }

    public string GameTitle
    {
        get
        {
            var result = Data.Slice(Rom.GameTitleAddress - 0xFFB0, Rom.GameTitleSize);
            return Rom.GameTitleEncoding.GetString(result).TrimEnd().Replace('\\', '¥');
        }
    }

    /// <summary>
    /// Gets or sets a value determining whether this is a HiROM or LoROM game.
    /// </summary>
    public MapMode MapMode
    {
        get
        {
            return (MapMode)Data[Rom.MapModeAddress - 0xFFB0];
        }
    }

    public CartridgeType CartridgeType
    {
        get
        {
            return (CartridgeType)Data[Rom.CartridgeTypeAddress - 0xFFB0];
        }
    }

    public RomSize RomSize
    {
        get
        {
            return (RomSize)Data[Rom.RomSizeAddress - 0xFFB0];
        }
    }

    public int RomCapacity
    {
        get
        {
            return Rom.RomSizeBaseValue << (int)RomSize;
        }
    }

    public RamSize RamSize
    {
        get
        {
            return (RamSize)Data[Rom.RamSizeAddress - 0xFFB0];
        }
    }

    public DestinationCode DestinationCode
    {
        get
        {
            return (DestinationCode)Data[Rom.DestinationCodeAddress - 0xFFB0];
        }
    }

    public int FixedValue
    {
        get
        {
            return Data[Rom.FixedValueAddress - 0xFFB0];
        }
    }

    public bool IsFixedValueValid
    {
        get
        {
            return FixedValue == Rom.IntendedFixedValue;
        }
    }

    public int ComplementCheck
    {
        get
        {
            return ReadInt16(Rom.ComplementCheckAddress - 0xFFB0);
        }
    }

    public int Checksum
    {
        get
        {
            return ReadInt16(Rom.CheckSumAddress - 0xFFB0);
        }
    }

    public bool IsComplementCheckValid
    {
        get
        {
            return (Checksum ^ ComplementCheck) == UInt16.MaxValue;
        }
    }

    public int EmuResetVector
    {
        get
        {
            return ReadInt16(Rom.EmuResetVectorAddress - 0xFFB0);
        }
    }

    public int Score
    {
        get
        {
            var score = 0;
            if (RomSize == RomSize.Size64Mbit && FullRomSize > 0x40_0000)
            {
                score += 5;
                if (AddressMode.IsExRom())
                {
                    score++;
                }
            }

            if (MapMode.IsHiRom())
            {
                if (AddressMode.IsHiRom())
                {
                    score += 2;
                }
            }
            else if (AddressMode.IsLoRom())
            {
                score += 3;
            }

            if (MapMode == MapMode.Mode23Sa1)
            {
                if (AddressMode.IsHiRom())
                {
                    score -= 2;
                }
            }
            else if (AddressMode.IsLoRom())
            {
                score += 2;
            }

            if (IsComplementCheckValid)
            {
                score += 2;
                if (Checksum != 0)
                {
                    score++;
                }
            }

            if (IsFixedValueValid)
            {
                score += 2;
            }

            if (((int)MapMode & 0x0F) < 4)
            {
                score += 2;
            }

            if ((EmuResetVector & 0x8000) == 0)
            {
                score -= 6;
            }

            if (EmuResetVector > 0xFFB0)
            {
                score -= 2;
            }

            if (!Enum.IsDefined<RomSize>(RomSize))
            {
                score--;
            }

            if (!MakerCode.All(Char.IsAscii))
            {
                score--;
            }

            if (!GameTitle.All(Char.IsAscii))
            {
                score--;
            }

            return score;
        }
    }

    public AddressMode AddressMode { get; }

    public ReadOnlySpan<byte> Data { get; }

    public int FullRomSize { get; }

    private int ReadInt16(int address)
    {
        return Data[address] | (Data[address + 1] << 8);
    }
}
