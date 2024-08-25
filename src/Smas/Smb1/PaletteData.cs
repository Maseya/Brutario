// <copyright file="PaletteData.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1;

using System;
using System.Linq;

using Snes;

public class PaletteData
{
    public const int ColorsPerRow = 0x10;
    public const int RowsPerPalette = 0x10;
    public const int TotalPaletteSize = ColorsPerRow * RowsPerPalette;

    private const int RowIndexTableSize = 0x220;
    private const int IndexTableSize = 0x42;
    private const int ColorTableSize = 0x3E0;
    private const int BonusAreaRowIndex = 7;
    private const int LuigiBonusAreaRowCount = 1;
    private const int LuigiBonusAreaColorTableSize = ColorsPerRow * LuigiBonusAreaRowCount;
    private const int PlayerPaletteRowIndex = 0x0F;
    private const int PlayerPaletteRowCount = 4;
    private const int PlayerPaletteTableSize = ColorsPerRow * PlayerPaletteRowCount;

    public PaletteData(Rom rom, PaletteDataPointers pointers)
    {
        RowIndexTable = rom.ReadBytesIndirect(
           pointers.RowIndexTablePointer,
           RowIndexTableSize);

        // Make sure the index tables stay within the bounds of the tables they index into.
        if (RowIndexTable.Any(rowIndex => rowIndex >= IndexTableSize))
        {
            throw new ArgumentException(
                "Element in palette row index table attempts to access a value outside of the index table.");
        }

        IndexTable = rom.ReadInt16ArrayIndirectAs(
           pointers.IndexTablePointer,
           IndexTableSize,
           x => x >> 1);

        if (IndexTable.Any(index => index > ColorTableSize - ColorsPerRow))
        {
            throw new ArgumentException(
                "Element in palette index table attempts to access a value outside of the color table.");
        }

        ColorTable = rom.ReadInt16ArrayIndirectAs(
           pointers.ColorTablePointer,
           ColorTableSize,
           x => Color32BppArgb.FromSnesColor(x));
        LuigiBonusAreaColorTable = rom.ReadInt16ArrayIndirectAs(
           pointers.LuigiBonusAreaColorTablePointer,
           LuigiBonusAreaColorTableSize,
           x => Color32BppArgb.FromSnesColor(x));
        PlayerPaletteTable = rom.ReadInt16ArrayIndirectAs(
           pointers.PlayerPaletteTablePointer,
           PlayerPaletteTableSize,
           x => Color32BppArgb.FromSnesColor(x));
    }

    private byte[] RowIndexTable
    {
        get;
    }

    private int[] IndexTable
    {
        get;
    }

    private Color32BppArgb[] ColorTable
    {
        get;
    }

    private Color32BppArgb[] LuigiBonusAreaColorTable
    {
        get;
    }

    private Color32BppArgb[] PlayerPaletteTable
    {
        get;
    }

    public void ReadPalette(int paletteIndex, Span<Color32BppArgb> dest)
    {
        ReadPalette(
            paletteIndex,
            isLuigiBonusArea: false,
            player: Player.Mario,
            state: PlayerState.Small,
            dest);
    }

    public void ReadPalette(
        int paletteIndex,
        bool isLuigiBonusArea,
        Player player,
        PlayerState state,
        Span<Color32BppArgb> dest)
    {
        var sourceRowStartIndex = paletteIndex * RowsPerPalette;
        for (var destRowIndex = 0; destRowIndex < RowsPerPalette; destRowIndex++)
        {
            var sourceRowIndex = RowIndexTable[sourceRowStartIndex + destRowIndex];
            var sourceIndex = IndexTable[sourceRowIndex];
            var sourceRow = new Span<Color32BppArgb>(
                ColorTable,
                sourceIndex,
                ColorsPerRow);

            var destColorIndex = destRowIndex * ColorsPerRow;
            var destRow = dest.Slice(destColorIndex, ColorsPerRow);

            sourceRow.CopyTo(destRow);
        }

        if (isLuigiBonusArea)
        {
            var bonusAreaIndex = BonusAreaRowIndex * ColorsPerRow;
            var bonusAreaRow = dest.Slice(bonusAreaIndex, ColorsPerRow);
            LuigiBonusAreaColorTable.CopyTo(bonusAreaRow);
        }

        var playerPaletteSourceIndex = 0;
        if (player == Player.Luigi)
        {
            playerPaletteSourceIndex |= 0x10;
        }

        if (state == PlayerState.Fire)
        {
            playerPaletteSourceIndex |= 0x20;
        }

        var playerPaletteSourceRow = new Span<Color32BppArgb>(
            PlayerPaletteTable,
            playerPaletteSourceIndex,
            ColorsPerRow);
        var playerPaletteDestRow = dest.Slice(0xF0, ColorsPerRow);
        playerPaletteSourceRow.CopyTo(playerPaletteDestRow);
    }

    public void ReadPlayerPalettes(Span<Color32BppArgb> dest)
    {
        PlayerPaletteTable.CopyTo(dest);
    }

    public void WritePlayerPalettes(Span<Color32BppArgb> src)
    {
        src.CopyTo(PlayerPaletteTable);
    }

    public void WritePalette(
        Span<Color32BppArgb> source,
        int paletteIndex,
        bool isLuigiBonusArea = false)
    {
        var destRowStartIndex = paletteIndex * RowsPerPalette;
        for (var sourceRowIndex = 0; sourceRowIndex < RowsPerPalette; sourceRowIndex++)
        {
            var sourceIndex = sourceRowIndex * ColorsPerRow;
            var sourceRow = source.Slice(sourceIndex, ColorsPerRow);
            if (isLuigiBonusArea && sourceRowIndex == BonusAreaRowIndex)
            {
                sourceRow.CopyTo(LuigiBonusAreaColorTable);
            }
            else if (sourceRowIndex == PlayerPaletteRowIndex)
            {
                // TODO(nrg): Player palette
            }
            else
            {
                var destRowIndex = RowIndexTable[destRowStartIndex + sourceRowIndex];
                var destIndex = IndexTable[destRowIndex];
                var destRow = new Span<Color32BppArgb>(
                    ColorTable,
                    destIndex,
                    ColorsPerRow);

                sourceRow.CopyTo(destRow);
            }
        }
    }

    public void WriteToGameData(Rom rom, PaletteDataPointers pointers)
    {
        rom.WriteArrayAsInt16Indirect<Color32BppArgb>(
            pointers.PlayerPaletteTablePointer,
            PlayerPaletteTable,
            x => (short)Color32BppArgb.ToSnesColor(x));
        rom.WriteArrayAsInt16Indirect<Color32BppArgb>(
            pointers.LuigiBonusAreaColorTablePointer,
            LuigiBonusAreaColorTable,
            x => (short)Color32BppArgb.ToSnesColor(x));
        rom.WriteArrayAsInt16Indirect<Color32BppArgb>(
            pointers.ColorTablePointer,
            ColorTable,
            x => (short)Color32BppArgb.ToSnesColor(x));
        rom.WriteArrayAsInt16Indirect<int>(
            pointers.IndexTablePointer,
            IndexTable,
            x => (short)(x << 1));
        rom.WriteBytesIndirect(
            pointers.RowIndexTablePointer,
            RowIndexTable);
    }
}
