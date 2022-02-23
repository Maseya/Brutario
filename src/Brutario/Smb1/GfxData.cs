// <copyright file="GfxData.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed under
//     GNU Affero General Public License. See LICENSE in project root for full
//     license information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Smb1
{
    using System;
    using System.Collections.Generic;
    using static GfxConverter;

    public class GfxData
    {
        public const int AreaGfxSize = 0x4000;
        public const int AnimatedPixelDataDestIndex = 0x3000;
        public const int AreaPixelDataSize = AreaGfxSize << 1;
        public const int TilesetCount = 0x18;

        public const int SpriteGfxSize = 0x4000;
        public const int SpritePixelDataSize = SpriteGfxSize << 1;

        public const int AnimatedGfxSize = 0x4000;
        public const int AnimatedPixelDataSize = AnimatedGfxSize << 1;
        public const int AnimatedPixelDataFrameSize = 0xC00;
        public const int AnimatedPixelDataFrameOffset = 0x1000;

        public const int PlayerGfxSize = 0x2000;
        public const int PlayerPixelDataSize = PlayerGfxSize << 1;

        public const int MenuGfxSize = 0x800;
        public const int MenuPixelDataSize = MenuGfxSize << 2;

        public const int AreaPixelStartIndex = 0;

        public const int SpritePixelDataStartIndex = 0xC000;

        public const int AnimatedPixelDataStartIndex = SpritePixelDataStartIndex + SpritePixelDataSize;
        public const int MarioPixelDataStartIndex = AnimatedPixelDataStartIndex + SpritePixelDataSize;
        public const int LuigiPixelDataStartIndex = MarioPixelDataStartIndex + PlayerPixelDataSize;
        public const int MenuPixelDataStartIndex = LuigiPixelDataStartIndex + PlayerPixelDataSize;
        public const int TotalPixelDataSize = MenuPixelDataStartIndex + MenuPixelDataSize;

        public GfxData()
        {
            AreaPixelData = new byte[PixelMapSize(AreaGfxSize)];
            SpritePixelData = new byte[PixelMapSize(SpriteGfxSize)];
            AnimatedPixelData = new byte[PixelMapSize(AnimatedGfxSize)];
            MarioPixelData = new byte[PixelMapSize(PlayerGfxSize)];
            LuigiPixelData = new byte[PixelMapSize(PlayerGfxSize)];
            MenuPixelData = new byte[PixelMapSize2Bpp(MenuGfxSize)];

            BonusAreaTileSetTable = new byte[2];
            TileSetTable = new byte[TilesetCount][];
            TileSetDestIndexTable = new int[TilesetCount];

            TileSetActions = new Func<int, IEnumerable<int>>[0x20]
            {
                null,
                GetUndergroundTileSets,
                GetGrassTileSets,
                GetUndergroundTileSets,
                GetBowserCastleTileSets,
                GetGrassTileSets,
                null,
                GetStarryNightTileSets,
                null,
                GetStarryNightTileSets,
                GetGameOverTileSets,
                GetGrassTileSets,
                GetGrassTileSets,
                null,
                GetGrassTileSets,
                null,
                GetGrassTileSets,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                GetUndergroundTileSets,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
            };
        }

        public GfxData(GameData gameData, GfxDataPointers pointers)
            : this()
        {
            ReadGameData(gameData, pointers);
        }

        private byte[] AreaPixelData
        {
            get;
        }

        private byte[] SpritePixelData
        {
            get;
        }

        private byte[] AnimatedPixelData
        {
            get;
        }

        private byte[] MarioPixelData
        {
            get;
        }

        private byte[] LuigiPixelData
        {
            get;
        }

        private byte[] MenuPixelData
        {
            get;
        }

        private byte[][] TileSetTable
        {
            get;
        }

        private int[] TileSetDestIndexTable
        {
            get;
        }

        private byte[] BonusAreaTileSetTable
        {
            get;
        }

        private Func<int, IEnumerable<int>>[] TileSetActions
        {
            get;
        }

        public void ReadGameData(GameData gameData, GfxDataPointers pointers)
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
            GfxToPixelMap(
                rom.ReadBytes(pointers.AreaGfxAddress, AreaGfxSize),
                AreaPixelData);
            GfxToPixelMap(
                rom.ReadBytes(pointers.SpriteGfxAddress, SpriteGfxSize),
                SpritePixelData);
            GfxToPixelMap(
                rom.ReadBytes(pointers.AnimatedGfxAddress, AnimatedGfxSize),
                AnimatedPixelData);
            GfxToPixelMap(
                rom.ReadBytes(pointers.MarioGfxAddress, PlayerGfxSize),
                MarioPixelData);
            GfxToPixelMap(
                rom.ReadBytes(pointers.LuigiGfxAddress, PlayerGfxSize),
                LuigiPixelData);
            Gfx2BppToPixelMap(
                rom.ReadBytes(pointers.MenuGfxAddress, MenuGfxSize),
                MenuPixelData);

            rom.ReadBytesIndirect(
               pointers.BonusAreaTileSetTablePointer,
               BonusAreaTileSetTable);

            for (var i = 1; i < TileSetTable.Length; i++)
            {
                var bank = (ushort)rom.ReadInt16IndirectIndexed(
                    pointers.TileSetAddressBankByteTablePointer, i);
                var word = (ushort)rom.ReadInt16IndirectIndexed(
                    pointers.TileSetAddressWordTablePointer, i);
                var size = (ushort)rom.ReadInt16IndirectIndexed(
                    pointers.TileSetSizeTablePointer, i);
                var address = (bank << 0x10) | word;
                TileSetTable[i] = GfxToPixelMap(rom.ReadBytes(address, size));
            }

            rom.ReadInt16ArrayIndirectAs<int>(
               pointers.TileSetDestIndexTablePointer,
               TileSetDestIndexTable,
               x => (ushort)x);
        }

        public void ReadStaticData(Span<byte> dest)
        {
            if (dest.Length < TotalPixelDataSize)
            {
                throw new ArgumentException();
            }

            AreaPixelData.CopyTo(dest);
            SpritePixelData.CopyTo(dest.Slice(SpritePixelDataStartIndex));
            AnimatedPixelData.CopyTo(dest.Slice(AnimatedPixelDataStartIndex));
            MarioPixelData.CopyTo(dest.Slice(MarioPixelDataStartIndex));
            LuigiPixelData.CopyTo(dest.Slice(LuigiPixelDataStartIndex));
            MenuPixelData.CopyTo(dest.Slice(MenuPixelDataStartIndex));
        }

        public void ReadAnimationFrame(int frame, Span<byte> pixelData)
        {
            var actualFrame = (frame >> 3) & 7;
            var src = new Span<byte>(
                AnimatedPixelData,
                AnimatedPixelDataFrameOffset * actualFrame,
                AnimatedPixelDataFrameSize);
            var dest = pixelData.Slice(
                AnimatedPixelDataDestIndex,
                AnimatedPixelDataFrameSize);
            src.CopyTo(dest);
        }

        public void ReadAreaTileSet(
            int areaIndex,
            int areaTileSetIndex,
            Player player,
            Span<byte> pixelData)
        {
            if (areaTileSetIndex == 1)
            {
                areaTileSetIndex = BonusAreaTileSetTable[(int)player];
            }
            ReadTileSet(areaTileSetIndex, pixelData);

            // HACK: These levels don't load a complete tileset. It uses
            // tile sets of the area before them. Eventually, I'll need to
            // devise a system to better load tile sets.
            if (areaIndex == 2)
            {
                ReadTileSet(4, pixelData);
            }
            else if (areaIndex == 0x0F)
            {
                ReadTileSet(0x17, pixelData);
            }

            var action = TileSetActions[areaTileSetIndex & 0x1F];
            if (!(action is null))
            {
                foreach (var tileset in action(areaIndex))
                {
                    ReadTileSet(tileset, pixelData);
                }
            }
        }

        public void ReadTileSet(int tileSetIndex, Span<byte> pixelData)
        {
            var tileSet = TileSetTable[tileSetIndex];
            var destIndex = (TileSetDestIndexTable[tileSetIndex] - 0x1000) << 2;
            tileSet.CopyTo(pixelData.Slice(destIndex, tileSet.Length));
        }

        public void WriteToGameData(GameData gameData, GfxDataPointers pointers)
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
            rom.WriteArrayAsInt16Indirect<int>(
                pointers.TileSetDestIndexTablePointer,
                TileSetDestIndexTable,
                x => (short)x);

            for (var i = 1; i < TileSetTable.Length; i++)
            {
                var bank = (ushort)rom.ReadInt16IndirectIndexed(
                    pointers.TileSetAddressBankByteTablePointer,
                    i);
                var word = (ushort)rom.ReadInt16IndirectIndexed(
                    pointers.TileSetAddressWordTablePointer,
                    i);
                var src = (bank << 0x10) | word;
                rom.WriteBytes(src, PixelMapToGfx(TileSetTable[i]));
            }
            rom.WriteBytesIndirect(
                pointers.BonusAreaTileSetTablePointer,
                BonusAreaTileSetTable);

            rom.WriteBytes(
                pointers.MenuGfxAddress,
                PixelMapToGfx2Bpp(MenuPixelData));
            rom.WriteBytes(
                pointers.LuigiGfxAddress,
                PixelMapToGfx(LuigiPixelData));
            rom.WriteBytes(
                pointers.MarioGfxAddress,
                PixelMapToGfx(MarioPixelData));
            rom.WriteBytes(
                pointers.AnimatedGfxAddress,
                PixelMapToGfx(AnimatedPixelData));
            rom.WriteBytes(
                pointers.SpriteGfxAddress,
                PixelMapToGfx(SpritePixelData));
            rom.WriteBytes(
                pointers.AreaGfxAddress,
                PixelMapToGfx(AreaPixelData));
        }

        private IEnumerable<int> GetGrassTileSets(int areaIndex)
        {
            if (areaIndex == 0x16 || areaIndex == 0x14 || areaIndex == 0x0D)
            {
                yield return 0x12;
            }
            else
            {
                yield return 0x17;
            }
        }

        private IEnumerable<int> GetUndergroundTileSets(int areaIndex)
        {
            yield return 0x11;
        }

        private IEnumerable<int> GetStarryNightTileSets(int areaIndex)
        {
            yield return 0x16;
            yield return 0x12;
        }

        private IEnumerable<int> GetBowserCastleTileSets(int areaIndex)
        {
            yield return 0x13;
            yield return 0x14;
        }

        private IEnumerable<int> GetGameOverTileSets(int areaIndex)
        {
            yield return 0x15;
        }
    }
}
