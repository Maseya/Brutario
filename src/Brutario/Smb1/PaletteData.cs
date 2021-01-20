// <copyright file="PaletteData.cs" company="Public Domain">
//     Copyright (c) 2020 Nelson Garcia. All rights reserved. Licensed under
//     GNU Affero General Public License. See LICENSE in project root for full
//     license information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Smb1
{
    using System;

    public class PaletteData
    {
        public PaletteData(GameData romData, PalettePointers palettePointers)
        {
            GameData = romData
                ?? throw new ArgumentNullException(nameof(romData));

            PalettePointers = palettePointers
                ?? throw new ArgumentNullException(nameof(palettePointers));

            GameData.AreaNumberChanged += AreaIndexChanged;

            LoadPalette();
        }

        public GameData GameData
        {
            get;
        }

        public RomIO Rom
        {
            get
            {
                return GameData.Rom;
            }
        }

        public PalettePointers PalettePointers
        {
            get;
        }

        public Color32BppArgb[] CurrentPalette
        {
            get;
            private set;
        }

        public int RowIndexTableAddress
        {
            get
            {
                var bank = PalettePointers.RowIndexTablePointer & ~0xFFFF;
                return bank | Rom.ReadInt16(PalettePointers.RowIndexTablePointer);
            }
        }

        public int IndexTableAddress
        {
            get
            {
                var bank = PalettePointers.IndexTablePointer & ~0xFFFF;
                return bank | Rom.ReadInt16(PalettePointers.IndexTablePointer);
            }
        }

        public int DataAddress
        {
            get
            {
                var bank = PalettePointers.DataPointer & ~0xFFFF;
                return bank | Rom.ReadInt16(PalettePointers.DataPointer);
            }
        }

        public int LuigiBonusAreaAddress
        {
            get
            {
                var bank = PalettePointers.LuigiBonusAreaDataPointer;
                return bank | Rom.ReadInt16(PalettePointers.LuigiBonusAreaDataPointer);
            }
        }

        public int GetSourceIndex(int areaIndex)
        {
            return areaIndex << 4;
        }

        public int GetSourceAddress(int sourceIndex)
        {
            return RowIndexTableAddress + sourceIndex;
        }

        public int GetPaletteRowAddress(int paletteRowIndex)
        {
            return IndexTableAddress + (paletteRowIndex << 1);
        }

        public int GetPaletteRowIndex(int sourceIndex)
        {
            var sourceAddress = GetSourceAddress(sourceIndex);
            return Rom.ReadByte(sourceAddress);
        }

        public void SetPaletteRowIndex(int sourceIndex, int value)
        {
            var sourceAddress = GetSourceAddress(sourceIndex);
            Rom.WriteByte(sourceAddress, value);
        }

        public int GetPaletteIndex(int paletteRowIndex)
        {
            var paletteRowAddress = GetPaletteRowAddress(paletteRowIndex);
            return Rom.ReadInt16(paletteRowAddress);
        }

        public void SetPaletteIndex(int paletteRowIndex, int value)
        {
            var paletteRowAddress = GetPaletteRowAddress(paletteRowIndex);
            Rom.WriteInt16(paletteRowAddress, value);
        }

        public Color32BppArgb[] CreatePalette(int areaIndex)
        {
            return CreatePalette(areaIndex, false);
        }

        public Color32BppArgb[] CreatePalette(
            int areaIndex,
            bool isLuigiBonusArea)
        {
            var result = new Color32BppArgb[0x100];
            var sourceIndex = GetSourceIndex(areaIndex);
            for (var destIndex = 0; destIndex < result.Length;)
            {
                var paletteRowIndex = GetPaletteRowIndex(sourceIndex++);
                var paletteIndex = GetPaletteIndex(paletteRowIndex);
                var address = DataAddress + paletteIndex;

                for (var i = 0; i < 0x10; i++)
                {
                    var color = Rom.ReadInt16(address + (i << 1));
                    result[destIndex++] = Color32BppArgb.FromSnesColor(color);
                }
            }

            if (isLuigiBonusArea)
            {
                var address = LuigiBonusAreaAddress;
                for (var i = 0; i < 0x10; i++)
                {
                    var color = Rom.ReadInt16(address + (i << 1));
                    result[PalettePointers.LuigiBonusAreaIndex + i] =
                        Color32BppArgb.FromSnesColor(color);
                }
            }

            return result;
        }

        protected void LoadPalette()
        {
            CurrentPalette = CreatePalette(GameData.AreaIndex);
        }

        private void AreaIndexChanged(object sender, EventArgs e)
        {
            LoadPalette();
        }
    }
}
