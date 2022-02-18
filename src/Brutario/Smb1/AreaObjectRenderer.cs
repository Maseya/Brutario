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

        private const int BackgroundSceneryMetaDataOffsetTableSize = 3;
        private const int BackgroundSceneryMetaDataTableSize = 0x90;
        private const int BackgroundSceneryTileDataTableSize = 0x24;

        private const int ForegroundSceneryDataOffsetTableSize = 3;
        private const int ForegroundSceneryDataTableSize = 0x27;

        private const int TerrainAreaTypeTableSize = 4;
        private const int TerrainBitMaskTableSize = 0x20;

        private const int BitmaskTableSize = 8;

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
        }

        public AreaObjectRenderer(
            GameData gameData,
            AreaObjectRendererPointers pointers)
            : this()
        {
            GameData = gameData
                ?? throw new ArgumentNullException(nameof(gameData));
            TileBuffer = new int[TileBufferSize];
            TileMap = new int[0x20 * 0x10 * 0x10];

            ReadGameData(gameData, pointers);
        }

        public int[] TileMap
        {
            get;
        }

        public byte[] BackgroundSceneryMetaDataOffsetTable
        {
            get;
        }

        public byte[] BackgroundSceneryMetaDataTable
        {
            get;
        }

        public byte[] BackgroundSceneryTileDataTable
        {
            get;
        }

        public byte[] ForegroundSceneryDataOffsetTable
        {
            get;
        }

        public byte[] ForegroundSceneryDataTable
        {
            get;
        }

        public byte[] TerrainAreaTypeTable
        {
            get;
        }

        public byte[] TerrainBitMaskTable
        {
            get;
        }

        public byte[] BitmaskTable
        {
            get;
        }

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

        private GameData GameData
        {
            get;
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
                areaObjectData,
                GameData,
                GameData.Pointers.AreaObjectParserPointers);

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
                        tile += 2;
                    }
                    else
                    {
                        waterTileChange = true;
                        tile += 1;
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
            // Should never have scenery type 0.
            var sceneryOffset = BackgroundSceneryMetaDataOffsetTable[
                (int)sceneryType - 1];

            var xIndex = ((page % 3) << 4) + x;
            return BackgroundSceneryMetaDataTable[sceneryOffset + xIndex];
        }

        private int GetForegroundSceneryIndex(ForegroundScenery sceneryType)
        {
            return ForegroundSceneryDataOffsetTable[(int)sceneryType - 1];
        }
    }
}
