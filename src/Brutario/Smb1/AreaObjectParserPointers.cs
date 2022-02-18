namespace Brutario.Smb1
{
    public class AreaObjectParserPointers
    {
        public static AreaObjectParserPointers Jp10 = new AreaObjectParserPointers(
            baseAddress1: 0x03A9B3,
            baseAddress2: 0x048F71);

        public static AreaObjectParserPointers Jp11 = new AreaObjectParserPointers(
            baseAddress1: 0x03A9B3,
            baseAddress2: 0x048F71);

        public static AreaObjectParserPointers Usa = new AreaObjectParserPointers(
            baseAddress1: 0x03A987,
            baseAddress2: 0x048F61);

        public static AreaObjectParserPointers UsaPlusW = new AreaObjectParserPointers(
            baseAddress1: 0x03A987,
            baseAddress2: 0x048F61);

        public static AreaObjectParserPointers Eu = new AreaObjectParserPointers(
            baseAddress1: 0x03AA34,
            singleTileObjectTablePointer: 0x03C01A,
            baseAddress2: 0x048F61);

        public static AreaObjectParserPointers EuPlusW = new AreaObjectParserPointers(
            baseAddress1: 0x03AA34,
            singleTileObjectTablePointer: 0x03C01A,
            baseAddress2: 0x048F61);

        public static AreaObjectParserPointers UsaSmb1 = new AreaObjectParserPointers(
            baseAddress1: 0x00A9BC,
            baseAddress2: 0x018F61);

        private AreaObjectParserPointers(
            int baseAddress1,
            int baseAddress2)
            : this(
            baseAddress1: baseAddress1,
            singleTileObjectTablePointer: baseAddress1 + 0x2A4,
            baseAddress2: baseAddress2)
        {
        }

        private AreaObjectParserPointers(
            int baseAddress1,
            int singleTileObjectTablePointer,
            int baseAddress2)
            : this(
            pulleyRopeTileTablePointer: baseAddress1,
            jPipeTiles1TablePointer: baseAddress1 + 0x2D,
            jPipeTiles2TablePointer: baseAddress1 + 0x3E,
            jPipeTiles3TablePointer: baseAddress1 + 0x44,
            jPipeTiles4TablePointer: baseAddress1 + 0x56,
            pipeTileTablePointer: baseAddress1 + 0xB7,
            waterSurfaceTileTablePointer: baseAddress1 + 0x106,
            coinRowTileTablePointer: baseAddress1 + 0x1A6,
            brickRowTileTablePointer: baseAddress1 + 0x1E6,
            stoneRowTileTablePointer: baseAddress1 + 0x1EE,
            singleTileObjectTablePointer: singleTileObjectTablePointer,
            castleTileTablePointer: baseAddress2,
            stoneStairYTablePointer: baseAddress2 + 0xCF,
            stoneStairHeightTablePointer: baseAddress2 + 0xD2,
            jPipeTiles5TablePointer: baseAddress2 + 0x233,
            jPipeTiles6TablePointer: baseAddress2 + 0x245,
            jPipeTiles7TablePointer: baseAddress2 + 0x24B)
        {
        }

        public AreaObjectParserPointers(
            int pulleyRopeTileTablePointer,
            int jPipeTiles1TablePointer,
            int jPipeTiles2TablePointer,
            int jPipeTiles3TablePointer,
            int jPipeTiles4TablePointer,
            int pipeTileTablePointer,
            int waterSurfaceTileTablePointer,
            int coinRowTileTablePointer,
            int brickRowTileTablePointer,
            int stoneRowTileTablePointer,
            int singleTileObjectTablePointer,
            int castleTileTablePointer,
            int stoneStairYTablePointer,
            int stoneStairHeightTablePointer,
            int jPipeTiles5TablePointer,
            int jPipeTiles6TablePointer,
            int jPipeTiles7TablePointer)
        {
            PulleyRopeTileTablePointer = pulleyRopeTileTablePointer;
            JPipeTilesTable1Pointer = jPipeTiles1TablePointer;
            JPipeTilesTable2Pointer = jPipeTiles2TablePointer;
            JPipeTilesTable3Pointer = jPipeTiles3TablePointer;
            JPipeTilesTable4Pointer = jPipeTiles4TablePointer;
            PipeTileTablePointer = pipeTileTablePointer;
            WaterSurfaceTileTablePointer = waterSurfaceTileTablePointer;
            CoinRowTileTablePointer = coinRowTileTablePointer;
            BrickRowTileTablePointer = brickRowTileTablePointer;
            StoneRowTileTablePointer = stoneRowTileTablePointer;
            SingleTileObjectTablePointer = singleTileObjectTablePointer;
            CastleTileTablePointer = castleTileTablePointer;
            StoneStairYTablePointer = stoneStairYTablePointer;
            StoneStairHeightTablePointer = stoneStairHeightTablePointer;
            JPipeTilesTable5Pointer = jPipeTiles5TablePointer;
            JPipeTilesTable6Pointer = jPipeTiles6TablePointer;
            JPipeTilesTable7Pointer = jPipeTiles7TablePointer;
        }

        public int PulleyRopeTileTablePointer { get; }

        public int JPipeTilesTable1Pointer { get; }

        public int JPipeTilesTable2Pointer { get; }

        public int JPipeTilesTable3Pointer { get; }

        public int JPipeTilesTable4Pointer { get; }

        public int PipeTileTablePointer { get; }

        public int WaterSurfaceTileTablePointer { get; }

        public int CoinRowTileTablePointer { get; }

        public int BrickRowTileTablePointer { get; }

        public int StoneRowTileTablePointer { get; }

        public int SingleTileObjectTablePointer { get; }

        public int CastleTileTablePointer { get; }

        public int StoneStairYTablePointer { get; }

        public int StoneStairHeightTablePointer { get; }

        public int JPipeTilesTable5Pointer { get; }

        public int JPipeTilesTable6Pointer { get; }

        public int JPipeTilesTable7Pointer { get; }
    }
}
