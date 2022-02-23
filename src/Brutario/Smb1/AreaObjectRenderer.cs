// <copyright file="AreaObjectRenderer.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed under
//     GNU Affero General Public License. See LICENSE in project root for full
//     license information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Smb1
{
    using System;
    using System.Collections.Generic;

    public class AreaObjectRenderer
    {
        /// <summary>
        /// The height of the tile buffer. This field is constant.
        /// </summary>
        public const int TileBufferSize = 0x0D;

        public const int ScreenCount = 0x20;
        public const int ScreenWidth = 0x10;
        public const int ScreenHeight = 0x10;
        public const int TilemapWidth = ScreenCount * ScreenWidth;
        public const int TilemapLength = TilemapWidth * ScreenHeight;

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
        private const int StoneRowTileTableSize = 4;
        private const int SingleTileObjectTableSize = 0x0E;
        private const int CastleTileTableSize = 0x6E;
        private const int StoneStairYTableSize = 9;
        private const int StoneStairHeightTableSize = 9;
        private const int JPipeTilesTable5Size = 4;
        private const int JPipeTilesTable6Size = 4;
        private const int JPipeTilesTable7Size = 4;

        public AreaObjectRenderer()
        {
            BackgroundSceneryMetaDataOffsetTable =
                new byte[BackgroundSceneryMetaDataOffsetTableSize];
            BackgroundSceneryMetaDataTable =
                new byte[BackgroundSceneryMetaDataTableSize];
            BackgroundSceneryTileDataTable =
                new byte[BackgroundSceneryTileDataTableSize];
            ForegroundSceneryDataOffsetTable =
                new byte[ForegroundSceneryDataOffsetTableSize];
            ForegroundSceneryDataTable = new byte[ForegroundSceneryDataTableSize];
            TerrainAreaTypeTable = new byte[TerrainAreaTypeTableSize];
            TerrainBitMaskTable = new byte[TerrainBitMaskTableSize];
            BitmaskTable = new byte[BitmaskTableSize];

            PulleyRopeTileTable = new byte[PulleyRopeTileTableSize];
            JPipeTilesTable1 = new byte[JPipeTilesTable1Size];
            JPipeTilesTable2 = new byte[JPipeTilesTable2Size];
            JPipeTilesTable3 = new byte[JPipeTilesTable3Size];
            JPipeTilesTable4 = new byte[JPipeTilesTable4Size];
            PipeTileTable = new byte[PipeTileTableSize];
            WaterSurfaceTileTable = new byte[WaterSurfaceTileTableSize];
            CoinRowTileTable = new byte[CoinRowTileTableSize];
            BrickRowTileTable = new byte[BrickRowTileTableSize];
            StoneRowTileTable = new byte[StoneRowTileTableSize];
            SingleTileObjectTable = new byte[SingleTileObjectTableSize];
            CastleTileTable = new byte[CastleTileTableSize];
            StoneStairYTable = new byte[StoneStairYTableSize];
            StoneStairHeightTable = new byte[StoneStairHeightTableSize];
            JPipeTilesTable5 = new byte[JPipeTilesTable5Size];
            JPipeTilesTable6 = new byte[JPipeTilesTable6Size];
            JPipeTilesTable7 = new byte[JPipeTilesTable7Size];

            TileBuffer = new int[TileBufferSize];
            TileMap = new int[TilemapLength];
        }

        public AreaObjectRenderer(
            GameData gameData,
            AreaObjectRendererPointers pointers)
            : this()
        {
            ReadGameData(gameData, pointers);
        }

        public int[] TileMap { get; }

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

        public byte[] StoneRowTileTable { get; }

        public byte[] SingleTileObjectTable { get; }

        public byte[] CastleTileTable { get; }

        public byte[] StoneStairYTable { get; }

        public byte[] StoneStairHeightTable { get; }

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
        /// Gets or sets $0725, which represents the screen the renderer is
        /// currently on.
        /// </summary>
        public int CurrentRenderingScreenX
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets $0726, which represents the X-coordinate of the
        /// current screen the renderer is currently on.
        /// </summary>
        public int CurrentRenderingScreen
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
                return (CurrentRenderingScreen * 0x10) + CurrentRenderingScreenX;
            }

            set
            {
                CurrentRenderingScreen = value >> 4;
                CurrentRenderingScreenX = value & 0x0F;
            }
        }

        public void ReadGameData(GameData gameData, AreaObjectRendererPointers pointers)
        {
            if (gameData is null)
            {
                throw new ArgumentNullException(nameof(gameData));
            }

            if (pointers is null)
            {
                throw new ArgumentNullException(nameof(pointers));
            }

            var rom = gameData.Rom;
            rom.ReadBytesIndirectIndexed(
                pointers.BackgroundSceneryMetaDataOffsetTablePointer,
                1,
                BackgroundSceneryMetaDataOffsetTable);
            rom.ReadBytesIndirect(
                pointers.BackgroundSceneryMetaDataTablePointer,
                BackgroundSceneryMetaDataTable);
            rom.ReadBytesIndirect(
                pointers.BackgroundSceneryTileDataTablePointer,
                BackgroundSceneryTileDataTable);
            rom.ReadBytesIndirectIndexed(
                pointers.ForegroundSceneryDataOffsetTablePointer,
                1,
                ForegroundSceneryDataOffsetTable);
            rom.ReadBytesIndirect(
                pointers.ForegroundSceneryDataTablePointer,
                ForegroundSceneryDataTable);
            rom.ReadBytesIndirect(
                pointers.TerrainAreaTypeTablePointer,
                TerrainAreaTypeTable);
            rom.ReadBytesIndirect(
                pointers.TerrainBitMaskTablePointer,
                TerrainBitMaskTable);
            rom.ReadBytesIndirect(
                pointers.BitmaskTablePointer,
                BitmaskTable);

            rom.ReadBytesIndirect(
                pointers.PulleyRopeTileTablePointer,
                PulleyRopeTileTable);
            rom.ReadBytesIndirect(
                pointers.JPipeTilesTable1Pointer,
                JPipeTilesTable1);
            rom.ReadBytesIndirect(
                pointers.JPipeTilesTable2Pointer,
                JPipeTilesTable2);
            rom.ReadBytesIndirect(
                pointers.JPipeTilesTable3Pointer,
                JPipeTilesTable3);
            rom.ReadBytesIndirect(
                pointers.JPipeTilesTable4Pointer,
                JPipeTilesTable4);
            rom.ReadBytesIndirect(
                pointers.PipeTileTablePointer,
                PipeTileTable);
            rom.ReadBytesIndirect(
                pointers.WaterSurfaceTileTablePointer,
                WaterSurfaceTileTable);
            rom.ReadBytesIndirect(
                pointers.CoinRowTileTablePointer,
                CoinRowTileTable);
            rom.ReadBytesIndirect(
                pointers.BrickRowTileTablePointer,
                BrickRowTileTable);
            rom.ReadBytesIndirect(
                pointers.StoneRowTileTablePointer,
                StoneRowTileTable);
            rom.ReadBytesIndirect(
                pointers.SingleTileObjectTablePointer,
                SingleTileObjectTable);
            rom.ReadBytesIndirect(
                pointers.CastleTileTablePointer,
                CastleTileTable);
            rom.ReadBytesIndirect(
                pointers.StoneStairYTablePointer,
                StoneStairYTable);
            rom.ReadBytesIndirect(
                pointers.StoneStairHeightTablePointer,
                StoneStairHeightTable);
            rom.ReadBytesIndirect(
                pointers.JPipeTilesTable5Pointer,
                JPipeTilesTable5);
            rom.ReadBytesIndirect(
                pointers.JPipeTilesTable6Pointer,
                JPipeTilesTable6);
            rom.ReadBytesIndirect(
                pointers.JPipeTilesTable7Pointer,
                JPipeTilesTable7);
        }

        public void WriteToGameData(
            GameData gameData,
            AreaObjectRendererPointers pointers)
        {
            if (gameData is null)
            {
                throw new ArgumentNullException(nameof(gameData));
            }

            if (pointers is null)
            {
                throw new ArgumentNullException(nameof(pointers));
            }

            var rom = gameData.Rom;
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
                pointers.StoneStairHeightTablePointer,
                StoneStairHeightTable);
            rom.WriteBytesIndirect(
                pointers.StoneStairYTablePointer,
                StoneStairYTable);
            rom.WriteBytesIndirect(
                pointers.CastleTileTablePointer,
                CastleTileTable);
            rom.WriteBytesIndirect(
                pointers.SingleTileObjectTablePointer,
                SingleTileObjectTable);
            rom.WriteBytesIndirect(
                pointers.StoneRowTileTablePointer,
                StoneRowTileTable);
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

            Array.Clear(TileMap, 0, TileMap.Length);

            areaObjectParser.ResetBuffer();
            for (CurrentRenderingScreen = 0; CurrentRenderingScreen < 0x20; CurrentRenderingScreen++)
            {
                for (CurrentRenderingScreenX = 0; CurrentRenderingScreenX < 0x10; CurrentRenderingScreenX++)
                {
                    ResetTileBuffer();
                    RenderBackground();
                    RenderForeground();
                    RenderTerrain(isUnderwaterCastle);
                    areaObjectParser.ParseAreaData();
                    WriteBufferToTileMap();
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

        private void WriteBufferToTileMap()
        {
            var startIndex = (2 * 0x20 * 0x10) + CurrentRenderingX;
            for (var i = 0; i < TileBuffer.Length; i++)
            {
                TileMap[startIndex + (i * 0x20 * 0x10)] = TileBuffer[i];
            }

            if (TileBuffer[0] == 0x40)
            {
                TileMap[startIndex + (-1 * 0x20 * 0x10)] = 0x40;
                TileMap[startIndex + (-2 * 0x20 * 0x10)] = 0x40;
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
                CurrentRenderingScreen,
                CurrentRenderingScreenX);

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
            var castleLongTileOffset = (CurrentRenderingScreenX & 1) != 0;

            // In the ROM, the actual check is for an underwater area in World
            // 8. That's a dumb check and I'm not keeping track of the world
            // number, so I'll do this check for the time being. See $03:A4D5
            // in the disassembly.
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
            if (terrainTile == 0x56 || terrainTile == 0x72)
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
}
