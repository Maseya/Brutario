using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brutario.Smb1
{
    public class GfxData
    {
        public const int AreaGfxAddress = 0x68000;

        public const int AreaGfxSize = 0x4000;

        public const int SpriteGfxAddress = 0x78000;

        public const int SpriteGfxSize = 0x4000;

        public const int TotalGfxSize = 0x400 * 0x20;

        public const int AnimatedGfxAddress = 0x6C000;

        public const int AnimatedGfxSize = 0x4000;

        public const int MarioGfxAddress = 0xA8000;

        public const int LuigiGfxAddress = 0xAC000;

        public const int PlayerGfxSize = 0x2000;

        public const int MenuGfxAddress = 0x0CF800;

        public const int MenuGfxSize = 0x800;

        public const int PlayerBonusRoomTileSetIndexPointer = 0x05E6C3;

        public const int GraphicsPageBankBytePointer = 0x05E82D;

        public const int GraphicsPageWordPointer = 0x05E835;

        public const int GraphicsPageDestPointer = 0x05E83B;

        public const int GraphicsPageSizePointer = 0x05E841;

        public const int LevelBasedGfxPagePointer = 0x03FF9E;

        public GfxData(GameData romData)
        {
            RomData = romData
                ?? throw new ArgumentNullException(nameof(romData));

            PixelData = new byte[0x28000];

            AreaPixelData = GfxToPixelMap(
                Rom.ReadBytes(AreaGfxAddress, AreaGfxSize, true));

            SpritePixelData = GfxToPixelMap(
                Rom.ReadBytes(SpriteGfxAddress, SpriteGfxSize, true));

            AnimatedPixelData = GfxToPixelMap(
                Rom.ReadBytes(AnimatedGfxAddress, AnimatedGfxSize, true));

            MarioGfxData = GfxToPixelMap(
                Rom.ReadBytes(MarioGfxAddress, PlayerGfxSize, true));

            LuigiGfxData = GfxToPixelMap(
                Rom.ReadBytes(LuigiGfxAddress, PlayerGfxSize, true));

            MenuGfxData = Gfx2BppToPixelMap(
                Rom.ReadBytes(MenuGfxAddress, MenuGfxSize, true));

            Array.Copy(AreaPixelData, PixelData, AreaPixelData.Length);

            Array.Copy(
                sourceArray: SpritePixelData,
                sourceIndex: 0,
                destinationArray: PixelData,
                destinationIndex: 0xC000,
                length: SpritePixelData.Length);

            Array.Copy(
                sourceArray: AnimatedPixelData,
                sourceIndex: 0,
                destinationArray: PixelData,
                destinationIndex: 0x14000,
                length: AnimatedPixelData.Length);

            Array.Copy(
                sourceArray: MarioGfxData,
                sourceIndex: 0,
                destinationArray: PixelData,
                destinationIndex: 0x1C000,
                length: MarioGfxData.Length);

            Array.Copy(
                sourceArray: LuigiGfxData,
                sourceIndex: 0,
                destinationArray: PixelData,
                destinationIndex: 0x20000,
                length: LuigiGfxData.Length);

            Array.Copy(
                sourceArray: MenuGfxData,
                sourceIndex: 0,
                destinationArray: PixelData,
                destinationIndex: 0x24000,
                length: MenuGfxData.Length);

            TileSetActions = new Action[0x20]
            {
                null,
                WriteUnderGroundTileSet,
                WriteGrassTileSet,
                WriteUnderGroundTileSet,
                WriteBowserCastleTileSet,
                WriteGrassTileSet,
                null,
                WriteStarryNightTileSet,
                null,
                WriteStarryNightTileSet,
                WriteGameOverTileSet,
                WriteGrassTileSet,
                WriteGrassTileSet,
                null,
                WriteGrassTileSet,
                null,
                WriteGrassTileSet,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                WriteUnderGroundTileSet,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
            };
        }

        public GameData RomData
        {
            get;
        }

        public RomIO Rom
        {
            get
            {
                return RomData.Rom;
            }
        }

        public byte[] PixelData
        {
            get;
        }

        public byte[] AnimatedPixelData
        {
            get;
        }

        public int PlayerBonusRoomTileSetIndexAddress
        {
            get
            {
                return 0x050000 | Rom.ReadInt16(
                    PlayerBonusRoomTileSetIndexPointer);
            }
        }

        public int GraphicsPageBankByteAddress
        {
            get
            {
                return 0x050000 | Rom.ReadInt16(
                    GraphicsPageBankBytePointer);
            }
        }

        public int GraphicsPageWordAddress
        {
            get
            {
                return 0x050000 | Rom.ReadInt16(
                    GraphicsPageWordPointer);
            }
        }

        public int GraphicsPageDestAddress
        {
            get
            {
                return 0x050000 | Rom.ReadInt16(
                    GraphicsPageDestPointer);
            }
        }

        public int GraphicsPageSizeAddress
        {
            get
            {
                return 0x050000 | Rom.ReadInt16(
                    GraphicsPageSizePointer);
            }
        }

        public int LevelBasedGfxPageAddress
        {
            get
            {
                return 0x050000 | Rom.ReadInt16(
                    LevelBasedGfxPagePointer);
            }
        }

        public int AnimationFrame
        {
            get
            {
                return RomData.AnimationFrame;
            }
        }

        public int GraphicsPage
        {
            get;
            private set;
        }

        public bool IsBonusArea
        {
            get;
            private set;
        }

        private byte[] AreaPixelData
        {
            get;
        }

        private byte[] SpritePixelData
        {
            get;
        }

        private byte[] MarioGfxData
        {
            get;
        }

        private byte[] LuigiGfxData
        {
            get;
        }

        private byte[] MenuGfxData
        {
            get;
        }

        private TilemapLoader TilemapLoader
        {
            get
            {
                return RomData.TilemapLoader;
            }
        }

        private Action[] TileSetActions
        {
            get;
        }

        public void Animate()
        {
            Array.Copy(
                sourceArray: AnimatedPixelData,
                sourceIndex: 0x40 * 0x40 * ((AnimationFrame & 0x38) >> 3),
                destinationArray: PixelData,
                destinationIndex: 0x3000,
                length: 0x30 * 0x40);
        }

        public void LoadTileSet(int graphicsPage)
        {
            if (graphicsPage == 1)
            {
                IsBonusArea = true;
                GraphicsPage = Rom.ReadByte(
                    PlayerBonusRoomTileSetIndexAddress +
                    (int)RomData.CurrentPlayer);
            }
            else
            {
                GraphicsPage = graphicsPage;
            }

            WriteTileSet(GraphicsPage);
            var action = TileSetActions[GraphicsPage & 0x1F];
            action?.Invoke();
        }

        private static byte[] GfxToPixelMap(byte[] gfxData)
        {
            var tileCount = gfxData.Length / 0x20;
            var result = new byte[0x40 * tileCount];
            var pixelIndex = 0;

            for (var tileIndex = 0; tileIndex < tileCount; tileIndex++)
            {
                var gfxIndex = tileIndex * 0x20;
                for (var y = 0; y < 8; y++)
                {
                    var offset = gfxIndex + (y << 1);

                    var val1 = gfxData[offset + 0];
                    var val2 = gfxData[offset + 1];
                    var val3 = gfxData[offset + 0 + (2 * 8)];
                    var val4 = gfxData[offset + 1 + (2 * 8)];

                    for (var x = 8; --x >= 0;)
                    {
                        result[pixelIndex++] = (byte)(
                            (((val1 >> x) & 1) << 0) |
                            (((val2 >> x) & 1) << 1) |
                            (((val3 >> x) & 1) << 2) |
                            (((val4 >> x) & 1) << 3));
                    }
                }
            }

            return result;
        }

        private static byte [] Gfx2BppToPixelMap(byte[] gfxData)
        {
            var tileCount = gfxData.Length / 0x10;
            var result = new byte[0x40 * tileCount];
            var pixelIndex = 0;

            for (var tileIndex = 0; tileIndex < tileCount; tileIndex++)
            {
                var gfxIndex = tileIndex * 0x10;
                for (var y = 0; y < 8; y++)
                {
                    var offset = gfxIndex + (y << 1);

                    var val1 = gfxData[offset + 0];
                    var val2 = gfxData[offset + 1];

                    for (var x = 8; --x >= 0;)
                    {
                        result[pixelIndex++] = (byte)(
                            (((val1 >> x) & 1) << 0) |
                            (((val2 >> x) & 1) << 1));
                    }
                }
            }

            return result;
        }

        private void WriteTileSet(int tileSetIndex)
        {
            var index = tileSetIndex << 1;
            var bank = Rom.ReadInt16(
                GraphicsPageBankByteAddress + index);

            var word = Rom.ReadInt16(
                GraphicsPageWordAddress + index);

            var dest = Rom.ReadInt16(
                GraphicsPageDestAddress + index);

            var size = Rom.ReadInt16(
                GraphicsPageSizeAddress + index);

            var src = (bank << 0x10) | word;
            var gfx = GfxToPixelMap(Rom.ReadBytes(src, size, true));

            var destIndex = (dest - 0x1000) << 2;
            Array.Copy(
                sourceArray: gfx,
                sourceIndex: 0,
                destinationArray: PixelData,
                destinationIndex: destIndex,
                length: gfx.Length);
        }

        private void WriteGrassTileSet()
        {
            var areaIndex = RomData.AreaIndex;
            if (areaIndex == 0x16 || areaIndex == 0x14 || areaIndex == 0x0D)
            {
                WriteTileSet(0x12);
            }
            else
            {
                WriteTileSet(0x17);
            }
        }

        private void WriteUnderGroundTileSet()
        {
            WriteTileSet(0x11);
        }

        private void WriteStarryNightTileSet()
        {
            WriteTileSet(0x16);
            WriteTileSet(0x12);
        }

        private void WriteBowserCastleTileSet()
        {
            WriteTileSet(0x13);
            WriteTileSet(0x14);
        }

        private void WriteGameOverTileSet()
        {
            WriteTileSet(0x15);
        }
    }
}
