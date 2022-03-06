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

            AreaLoader = new AreaLoader(this, pointers.AreaLoaderPointers);
            PaletteData = new PaletteData(this, pointers.PaletteDataPointers);
            GfxData = new GfxData(this, pointers.GfxDataPointers);
            Map16Data = new Map16Data(this, pointers.Map16DataPointers);
            TilemapLoader = new TilemapLoader(this, pointers.TilemapLoaderPointers);
            AreaObjectRenderer = new AreaObjectRenderer(
                this,
                pointers.AreaObjectRendererPointers);
            AreaSpriteRenderer = new AreaSpriteRenderer(this);

            Palette = new Color32BppArgb[0x140];
            PixelData = new byte[GfxData.TotalPixelDataSize];
            Map16Tiles = new Obj16Tile[0x100];
            BG1 = new ObjTile[AreaObjectRenderer.TileMap.Length * 4];

            ObjectData = new ObjectListEditor();
            SpriteData = new SpriteListEditor();

            GfxData.ReadStaticData(PixelData);
            Map16Data.ReadStaticTiles(Map16Tiles);

            Player = Player.Mario;
            PlayerState = PlayerState.Big;

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

        public ObjectListEditor ObjectData
        {
            get;
        }

        public SpriteListEditor SpriteData
        {
            get;
        }

        public int NumberOfAreas
        {
            get
            {
                return AreaLoader?.NumberOfAreas ?? 0;
            }
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

        public ObjTile[] BG1
        {
            get;
        }

        internal RomIO Rom
        {
            get;
        }

        internal Pointers Pointers
        {
            get;
        }

        private PaletteData PaletteData
        {
            get;
        }

        private GfxData GfxData
        {
            get;
        }

        private Map16Data Map16Data
        {
            get;
        }

        private AreaLoader AreaLoader
        {
            get;
        }

        private TilemapLoader TilemapLoader
        {
            get;
        }

        private AreaObjectRenderer AreaObjectRenderer
        {
            get;
        }

        private AreaSpriteRenderer AreaSpriteRenderer
        {
            get;
        }

        public void UpdateAnimatedPixelData(int frame)
        {
            GfxData.ReadAnimationFrame(frame, PixelData);
        }

        public bool IsValidAreaNumber(int areaNumber)
        {
            return (uint)AreaLoader.GetObjectAreaIndex(areaNumber) < AreaLoader.NumberOfAreas
                && (uint)AreaLoader.GetSpriteAreaIndex(areaNumber) < AreaLoader.NumberOfAreas;
        }

        public IEnumerable<Rectangle> GetObjectRectangles()
        {
            return ObjectData.EnumeratePositions().Select(
                p => new Rectangle(p.x << 4, (p.y + 2) << 4, 0x10, 0x10));
        }

        public IEnumerable<Rectangle> GetSpriteRectangles()
        {
            foreach ((var x, var y) in SpriteData.EnumeratePositions())
            {
                yield return new Rectangle(x << 4, y << 4, 0x10, 0x10);
            }
        }

        public int MoveObject(int index, Point dest)
        {
            if (dest.Y < 0)
            {
                if (ObjectData[index].Y == 0)
                {
                    return -1;
                }
                dest.Y -= 1;
            }
            else if (dest.Y > 0)
            {
                if (ObjectData[index].Y >= 0x0E)
                {
                    return -1;
                }

                dest.Y += 1;
            }
            else if (dest.X < 0)
            {
                dest.X = -1;
            }
            else if (dest.X > 0)
            {
                dest.X += 1;
            }

            // We can't use binary search because we're not guaranteed a sorted X
            // array thanks to backwards screen jumps. It's a just a goofy thing I'll
            // have to account for in the future.
            var newIndex = 0;
            if (newIndex < 0)
            {
                newIndex = (-newIndex) - 1;
            }

            // We're modifying the object in place. This is the easiest case.
            if (newIndex == index)
            {
                /* Cases to analyze:
                 *
                 * Easiest case: We stay on the same page. We do not need to update the
                 * screen flag or insert any screen jumps
                 *
                 * Medium case: The object goes back or forward one page.
                 *  Part 1: If it goes back one page, then this object definitely had
                 *      the screen flag enabled, otherwise it would have crossed
                 *      another object to go back a page. The only exception is if the
                 *      object is on screen 0, in which case, we stop the object at X=0.
                 *      Note: It's actually possible it didn't have screen flag enabled.
                 *      It's also possible that the previous object was a screen skip
                 *      command. If this is the case, then the command needs to be
                 *      decremented by one.
                 *
                 *  Part 2. If the object goes to the next page, then we enable the
                 *      screen skip flag (unless it is as the last page, then we stop it
                 *      at X=15). We need to check if another object comes after it. If
                 *      so, then it definitely has the screen skip flag enabled, so we
                 *      disable its screen flag.
                 *
                 * Hardest case: The object goes forward or back multiple pages. In this
                 * case, a screen skip object must be inserted (or possibly deleted).
                 *  Part 1: The object goes back multiple pages. In this case, there
                 *  was definitely a screen skip object before this object. Otherwise,
                 *  how it could it cross multiple pages without crossing another
                 *  object? Next, we need to consider if the screen skip object should
                 *  still exist, and if one needs to be inserted after the object too.
                 *  We're dealing with insertions, deletions, and swapping. First, we
                 *  need to calculate the page difference between the new location of
                 *  the current object and the location of the screen skip before it.
                 *      Part A: If the difference is zero, then we swap the new object
                 *      index with the screen skip object. The screen skip object still
                 *      needs to exist for the following objects (unless this is the
                 *      last object, then we can delete it). If the next object has the
                 *      screen skip flag enabled, then clear the flag and increment the
                 *      screen jump command by 1 (this is an optional step). However, if
                 *      the next object is a screen skip, then these two screen skips
                 *      must be merged, and we ending up deleting an item from the
                 *      object list.
                 *
                 *      Part B: If the difference is one, then it may be possible to
                 *      avoid inserting still. If there is another object on the same
                 *      page as the screen jump command, then we enable the screen skip
                 *      command for the new object, and move the screen jump command to
                 *      come after the object. If there is not an object on the same
                 *      page as the screen skip, then either the screen skip is the
                 *      first object, or there are two consecutive screen skips for no
                 *      reason (the Alcaro edge case). I'll operate on the assumption
                 *      that I forbid multiple screen skips. This will also mean I need
                 *      to forbid backward screen skips (maybe? that's a tough call and
                 *      I really need to decide if there is any benefit for it? For now,
                 *      I will say no, but it may have some Z-order advantage I cannot
                 *      see right now). So assuming the screen jump command was the
                 *      first command, then our new object will become the first command
                 *      and we ask ourselves the same question: do we need to keep the
                 *      old screen jump command, and what must we set it to? If the new
                 *      object was the last object, then we delete the screen jump
                 *      command. Otherwise we check if the next object is a screen jump.
                 *      If it was, then we merge them. Otherwise, because the new
                 *      object went back multiple pages, the next object will
                 *      definitely be multiple pages away. This is a repeat of step A
                 *      basically.
                 *
                 *      Note: Backwards screen jumps DO NOT WORK. So just forbid them
                 *      outright.
                 *
                 *  Part 2: The object goes multiple pages forward. This is only
                 *  possible if this was the last object in the list. Otherwise it must
                 *  have crossed at least a page skip object. So in this case, we add
                 *  a new screen jump command (or update the previous one) and we're
                 *  done. This is a much easier case.
                 */
            }

            // We're moving the object backwards.
            else if (newIndex < index)
            {
                /* If we're moving backwards through objects, then we have to consider
                 * possibly identical cases.
                 *
                 * We look at the object we are placing the new object in front of.
                 * Case 1: They are on the same page. Just account for screen skip flags
                 * and rotate the contents of the array by one.
                 *
                 * Case 2: They are one page apart.
                 */
            }

            // We're moving the object forwards
            else
            {
            }

            return newIndex;
        }

        public IEnumerable<Sprite> EnumerateSprites(int frame)
        {
            var result = AreaSpriteRenderer.GetSprites(
                SpriteData.ToArray(),
                ObjectData.ToArray(),
                frame,
                AreaType,
                true);

            result = result.Concat(AreaSpriteRenderer.GetSprites(
                AreaObjectRenderer.TileMap));

            return result.Concat(
                AreaSpriteRenderer.GetPlayerSprite(
                    x: 0x28,
                    AreaHeader.StartYPixel,
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
            ReadBG1Tiles(BG1);
        }

        public byte[] ExportTileMap()
        {
            var data = new byte[0x20 * 0x10 * 0x0D];
            for (var i = 0; i < data.Length; i++)
            {
                data[i] = (byte)AreaObjectRenderer.TileMap[i + (2 * 0x20 * 0x10)];
            }

            return data;
        }

        public void WriteArea()
        {
            AreaLoader.WriteSpriteData(SpriteAreaIndex, SpriteData);
            AreaLoader.WriteObjectData(ObjectAreaIndex, AreaHeader, ObjectData);
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

        private void ReadBG1Tiles(Span<ObjTile> dest)
        {
            const int width = 0x200;
            var tiles = AreaObjectRenderer.TileMap;
            var height = tiles.Length / width;
            if (dest.Length < tiles.Length * 4)
            {
                throw new ArgumentException();
            }

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

                    dest[destRow + destX] = tile.TopLeft;
                    dest[destRow + destX + 1] = tile.TopRight;
                    dest[destRow + (width << 1) + destX] = tile.BottomLeft;
                    dest[destRow + (width << 1) + destX + 1] = tile.BottomRight;
                }
            }
        }

        private void UpdateArea()
        {
            AreaHeader = AreaLoader.Headers[ObjectAreaIndex];
            ObjectData.Reset(AreaLoader.AreaObjectData[ObjectAreaIndex]);
            SpriteData.Reset(AreaLoader.AreaSpriteData[SpriteAreaIndex]);
            RenderAreaTilemap();
        }
    }
}
