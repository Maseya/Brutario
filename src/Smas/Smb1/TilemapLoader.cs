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
                    indexes[i] + j);
                if (command.IsTerminationCommand)
                {
                    break;
                }

                commands.Add(command);
            }

            TilemapCommands[i] = commands.ToArray();
        }

        Layer2Tilemap = new int[0xD00 >> 1];
        BackgroundGenerationCommands = new Action<TilemapCommand>[0x0D]
        {
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
        };
    }

    public byte TileSetIndex
    {
        get;
        private set;
    }

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
                    Layer2Tilemap[++Layer2TilemapIndex] = 0xFFFF;
                    LoadTilemapTiles();
                }
                else
                {
                    BackgroundGenerationCommands[command.CommandEF](command);
                }
            }
            else
            {
            }
        }
    }

    private void LoadTilemapTiles()
    {
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
        Layer2TilemapIndex = command.CommandF1 | 0x10;
    }

    private void GenerateGoombaPillars(TilemapCommand command)
    {
    }
}
