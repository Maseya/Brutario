// <copyright file="SpriteTile.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Snes;

using System;

public struct SpriteTile : IEquatable<SpriteTile>
{
    public int TileIndex;
    public int PaletteIndex;
    public int Priority;
    public bool FlipX;
    public bool FlipY;

    public SpriteTile(int tileIndex, int paletteIndex, int priority = 0)
        : this(tileIndex, paletteIndex, priority, false, false)
    { }

    public SpriteTile(
        int tileIndex,
        int paletteIndex,
        int priority,
        bool flipX,
        bool flipY)
    {
        TileIndex = tileIndex;
        PaletteIndex = paletteIndex;
        Priority = priority;
        FlipX = flipX;
        FlipY = flipY;
    }

    public SpriteTile(ChrTile tile, int tileStartIndex)
    {
        TileIndex = tile.TileIndex + tileStartIndex;
        PaletteIndex = tile.PaletteIndex + 8;
        Priority = ((int)tile.Priority * 3) + 2;
        FlipX = tile.XFlipped;
        FlipY = tile.YFlipped;
    }

    public SpriteTile(ObjTile tile, int tileStartIndex, int layer)
    {
        TileIndex = tile.TileIndex + tileStartIndex;
        PaletteIndex = tile.PaletteIndex;
        Priority = ((int)tile.Priority * 3) + layer;
        FlipX = tile.XFlipped;
        FlipY = tile.YFlipped;
    }

    public static bool operator ==(SpriteTile left, SpriteTile right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SpriteTile left, SpriteTile right)
    {
        return !(left == right);
    }

    public bool Equals(SpriteTile other)
    {
        return TileIndex.Equals(other.TileIndex)
            && PaletteIndex.Equals(other.PaletteIndex)
            && Priority.Equals(other.Priority)
            && FlipX.Equals(other.FlipX)
            && FlipY.Equals(other.FlipY);
    }

    public override bool Equals(object? obj)
    {
        return obj is SpriteTile other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TileIndex, PaletteIndex, Priority, FlipX, FlipY);
    }

    public override string ToString()
    {
        return $"{TileIndex:X4}";
    }
}
