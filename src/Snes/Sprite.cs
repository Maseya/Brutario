// <copyright file="Sprite.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Snes;

using System;

public struct Sprite : IEquatable<Sprite>
{
    public Sprite(int x, int y, SpriteTile tile, TileProperties tileProperties = 0)
    {
        X = x;
        Y = y;
        Tile = tile;
        TileProperties = tileProperties;
    }

    public int X
    {
        get;
        set;
    }

    public int Y
    {
        get;
        set;
    }

    public SpriteTile Tile
    {
        get;
        set;
    }

    public TileProperties TileProperties
    {
        get;
        set;
    }

    public static bool operator ==(Sprite left, Sprite right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Sprite left, Sprite right)
    {
        return !(left == right);
    }

    public bool Equals(Sprite other)
    {
        return X.Equals(other.X)
            && Y.Equals(other.Y)
            && Tile.Equals(other.Tile)
            && TileProperties.Equals(other.TileProperties);
    }

    public override bool Equals(object? obj)
    {
        return obj is Sprite other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Tile, X, Y);
    }

    public override string ToString()
    {
        return $"{Tile}:{X:X3},{Y:X3}";
    }
}
