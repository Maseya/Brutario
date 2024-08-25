// <copyright file="GfxData.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1;

using System;
using System.Collections.Generic;

using Snes;

using static Snes.GfxConverter;

public class GfxData
{
    public const int SpritePixelDataStartIndex =
        AreaBgPixelStartIndex + AreaBgPixelDataSize;

    public const int MarioPixelDataStartIndex =
        AnimatedPixelDataStartIndex + SpritePixelDataSize;

    public const int TotalPixelDataSize =
        MenuPixelDataStartIndex + MenuPixelDataSize;

    private const int AreaGfxSize = 0x4000;
    private const int AnimatedPixelDataDestIndex = 0x3000;
    private const int AreaPixelDataSize = AreaGfxSize << 1;

    private const int AreaBgGfxSize = 0x2000;
    private const int AreaBgPixelDataSize = AreaBgGfxSize << 1;
    private const int TilesetCount = 0x19;

    private const int SpriteGfxSize = 0x4000;
    private const int SpritePixelDataSize = SpriteGfxSize << 1;

    private const int AnimatedGfxSize = 0x4000;
    private const int AnimatedPixelDataSize = AnimatedGfxSize << 1;
    private const int AnimatedPixelDataFrameSize = 0xC00;
    private const int AnimatedPixelDataFrameOffset = 0x1000;

    private const int PlayerGfxSize = 0x2000;
    private const int PlayerPixelDataSize = PlayerGfxSize << 1;

    private const int MenuGfxSize = 0x800;
    private const int MenuPixelDataSize = MenuGfxSize << 2;

    private const int AreaPixelStartIndex = 0;

    private const int AreaBgPixelStartIndex = AreaPixelStartIndex + AreaPixelDataSize;

    private const int AnimatedPixelDataStartIndex =
        SpritePixelDataStartIndex + SpritePixelDataSize;

    private const int LuigiPixelDataStartIndex =
        MarioPixelDataStartIndex + PlayerPixelDataSize;

    private const int MenuPixelDataStartIndex =
        LuigiPixelDataStartIndex + PlayerPixelDataSize;

    public GfxData(Rom rom, GfxDataPointers pointers)
    {
        AreaPixelData = GfxToPixelMap(
            rom.ReadBytes(pointers.AreaGfxAddress, AreaGfxSize));
        SpritePixelData = GfxToPixelMap(
            rom.ReadBytes(pointers.SpriteGfxAddress, SpriteGfxSize));
        AnimatedPixelData = GfxToPixelMap(
            rom.ReadBytes(pointers.AnimatedGfxAddress, AnimatedGfxSize));
        MarioPixelData = GfxToPixelMap(
            rom.ReadBytes(pointers.MarioGfxAddress, PlayerGfxSize));
        LuigiPixelData = GfxToPixelMap(
            rom.ReadBytes(pointers.LuigiGfxAddress, PlayerGfxSize));
        MenuPixelData = Gfx2BppToPixelMap(
            rom.ReadBytes(pointers.MenuGfxAddress, MenuGfxSize));

        BonusAreaTileSetTable = rom.ReadBytesIndirect(
           pointers.BonusAreaTileSetTablePointer,
           count: 2);

        if (BonusAreaTileSetTable.Any(index => index >= TilesetCount))
        {
            throw new ArgumentException("GFX Data is invalid");
        }

        TileSetDestIndexTable = rom.ReadInt16ArrayIndirectAs<int>(
           pointers.TileSetDestIndexTablePointer,
           TilesetCount,
           x => (ushort)x);

        if (TileSetDestIndexTable.Skip(1).Any(index => index < 0x1000))
        {
            throw new ArgumentException("GFX data is invalid!");
        }

        var banks = rom.ReadInt16ArrayIndirectAs(
            pointers.TileSetAddressBankByteTablePointer,
            TilesetCount,
            x => (ushort)x);
        var words = rom.ReadInt16ArrayIndirectAs(
            pointers.TileSetAddressWordTablePointer,
            TilesetCount,
            x => (ushort)x);
        var sizes = rom.ReadInt16ArrayIndirectAs(
            pointers.TileSetSizeTablePointer,
            TilesetCount,
            x => (ushort)x);

        TileSetTable = new byte[TilesetCount][];
        for (var i = 1; i < TileSetTable.Length; i++)
        {
            var address = (banks[i] << 0x10) | words[i];
            TileSetTable[i] = GfxToPixelMap(rom.ReadBytes(address, sizes[i]));
        }

        var maxIndexes = TileSetTable.Skip(1).Zip(
            TileSetDestIndexTable.Skip(1),
            (tiles, index) => ((index - 0x1000) << 2) + tiles.Length);

        // Next we must ensure that every tileset will fit into the pixel data array.
        if (maxIndexes.Any(index => index >= TotalPixelDataSize))
        {
            throw new ArgumentException("GFX data is invalid!");
        }

        TileSetActions = new Func<int, IEnumerable<int>>?[0x20]
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

    private Func<int, IEnumerable<int>>?[] TileSetActions
    {
        get;
    }

    public void ReadStaticData(Span<byte> dest)
    {
        if (dest.Length < TotalPixelDataSize)
        {
            throw new ArgumentException(
                "Destination array is of insufficient size.",
                nameof(dest));
        }

        AreaPixelData.CopyTo(dest);
        SpritePixelData.CopyTo(dest[SpritePixelDataStartIndex..]);
        AnimatedPixelData.CopyTo(dest[AnimatedPixelDataStartIndex..]);
        MarioPixelData.CopyTo(dest[MarioPixelDataStartIndex..]);
        LuigiPixelData.CopyTo(dest[LuigiPixelDataStartIndex..]);
        MenuPixelData.CopyTo(dest[MenuPixelDataStartIndex..]);
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

        // HACK: These levels don't load a complete tileset. It uses tile sets of the
        // area before them. Eventually, I'll need to devise a system to better load
        // tile sets.
        if (areaIndex == 2)
        {
            ReadTileSet(4, pixelData);
        }
        else if (areaIndex == 0x0F)
        {
            ReadTileSet(0x17, pixelData);
        }

        var action = TileSetActions[areaTileSetIndex & 0x1F];
        if (action is not null)
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

    public void WriteToGameData(Rom rom, GfxDataPointers pointers)
    {
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
        yield return areaIndex is 0x16 or 0x14 or 0x0D
            ? 0x12
            : 0x17;
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
