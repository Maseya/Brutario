namespace Brutario.Smb1
{
    public class AreaObjectRendererPointers
    {
        public static AreaObjectRendererPointers Jp10 = new AreaObjectRendererPointers(
            baseAddress: 0x03A48A);

        public static AreaObjectRendererPointers Jp11 = new AreaObjectRendererPointers(
            baseAddress: 0x03A48A);

        public static AreaObjectRendererPointers Usa = new AreaObjectRendererPointers(
            baseAddress: 0x03A45E);

        public static AreaObjectRendererPointers UsaPlusW = new AreaObjectRendererPointers(
            baseAddress: 0x03A45E);

        public static AreaObjectRendererPointers Eu = new AreaObjectRendererPointers(
            baseAddress: 0x03A50B);

        public static AreaObjectRendererPointers EuPlusW = new AreaObjectRendererPointers(
            baseAddress: 0x03A50B);

        public static AreaObjectRendererPointers UsaSmb1 = new AreaObjectRendererPointers(
            baseAddress: 0x00A493);

        public AreaObjectRendererPointers(
            int backgroundSceneryMetaDataOffsetTablePointer,
            int backgroundSceneryMetaDataTablePointer,
            int backgroundSceneryTileDataTablePointer,
            int foregroundSceneryDataOffsetTablePointer,
            int foregroundSceneryDataTablePointer,
            int terrainAreaTypeTablePointer,
            int terrainBitMaskTablePointer,
            int bitmaskTablePointer)
        {
            BackgroundSceneryMetaDataOffsetTablePointer =
                backgroundSceneryMetaDataOffsetTablePointer;
            BackgroundSceneryMetaDataTablePointer =
                backgroundSceneryMetaDataTablePointer;
            BackgroundSceneryTileDataTablePointer =
                backgroundSceneryTileDataTablePointer;
            ForegroundSceneryDataOffsetTablePointer =
                foregroundSceneryDataOffsetTablePointer;
            ForegroundSceneryDataTablePointer = foregroundSceneryDataTablePointer;
            TerrainAreaTypeTablePointer = terrainAreaTypeTablePointer;
            TerrainBitMaskTablePointer = terrainBitMaskTablePointer;
            BitmaskTablePointer = bitmaskTablePointer;
        }

        private AreaObjectRendererPointers(int baseAddress)
          : this(
                backgroundSceneryMetaDataOffsetTablePointer: baseAddress,
                backgroundSceneryMetaDataTablePointer: baseAddress + 0x07,
                backgroundSceneryTileDataTablePointer: baseAddress + 0x22,
                foregroundSceneryDataOffsetTablePointer: baseAddress + 0x37,
                foregroundSceneryDataTablePointer: baseAddress + 0x3C,
                terrainAreaTypeTablePointer: baseAddress + 0x87,
                terrainBitMaskTablePointer: baseAddress + 0x9A,
                bitmaskTablePointer: baseAddress + 0xB3)
        {
        }

        public int BackgroundSceneryMetaDataOffsetTablePointer { get; }

        public int BackgroundSceneryMetaDataTablePointer { get; }

        public int BackgroundSceneryTileDataTablePointer { get; }

        public int ForegroundSceneryDataOffsetTablePointer { get; }

        public int ForegroundSceneryDataTablePointer { get; }

        public int TerrainAreaTypeTablePointer { get; }

        public int TerrainBitMaskTablePointer { get; }

        public int BitmaskTablePointer { get; }
    }
}
