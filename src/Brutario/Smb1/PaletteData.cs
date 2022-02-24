// <copyright file="PaletteData.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed under GNU
//     Affero General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
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
            PlayerPaletteTable = new Color32BppArgb[ColorsPerRow * 4];
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

        public Color32BppArgb[] PlayerPaletteTable
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
            rom.ReadInt16ArrayIndirectAs(
               pointers.PlayerPaletteTablePointer,
               PlayerPaletteTable,
               x => Color32BppArgb.FromSnesColor(x));
        }

        public void ReadPalette(int index, Span<Color32BppArgb> dest)
        {
            ReadPalette(
                index,
                isLuigiBonusArea: false,
                player: Player.Mario,
                state: PlayerState.Small,
                dest);
        }

        public void ReadPalette(
            int index,
            bool isLuigiBonusArea,
            Player player,
            PlayerState state,
            Span<Color32BppArgb> dest)
        {
            if (dest.Length < TotalPaletteSize)
            {
                throw new ArgumentException();
            }

            var sourceIndex = index * ColorsPerRow;
            for (var i = 0; i < RowsPerPalette; i++)
            {
                var rowIndex = RowIndexTable[sourceIndex++];
                var startIndex = IndexTable[rowIndex];
                new Span<Color32BppArgb>(ColorTable, startIndex, ColorsPerRow).CopyTo(
                    dest.Slice(i * ColorsPerRow));
            }

            if (isLuigiBonusArea)
            {
                LuigiBonusAreaColorTable.CopyTo(dest.Slice(BonusAreaRow * ColorsPerRow));
            }

            sourceIndex = 0;
            if (player == Player.Luigi)
            {
                sourceIndex |= 0x10;
            }

            if (state == PlayerState.Fire)
            {
                sourceIndex |= 0x20;
            }

            new Span<Color32BppArgb>(PlayerPaletteTable, sourceIndex, 0x10).CopyTo(
              dest.Slice(0xF0));
        }

        public void ReadPlayerPalettes(
            Span<Color32BppArgb> dest)
        {
            if (dest.Length < PlayerPaletteTable.Length)
            {
                throw new ArgumentException();
            }

            PlayerPaletteTable.CopyTo(dest);
        }

        public void WritePlayerPalettes(
            Span<Color32BppArgb> src)
        {
            if (src.Length > PlayerPaletteTable.Length)
            {
                throw new ArgumentException();
            }

            src.CopyTo(PlayerPaletteTable);
        }

        public void WritePalette(
            Span<Color32BppArgb> source,
            int index,
            bool isLuigiBonusArea = false)
        {
            if (source.Length < TotalPaletteSize)
            {
                throw new ArgumentException();
            }

            var sourceIndex = index * ColorsPerRow;
            for (var i = 0; i < RowsPerPalette; i++)
            {
                if (isLuigiBonusArea && i == BonusAreaRow)
                {
                    source.Slice(BonusAreaRow * ColorsPerRow, ColorsPerRow).CopyTo(
                        LuigiBonusAreaColorTable);
                }
                else
                {
                    var rowIndex = RowIndexTable[sourceIndex++];
                    var startIndex = IndexTable[rowIndex];
                    source.Slice(i * ColorsPerRow, ColorsPerRow).CopyTo(
                        new Span<Color32BppArgb>(ColorTable, startIndex, ColorsPerRow));
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
}
