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
        int tilemapDataPointer)
    {
        TilemapDataIndexPointer = tilemapDataIndexPointer;
        TilemapDataPointer = tilemapDataPointer;
    }

    private TilemapLoaderPointers(int baseAddress)
            : this(
            tilemapDataIndexPointer: baseAddress,
            tilemapDataPointer: baseAddress + 0x09)
    { }

    public int TilemapDataIndexPointer { get; }

    public int TilemapDataPointer { get; }
}
