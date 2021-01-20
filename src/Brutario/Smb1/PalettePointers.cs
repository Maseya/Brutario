// <copyright file="PalettePointers.cs" company="Public Domain">
//     Copyright (c) 2020 Nelson Garcia. All rights reserved. Licensed under
//     GNU Affero General Public License. See LICENSE in project root for full
//     license information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Smb1
{
    public class PalettePointers
    {
        public PalettePointers(
            int rowIndexTablePointer,
            int indexTablePointer,
            int dataPointer,
            int luigiBonusAreaDataPointer)
            : this(
                rowIndexTablePointer,
                indexTablePointer,
                dataPointer,
                luigiBonusAreaDataPointer,
                autoWalkAreaIndex: 0x0C,
                undergroundAreaIndex: 0x19,
                underWaterLevelAreaIndex: 0x01,
                luigiBonusAreaIndex: 0xE0 >> 1)
        {
        }

        public PalettePointers(
            int rowIndexTablePointer,
            int indexTablePointer,
            int dataPointer,
            int luigiBonusDataPointer,
            int autoWalkAreaIndex,
            int undergroundAreaIndex,
            int underWaterLevelAreaIndex,
            int luigiBonusAreaIndex)
        {
            RowIndexTablePointer = rowIndexTablePointer;
            IndexTablePointer = indexTablePointer;
            DataPointer = dataPointer;
            LuigiBonusAreaDataPointer = luigiBonusDataPointer;
            AutoWalkAreaIndex = autoWalkAreaIndex;
            UndergroundAreaIndex = undergroundAreaIndex;
            UnderWaterLevelAreaIndex = underWaterLevelAreaIndex;
            LuigiBonusAreaIndex = luigiBonusAreaIndex;
        }

        public int RowIndexTablePointer
        {
            get;
            set;
        }

        public int IndexTablePointer
        {
            get;
            set;
        }

        public int DataPointer
        {
            get;
            set;
        }

        public int LuigiBonusAreaDataPointer
        {
            get;
            set;
        }

        public int AutoWalkAreaIndex
        {
            get;
            set;
        }

        public int UndergroundAreaIndex
        {
            get;
            set;
        }

        public int UnderWaterLevelAreaIndex
        {
            get;
            set;
        }

        public int LuigiBonusAreaIndex
        {
            get;
            set;
        }

        public static PalettePointers Usa()
        {
            return new PalettePointers(
                rowIndexTablePointer: 0x04961D,
                indexTablePointer: 0x049628,
                dataPointer: 0x49633,
                luigiBonusAreaDataPointer: 0x049669);
        }
    }
}
