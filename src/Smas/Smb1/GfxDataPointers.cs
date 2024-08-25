// <copyright file="GfxDataPointers.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1;

public class GfxDataPointers
{
    public static readonly GfxDataPointers Jp10 = new(
        baseAddress: 0x05E6BA);

    public static readonly GfxDataPointers Jp11 = new(
        baseAddress: 0x05E70B);

    public static readonly GfxDataPointers Usa = new(
        baseAddress: 0x05E6C3);

    public static readonly GfxDataPointers UsaPlusW = new(
        baseAddress: 0x05E714);

    public static readonly GfxDataPointers Eu = new(
        baseAddress: 0x05E714);

    public static readonly GfxDataPointers EuPlusW = new(
        baseAddress: 0x05E714);

    public static readonly GfxDataPointers UsaSmb1 = new(
        gfxBaseAddress: 0x038000,
        baseAddress: 0x02E6C6);

    public GfxDataPointers(
        int bonusAreaTileSetTablePointer,
        int tileSetAddressBankByteTablePointer,
        int tileSetAddressWordTablePointer,
        int tileSetDestIndexTablePointer,
        int tileSetSizeTablePointer,
        int gfxBaseAddress = 0x68000)
        : this(
            areaGfxAddress: gfxBaseAddress,
            spriteGfxAddress: gfxBaseAddress + 0x10000,
            animatedGfxAddress: gfxBaseAddress + 0x4000,
            marioGfxAddress: gfxBaseAddress + 0x40000,
            luigiGfxAddress: gfxBaseAddress + 0x44000,
            menuGfxAddress: gfxBaseAddress + 0x67800,
            bonusAreaTileSetTablePointer,
            tileSetAddressBankByteTablePointer,
            tileSetAddressWordTablePointer,
            tileSetDestIndexTablePointer,
            tileSetSizeTablePointer)
    { }

    public GfxDataPointers(
        int areaGfxAddress,
        int spriteGfxAddress,
        int animatedGfxAddress,
        int marioGfxAddress,
        int luigiGfxAddress,
        int menuGfxAddress,
        int bonusAreaTileSetTablePointer,
        int tileSetAddressBankByteTablePointer,
        int tileSetAddressWordTablePointer,
        int tileSetDestIndexTablePointer,
        int tileSetSizeTablePointer)
    {
        AreaGfxAddress = areaGfxAddress;
        SpriteGfxAddress = spriteGfxAddress;
        AnimatedGfxAddress = animatedGfxAddress;
        MarioGfxAddress = marioGfxAddress;
        LuigiGfxAddress = luigiGfxAddress;
        MenuGfxAddress = menuGfxAddress;
        BonusAreaTileSetTablePointer = bonusAreaTileSetTablePointer;
        TileSetAddressBankByteTablePointer = tileSetAddressBankByteTablePointer;
        TileSetAddressWordTablePointer = tileSetAddressWordTablePointer;
        TileSetDestIndexTablePointer = tileSetDestIndexTablePointer;
        TileSetSizeTablePointer = tileSetSizeTablePointer;
    }

    private GfxDataPointers(
                int baseAddress,
        int gfxBaseAddress = 0x68000)
        : this(
              bonusAreaTileSetTablePointer: baseAddress,
              tileSetAddressBankByteTablePointer: baseAddress + 0x16A,
              tileSetAddressWordTablePointer: baseAddress + 0x172,
              tileSetDestIndexTablePointer: baseAddress + 0x178,
              tileSetSizeTablePointer: baseAddress + 0x17E,
              gfxBaseAddress: gfxBaseAddress)
    { }

    public int AreaGfxAddress { get; }

    public int SpriteGfxAddress { get; }

    public int AnimatedGfxAddress { get; }

    public int MarioGfxAddress { get; }

    public int LuigiGfxAddress { get; }

    public int MenuGfxAddress { get; }

    public int BonusAreaTileSetTablePointer { get; }

    public int TileSetAddressBankByteTablePointer { get; }

    public int TileSetAddressWordTablePointer { get; }

    public int TileSetDestIndexTablePointer { get; }

    public int TileSetSizeTablePointer { get; }
}
