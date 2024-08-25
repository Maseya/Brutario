// <copyright file="AreaObjectRendererPointers.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1.AreaData.ObjectData;

public class AreaObjectRendererPointers
{
    public static readonly AreaObjectRendererPointers Jp10 = new(
        baseAddress1: 0x03A48A,
        baseAddress2: 0x03A9B3,
        baseAddress3: 0x048F71);

    public static readonly AreaObjectRendererPointers Jp11 = new(
        baseAddress1: 0x03A48A,
        baseAddress2: 0x03A9B3,
        baseAddress3: 0x048F71);

    public static readonly AreaObjectRendererPointers Usa = new(
        baseAddress1: 0x03A45E,
        baseAddress2: 0x03A987,
        baseAddress3: 0x048F61);

    public static readonly AreaObjectRendererPointers UsaPlusW = new(
        baseAddress1: 0x03A45E,
        baseAddress2: 0x03A987,
        baseAddress3: 0x048F61);

    public static readonly AreaObjectRendererPointers Eu = new(
        baseAddress1: 0x03A50B,
        baseAddress2: 0x03AA34,
        singleTileObjectTablePointer: 0x03C01A,
        baseAddress3: 0x048F61);

    public static readonly AreaObjectRendererPointers EuPlusW = new(
        baseAddress1: 0x03A50B,
        baseAddress2: 0x03AA34,
        singleTileObjectTablePointer: 0x03C01A,
        baseAddress3: 0x048F61);

    public static readonly AreaObjectRendererPointers UsaSmb1 = new(
        baseAddress1: 0x00A493,
        baseAddress2: 0x00A9BC,
        baseAddress3: 0x018F61);

    public AreaObjectRendererPointers(
        int backgroundSceneryMetaDataOffsetTablePointer,
        int backgroundSceneryMetaDataTablePointer,
        int backgroundSceneryTileDataTablePointer,
        int foregroundSceneryDataOffsetTablePointer,
        int foregroundSceneryDataTablePointer,
        int terrainAreaTypeTablePointer,
        int terrainBitMaskTablePointer,
        int bitmaskTablePointer,
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

        PulleyRopeTileTablePointer = pulleyRopeTileTablePointer;
        JPipeTilesTable1Pointer = jPipeTiles1TablePointer;
        JPipeTilesTable2Pointer = jPipeTiles2TablePointer;
        JPipeTilesTable3Pointer = jPipeTiles3TablePointer;
        JPipeTilesTable4Pointer = jPipeTiles4TablePointer;
        PipeTileTablePointer = pipeTileTablePointer;
        WaterSurfaceTileTablePointer = waterSurfaceTileTablePointer;
        CoinRowTileTablePointer = coinRowTileTablePointer;
        BrickRowTileTablePointer = brickRowTileTablePointer;
        BlockRowTileTablePointer = stoneRowTileTablePointer;
        SingleTileObjectTablePointer = singleTileObjectTablePointer;
        CastleTileTablePointer = castleTileTablePointer;
        BlockStairYTablePointer = stoneStairYTablePointer;
        BlockStairHeightTablePointer = stoneStairHeightTablePointer;
        JPipeTilesTable5Pointer = jPipeTiles5TablePointer;
        JPipeTilesTable6Pointer = jPipeTiles6TablePointer;
        JPipeTilesTable7Pointer = jPipeTiles7TablePointer;
    }

    private AreaObjectRendererPointers(
            int baseAddress1,
        int baseAddress2,
        int baseAddress3)
        : this(
              baseAddress1,
              baseAddress2,
              singleTileObjectTablePointer: baseAddress2 + 0x2A4,
              baseAddress3)
    { }

    private AreaObjectRendererPointers(
        int baseAddress1,
        int baseAddress2,
        int singleTileObjectTablePointer,
        int baseAddress3)
      : this(
            backgroundSceneryMetaDataOffsetTablePointer: baseAddress1,
            backgroundSceneryMetaDataTablePointer: baseAddress1 + 0x07,
            backgroundSceneryTileDataTablePointer: baseAddress1 + 0x22,
            foregroundSceneryDataOffsetTablePointer: baseAddress1 + 0x37,
            foregroundSceneryDataTablePointer: baseAddress1 + 0x3C,
            terrainAreaTypeTablePointer: baseAddress1 + 0x87,
            terrainBitMaskTablePointer: baseAddress1 + 0x9A,
            bitmaskTablePointer: baseAddress1 + 0xB3,
            pulleyRopeTileTablePointer: baseAddress2,
            jPipeTiles1TablePointer: baseAddress2 + 0x2D,
            jPipeTiles2TablePointer: baseAddress2 + 0x3E,
            jPipeTiles3TablePointer: baseAddress2 + 0x44,
            jPipeTiles4TablePointer: baseAddress2 + 0x56,
            pipeTileTablePointer: baseAddress2 + 0xB7,
            waterSurfaceTileTablePointer: baseAddress2 + 0x106,
            coinRowTileTablePointer: baseAddress2 + 0x1A6,
            brickRowTileTablePointer: baseAddress2 + 0x1E6,
            stoneRowTileTablePointer: baseAddress2 + 0x1EE,
            singleTileObjectTablePointer: singleTileObjectTablePointer,
            castleTileTablePointer: baseAddress3,
            stoneStairYTablePointer: baseAddress3 + 0xCF,
            stoneStairHeightTablePointer: baseAddress3 + 0xD2,
            jPipeTiles5TablePointer: baseAddress3 + 0x233,
            jPipeTiles6TablePointer: baseAddress3 + 0x245,
            jPipeTiles7TablePointer: baseAddress3 + 0x24B)
    { }

    public int BackgroundSceneryMetaDataOffsetTablePointer { get; }

    public int BackgroundSceneryMetaDataTablePointer { get; }

    public int BackgroundSceneryTileDataTablePointer { get; }

    public int ForegroundSceneryDataOffsetTablePointer { get; }

    public int ForegroundSceneryDataTablePointer { get; }

    public int TerrainAreaTypeTablePointer { get; }

    public int TerrainBitMaskTablePointer { get; }

    public int BitmaskTablePointer { get; }

    public int PulleyRopeTileTablePointer { get; }

    public int JPipeTilesTable1Pointer { get; }

    public int JPipeTilesTable2Pointer { get; }

    public int JPipeTilesTable3Pointer { get; }

    public int JPipeTilesTable4Pointer { get; }

    public int PipeTileTablePointer { get; }

    public int WaterSurfaceTileTablePointer { get; }

    public int CoinRowTileTablePointer { get; }

    public int BrickRowTileTablePointer { get; }

    public int BlockRowTileTablePointer { get; }

    public int SingleTileObjectTablePointer { get; }

    public int CastleTileTablePointer { get; }

    public int BlockStairYTablePointer { get; }

    public int BlockStairHeightTablePointer { get; }

    public int JPipeTilesTable5Pointer { get; }

    public int JPipeTilesTable6Pointer { get; }

    public int JPipeTilesTable7Pointer { get; }
}
