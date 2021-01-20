using System;
using System.Collections.Generic;

namespace Brutario.Smb1
{
    public class AreaObjectRenderer
    {
        /// <summary>
        /// The height of the tile buffer. This field is constant.
        /// </summary>
        public const int TileBufferSize = 0x0D;

        public const int BackgroundSceneryMetaDataOffsetPointer = 0x3A45E;

        public const int BackgroundSceneryMetaDataPointer = 0x03A465;

        public const int BackgroundSceneryTileDataPointer = 0x03A480;

        public const int ForegroundSceneryDataOffsetPointer = 0x03A495;

        public const int ForegroundSceneryDataPointer = 0x03A49A;

        public const int TerrainAreaTypePointer = 0x03A4E5;

        public const int TerrainBitMaskPointer = 0x03A4F8;

        public const int BitmaskTablePointer = 0x03A511;

        public AreaObjectRenderer(
            AreaObjectLoader areaObjectLoader)
        {
            AreaObjectLoader = areaObjectLoader;
            TileBuffer = new int[TileBufferSize];
            TileMap = new int[0x20 * 0x10 * 0x10];
        }

        public AreaObjectLoader AreaObjectLoader
        {
            get;
        }

        public AreaLoader AreaLoader
        {
            get
            {
                return AreaObjectLoader.AreaLoader;
            }
        }

        public GameData RomData
        {
            get
            {
                return AreaLoader.RomData;
            }
        }

        public RomIO Rom
        {
            get
            {
                return RomData.Rom;
            }
        }

        public int[] TileMap
        {
            get;
        }

        public int BackgroundSceneryMetaDataOffsetAddress
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(BackgroundSceneryMetaDataOffsetPointer);
            }
        }

        public int BackgroundSceneryMetaDataAddress
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(BackgroundSceneryMetaDataPointer);
            }
        }

        public int BackgroundSceneryTileDataAddress
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(BackgroundSceneryTileDataPointer);
            }
        }

        public int ForegroundSceneryDataOffsetAddress
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(ForegroundSceneryDataOffsetPointer);
            }
        }

        public int ForegroundSceneryDataAddress
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(ForegroundSceneryDataPointer);
            }
        }

        public int TerrainAreaTypeAddress
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(TerrainAreaTypePointer);
            }
        }

        public int TerrainBitMaskAddress
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(TerrainBitMaskPointer);
            }
        }

        public int BitmaskTableAddress
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(BitmaskTablePointer);
            }
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
                return CurrentHeader.MiscPlatformType == AreaPlatformType.CloudGround;
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
                return (CurrentRenderingScreen * 0x10)
                    + CurrentRenderingScreenX;
            }

            set
            {
                CurrentRenderingScreen = value >> 4;
                CurrentRenderingScreenX = value & 0x0F;
            }
        }

        private AreaType AreaType2
        {
            get
            {
                return RomData.AreaNumber == 2 ? AreaType.Castle : AreaType;
            }
        }

        public void RenderTileMap(
            AreaType areaType,
            AreaHeader header,
            IList<AreaObjectCommand> areaObjectData)
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
                    RenderTerrain();
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
                var tile = Rom.ReadByte(BackgroundSceneryTileDataAddress + startIndex + i);
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
                var tile = Rom.ReadByte(
                    ForegroundSceneryDataAddress + index + y);

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

        private void RenderTerrain()
        {
            var useOtherCastleTile = false;
            var castleTile = (byte)0;
            var castleLongTileOffset = (CurrentRenderingScreenX & 1) != 0;

            // In the ROM, the actual check is for an underwater area in World
            // 8. That's a dumb check and I'm not keeping track of the world
            // number, so I'll do this check for the time being. See $03:A4D5
            // in the disassembly.
            var terrainTile = RomData.AreaNumber == 2
                ? (byte)0x65
                : IsCloudPlatform
                ? (byte)0x8C
                : Rom.ReadByte(
                    TerrainAreaTypeAddress + (int)AreaType);

            var y = 0;
            var terrainIndex = ((int)CurrentHeader.TerrainMode << 1);

            terrain_loop:
            var terrainBits = Rom.ReadByte(
                TerrainBitMaskAddress + terrainIndex);

            terrainIndex++;
            if (IsCloudPlatform && y != 0)
            {
                terrainBits &= 8;
            }

            var j = 0;
            bit_loop:
            var bitmask = Rom.ReadByte(BitmaskTableAddress + j);
            if ((terrainBits & bitmask) != 0)
            {
                if (AreaType2 == AreaType.Castle && castleTile != 0)
                {
                    terrainTile = 0x68;
                }

                TileBuffer[y] = terrainTile;
                if (castleTile != 0 && AreaType2 == AreaType.Castle)
                {
                    castleTile++;
                    if (castleTile == 0)
                    {
                        TileBuffer[y]++;
                        terrainTile++;
                    }
                }
                else if (AreaType2 == AreaType.Castle && !useOtherCastleTile && !castleLongTileOffset)
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

        private int GetBackgroundSceneryMetaTile(BackgroundScenery sceneryType, int page, int x)
        {
            var sceneryOffset = Rom.ReadByte(
                BackgroundSceneryMetaDataOffsetAddress + (int)sceneryType);

            var xIndex = ((page % 3) << 4) + x;
            return Rom.ReadByte(
                BackgroundSceneryMetaDataAddress + sceneryOffset + xIndex);
        }

        private int GetForegroundSceneryIndex(ForegroundScenery sceneryType)
        {
            return Rom.ReadByte(
                ForegroundSceneryDataOffsetAddress + (int)sceneryType);
        }
    }
}
