// <copyright file="ChrTile.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Snes;

using System;

public struct ChrTile : IEquatable<ChrTile>
{
    public const int SizeOf = sizeof(ushort);

    private const int TileIndexMask = 0x1FF;
    private const int PaletteOffset = 9;
    private const int PaletteMask = 7;
    private const int PriorityOffset = 12;
    private const int PriorityMask = 3;
    private const int FlipOffset = 14;
    private const int FlipXOffset = FlipOffset;
    private const int FlipYOffset = FlipOffset + 1;
    private const int FlipMask = 3;

    private ushort _value;

    public ChrTile(
        int tileIndex,
        int paletteIndex,
        LayerPriority layerPriority,
        TileFlip tileFlip)
    {
        _value = (ushort)(tileIndex & TileIndexMask);
        Value |= (paletteIndex & PaletteMask) << PaletteOffset;
        Value |= ((int)layerPriority & PriorityMask) << PriorityOffset;
        Value |= ((int)tileFlip & FlipMask) << FlipOffset;
    }

    private ChrTile(int value)
    {
        _value = (ushort)value;
    }

    public int Value
    {
        readonly get
        {
            return _value;
        }

        set
        {
            _value = (ushort)value;
        }
    }

    public int TileIndex
    {
        readonly get
        {
            return Value & TileIndexMask;
        }

        set
        {
            Value &= ~TileIndexMask;
            Value |= value & TileIndexMask;
        }
    }

    public int PaletteIndex
    {
        readonly get
        {
            return (Value >> PaletteOffset) & PaletteMask;
        }

        set
        {
            Value &= ~(PaletteMask << PaletteOffset);
            Value |= (value & PaletteMask) << PaletteOffset;
        }
    }

    public LayerPriority Priority
    {
        readonly get
        {
            return (LayerPriority)(
                (Value >> PriorityOffset) & PriorityMask);
        }

        set
        {
            Value &= ~(PriorityMask << PriorityOffset);
            Value |= ((int)value & PriorityMask) << PriorityOffset;
        }
    }

    public TileFlip TileFlip
    {
        readonly get
        {
            return (TileFlip)((Value >> FlipOffset) & FlipMask);
        }

        set
        {
            Value &= ~(FlipMask << FlipOffset);
            Value |= ((int)value & FlipMask) << FlipOffset;
        }
    }

    public bool XFlipped
    {
        readonly get
        {
            return ((Value >> FlipXOffset) & 1) != 0;
        }

        set
        {
            if (value)
            {
                Value |= 1 << FlipXOffset;
            }
            else
            {
                Value &= ~(1 << FlipXOffset);
            }
        }
    }

    public bool YFlipped
    {
        readonly get
        {
            return ((Value >> FlipYOffset) & 1) != 0;
        }

        set
        {
            if (value)
            {
                Value |= 1 << FlipYOffset;
            }
            else
            {
                Value &= ~(1 << FlipYOffset);
            }
        }
    }

    public static implicit operator int(ChrTile tile)
    {
        return tile.Value;
    }

    public static implicit operator ChrTile(int value)
    {
        return new ChrTile(value);
    }

    public static bool operator ==(ChrTile left, ChrTile right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ChrTile left, ChrTile right)
    {
        return !(left == right);
    }

    public readonly bool Equals(ChrTile obj)
    {
        return Value.Equals(obj.Value);
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is ChrTile tile && Equals(tile);
    }

    public override readonly int GetHashCode()
    {
        return Value;
    }

    public override readonly string ToString()
    {
        return $"{Value:X4}";
    }
}
