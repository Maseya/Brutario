using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brutario.Smb1
{
    public class PaletteHelper
    {
        public ByteReference UnderWaterLevelAreaIndex
        {
            get;
        }

        public ByteReference AutoWalkAreaIndex
        {
            get;
        }

        public ByteReference UndergroundAreaIndex
        {
            get;
        }

        public ArrayReference LuigiBonusRoomPalette
        {
            get;
        }

        public ArrayReference PaletteRowIndexTable
        {
            get;
        }

        public ArrayReference PaletteIndexTable
        {
            get;
        }

        public ArrayReference PaletteData
        {
            get;
        }
    }
}
