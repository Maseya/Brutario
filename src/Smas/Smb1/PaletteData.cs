// <copyright file="PaletteData.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

using Maseya.Smas.Smb1.AreaData;

using Snes;

public class PaletteData
{
    public const int ColorsPerRow = 0x10;
    public const int RowsPerPalette = 0x10;
    public const int TotalPaletteSize = ColorsPerRow * RowsPerPalette;

    public const int AreaPaletteCount = AreaLoader.DefaultNumberOfAreas;
    public const int RowIndexTableSize = AreaPaletteCount * RowsPerPalette;
    public const int IndexTableSize = 0x42;
    public const int ColorTableSize = 0x3E * ColorsPerRow;
    public const int BonusAreaRowIndex = 7;
    public const int LuigiBonusAreaRowCount = 1;
    public const int LuigiBonusAreaColorTableSize = ColorsPerRow * LuigiBonusAreaRowCount;
    public const int PlayerPaletteRowIndex = 0x0F;
    public const int PlayerPaletteRowCount = 4;
    public const int PlayerPaletteTableSize = ColorsPerRow * PlayerPaletteRowCount;

    private static readonly ReadOnlyDictionary<ForegroundPalette, byte[]>
        ForegroundPalettes = new(new Dictionary<ForegroundPalette, byte[]>()
        {
            { ForegroundPalette.Normal, [0x00, 0x01, 0x02, 0x03, 0x04] },
            { ForegroundPalette.SnowDay, [0x00, 0x01, 0x34, 0x40, 0x04]},
            { ForegroundPalette.SnowNight, [0x00, 0x01, 0x35, 0x41, 0x04]},
            { ForegroundPalette.MushroomIsland, [0x00, 0x01, 0x26, 0x03, 0x28]},
            { ForegroundPalette.MushroomIslandWarpZone, [0x00, 0x01, 0x26, 0x27, 0x28]},
            { ForegroundPalette.Underground, [0x00, 0x01, 0x13, 0x14, 0x04]},
            { ForegroundPalette.Castle, [0x00, 0x01, 0x1B, 0x1C, 0x04]},
            { ForegroundPalette.CastleUnderwater, [0x00, 0x01, 0x1B, 0x03, 0x04]},
        });

    private static readonly ReadOnlyDictionary<BackgroundPalette, byte[]>
        BackgroundPalettes = new(new Dictionary<BackgroundPalette, byte[]>()
        {
            { BackgroundPalette.Normal, [0x05, 0x06, 0x07]},
            { BackgroundPalette.Mountains, [0x05, 0x31, 0x07]},
            { BackgroundPalette.Waterall, [0x05, 0x06, 0x38]},
            { BackgroundPalette.GoombaPillars, [0x3A, 0x06, 0x3B]},
            { BackgroundPalette.GreenPeaks, [0x05, 0x06, 0x2B]},
            { BackgroundPalette.OrangePeaks, [0x05, 0x06, 0x3C]},
            { BackgroundPalette.SnowPeaks, [0x3D, 0x06, 0x2D]},
            { BackgroundPalette.StarryNight, [0x36, 0x06, 0x37]},
            { BackgroundPalette.MushroomIsland, [0x05, 0x29, 0x2A]},
            { BackgroundPalette.CastleWall, [0x05, 0x06, 0x2E]},
            { BackgroundPalette.BonusRoom, [0x19, 0x06, 0x1A]},
            { BackgroundPalette.Underwater, [0x10, 0x11, 0x12]},
            { BackgroundPalette.Underground, [0x15, 0x06, 0x16]},
            { BackgroundPalette.Castle, [0x1D, 0x1E, 0x1F]},
            { BackgroundPalette.W8Castle, [0x2F, 0x1E, 0x30]},
            { BackgroundPalette.CastleUnderwater, [0x39, 0x11, 0x12]},
        });

    private static readonly ReadOnlyDictionary<SpritePalette, byte[]>
        SpritePalettes = new(new Dictionary<SpritePalette, byte[]>()
        {
            {SpritePalette.Normal, [0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F]},
            {SpritePalette.Underground, [0x08, 0x17, 0x0A, 0x0B, 0x0C, 0x18, 0x0E, 0x0F]},
            {SpritePalette.Castle, [0x08, 0x20, 0x0A, 0x0B, 0x0C, 0x21, 0x0E, 0x0F]},
        });

    public PaletteData()
    {
        RowIndexTable = new byte[RowIndexTableSize];
        IndexTable = new int[IndexTableSize];
        ColorTable = new Color32BppArgb[ColorTableSize];
        LuigiBonusAreaColorTable = new Color32BppArgb[LuigiBonusAreaColorTableSize];
        PlayerPaletteTable = new Color32BppArgb[PlayerPaletteTableSize];
    }

    public PaletteData(Rom rom, PaletteDataPointers pointers) : this()
    {
        Reset(rom, pointers);
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

    public void Reset(Rom rom, PaletteDataPointers pointers)
    {
        var rowIndexTable = rom.ReadBytesIndirect(
            pointers.RowIndexTablePointer,
            RowIndexTableSize);

        if (rowIndexTable.Any(rowIndex => rowIndex >= IndexTableSize))
        {
            throw new ArgumentException(
                "Element in palette row index table attempts to access a value outside of the index table.");
        }

        var indexTable = rom.ReadInt16ArrayIndirectAs(
            pointers.IndexTablePointer,
            IndexTableSize,
            x => x >> 1);

        if (indexTable.Any(index => index > ColorTableSize - ColorsPerRow))
        {
            throw new ArgumentException(
                "Element in palette index table attempts to access a value outside of the color table.");
        }

        Array.Copy(
            sourceArray: rowIndexTable,
            destinationArray: RowIndexTable,
            length: RowIndexTableSize);
        Array.Copy(
            sourceArray: indexTable,
            destinationArray: IndexTable,
            length: IndexTableSize);

        rom.ReadInt16ArrayIndirectAs(
           pointers.ColorTablePointer,
           ColorTable,
           x => Color32BppArgb.FromSnesColor(x));
        rom.ReadInt16ArrayIndirectAs(
           pointers.LuigiBonusAreaColorTablePointer,
           LuigiBonusAreaColorTable,
           x => Color32BppArgb.FromSnesColor(x));
        rom.ReadInt16ArrayIndirectAs(
           pointers.PlayerPaletteTablePointer,
           PlayerPaletteTable,
           x => Color32BppArgb.FromSnesColor(x));
    }

    public int GetRowIndex(int paletteIndex, int row)
    {
        return (uint)paletteIndex >= AreaPaletteCount
            ? throw new ArgumentOutOfRangeException(nameof(paletteIndex))
            : (uint)row >= RowsPerPalette
            ? throw new ArgumentOutOfRangeException(nameof(row))
            : RowIndexTable[(paletteIndex * RowsPerPalette) + row];
    }

    public static int GetRowIndex(AreaPalette areaPalette, int row)
    {
        return (uint)row < 5
            ? ForegroundPalettes[areaPalette.ForegroundPalette][row]
            : (uint)(row - 5u) < 8 - 5
            ? BackgroundPalettes[areaPalette.BackgroundPalette][row - 5]
            : (uint)(row - 8u) < RowsPerPalette - 8
            ? SpritePalettes[areaPalette.SpritePalette][row - 8]
            : throw new ArgumentOutOfRangeException(nameof(row));
    }

    public Color32BppArgb GetColor(int rowIndex, int column)
    {
        return (uint)column >= ColorsPerRow
            ? throw new ArgumentOutOfRangeException(nameof(column))
            : (uint)rowIndex >= IndexTableSize
            ? throw new ArgumentOutOfRangeException(nameof(rowIndex))
            : ColorTable[IndexTable[rowIndex] + column];
    }

    public void SetColor(int rowIndex, int column, Color32BppArgb value)
    {
        if ((uint)column >= ColorsPerRow)
        {
            throw new ArgumentOutOfRangeException(nameof(column));
        }

        if ((uint)rowIndex >= IndexTableSize)
        {
            throw new ArgumentOutOfRangeException(nameof(rowIndex));
        }

        ColorTable[IndexTable[rowIndex] + column] = value;
    }

    public Color32BppArgb GetColor(
        int rowIndex,
        int column,
        Player player,
        PlayerState playerState,
        bool IsLuigiBonusArea)
    {
        if ((uint)column >= ColorsPerRow)
        {
            throw new ArgumentOutOfRangeException(nameof(column));
        }

        if ((uint)rowIndex >= IndexTableSize)
        {
            throw new ArgumentOutOfRangeException(nameof(rowIndex));
        }

        if (rowIndex == BonusAreaRowIndex && IsLuigiBonusArea && player == Player.Luigi)
        {
            return LuigiBonusAreaColorTable[column];
        }
        else if (rowIndex == PlayerPaletteRowIndex)
        {
            var playerPaletteSourceIndex = column;
            if (player == Player.Luigi)
            {
                playerPaletteSourceIndex += ColorsPerRow;
            }

            if (playerState == PlayerState.Fire)
            {
                playerPaletteSourceIndex += ColorsPerRow * 2;
            }

            return PlayerPaletteTable[playerPaletteSourceIndex];
        }
        else
        {
            return ColorTable[IndexTable[rowIndex] + column];
        }
    }

    public void SetColor(
        int rowIndex,
        int column,
        Player player,
        PlayerState playerState,
        bool IsBonusArea,
        Color32BppArgb value)
    {
        if ((uint)column >= ColorsPerRow)
        {
            throw new ArgumentOutOfRangeException(nameof(column));
        }

        if ((uint)rowIndex >= IndexTableSize)
        {
            throw new ArgumentOutOfRangeException(nameof(rowIndex));
        }

        if (rowIndex == BonusAreaRowIndex && IsBonusArea && player == Player.Luigi)
        {
            LuigiBonusAreaColorTable[column] = value;
        }
        else if (rowIndex == PlayerPaletteRowIndex)
        {
            var playerPaletteSourceIndex = column;
            if (player == Player.Luigi)
            {
                playerPaletteSourceIndex += ColorsPerRow;
            }

            if (playerState == PlayerState.Fire)
            {
                playerPaletteSourceIndex += ColorsPerRow * 2;
            }

            PlayerPaletteTable[playerPaletteSourceIndex] = value;
        }
        else
        {
            ColorTable[IndexTable[rowIndex] + column] = value;
        }
    }

    public bool TryGetAreaPalette(int paletteIndex, out AreaPalette areaPalette)
    {
        var result = true;
        result &= TryGetForegroundPalette(paletteIndex, out var foregroundPalette);
        result &= TryGetBackgroundPalette(paletteIndex, out var backgroundPalette);
        result &= TryGetSpritePalette(paletteIndex, out var spritePalette);
        areaPalette = new AreaPalette(
            foregroundPalette,
            backgroundPalette,
            spritePalette);
        return result;
    }

    public bool TryGetForegroundPalette(
        int paletteIndex,
        out ForegroundPalette foregroundPalette)
    {
        var foreground = GetPaletteRows(paletteIndex, 0, 5);
        foreach (var kvp in ForegroundPalettes)
        {
            if (foreground.SequenceEqual(kvp.Value))
            {
                foregroundPalette = kvp.Key;
                return true;
            }
        }

        foregroundPalette = default;
        return false;
    }

    public bool TryGetBackgroundPalette(
        int paletteIndex,
        out BackgroundPalette backgroundPalette)
    {
        var background = GetPaletteRows(paletteIndex, 5, 3);
        foreach (var kvp in BackgroundPalettes)
        {
            if (background.SequenceEqual(kvp.Value))
            {
                backgroundPalette = kvp.Key;
                return true;
            }
        }

        backgroundPalette = default;
        return false;
    }

    public bool TryGetSpritePalette(
        int paletteIndex,
        out SpritePalette spritePalette)
    {
        var sprite = GetPaletteRows(paletteIndex, 8, 8);
        foreach (var kvp in SpritePalettes)
        {
            if (sprite.SequenceEqual(kvp.Value))
            {
                spritePalette = kvp.Key;
                return true;
            }
        }

        spritePalette = default;
        return false;
    }

    public void UpdateAreaPalette(
        int paletteIndex,
        AreaPalette areaPalette)
    {
        WritePaletteRows(paletteIndex, 0, ForegroundPalettes[areaPalette.ForegroundPalette]);
        WritePaletteRows(paletteIndex, 5, BackgroundPalettes[areaPalette.BackgroundPalette]);
        WritePaletteRows(paletteIndex, 8, SpritePalettes[areaPalette.SpritePalette]);
    }

    public void ReadPalette(
        int paletteIndex,
        bool isBonusArea,
        Player player,
        PlayerState playerState,
        Span<Color32BppArgb> dest)
    {
        if ((uint)paletteIndex >= AreaLoader.DefaultNumberOfAreas)
        {
            throw new ArgumentOutOfRangeException(nameof(paletteIndex));
        }

        var rows = new ReadOnlySpan<byte>(
            RowIndexTable,
            paletteIndex * RowsPerPalette,
            RowsPerPalette);

        ReadPalette(rows, isBonusArea, player, playerState, dest);
    }

    public void ReadPalette(
        AreaPalette areaPalette,
        bool isBonusArea,
        Player player,
        PlayerState playerState,
        Span<Color32BppArgb> dest)
    {
        var rows = new byte[RowsPerPalette];
        ForegroundPalettes[areaPalette.ForegroundPalette].CopyTo(new Span<byte>(rows, 0, 5));
        BackgroundPalettes[areaPalette.BackgroundPalette].CopyTo(new Span<byte>(rows, 5, 8));
        SpritePalettes[areaPalette.SpritePalette].CopyTo(new Span<byte>(rows, 8, 8));

        ReadPalette(rows, isBonusArea, player, playerState, dest);
    }

    public void WritePalette(
        AreaPalette areaPalette,
        bool isBonusArea,
        Player player,
        PlayerState state,
        ReadOnlySpan<Color32BppArgb> source)
    {
        var rows = new byte[RowsPerPalette];
        ForegroundPalettes[areaPalette.ForegroundPalette].CopyTo(new Span<byte>(rows, 0, 5));
        BackgroundPalettes[areaPalette.BackgroundPalette].CopyTo(new Span<byte>(rows, 5, 8));
        SpritePalettes[areaPalette.SpritePalette].CopyTo(new Span<byte>(rows, 8, 8));
        WritePalette(rows, isBonusArea, player, state, source);
    }

    public void WritePalette(
        int paletteIndex,
        bool isBonusArea,
        Player player,
        PlayerState state,
        ReadOnlySpan<Color32BppArgb> source)
    {
        if ((uint)paletteIndex >= AreaPaletteCount)
        {
            throw new ArgumentOutOfRangeException(nameof(paletteIndex));
        }

        var rows = new ReadOnlySpan<byte>(
            RowIndexTable,
            paletteIndex * RowsPerPalette,
            RowsPerPalette);
        WritePalette(rows, isBonusArea, player, state, source);
    }

    public void WriteRowDataToGameData(Rom rom, PaletteDataPointers pointers)
    {
        rom.WriteArrayAsInt16Indirect<int>(
            pointers.IndexTablePointer,
            IndexTable,
            x => (short)(x << 1));
        rom.WriteBytesIndirect(
            pointers.RowIndexTablePointer,
            RowIndexTable);
    }

    public void WriteColorDataToGameData(Rom rom, PaletteDataPointers pointers)
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
    }

    public void WriteToGameData(Rom rom, PaletteDataPointers pointers)
    {
        WriteColorDataToGameData(rom, pointers);
        WriteRowDataToGameData(rom, pointers);
    }

    private Span<byte> GetPaletteRows(int paletteIndex, int start, int length)
    {
        var destIndex = paletteIndex * RowsPerPalette;
        return new Span<byte>(RowIndexTable, destIndex + start, length);
    }

    private void WritePaletteRows(int paletteIndex, int start, ReadOnlySpan<byte> rows)
    {
        var destIndex = paletteIndex * RowsPerPalette;
        var dest = new Span<byte>(RowIndexTable, destIndex + start, rows.Length);
        rows.CopyTo(dest);
    }

    private void ReadPalette(
        ReadOnlySpan<byte> rows,
        bool isBonusArea,
        Player player,
        PlayerState playerState,
        Span<Color32BppArgb> dest)
    {
        for (var destRowIndex = 0; destRowIndex < RowsPerPalette; destRowIndex++)
        {
            var sourceIndex = IndexTable[rows[destRowIndex]];
            var sourceRow = new Span<Color32BppArgb>(
                ColorTable,
                sourceIndex,
                ColorsPerRow);

            var destColorIndex = destRowIndex * ColorsPerRow;
            var destRow = dest.Slice(destColorIndex, ColorsPerRow);

            sourceRow.CopyTo(destRow);
        }

        var playerPaletteSourceIndex = 0;
        if (player == Player.Luigi)
        {
            playerPaletteSourceIndex |= 0x10;

            if (isBonusArea)
            {
                var bonusAreaIndex = BonusAreaRowIndex * ColorsPerRow;
                var bonusAreaRow = dest.Slice(bonusAreaIndex, ColorsPerRow);
                LuigiBonusAreaColorTable.CopyTo(bonusAreaRow);
            }
        }

        if (playerState == PlayerState.Fire)
        {
            playerPaletteSourceIndex |= 0x20;
        }

        var playerPaletteSourceRow = new Span<Color32BppArgb>(
            PlayerPaletteTable,
            playerPaletteSourceIndex,
            ColorsPerRow);
        var playerPaletteDestRow = dest.Slice(PlayerPaletteRowIndex * ColorsPerRow, ColorsPerRow);
        playerPaletteSourceRow.CopyTo(playerPaletteDestRow);
    }

    private void WritePalette(
        ReadOnlySpan<byte> rows,
        bool isBonusArea,
        Player player,
        PlayerState state,
        ReadOnlySpan<Color32BppArgb> source)
    {
        for (var sourceRowIndex = 0; sourceRowIndex < RowsPerPalette; sourceRowIndex++)
        {
            var sourceIndex = sourceRowIndex * ColorsPerRow;
            var sourceRow = source.Slice(sourceIndex, ColorsPerRow);
            if (isBonusArea && player == Player.Luigi && sourceRowIndex == BonusAreaRowIndex)
            {
                sourceRow.CopyTo(LuigiBonusAreaColorTable);
            }
            else if (sourceRowIndex == PlayerPaletteRowIndex)
            {
                var playerPaletteSourceIndex = 0;
                if (player == Player.Luigi)
                {
                    playerPaletteSourceIndex |= 0x10;
                }

                if (state == PlayerState.Fire)
                {
                    playerPaletteSourceIndex |= 0x20;
                }

                var playerPaletteDestRow = new Span<Color32BppArgb>(
                    PlayerPaletteTable,
                    playerPaletteSourceIndex,
                    ColorsPerRow);
                var playerPaletteSourceRow = source.Slice(0xF0, ColorsPerRow);
                playerPaletteSourceRow.CopyTo(playerPaletteDestRow);
            }
            else
            {
                var destRowIndex = rows[sourceRowIndex];
                var destIndex = IndexTable[destRowIndex];
                var destRow = new Span<Color32BppArgb>(
                    ColorTable,
                    destIndex,
                    ColorsPerRow);

                sourceRow.CopyTo(destRow);
            }
        }
    }
}
