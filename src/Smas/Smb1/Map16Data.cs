// <copyright file="Map16Data.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1;

using System;
using System.Collections.ObjectModel;

using Snes;

public class Map16Data
{
    public Map16Data(Rom rom, Map16DataPointers pointers)
    {
        Tiles = new ReadOnlyCollection<Obj16Tile[]>(
            new Obj16Tile[][]
            {
                new Obj16Tile[0x2B],
                new Obj16Tile[0x38],
                new Obj16Tile[0x0E],
                new Obj16Tile[0x3E],
            }
        );

        var isTileAccessible = new bool[0x100];
        for (var i = 0; i < Tiles.Count; i++)
        {
            var startIndex = i << 6;
            for (var j = 0; j < Tiles[i].Length; j++)
            {
                isTileAccessible[startIndex + j] = true;
            }
        }

        IsTileAccessible = new ReadOnlyCollection<bool>(isTileAccessible);

        var bank = pointers.LowBytePointer & 0xFF0000;
        var lows = rom.ReadBytesIndirect(
            pointers.LowBytePointer, Tiles.Count);
        var highs = rom.ReadBytesIndirect(
            pointers.HighBytePointer, Tiles.Count);

        for (var i = 0; i < Tiles.Count; i++)
        {
            unsafe
            {
                fixed (Obj16Tile* ptr = Tiles[i])
                {
                    var dest = new Span<short>(
                        (short*)ptr,
                        Tiles[i].Length * Obj16Tile.NumberOfTiles);
                    var address = bank | (highs[i] << 8) | lows[i];
                    rom.ReadInt16Array(address, dest);
                }
            }
        }
    }

    public ReadOnlyCollection<bool> IsTileAccessible
    {
        get;
    }

    private ReadOnlyCollection<Obj16Tile[]> Tiles
    {
        get;
    }

    public void ReadStaticTiles(Span<Obj16Tile> dest)
    {
        for (var i = 0; i < Tiles.Count; i++)
        {
            var destSlice = dest.Slice(i << 6, 0x40);
            Tiles[i].CopyTo(destSlice);
        }
    }

    public void WriteTiles(Span<Obj16Tile> source)
    {
        for (var i = 0; i < Tiles.Count; i++)
        {
            var sourceSlice = source.Slice(i << 6, 0x40);
            sourceSlice.CopyTo(Tiles[i]);
        }
    }

    public void WriteToGameData(Rom rom, Map16DataPointers pointers)
    {
        var bank = pointers.LowBytePointer & 0xFF0000;
        var lows = rom.ReadBytesIndirect(pointers.LowBytePointer, Tiles.Count);
        var highs = rom.ReadBytesIndirect(pointers.HighBytePointer, Tiles.Count);
        for (var i = 0; i < Tiles.Count; i++)
        {
            unsafe
            {
                fixed (Obj16Tile* ptr = Tiles[i])
                {
                    var src = new Span<short>(
                        (short*)ptr,
                        Tiles[i].Length * Obj16Tile.NumberOfTiles);
                    var address = bank | (highs[i] << 8) | lows[i];
                    rom.WriteInt16Array(address, src);
                }
            }
        }
    }
}
