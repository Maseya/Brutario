using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brutario.Smb1
{
    public class PaletteData
    {
        public const int UnderWaterLevelAreaIndex = 0x01;

        public const int AutoWalkAreaIndex = 0x0C;

        public const int UndergroundAreaIndex = 0x19;

        public const int LuigiBonusAreaPalettePointer = 0x049669;

        public const int LuigiBonusAreaPaletteIndex = 0xE0 >> 1;

        public const int PaletteRowIndexTablePointer = 0x04961D;

        public const int PaletteIndexTablePointer = 0x049628;

        public const int PaletteDataPointer = 0x49633;

        public PaletteData(RomData romData)
        {
            RomData = romData
                ?? throw new ArgumentNullException(nameof(romData));

            RomData.AreaNumberChanged += AreaIndexChanged;

            if ((PaletteRowIndexTableAddress & 0x8000) == 0 ||
                (PaletteIndexTableAddress & 0x8000) == 0 ||
                (PaletteDataAddress & 0x8000) == 0)
            {
                throw new ArgumentException(
                    "Could not find palette data pointers in ROM data.");
            }

            LoadPalette();
        }

        public RomData RomData
        {
            get;
        }

        public RomIO Rom
        {
            get
            {
                return RomData.Rom;
            }
        }

        public Color32BppArgb[] CurrentPalette
        {
            get;
            private set;
        }

        public int PaletteRowIndexTableAddress
        {
            get
            {
                return 0x040000 | Rom.ReadInt16(PaletteRowIndexTablePointer);
            }
        }

        public int PaletteIndexTableAddress
        {
            get
            {
                return 0x040000 | Rom.ReadInt16(PaletteIndexTablePointer);
            }
        }

        public int PaletteDataAddress
        {
            get
            {
                return 0x040000 | Rom.ReadInt16(PaletteDataPointer);
            }
        }

        public int LuigiBonusAreaPaletteAddress
        {
            get
            {
                return 0x040000 | Rom.ReadInt16(LuigiBonusAreaPalettePointer);
            }
        }

        public Color32BppArgb[] GetPalette(
            int areaIndex,
            bool isLuigiBonusArea)
        {
            var result = new Color32BppArgb[0x100];
            var srcIndex = PaletteRowIndexTableAddress + (areaIndex << 4);
            for (var destIndex = 0; destIndex < 0x100;)
            {
                var paletteRowIndex = Rom.ReadByte(srcIndex++);

                var paletteIndex = Rom.ReadInt16(
                    PaletteIndexTableAddress + (paletteRowIndex << 1));

                var address = PaletteDataAddress + paletteIndex;

                for (var i = 0; i < 0x20; i += 2)
                {
                    var color = Rom.ReadInt16(address + i);
                    result[destIndex++] = Color32BppArgb.FromSnesColor(color);
                }
            }

            if (isLuigiBonusArea)
            {
                var address = LuigiBonusAreaPaletteAddress;
                for (var i = 0; i < 0x10; i++)
                {
                    var color = Rom.ReadInt16(address + (i << 1));
                    result[LuigiBonusAreaPaletteIndex + i] =
                        Color32BppArgb.FromSnesColor(color);
                }
            }

            return result;
        }

        protected void LoadPalette()
        {
            CurrentPalette = GetPalette(RomData.AreaIndex, false);
        }

        private void AreaIndexChanged(object sender, EventArgs e)
        {
            LoadPalette();
        }
    }
}
