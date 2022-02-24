// <copyright file="RomData.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed under GNU
//     Affero General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Smb1
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class GameData
    {
        private int _areaNumber;

        private AreaHeader _areaHeader;

        private Player _player;

        private PlayerState _playerState;

        public GameData(RomIO rom, Pointers pointers)
        {
            Rom = rom
                ?? throw new ArgumentOutOfRangeException(nameof(rom));
            Pointers = pointers
                ?? throw new ArgumentNullException(nameof(pointers));

            PaletteData = new PaletteData(this, pointers.PaletteDataPointers);
            GfxData = new GfxData(this, pointers.GfxDataPointers);
            Map16Data = new Map16Data(this, pointers.Map16DataPointers);
            TilemapLoader = new TilemapLoader(this, pointers.TilemapLoaderPointers);
            AreaLoader = new AreaLoader(this, pointers.AreaLoaderPointers);
            AreaObjectRenderer = new AreaObjectRenderer(this, pointers.AreaObjectRendererPointers);
            AreaSpriteRenderer = new AreaSpriteRenderer(this);

            Palette = new Color32BppArgb[0x140];
            PixelData = new byte[GfxData.TotalPixelDataSize];
            Map16Tiles = new Obj16Tile[0x100];

            ObjectData = new List<AreaObjectCommand>();
            SpriteData = new List<AreaSpriteCommand>();

            GfxData.ReadStaticData(PixelData);
            Map16Data.ReadStaticTiles(Map16Tiles);

            _areaNumber = AreaLoader.GetAreaNumber(world: 0, level: 0);
            ReloadArea();
        }

        public event EventHandler AreaNumberChanged;

        public event EventHandler AreaHeaderChanged;

        public event EventHandler PlayerChanged;

        public event EventHandler PlayerStateChanged;

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

        public Player Player
        {
            get
            {
                return _player;
            }

            set
            {
                if (Player == value)
                {
                    return;
                }

                _player = value;
                PlayerChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public PlayerState PlayerState
        {
            get
            {
                return _playerState;
            }

            set
            {
                if (PlayerState == value)
                {
                    return;
                }

                _playerState = value;
                PlayerStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public int ObjectAreaIndex
        {
            get
            {
                return AreaLoader.GetObjectAreaIndex(AreaNumber);
            }
        }

        public int SpriteAreaIndex
        {
            get
            {
                return AreaLoader.GetSpriteAreaIndex(AreaNumber);
            }
        }

        public AreaHeader AreaHeader
        {
            get
            {
                return _areaHeader;
            }

            set
            {
                if (_areaHeader == value)
                {
                    return;
                }

                _areaHeader = value;
                OnAreaHeaderChanged(EventArgs.Empty);
            }
        }

        public List<AreaObjectCommand> ObjectData
        {
            get;
        }

        public List<AreaSpriteCommand> SpriteData
        {
            get;
        }

        public Color32BppArgb[] Palette
        {
            get;
        }

        public byte[] PixelData
        {
            get;
        }

        public Obj16Tile[] Map16Tiles
        {
            get;
        }

        public RomIO Rom
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

        public Map16Data Map16Data
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

        public AreaObjectRenderer AreaObjectRenderer
        {
            get;
        }

        public AreaSpriteRenderer AreaSpriteRenderer
        {
            get;
        }

        internal Pointers Pointers
        {
            get;
        }

        public void UpdateAnimatedPixelData(int frame)
        {
            GfxData.ReadAnimationFrame(frame, PixelData);
        }

        public int GetObjectIndex(Point location)
        {
            var i = 0;
            foreach (var rect in GetObjectRectangles())
            {
                if (rect.Contains(location))
                {
                    return i;
                }

                i++;
            }

            return -1;
        }

        public IEnumerable<Rectangle> GetObjectRectangles()
        {
            var en = AreaObjectCommand.EnumeratePositions(ObjectData);
            foreach ((var x, var y) in en)
            {
                yield return new Rectangle(x, y, 16, 16);
            }
        }

        public int GetSpriteIndex(Point location)
        {
            var i = 0;
            foreach (var rect in GetSpriteRectangles())
            {
                if (rect.Contains(location))
                {
                    return i;
                }

                i++;
            }

            return -1;
        }

        public IEnumerable<Rectangle> GetSpriteRectangles()
        {
            var en = AreaSpriteCommand.EnumeratePositions(SpriteData);
            foreach ((var x, var y) in en)
            {
                yield return new Rectangle(x, y, 16, 16);
            }
        }

        public ObjTile[] ReadBG1Tiles()
        {
            var tiles = AreaObjectRenderer.TileMap;
            const int width = 0x200;
            var height = tiles.Length / width;
            var result = new ObjTile[tiles.Length * 4];
            for (var y = 0; y < height; y++)
            {
                var srcRow = y * width;
                var destRow = srcRow << 2;
                for (var srcX = 0; srcX < width; srcX++)
                {
                    var destX = srcX << 1;
                    var index = srcRow + srcX;
                    var tileIndex = (byte)tiles[index];
                    var tile = Map16Tiles[tileIndex];
                    if (tileIndex == 0x56 || tileIndex == 0x57)
                    {
                        if (srcX > 0 && ((byte)tiles[index - 1]) == 0)
                        {
                            tile.TopLeft += 4;
                            tile.BottomLeft += 4;
                        }

                        if (srcX + 1 < width && ((byte)tiles[index + 1]) == 0)
                        {
                            tile.TopRight += 4;
                            tile.BottomRight += 4;
                        }
                    }

                    result[destRow + destX] = tile.TopLeft;
                    result[destRow + destX + 1] = tile.TopRight;
                    result[destRow + (width << 1) + destX] = tile.BottomLeft;
                    result[destRow + (width << 1) + destX + 1] = tile.BottomRight;
                }
            }

            return result;
        }

        public IEnumerable<Sprite> EnumerateSprites(int frame)
        {
            var result = AreaSpriteRenderer.GetSprites(
                SpriteData.ToArray(),
                ObjectData.ToArray(),
                frame,
                AreaType,
                true);

            return result.Concat(
                AreaSpriteRenderer.GetPlayerSprite(
                    0x28,
                    StartY(AreaHeader.StartYPosition),
                    Player,
                    PlayerState,
                    PlayerFrame(AreaHeader.StartYPosition)));
        }

        public void ReloadArea()
        {
            UpdateArea();
            ReloadPalette();
            TilemapLoader.LoadTilemap(ObjectAreaIndex);
            ReloadGfx();
        }

        public void ReloadPalette()
        {
            PaletteData.ReadPalette(
                ObjectAreaIndex,
                isLuigiBonusArea: false,
                Player,
                PlayerState,
                Palette);
        }

        public void ReloadGfx()
        {
            GfxData.ReadAreaTileSet(
               ObjectAreaIndex,
               TilemapLoader.TileSetIndex,
               Player,
               PixelData);
        }

        public void RenderAreaTilemap()
        {
            AreaObjectRenderer.RenderTileMap(
                AreaType,
                AreaHeader,
                ObjectData,
                AreaNumber == 2);
        }

        public void WriteArea()
        {
            AreaLoader.Headers[ObjectAreaIndex] = AreaHeader;
            AreaLoader.AreaObjectData[ObjectAreaIndex] = ObjectData.ToArray();
            AreaLoader.AreaSpriteData[SpriteAreaIndex] = SpriteData.ToArray();
        }

        public void Save()
        {
            PaletteData.WriteToGameData(this, Pointers.PaletteDataPointers);
            GfxData.WriteToGameData(this, Pointers.GfxDataPointers);
            Map16Data.WriteToGameData(this, Pointers.Map16DataPointers);
            AreaLoader.WriteToGameData(this, Pointers.AreaLoaderPointers);
            AreaObjectRenderer.WriteToGameData(this, Pointers.AreaObjectRendererPointers);
        }

        protected virtual void OnAreaNumberChanged(EventArgs e)
        {
            ReloadArea();
            AreaNumberChanged?.Invoke(this, e);
        }

        protected virtual void OnAreaHeaderChanged(EventArgs e)
        {
            RenderAreaTilemap();
            AreaHeaderChanged?.Invoke(this, e);
        }

        private static int PlayerFrame(StartYPosition position)
        {
            return position switch
            {
                StartYPosition.Y00 => 4,
                StartYPosition.Y20 => 4,
                StartYPosition.YB0 => 2,
                StartYPosition.Y50 => 2,
                StartYPosition.Alt1Y00 => 4,
                StartYPosition.Alt2Y00 => 4,
                StartYPosition.PipeIntroYB0 => 0,
                StartYPosition.AltPipeIntroYB0 => 0,
                _ => 6,
            };
        }

        private static int StartY(StartYPosition position)
        {
            return position switch
            {
                StartYPosition.Y00 => 0x00,
                StartYPosition.Y20 => 0x20,
                StartYPosition.YB0 => 0xB0,
                StartYPosition.Y50 => 0x50,
                StartYPosition.Alt1Y00 => 0x00,
                StartYPosition.Alt2Y00 => 0x00,
                StartYPosition.PipeIntroYB0 => 0xB0,
                StartYPosition.AltPipeIntroYB0 => 0xB0,
                _ => 0x00,
            };
        }

        private void UpdateArea()
        {
            AreaHeader = AreaLoader.Headers[ObjectAreaIndex];
            ObjectData.Clear();
            ObjectData.AddRange(AreaLoader.AreaObjectData[ObjectAreaIndex]);
            SpriteData.Clear();
            SpriteData.AddRange(AreaLoader.AreaSpriteData[SpriteAreaIndex]);
            RenderAreaTilemap();
        }
    }
}
