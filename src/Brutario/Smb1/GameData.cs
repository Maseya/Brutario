// <copyright file="RomData.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed under
//     GNU Affero General Public License. See LICENSE in project root for full
//     license information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Smb1
{
    using System;
    using System.Collections.Generic;

    public class GameData
    {
        private int _areaNumber;

        public GameData(RomIO rom, Pointers pointers)
        {
            Pointers = pointers;
            Rom = rom ?? throw new ArgumentOutOfRangeException(nameof(rom));

            // I should do more checks, but not really sure what are good
            // choices.
            if (false && Rom.HeaderlessSize < 0x200000)
            {
                throw new ArgumentException("ROM data size is insufficient.");
            }

            PaletteData = new PaletteData(this, pointers.PaletteDataPointers);
            GfxData = new GfxData(this, pointers.GfxDataPointers);
            Map16Data = new Map16Data(this, pointers.Map16DataPointers);
            TilemapLoader = new TilemapLoader(this, pointers.TilemapLoaderPointers);
            AreaLoader = new AreaLoader(this, pointers.AreaLoaderPointers);
            AreaObjectRenderer = new AreaObjectRenderer(this, pointers.AreaObjectRendererPointers);
            AreaSpriteRenderer = new AreaSpriteRenderer();

            CurrentPalette = new Color32BppArgb[0x100];
            PixelData = new byte[GfxData.TotalPixelDataSize];
            Map16Tiles = new Obj16Tile[0x100];

            CurrentObjectData = new List<AreaObjectCommand>();
            CurrentSpriteData = new List<AreaSpriteCommand>();

            GfxData.ReadStaticData(PixelData);
            Map16Data.ReadTiles(Map16Tiles);

            AreaNumber = AreaLoader.GetAreaNumber(0, 0);

            // We will have to manually call ReloadArea since assigning zero
            // will not trigger the event the first time.
            if (AreaNumber == 0)
            {
                ReloadArea();
            }
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

        public AreaObjectRenderer AreaObjectRenderer
        {
            get;
        }

        public AreaSpriteRenderer AreaSpriteRenderer
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
        }

        public List<AreaSpriteCommand> CurrentSpriteData
        {
            get;
        }

        public int AnimationFrame
        {
            get;
            private set;
        }

        public Color32BppArgb[] CurrentPalette
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

        public Map16Data Map16Data
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

        internal Pointers Pointers
        {
            get;
        }

        public void Animate(int frame)
        {
            AnimationFrame = frame;
            GfxData.ReadAnimationFrame(frame, PixelData);
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
                    var tile = Map16Tiles[tileIndex];
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

        public void ReloadArea()
        {
            UpdateArea();
            PaletteData.ReadPalette(ObjectAreaIndex, CurrentPalette);
            TilemapLoader.LoadTilemap(ObjectAreaIndex);
            GfxData.ReadAreaTileSet(
               ObjectAreaIndex,
               TilemapLoader.TileSetIndex,
               Player.Mario,
               PixelData);
        }

        public void RenderAreaTilemap()
        {
            AreaObjectRenderer.RenderTileMap(
                AreaType,
                CurrentAreaHeader,
                CurrentObjectData,
                AreaNumber == 2);
        }

        public void WriteArea()
        {
            AreaLoader.Headers[ObjectAreaIndex] = CurrentAreaHeader;
            AreaLoader.AreaObjectData[ObjectAreaIndex] = CurrentObjectData.ToArray();
            AreaLoader.AreaSpriteData[SpriteAreaIndex] = CurrentSpriteData.ToArray();
        }

        public void ApplyChanges()
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

        private void UpdateArea()
        {
            CurrentAreaHeader = AreaLoader.Headers[ObjectAreaIndex];
            CurrentObjectData.Clear();
            CurrentObjectData.AddRange(
                AreaLoader.AreaObjectData[ObjectAreaIndex]);
            CurrentSpriteData.Clear();
            CurrentSpriteData.AddRange(
                AreaLoader.AreaSpriteData[SpriteAreaIndex]);
            RenderAreaTilemap();
        }
    }
}
