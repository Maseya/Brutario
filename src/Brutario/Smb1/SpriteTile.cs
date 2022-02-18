namespace Brutario.Smb1
{
    public struct SpriteTile
    {
        public int TileIndex;
        public int PaletteIndex;
        public int Priority;
        public bool FlipX;
        public bool FlipY;

        public SpriteTile(
            int tileIndex,
            int paletteIndex,
            int priority,
            bool flipX,
            bool flipY)
        {
            TileIndex = tileIndex;
            PaletteIndex = paletteIndex;
            Priority = priority;
            FlipX = flipX;
            FlipY = flipY;
        }

        public SpriteTile(ChrTile tile, int tileStartIndex)
        {
            TileIndex = tile.TileIndex + tileStartIndex;
            PaletteIndex = tile.PaletteIndex + 8;
            Priority = (int)tile.Priority * 3 + 2;
            FlipX = tile.XFlipped;
            FlipY = tile.YFlipped;
        }

        public SpriteTile(ObjTile tile, int tileStartIndex, int layer)
        {
            TileIndex = tile.TileIndex + tileStartIndex;
            PaletteIndex = tile.PaletteIndex;
            Priority = (int)tile.Priority * 3 + layer;
            FlipX = tile.XFlipped;
            FlipY = tile.YFlipped;
        }
    }
}
