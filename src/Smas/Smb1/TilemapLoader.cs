// <copyright file="TilemapLoader.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1;

using System;
using System.Collections.Generic;

using Snes;

public class TilemapLoader
{
    public TilemapLoader(Rom rom, TilemapLoaderPointers pointers, int numberOfAreas)
    {
        TilemapCommands = new TilemapCommand[numberOfAreas][];
        var indexes = rom.ReadInt16ArrayIndirectAs(
            pointers.TilemapDataIndexPointer,
            TilemapCommands.Length,
            x => x >> 1);

        for (var i = 0; i < TilemapCommands.Length; i++)
        {
            var commands = new List<TilemapCommand>();
            for (var j = 0; true; j++)
            {
                TilemapCommand command = rom.ReadInt16IndirectIndexed(
                    pointers.TilemapDataPointer,
                    (indexes[i] + j) << 1);
                if (command.IsTerminationCommand)
                {
                    break;
                }

                commands.Add(command);
            }

            TilemapCommands[i] = commands.ToArray();
        }

        Layer2BackgroundIndexTable = rom.ReadInt16ArrayIndirectAs(
            pointers.Layer2BackgroundIndexTablePointer,
            numberOfAreas, x => (ushort)x);
        Layer2BackgroundPointersTable = rom.ReadInt16ArrayIndirectAs(
            pointers.Layer2BackgroundPointersTablePointer,
            numberOfAreas,
            x => (0x50000 | (ushort)x) - pointers.Layer2Obj16TileTablePointer);

        Layer2BackgroundTileset = new Obj16Tile[pointers.Layer2Obj16TileTableSize];
        unsafe
        {
            fixed (Obj16Tile* ptr = Layer2BackgroundTileset)
            {
                var span = new Span<short>(
                    ptr,
                    Layer2BackgroundTileset.Length * Obj16Tile.NumberOfTiles);
                rom.ReadInt16Array(pointers.Layer2Obj16TileTablePointer, span);
            }
        }

        Layer2Tilemap = new int[0xD00 >> 1];
        BackgroundGenerationCommands =
        [
            x => Layer2TilemapIndex++,
            EnableHdmaGradient,
            EnableHdmaWaving,
            UnknownCommand03,
            SetTilemapIndex,
            FillTopAreaTilemap,
            FillUndergroundRockPattern,
            FillUnderwaterTopAreaTilemap,
            FillWaterFallRockPattern,
            x => EnableLayer3 = true,
            GenerateWaterfallTiles,
            SetSpecialTilemapIndex,
            GenerateGoombaPillars,
        ];
    }

    public byte TileSetIndex
    {
        get;
        private set;
    }

    /// <summary>
    /// $7E:D000-$7E:DDFF
    /// </summary>
    private int[] Layer2Tilemap
    {
        get;
    }

    private bool EnableLayer3
    {
        get;
        set;
    }

    private int Layer2TilemapIndex
    {
        get;
        set;
    }

    private TilemapCommand[][] TilemapCommands
    {
        get;
    }

    private Action<TilemapCommand>[] BackgroundGenerationCommands
    {
        get;
    }

    private ushort[] Layer2BackgroundIndexTable;

    private int[] Layer2BackgroundPointersTable;

    private Obj16Tile[] Layer2BackgroundTileset;

    public void LoadTilemap(int areaIndex)
    {
        EnableLayer3 = false;
        Array.Clear(Layer2Tilemap, 0, Layer2Tilemap.Length);
        foreach (var command in TilemapCommands[areaIndex])
        {
            if ((command.CommandED & 0xF0) == 0xE0)
            {
                if (command.CommandEF == 0x3F)
                {
                    Layer2Tilemap[(++Layer2TilemapIndex) << 8] = 0xFFFF;
                    WriteTilemapFromBuffer(areaIndex);
                }
                else
                {
                    BackgroundGenerationCommands[command.CommandEF](command);
                }
            }
            else
            {
                Func580B3(command);
            }
        }
    }

    private void Func580B3(TilemapCommand command)
    {
    }

    private void WriteTilemapFromBuffer(int areaIndex)
    {
        // CODE_59166
        // Use the $7E:D000 map16 tiles to fill the $7E:2000 array with the
        // obj tile data that will eventually be copied to vram.
        // This therefore only gets called after the $7E:D000 array is filled.

        var DATA_7ED000 = new ushort[0x1000];
        var DATA_7E2000 = new Obj16Tile[0x1000];

        var layer2BackgroundIndex = Layer2BackgroundIndexTable[areaIndex];
        var layer2BackgroundPointer =
            Layer2BackgroundPointersTable[layer2BackgroundIndex];

        unsafe
        {
            fixed (Obj16Tile* ptr = DATA_7E2000)
            {
                for (var y = 0; y < DATA_7ED000.Length; y++)
                {
                    if (DATA_7ED000[y] == 0xFFFF)
                    {
                        break;
                    }
                }
            }
        }
    }

    private void EnableHdmaGradient(TilemapCommand command)
    {
    }

    private void EnableHdmaWaving(TilemapCommand command)
    {
    }

    private void UnknownCommand03(TilemapCommand command)
    {
    }

    private void SetTilemapIndex(TilemapCommand command)
    {
        TileSetIndex = (byte)command.CommandF1;
    }

    private void FillTopAreaTilemap(TilemapCommand command)
    {
    }

    private void FillUndergroundRockPattern(TilemapCommand command)
    {
    }

    private void FillUnderwaterTopAreaTilemap(TilemapCommand command)
    {
    }

    private void FillWaterFallRockPattern(TilemapCommand command)
    {
    }

    private void GenerateWaterfallTiles(TilemapCommand command)
    {
    }

    private void SetSpecialTilemapIndex(TilemapCommand command)
    {
        TileSetIndex = (byte)(command.CommandF1 | 0x10);
    }

    private void GenerateGoombaPillars(TilemapCommand command)
    {
    }
}
