// <copyright file="TilemapLoaderPointers.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1;

public class TilemapLoaderPointers
{
    public static readonly TilemapLoaderPointers Jp10 = new(
        baseAddress: 0x058057);

    public static readonly TilemapLoaderPointers Jp11 = new(
        baseAddress: 0x058057);

    public static readonly TilemapLoaderPointers Usa = new(
        baseAddress: 0x058057);

    public static readonly TilemapLoaderPointers UsaPlusW = new(
        baseAddress: 0x058057);

    public static readonly TilemapLoaderPointers Eu = new(
        baseAddress: 0x058057);

    public static readonly TilemapLoaderPointers EuPlusW = new(
        baseAddress: 0x058057);

    public static readonly TilemapLoaderPointers UsaSmb1 = new(
        baseAddress: 0x028057);

    public TilemapLoaderPointers(
        int tilemapDataIndexPointer,
        int tilemapDataPointer,
        int layer2BackgroundIndexTablePointer,
        int layer2BackgroundPointersTablePointer,
        int layer2Obj16TileTableSize,
        int layer2Obj16TileTablePointer)
    {
        TilemapDataIndexPointer = tilemapDataIndexPointer;
        TilemapDataPointer = tilemapDataPointer;
        Layer2BackgroundIndexTablePointer = layer2BackgroundIndexTablePointer;
        Layer2BackgroundPointersTablePointer = layer2BackgroundPointersTablePointer;
        Layer2Obj16TileTableSize = layer2Obj16TileTableSize;
        Layer2Obj16TileTablePointer = layer2Obj16TileTablePointer;
    }

    private TilemapLoaderPointers(int baseAddress)
            : this(
            tilemapDataIndexPointer: baseAddress,
            tilemapDataPointer: baseAddress + 0x09,
            layer2BackgroundIndexTablePointer: baseAddress + 0x1114,
            layer2BackgroundPointersTablePointer: baseAddress + 0x1119,
            layer2Obj16TileTablePointer: baseAddress + 0x118D,
            layer2Obj16TileTableSize: 364)
    { }

    public int TilemapDataIndexPointer { get; }

    public int TilemapDataPointer { get; }

    public int Layer2BackgroundIndexTablePointer { get; }

    public int Layer2BackgroundPointersTablePointer { get; }

    public int Layer2Obj16TileTablePointer { get; }

    public int Layer2Obj16TileTableSize { get; }
}
