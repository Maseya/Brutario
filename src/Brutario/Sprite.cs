using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brutario
{
    public struct Sprite : IEquatable<Sprite>
    {
        public Sprite(int x, int y, ChrTile tile, TileProperties tileProperties = 0)
        {
            X = x;
            Y = y;
            Tile = tile;
            TileProperties = tileProperties;
        }

        public int X
        {
            get;
            set;
        }

        public int Y
        {
            get;
            set;
        }

        public ChrTile Tile
        {
            get;
            set;
        }

        public TileProperties TileProperties
        {
            get;
            set;
        }

        public bool Equals(Sprite other)
        {
            return X.Equals(other.X)
                && Y.Equals(other.Y)
                && Tile.Equals(other.Tile)
                && TileProperties.Equals(other.TileProperties);
        }

        public override bool Equals(object obj)
        {
            return obj is Sprite other ? Equals(other) : false;
        }

        public override int GetHashCode()
        {
            return Tile.GetHashCode() ^ X ^ Y;
        }

        public override string ToString()
        {
            return $"{Tile}:{X:X3},{Y:X3}";
        }
    }
}
