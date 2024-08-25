// <copyright file="AreaObjectRenderer.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1.AreaData.ObjectData;

using System;
using System.Collections.Generic;

using HeaderData;

using Snes;

public class AreaObjectRenderer
{
    /// <summary>
    /// The height of the tile buffer. This field is constant.
    /// </summary>
    public const int TileBufferSize = 0x0D;

    private const int BackgroundSceneryMetaDataOffsetTableSize = 3;
    private const int BackgroundSceneryMetaDataTableSize = 0x90;
    private const int BackgroundSceneryTileDataTableSize = 0x24;

    private const int ForegroundSceneryDataOffsetTableSize = 3;
    private const int ForegroundSceneryDataTableSize = 0x27;

    private const int TerrainAreaTypeTableSize = 4;
    private const int TerrainBitMaskTableSize = 0x20;

    private const int BitmaskTableSize = 8;

    private const int PulleyRopeTileTableSize = 3;
    private const int JPipeTilesTable1Size = 4;
    private const int JPipeTilesTable2Size = 4;
    private const int JPipeTilesTable3Size = 4;
    private const int JPipeTilesTable4Size = 8;
    private const int PipeTileTableSize = 8;
    private const int WaterSurfaceTileTableSize = 6;
    private const int CoinRowTileTableSize = 4;
    private const int BrickRowTileTableSize = 5;
    private const int BlockRowTileTableSize = 4;
    private const int SingleTileObjectTableSize = 0x0E;
    private const int CastleTileTableSize = 0x6E;
    private const int BlockStairYTableSize = 9;
    private const int BlockStairHeightTableSize = 9;
    private const int JPipeTilesTable5Size = 4;
    private const int JPipeTilesTable6Size = 4;
    private const int JPipeTilesTable7Size = 4;

    public AreaObjectRenderer(Rom rom, AreaObjectRendererPointers pointers)
    {
        BackgroundSceneryMetaDataOffsetTable = rom.ReadBytesIndirectIndexed(
            pointers.BackgroundSceneryMetaDataOffsetTablePointer,
            index: 1,
            BackgroundSceneryMetaDataOffsetTableSize);
        BackgroundSceneryMetaDataTable = rom.ReadBytesIndirect(
            pointers.BackgroundSceneryMetaDataTablePointer,
            BackgroundSceneryMetaDataTableSize);
        BackgroundSceneryTileDataTable = rom.ReadBytesIndirect(
            pointers.BackgroundSceneryTileDataTablePointer,
            BackgroundSceneryTileDataTableSize);
        ForegroundSceneryDataOffsetTable = rom.ReadBytesIndirectIndexed(
            pointers.ForegroundSceneryDataOffsetTablePointer,
            index: 1,
            ForegroundSceneryDataOffsetTableSize);
        ForegroundSceneryDataTable = rom.ReadBytesIndirect(
            pointers.ForegroundSceneryDataTablePointer,
            ForegroundSceneryDataTableSize);
        TerrainAreaTypeTable = rom.ReadBytesIndirect(
            pointers.TerrainAreaTypeTablePointer,
            TerrainAreaTypeTableSize);
        TerrainBitMaskTable = rom.ReadBytesIndirect(
            pointers.TerrainBitMaskTablePointer,
            TerrainBitMaskTableSize);
        BitmaskTable = rom.ReadBytesIndirect(
            pointers.BitmaskTablePointer,
            BitmaskTableSize);

        PulleyRopeTileTable = rom.ReadBytesIndirect(
            pointers.PulleyRopeTileTablePointer,
            PulleyRopeTileTableSize);
        JPipeTilesTable1 = rom.ReadBytesIndirect(
            pointers.JPipeTilesTable1Pointer,
            JPipeTilesTable1Size);
        JPipeTilesTable2 = rom.ReadBytesIndirect(
            pointers.JPipeTilesTable2Pointer,
            JPipeTilesTable2Size);
        JPipeTilesTable3 = rom.ReadBytesIndirect(
            pointers.JPipeTilesTable3Pointer,
            JPipeTilesTable3Size);
        JPipeTilesTable4 = rom.ReadBytesIndirect(
            pointers.JPipeTilesTable4Pointer,
            JPipeTilesTable4Size);
        PipeTileTable = rom.ReadBytesIndirect(
            pointers.PipeTileTablePointer,
            PipeTileTableSize);
        WaterSurfaceTileTable = rom.ReadBytesIndirect(
            pointers.WaterSurfaceTileTablePointer,
            WaterSurfaceTileTableSize);
        CoinRowTileTable = rom.ReadBytesIndirect(
            pointers.CoinRowTileTablePointer,
            CoinRowTileTableSize);
        BrickRowTileTable = rom.ReadBytesIndirect(
            pointers.BrickRowTileTablePointer,
            BrickRowTileTableSize);
        BlockRowTileTable = rom.ReadBytesIndirect(
            pointers.BlockRowTileTablePointer,
            BlockRowTileTableSize);
        SingleTileObjectTable = rom.ReadBytesIndirect(
            pointers.SingleTileObjectTablePointer,
            SingleTileObjectTableSize);
        CastleTileTable = rom.ReadBytesIndirect(
            pointers.CastleTileTablePointer,
            CastleTileTableSize);
        BlockStairYTable = rom.ReadBytesIndirect(
            pointers.BlockStairYTablePointer,
            BlockStairYTableSize);
        BlockStairHeightTable = rom.ReadBytesIndirect(
            pointers.BlockStairHeightTablePointer,
            BlockStairHeightTableSize);
        JPipeTilesTable5 = rom.ReadBytesIndirect(
            pointers.JPipeTilesTable5Pointer,
            JPipeTilesTable5Size);
        JPipeTilesTable6 = rom.ReadBytesIndirect(
            pointers.JPipeTilesTable6Pointer,
            JPipeTilesTable6Size);
        JPipeTilesTable7 = rom.ReadBytesIndirect(
            pointers.JPipeTilesTable7Pointer,
            JPipeTilesTable7Size);

        TileBuffer = new int[TileBufferSize];
    }

    public byte[] BackgroundSceneryMetaDataOffsetTable { get; }

    public byte[] BackgroundSceneryMetaDataTable { get; }

    public byte[] BackgroundSceneryTileDataTable { get; }

    public byte[] ForegroundSceneryDataOffsetTable { get; }

    public byte[] ForegroundSceneryDataTable { get; }

    public byte[] TerrainAreaTypeTable { get; }

    public byte[] TerrainBitMaskTable { get; }

    public byte[] BitmaskTable { get; }

    public byte[] PulleyRopeTileTable { get; }

    public byte[] JPipeTilesTable1 { get; }

    public byte[] JPipeTilesTable2 { get; }

    public byte[] JPipeTilesTable3 { get; }

    public byte[] JPipeTilesTable4 { get; }

    public byte[] PipeTileTable { get; }

    public byte[] WaterSurfaceTileTable { get; }

    public byte[] CoinRowTileTable { get; }

    public byte[] BrickRowTileTable { get; }

    public byte[] BlockRowTileTable { get; }

    public byte[] SingleTileObjectTable { get; }

    public byte[] CastleTileTable { get; }

    public byte[] BlockStairYTable { get; }

    public byte[] BlockStairHeightTable { get; }

    public byte[] JPipeTilesTable5 { get; }

    public byte[] JPipeTilesTable6 { get; }

    public byte[] JPipeTilesTable7 { get; }

    public AreaType AreaType
    {
        get;
        private set;
    }

    public AreaHeader CurrentHeader
    {
        get;
        set;
    }

    public bool IsCloudPlatform
    {
        get
        {
            return CurrentHeader.AreaPlatformType == AreaPlatformType.CloudGround;
        }
    }

    public int[] TileBuffer
    {
        get;
    }

    /// <summary>
    /// Gets or sets $0725, which represents the screen the renderer is currently on.
    /// </summary>
    public int CurrentRenderingPageX
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets $0726, which represents the X-coordinate of the current screen the
    /// renderer is currently on.
    /// </summary>
    public int CurrentRenderingPage
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the full X-coordinate the renderer is currently on.
    /// </summary>
    public int CurrentRenderingX
    {
        get
        {
            return (CurrentRenderingPage * 0x10) + CurrentRenderingPageX;
        }

        set
        {
            CurrentRenderingPage = value >> 4;
            CurrentRenderingPageX = value & 0x0F;
        }
    }

    public void WriteToGameData(
        Rom rom,
        AreaObjectRendererPointers pointers)
    {
        rom.WriteBytesIndirect(
            pointers.JPipeTilesTable7Pointer,
            JPipeTilesTable7);
        rom.WriteBytesIndirect(
            pointers.JPipeTilesTable6Pointer,
            JPipeTilesTable6);
        rom.WriteBytesIndirect(
            pointers.JPipeTilesTable5Pointer,
            JPipeTilesTable5);
        rom.WriteBytesIndirect(
            pointers.BlockStairHeightTablePointer,
            BlockStairHeightTable);
        rom.WriteBytesIndirect(
            pointers.BlockStairYTablePointer,
            BlockStairYTable);
        rom.WriteBytesIndirect(
            pointers.CastleTileTablePointer,
            CastleTileTable);
        rom.WriteBytesIndirect(
            pointers.SingleTileObjectTablePointer,
            SingleTileObjectTable);
        rom.WriteBytesIndirect(
            pointers.BlockRowTileTablePointer,
            BlockRowTileTable);
        rom.WriteBytesIndirect(
            pointers.BrickRowTileTablePointer,
            BrickRowTileTable);
        rom.WriteBytesIndirect(
            pointers.CoinRowTileTablePointer,
            CoinRowTileTable);
        rom.WriteBytesIndirect(
            pointers.WaterSurfaceTileTablePointer,
            WaterSurfaceTileTable);
        rom.WriteBytesIndirect(
            pointers.PipeTileTablePointer,
            PipeTileTable);
        rom.WriteBytesIndirect(
            pointers.JPipeTilesTable4Pointer,
            JPipeTilesTable4);
        rom.WriteBytesIndirect(
            pointers.JPipeTilesTable3Pointer,
            JPipeTilesTable3);
        rom.WriteBytesIndirect(
            pointers.JPipeTilesTable2Pointer,
            JPipeTilesTable2);
        rom.ReadBytesIndirect(
            pointers.JPipeTilesTable1Pointer,
            JPipeTilesTable1);
        rom.WriteBytesIndirect(
            pointers.PulleyRopeTileTablePointer,
            PulleyRopeTileTable);

        rom.WriteBytesIndirect(
            pointers.BitmaskTablePointer,
            BitmaskTable);
        rom.WriteBytesIndirect(
            pointers.TerrainBitMaskTablePointer,
            TerrainBitMaskTable);
        rom.WriteBytesIndirect(
            pointers.TerrainAreaTypeTablePointer,
            TerrainAreaTypeTable);
        rom.WriteBytesIndirect(
            pointers.ForegroundSceneryDataTablePointer,
            ForegroundSceneryDataTable);
        rom.WriteBytesIndirectIndexed(
            pointers.ForegroundSceneryDataOffsetTablePointer,
            1,
            ForegroundSceneryDataOffsetTable);
        rom.WriteBytesIndirect(
            pointers.BackgroundSceneryTileDataTablePointer,
            BackgroundSceneryTileDataTable);
        rom.WriteBytesIndirect(
            pointers.BackgroundSceneryMetaDataTablePointer,
            BackgroundSceneryMetaDataTable);
        rom.WriteBytesIndirectIndexed(
            pointers.BackgroundSceneryMetaDataOffsetTablePointer,
            1,
            BackgroundSceneryMetaDataOffsetTable);
    }

    public void RenderTileMap(
        Span<int> tilemap,
        AreaType areaType,
        AreaHeader header,
        IList<AreaObjectCommand> areaObjectData,
        bool isUnderwaterCastle)
    {
        CurrentHeader = header;
        AreaType = areaType;
        var areaObjectParser = new AreaObjectParser(
            this,
            areaObjectData);

        tilemap.Clear();

        areaObjectParser.ResetBuffer();
        for (CurrentRenderingPage = 0; CurrentRenderingPage < 0x20; CurrentRenderingPage++)
        {
            for (CurrentRenderingPageX = 0; CurrentRenderingPageX < 0x10; CurrentRenderingPageX++)
            {
                ResetTileBuffer();
                RenderBackground();
                RenderForeground();
                RenderTerrain(isUnderwaterCastle);
                areaObjectParser.ParseAreaData();
                WriteBufferToTileMap(tilemap);
            }
        }
    }

    private void ResetTileBuffer()
    {
        for (var i = 0; i < TileBuffer.Length; i++)
        {
            TileBuffer[i] = 0;
        }
    }

    private void WriteBufferToTileMap(Span<int> tilemap)
    {
        var startIndex = (2 * 0x20 * 0x10) + CurrentRenderingX;
        for (var i = 0; i < TileBuffer.Length; i++)
        {
            tilemap[startIndex + (i * 0x20 * 0x10)] = TileBuffer[i];
        }

        if (TileBuffer[0] == 0x40)
        {
            tilemap[startIndex + (-1 * 0x20 * 0x10)] = 0x40;
            tilemap[startIndex + (-2 * 0x20 * 0x10)] = 0x40;
        }
    }

    private void RenderBackground()
    {
        if (CurrentHeader.BackgroundScenery == BackgroundScenery.None)
        {
            return;
        }

        var sceneryTile = GetBackgroundSceneryMetaTile(
            CurrentHeader.BackgroundScenery,
            CurrentRenderingPage,
            CurrentRenderingPageX);

        if (sceneryTile == 0)
        {
            return;
        }

        var startIndex = ((sceneryTile & 0x0F) - 1) * 3;
        var y = sceneryTile >> 4;
        for (var i = 0; i < 3; i++)
        {
            var tile = BackgroundSceneryTileDataTable[startIndex + i];
            TileBuffer[y + i] = tile;

            if (y + i + 1 == 0x0B)
            {
                break;
            }
        }
    }

    private void RenderForeground()
    {
        if (CurrentHeader.ForegroundScenery == ForegroundScenery.None)
        {
            return;
        }

        var index = GetForegroundSceneryIndex(CurrentHeader.ForegroundScenery);
        var waterTileChange = false;
        for (var y = 0; y < 0x0D; y++)
        {
            var tile = ForegroundSceneryDataTable[index + y];
            if (tile == 0)
            {
                continue;
            }

            if (AreaType != AreaType.Water)
            {
                if (AreaType == AreaType.Castle && tile == 0x86)
                {
                    tile += 2;
                }
            }
            else
            {
                if (waterTileChange)
                {
                    waterTileChange = false;
                }
                else
                {
                    waterTileChange = true;
                    tile += 1 + 2;
                }
            }

            TileBuffer[y] = tile;
        }
    }

    private void RenderTerrain(bool isUnderwaterCastle)
    {
        var useOtherCastleTile = false;
        var castleTile = (byte)0;
        var castleLongTileOffset = (CurrentRenderingPageX & 1) != 0;

        // In the ROM, the actual check is for an underwater area in World
        // 8. That's a dumb check and I'm not keeping track of the world number, so
        // I'll do this check for the time being. See $03:A4D5 in the disassembly.
        var terrainTile = isUnderwaterCastle
            ? (byte)0x65
            : IsCloudPlatform
            ? (byte)0x8C
            : TerrainAreaTypeTable[(int)AreaType];

        var y = 0;
        var terrainIndex = (int)CurrentHeader.TerrainMode << 1;

        var areaType2 = isUnderwaterCastle ? AreaType.Castle : AreaType;

    terrain_loop:
        var terrainBits = TerrainBitMaskTable[terrainIndex];

        terrainIndex++;
        if (IsCloudPlatform && y != 0)
        {
            terrainBits &= 8;
        }

        var j = 0;
    bit_loop:
        var bitmask = BitmaskTable[j];
        if ((terrainBits & bitmask) != 0)
        {
            if (areaType2 == AreaType.Castle && castleTile != 0)
            {
                terrainTile = 0x68;
            }

            TileBuffer[y] = terrainTile;
            if (castleTile != 0 && areaType2 == AreaType.Castle)
            {
                castleTile++;
                if (castleTile == 0)
                {
                    TileBuffer[y]++;
                    terrainTile++;
                }
            }
            else if (areaType2 == AreaType.Castle
                && !useOtherCastleTile
                && !castleLongTileOffset)
            {
                TileBuffer[y]++;
            }
        }
        else
        {
            castleTile = 0xFE;
            useOtherCastleTile = true;
        }

        if (++y == 0x0D)
        {
            goto end_loop;
        }

        if (AreaType == AreaType.Underground && y == 0x0B)
        {
            terrainTile = 0x56;
        }

        castleLongTileOffset ^= true;
        if (++j != 8)
        {
            goto bit_loop;
        }

        if (terrainIndex != 0)
        {
            goto terrain_loop;
        }

    end_loop:
        terrainTile = (byte)TileBuffer[0x0C];
        if (terrainTile is 0x56 or 0x72)
        {
            TileBuffer[0x0C] = terrainTile + 1;
        }
    }

    private int GetBackgroundSceneryMetaTile(
        BackgroundScenery sceneryType,
        int page,
        int x)
    {
        var xIndex = ((page % 3) << 4) + x;

        // Should never have scenery type 0.
        var sceneryOffset = BackgroundSceneryMetaDataOffsetTable[
            (int)sceneryType - 1];
        return BackgroundSceneryMetaDataTable[sceneryOffset + xIndex];
    }

    private int GetForegroundSceneryIndex(ForegroundScenery sceneryType)
    {
        return ForegroundSceneryDataOffsetTable[(int)sceneryType - 1];
    }
}
