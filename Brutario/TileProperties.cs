using System;

namespace Brutario
{
    [Flags]
    public enum TileProperties
    {
        None = 0,
        Invert = 1 << 8,
        Red = 1 << 9,
        Green = 1 << 10,
        Blue = 1 << 11,
        Yellow = Red | Green,
        Magenta = Red | Blue,
        Cyan = Green | Blue,
        White = Red | Green | Blue,
        Transparent = 1 << 12,
    }
}
