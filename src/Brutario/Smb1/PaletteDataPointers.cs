// <copyright file="PalettePointers.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed under
//     GNU Affero General Public License. See LICENSE in project root for full
//     license information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Smb1
{
    public class PaletteDataPointers
    {
        public static readonly PaletteDataPointers Jp10 = new PaletteDataPointers(
            baseAddress: 0x049630);

        public static readonly PaletteDataPointers Jp11 = new PaletteDataPointers(
            baseAddress: 0x049630);

        public static readonly PaletteDataPointers Usa = new PaletteDataPointers(
            baseAddress: 0x04961D);

        public static readonly PaletteDataPointers UsaPlusW = new PaletteDataPointers(
            baseAddress: 0x04961D);

        public static readonly PaletteDataPointers Eu = new PaletteDataPointers(
            baseAddress: 0x049643);

        public static readonly PaletteDataPointers EuPlusW = new PaletteDataPointers(
            baseAddress: 0x049643);

        public static readonly PaletteDataPointers UsaSmb1 = new PaletteDataPointers(
            baseAddress: 0x01961D);

        public PaletteDataPointers(
            int rowIndexTablePointer,
            int indexTablePointer,
            int colorTablePointer,
            int luigiBonusAreaColorTablePointer)
        {
            RowIndexTablePointer = rowIndexTablePointer;
            IndexTablePointer = indexTablePointer;
            ColorTablePointer = colorTablePointer;
            LuigiBonusAreaColorTablePointer = luigiBonusAreaColorTablePointer;
        }

        private PaletteDataPointers(int baseAddress)
                    : this(
                  rowIndexTablePointer: baseAddress,
                  indexTablePointer: baseAddress + 0x0B,
                  colorTablePointer: baseAddress + 0x16,
                  luigiBonusAreaColorTablePointer: baseAddress + 0x4C)
        {
        }

        public int RowIndexTablePointer
        {
            get;
        }

        public int IndexTablePointer
        {
            get;
        }

        public int ColorTablePointer
        {
            get;
        }

        public int LuigiBonusAreaColorTablePointer
        {
            get;
        }
    }
}
