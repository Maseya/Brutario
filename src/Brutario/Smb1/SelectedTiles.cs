using System;
using System.Collections.Generic;

namespace Brutario.Smb1
{
    public class SelectedTiles
    {
        public SelectedTiles(AreaObjectLoader areaObjectLoader)
        {
            AreaObjectLoader = areaObjectLoader
                ?? throw new ArgumentNullException(nameof(areaObjectLoader));

            AreaObjectData = new List<AreaObjectCommand>();
        }

        public AreaObjectLoader AreaObjectLoader
        {
            get;
        }

        public AreaLoader AreaLoader
        {
            get
            {
                return AreaObjectLoader.AreaLoader;
            }
        }

        public GameData RomData
        {
            get
            {
                return AreaLoader.RomData;
            }
        }

        public RomIO Rom
        {
            get
            {
                return RomData.Rom;
            }
        }

        public List<AreaObjectCommand> AreaObjectData
        {
            get;
        }

        public int GetIndexOfObject(int x, int y)
        {
            var screen = 0;
            var result = -1;
            for (var i = 0; i < AreaObjectData.Count; i++)
            {
                var command = AreaObjectData[i];
                var fullX = screen | command.X;
                if (fullX > x)
                {
                    break;
                }

                if (fullX == x && y == command.Y)
                {
                    result = i;
                }
            }

            return -result;
        }

        public TileProperties[] GetTileProperties(
            IEnumerable<AreaObjectCommand> areaObjectData)
        {
            var result = new TileProperties[0x10 * 0x20 * 0x10];
            foreach (var command in areaObjectData)
            {
            }

            return result;
        }
    }
}
