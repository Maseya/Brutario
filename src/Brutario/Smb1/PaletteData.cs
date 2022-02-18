// <copyright file="PaletteData.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed under
//     GNU Affero General Public License. See LICENSE in project root for full
//     license information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Smb1
{
    using System;

    public class PaletteData
    {
        public const int ColorsPerRow = 0x10;
        public const int RowsPerPalette = 0x10;
        public const int TotalPaletteSize = ColorsPerRow * RowsPerPalette;

        private const int RowIndexTableSize = 0x220;
        private const int IndexTableSize = 0x42;
        private const int DataTableSize = 0x3E0;
        private const int BonusAreaRow = 7;

        public PaletteData()
        {
            RowIndexTable = new byte[RowIndexTableSize];
            IndexTable = new int[IndexTableSize];
            ColorTable = new Color32BppArgb[DataTableSize];
        }

        public PaletteData(GameData gameData, PaletteDataPointers pointers)
            : this()
        {
            ReadGameData(gameData, pointers);
        }

        public byte[] RowIndexTable
        {
            get;
        }

        public int[] IndexTable
        {
            get;
        }

        public Color32BppArgb[] ColorTable
        {
            get;
        }

        public Color32BppArgb[] LuigiBonusAreaColorTable
        {
            get;
        }

        public void ReadGameData(GameData gameData, PaletteDataPointers pointers)
        {
            if (gameData is null)
            {
                throw new ArgumentNullException(nameof(gameData));
            }

            if (pointers is null)
            {
                throw new ArgumentNullException(nameof(pointers));
            }

            var rom = gameData.Rom;
            rom.ReadBytesIndirect(
               pointers.RowIndexTablePointer,
               RowIndexTable);
            rom.ReadInt16ArrayIndirectAs(
               pointers.IndexTablePointer,
               IndexTable,
               x => x >> 1);
            rom.ReadInt16ArrayIndirectAs(
               pointers.ColorTablePointer,
               ColorTable,
               x => Color32BppArgb.FromSnesColor(x));
            rom.ReadInt16ArrayIndirectAs(
               pointers.LuigiBonusAreaColorTablePointer,
               LuigiBonusAreaColorTable,
               x => Color32BppArgb.FromSnesColor(x));
        }

        public void ReadPalette(int areaIndex, Span<Color32BppArgb> dest)
        {
            ReadPalette(areaIndex, isLuigiBonusArea: false, dest);
        }

        public void ReadPalette(
            int areaIndex,
            bool isLuigiBonusArea,
            Span<Color32BppArgb> dest)
        {
            if (dest.Length < TotalPaletteSize)
            {
                throw new ArgumentException();
            }

            var sourceIndex = areaIndex * ColorsPerRow;
            for (var i = 0; i < RowsPerPalette; i++)
            {
                var paletteRowIndex = RowIndexTable[sourceIndex++];
                var paletteIndex = IndexTable[paletteRowIndex];
                new Span<Color32BppArgb>(ColorTable, paletteIndex, ColorsPerRow).CopyTo(
                    dest.Slice(i * ColorsPerRow));
            }

            if (isLuigiBonusArea)
            {
                LuigiBonusAreaColorTable.CopyTo(dest.Slice(BonusAreaRow * ColorsPerRow));
            }
        }

        public void WritePalette(
            Span<Color32BppArgb> source,
            int areaIndex,
            bool isLuigiBonusArea = false)
        {
            if (source.Length < TotalPaletteSize)
            {
                throw new ArgumentException();
            }

            var sourceIndex = areaIndex * ColorsPerRow;
            for (var i = 0; i < RowsPerPalette; i++)
            {
                if (isLuigiBonusArea && i == BonusAreaRow)
                {
                    source.Slice(BonusAreaRow * ColorsPerRow, ColorsPerRow).CopyTo(
                        LuigiBonusAreaColorTable);
                }
                else
                {
                    var paletteRowIndex = RowIndexTable[sourceIndex++];
                    var paletteIndex = IndexTable[paletteRowIndex];
                    source.Slice(i * ColorsPerRow, ColorsPerRow).CopyTo(
                        new Span<Color32BppArgb>(ColorTable, paletteIndex, ColorsPerRow));
                }
            }
        }

        public void WriteToGameData(GameData gameData, PaletteDataPointers pointers)
        {
            if (gameData is null)
            {
                throw new ArgumentNullException(nameof(gameData));
            }

            var rom = gameData.Rom;
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
}
