// <copyright file="AreaLoaderPointers.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1.AreaData;

public class AreaLoaderPointers
{
    public static readonly AreaLoaderPointers Jp10 = new(
        baseAddress: 0x04C026);

    public static readonly AreaLoaderPointers Jp11 = new(
        baseAddress: 0x04C026);

    public static readonly AreaLoaderPointers Usa = new(
        baseAddress: 0x04C026);

    public static readonly AreaLoaderPointers UsaLL = new(
        numberOfWorldsAddress: 0x0EC37B,
        worldLevelOffsetPointer: 0x0EC38A,
        areaIndexTablePointer: 0x0EC392,
        spriteAreaTypeOffsetPointer: 0x0EC3EC,
        spriteLowBytePointer: 0x0EC3F4,
        spriteHighBytePointer: 0x0EC3F9,
        objectAreaTypeOffsetPointer: 0x0EC404,
        objectLowBytePointer: 0x0EC40E,
        objectHighBytePointer: 0x0EC413,
        areaDataStartPointer: 0x0EC624,
        areaDataEndPointer: 0x0EFFFF,
        numberOfAreas: 0x47,
        areaNumberTableSize: 0x3A);

    public static readonly AreaLoaderPointers UsaPlusW = new(
        baseAddress: 0x04C026);

    public static readonly AreaLoaderPointers Eu = new(
        baseAddress: 0x04C026);

    public static readonly AreaLoaderPointers EuPlusW = new(
        baseAddress: 0x04C026);

    public static readonly AreaLoaderPointers UsaSmb1 = new(
        baseAddress: 0x01C026);

    public AreaLoaderPointers(
        int numberOfWorldsAddress,
        int worldLevelOffsetPointer,
        int areaIndexTablePointer,
        int spriteAreaTypeOffsetPointer,
        int spriteLowBytePointer,
        int spriteHighBytePointer,
        int objectAreaTypeOffsetPointer,
        int objectLowBytePointer,
        int objectHighBytePointer,
        int areaDataStartPointer,
        int areaDataEndPointer,
        int numberOfAreas = 0x22,
        int areaNumberTableSize = 0x24)
    {
        NumberOfWorldsAddress = numberOfWorldsAddress;
        WorldAreaNumberOffsetPointer = worldLevelOffsetPointer;
        AreaNumberTablePointer = areaIndexTablePointer;
        SpriteAreaTypeOffsetPointer = spriteAreaTypeOffsetPointer;
        SpriteLowBytePointer = spriteLowBytePointer;
        SpriteHighBytePointer = spriteHighBytePointer;
        ObjectAreaTypeOffsetPointer = objectAreaTypeOffsetPointer;
        ObjectLowBytePointer = objectLowBytePointer;
        ObjectHighBytePointer = objectHighBytePointer;
        AreaDataStartPointer = areaDataStartPointer;
        AreaDataEndPointer = areaDataEndPointer;
        NumberOfAreas = numberOfAreas;
        AreaNumberTableSize = areaNumberTableSize;
    }

    private AreaLoaderPointers(int baseAddress)
        : this(
            numberOfWorldsAddress: baseAddress,
            worldLevelOffsetPointer: baseAddress + 0x0F,
            areaIndexTablePointer: baseAddress + 0x17,
            spriteAreaTypeOffsetPointer: baseAddress + 0x35,
            spriteLowBytePointer: baseAddress + 0x3D,
            spriteHighBytePointer: baseAddress + 0x42,
            objectAreaTypeOffsetPointer: baseAddress + 0x4D,
            objectLowBytePointer: baseAddress + 0x6D,
            objectHighBytePointer: baseAddress + 0x72,
            areaDataStartPointer: baseAddress + 0x01B2,
            areaDataEndPointer: baseAddress + 0x17DA)
    { }

    public int NumberOfWorldsAddress { get; }

    public int NumberOfAreas { get; }

    public int AreaNumberTableSize { get; }

    public int WorldAreaNumberOffsetPointer { get; }

    public int AreaNumberTablePointer { get; }

    public int SpriteAreaTypeOffsetPointer { get; }

    public int SpriteLowBytePointer { get; }

    public int SpriteHighBytePointer { get; }

    public int ObjectAreaTypeOffsetPointer { get; }

    public int ObjectLowBytePointer { get; }

    public int ObjectHighBytePointer { get; }

    public int AreaDataStartPointer { get; }

    public int AreaDataEndPointer { get; }
}
