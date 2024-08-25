// <copyright file="Map16DataPointers.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1;

public class Map16DataPointers
{
    public static readonly Map16DataPointers Jp10 = new(
        baseAddress: 0x039298);

    public static readonly Map16DataPointers Jp11 = new(
        baseAddress: 0x0392A5);

    public static readonly Map16DataPointers Usa = new(
        baseAddress: 0x03927D);

    public static readonly Map16DataPointers UsaPlusW = new(
        baseAddress: 0x03928A);

    public static readonly Map16DataPointers Eu = new(
        baseAddress: 0x03929B);

    public static readonly Map16DataPointers EuPlusW = new(
        baseAddress: 0x03929B);

    public static readonly Map16DataPointers UsaSmb1 = new(
        baseAddress: 0x0092BC);

    public Map16DataPointers(int lowBytePointer, int highBytePointer)
    {
        LowBytePointer = lowBytePointer;
        HighBytePointer = highBytePointer;
    }

    private Map16DataPointers(int baseAddress)
        : this(
          lowBytePointer: baseAddress,
          highBytePointer: baseAddress + 0x05)
    { }

    public int LowBytePointer
    {
        get;
    }

    public int HighBytePointer
    {
        get;
    }
}
