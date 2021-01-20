using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Brutario.Smb1
{
    public class GameData
    {
        public const int Map16DataLowBytePointer = 0x03927D;

        public const int Map16DataHighBytePointer = 0x039282;

        private int _areaNumber;

        public GameData(RomIO rom)
        {
            Rom = rom ?? throw new ArgumentOutOfRangeException(nameof(rom));
            if (Rom.HeaderlessSize < 0x200000)
            {
                throw new ArgumentException(
                    "ROM data size is insufficient.");
            }

            AreaLoader = new AreaLoader(this);
            PaletteData = new PaletteData(this, PalettePointers.Usa());
            GfxData = new GfxData(this);
            AreaBg2Map = new AreaBg2Map(this);
            Map16Data = new Obj16Tile[0x200];
            var offsets = new byte[] { 0x00, 0x40, 0x80, 0xC0 };
            var sizes = new byte[] { 0x2B, 0x38, 0x0E, 0x3E };
            for (var i = 0; i < 4; i++)
            {
                var low = Rom.ReadByte(Map16DataLowByteAddress + i);
                var high = Rom.ReadByte(Map16DataHighByteAddress + i);
                var address = 0x030000 | (high << 8) | low;

                var tiles = Rom.ReadBytes(address, sizes[i] << 3, true);
                unsafe
                {
                    fixed (byte* src = tiles)
                    fixed (Obj16Tile* dest = Map16Data)
                    {
                        Buffer.MemoryCopy(
                            src,
                            dest + offsets[i],
                            0x800,
                            tiles.Length);
                    }
                }
            }

            TilemapLoader = new TilemapLoader(this);

            AreaObjectRenderer = new AreaObjectRenderer(AreaObjectLoader);
            AreaSpriteRenderer = new AreaSpriteRenderer(AreaSpriteLoader);

            AreaNumber = AreaLoader.GetAreaNumber(0, 0);
        }

        public event EventHandler AreaNumberChanged;

        public RomIO Rom
        {
            get;
        }

        public AreaLoader AreaLoader
        {
            get;
        }

        public TilemapLoader TilemapLoader
        {
            get;
        }

        public AreaObjectLoader AreaObjectLoader
        {
            get
            {
                return AreaLoader.AreaObjectLoader;
            }
        }

        public AreaSpriteLoader AreaSpriteLoader
        {
            get
            {
                return AreaLoader.AreaSpriteLoader;
            }
        }

        public AreaObjectRenderer AreaObjectRenderer
        {
            get;
        }

        public AreaSpriteRenderer AreaSpriteRenderer
        {
            get;
        }

        public AreaBg2Map AreaBg2Map
        {
            get;
        }

        public AreaHeader CurrentAreaHeader
        {
            get;
            set;
        }

        public List<AreaObjectCommand> CurrentObjectData
        {
            get;
            private set;
        }

        public List<AreaSpriteCommand> CurrentSpriteData
        {
            get;
            private set;
        }

        public int AnimationFrame
        {
            get;
            private set;
        }

        public Color32BppArgb[] CurrentPalette
        {
            get
            {
                return PaletteData.CurrentPalette;
            }
        }

        public byte[] PixelData
        {
            get
            {
                return GfxData.PixelData;
            }
        }

        public Obj16Tile[] Map16Data
        {
            get;
        }

        public PaletteData PaletteData
        {
            get;
        }

        public GfxData GfxData
        {
            get;
        }

        public Player CurrentPlayer
        {
            get;
            set;
        }

        public int AreaNumber
        {
            get
            {
                return _areaNumber;
            }

            set
            {
                if (AreaNumber == value)
                {
                    return;
                }

                _areaNumber = value;
                OnAreaNumberChanged(EventArgs.Empty);
            }
        }

        public AreaType AreaType
        {
            get
            {
                return (AreaType)((AreaNumber & 0x7F) >> 5);
            }
        }

        public int AreaIndex
        {
            get
            {
                return AreaObjectLoader.GetAreaIndex(AreaNumber);
            }
        }

        public int Map16DataLowByteAddress
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(Map16DataLowBytePointer);
            }
        }

        public int Map16DataHighByteAddress
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(Map16DataHighBytePointer);
            }
        }

        public void Animate(int frame)
        {
            AnimationFrame = frame;
            GfxData?.Animate();
        }

        public ObjTile[] RenderScreenTiles(int[] tiles, int width)
        {
            if (tiles is null)
            {
                throw new ArgumentNullException(nameof(tiles));
            }

            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }

            if ((tiles.Length % width) != 0)
            {
                throw new ArgumentException();
            }

            var height = tiles.Length / width;
            var result = new ObjTile[tiles.Length * 4];
            for (var y = 0; y < height; y++)
            {
                var rowIndex = y * width;
                var rowIndex2 = rowIndex << 2;
                for (var x = 0; x < width; x++)
                {
                    var x2 = x << 1;
                    var index = rowIndex + x;
                    var tileIndex = (byte)tiles[index];
                    var tile = Map16Data[tileIndex];
                    if (tileIndex == 0x56 || tileIndex == 0x57)
                    {
                        if (x > 0 && ((byte)tiles[index - 1]) == 0)
                        {
                            tile.TopLeft += 4;
                            tile.BottomLeft += 4;
                        }

                        if (x + 1 < width && ((byte)tiles[index + 1]) == 0)
                        {
                            tile.TopRight += 4;
                            tile.BottomRight += 4;
                        }
                    }

                    result[rowIndex2 + x2] = tile.TopLeft;
                    result[rowIndex2 + x2 + 1] = tile.TopRight;
                    result[rowIndex2 + (width << 1) + x2] = tile.BottomLeft;
                    result[rowIndex2 + (width << 1) + x2 + 1] = tile.BottomRight;
                }
            }

            return result;
        }

        public ObjTile[] RenderBg2Tiles(int[] tiles, int width)
        {
            if (tiles is null)
            {
                throw new ArgumentNullException(nameof(tiles));
            }

            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }

            if ((tiles.Length % width) != 0)
            {
                throw new ArgumentException();
            }

            var height = (tiles.Length / width) + 2;
            var result = new ObjTile[width * height * 4];
            for (var x = 0; x < width; x++)
            {
                var x2 = x << 1;
                var tile = Map16Data[0x100];
                result[x2] = tile.TopLeft;
                result[x2 + 1] = tile.BottomLeft;
                result[(width << 1) + x2] = tile.TopRight;
                result[(width << 1) + x2 + 1] = tile.BottomRight;
            }

            for (var s = 0; s < 8; s++)
            {
                var screenIndex = s * 0x100;
                var screenIndex2 = s * 0x20;
                for (var y = 0; y < 0x0E; y++)
                {
                    var rowIndex = screenIndex + (y * 0x10);
                    var rowIndex2 = screenIndex2 + (((y + 1) << 1) * (width << 1));
                    for (var x = 0; x < 0x10; x++)
                    {
                        var x2 = x << 1;
                        var index = rowIndex + x;
                        var tileIndex = (byte)tiles[index];
                        if (tileIndex == 0xFF && index + 1 < tiles.Length)
                        {
                            if (index > 0 && (byte)tiles[index + 1] == 0xFF)
                            {
                                return result;
                            }
                        }

                        var tile = Map16Data[0x100 + tileIndex];
                        result[rowIndex2 + x2] = tile.TopLeft;
                        result[rowIndex2 + x2 + 1] = tile.BottomLeft;
                        result[rowIndex2 + (width << 1) + x2] = tile.TopRight;
                        result[rowIndex2 + (width << 1) + x2 + 1] = tile.BottomRight;
                    }
                }
            }

            return result;
        }

        protected virtual void OnAreaNumberChanged(EventArgs e)
        {
            UpdateArea();
            UpdateMap16();
            TilemapLoader.LoadTilemap(AreaIndex);
            GfxData.LoadTileSet(TilemapLoader.TileSetIndex);
            AreaNumberChanged?.Invoke(this, e);
        }

        private void UpdateMap16()
        {
            var bg2 = AreaBg2Map.GetTileSet(AreaIndex);
            Array.Copy(
                sourceArray: bg2,
                sourceIndex: 0,
                destinationArray: Map16Data,
                destinationIndex: 0x100,
                length: 0x100);
        }

        private void UpdateArea()
        {
            var objectAddress = AreaObjectLoader.GetAreaAddress(AreaNumber);
            CurrentAreaHeader = AreaObjectLoader.GetAreaHeader(objectAddress);
            CurrentObjectData = new List<AreaObjectCommand>(
                AreaObjectLoader.GetAreaData(objectAddress + 2));

            AreaObjectRenderer.RenderTileMap(
                AreaType,
                CurrentAreaHeader,
                CurrentObjectData);

            var spriteAddress = AreaSpriteLoader.GetAreaAddress(AreaNumber);
            CurrentSpriteData = new List<AreaSpriteCommand>(
                AreaSpriteLoader.GetAreaData(spriteAddress));
        }
    }
}
