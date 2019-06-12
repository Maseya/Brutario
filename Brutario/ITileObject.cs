using System.Collections.Generic;

namespace Brutario
{
    public interface ITileObject
    {
        int X
        {
            get; set;
        }

        int Y
        {
            get; set;
        }

        int Width
        {
            get;
            set;
        }

        int Height
        {
            get;
            set;
        }

        void WriteToTileMap(IList<int> tileMap);
    }
}
