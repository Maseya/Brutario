namespace Brutario.Smb1
{
    public class Pointers
    {
        public static readonly Pointers Jp10 = new Pointers(
            PaletteDataPointers.Jp10,
            GfxDataPointers.Jp10,
            Map16DataPointers.Jp10,
            TilemapLoaderPointers.Jp10,
            AreaLoaderPointers.Jp10,
            AreaObjectRendererPointers.Jp10,
            AreaObjectParserPointers.Jp10);

        public static readonly Pointers Jp11 = new Pointers(
            PaletteDataPointers.Jp11,
            GfxDataPointers.Jp11,
            Map16DataPointers.Jp11,
            TilemapLoaderPointers.Jp11,
            AreaLoaderPointers.Jp11,
            AreaObjectRendererPointers.Jp11,
            AreaObjectParserPointers.Jp11);

        public static readonly Pointers Usa = new Pointers(
            PaletteDataPointers.Usa,
            GfxDataPointers.Usa,
            Map16DataPointers.Usa,
            TilemapLoaderPointers.Usa,
            AreaLoaderPointers.Usa,
            AreaObjectRendererPointers.Usa,
            AreaObjectParserPointers.Usa);

        public static readonly Pointers UsaPlusW = new Pointers(
            PaletteDataPointers.UsaPlusW,
            GfxDataPointers.UsaPlusW,
            Map16DataPointers.UsaPlusW,
            TilemapLoaderPointers.UsaPlusW,
            AreaLoaderPointers.UsaPlusW,
            AreaObjectRendererPointers.UsaPlusW,
            AreaObjectParserPointers.UsaPlusW);

        public static readonly Pointers Eu = new Pointers(
            PaletteDataPointers.Eu,
            GfxDataPointers.Eu,
            Map16DataPointers.Eu,
            TilemapLoaderPointers.Eu,
            AreaLoaderPointers.Eu,
            AreaObjectRendererPointers.Eu,
            AreaObjectParserPointers.Eu);

        public static readonly Pointers EuPlusW = new Pointers(
            PaletteDataPointers.EuPlusW,
            GfxDataPointers.EuPlusW,
            Map16DataPointers.EuPlusW,
            TilemapLoaderPointers.EuPlusW,
            AreaLoaderPointers.EuPlusW,
            AreaObjectRendererPointers.EuPlusW,
            AreaObjectParserPointers.EuPlusW);

        public static readonly Pointers UsaSmb1 = new Pointers(
            PaletteDataPointers.UsaSmb1,
            GfxDataPointers.UsaSmb1,
            Map16DataPointers.UsaSmb1,
            TilemapLoaderPointers.UsaSmb1,
            AreaLoaderPointers.UsaSmb1,
            AreaObjectRendererPointers.UsaSmb1,
            AreaObjectParserPointers.UsaSmb1);

        private Pointers(
            PaletteDataPointers paletteDataPointers,
            GfxDataPointers gfxDataPointers,
            Map16DataPointers map16DataPointers,
            TilemapLoaderPointers tilemapLoaderPointers,
            AreaLoaderPointers areaLoaderPointers,
            AreaObjectRendererPointers areaObjectRendererPointers,
            AreaObjectParserPointers areaObjectParserPointers)
        {
            PaletteDataPointers = paletteDataPointers;
            GfxDataPointers = gfxDataPointers;
            Map16DataPointers = map16DataPointers;
            TilemapLoaderPointers = tilemapLoaderPointers;
            AreaLoaderPointers = areaLoaderPointers;
            AreaObjectRendererPointers = areaObjectRendererPointers;
            AreaObjectParserPointers = areaObjectParserPointers;
        }

        public PaletteDataPointers PaletteDataPointers
        {
            get;
        }

        public GfxDataPointers GfxDataPointers
        {
            get;
        }

        public Map16DataPointers Map16DataPointers
        {
            get;
        }

        public TilemapLoaderPointers TilemapLoaderPointers
        {
            get;
        }

        public AreaLoaderPointers AreaLoaderPointers
        {
            get;
        }

        public AreaObjectRendererPointers AreaObjectRendererPointers
        {
            get;
        }

        public AreaObjectParserPointers AreaObjectParserPointers
        {
            get;
        }
    }
}
