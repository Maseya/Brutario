namespace Maseya.Snes;

using System.ComponentModel;

public static class ExtensionMethods
{
    public static bool IsLoRom(this AddressMode addressMode)
    {
        return addressMode switch
        {
            AddressMode.LoRom or
            AddressMode.LoRom2 or
            AddressMode.ExLoRom => true,
            _ => false,
        };
    }

    public static bool IsHiRom(this AddressMode addressMode)
    {
        return addressMode switch
        {
            AddressMode.HiRom or
            AddressMode.HiRom2 or
            AddressMode.ExHiRom => true,
            _ => false,
        };
    }

    public static bool IsExRom(this AddressMode addressMode)
    {
        return addressMode switch
        {
            AddressMode.ExLoRom or
            AddressMode.ExHiRom => true,
            _ => false,
        };
    }

    public static int MinSize(this AddressMode addressMode)
    {
        return addressMode switch
        {
            AddressMode.LoRom => 0x8000,
            AddressMode.HiRom => 0x1_0000,
            AddressMode.ExHiRom => 0x41_0000,
            AddressMode.ExLoRom => 0x40_8000,
            AddressMode.LoRom2 => 0x8000,
            AddressMode.HiRom2 => 0x1_0000,
            _ => throw new InvalidEnumArgumentException(
                nameof(addressMode),
                (int)addressMode,
                typeof(AddressMode)),
        };
    }

    public static bool IsFastRom(this AddressMode addressMode)
    {
        return addressMode switch
        {
            AddressMode.LoRom2 or
            AddressMode.HiRom2 => true,
            _ => false,
        };
    }

    public static bool IsFastMode(this MapMode mapMode)
    {
        return (mapMode & MapMode.Mode20Fast) == MapMode.Mode20Fast;
    }

    public static bool IsHiRom(this MapMode mapMode)
    {
        return ((int)mapMode & 1) != 0;
    }
}
