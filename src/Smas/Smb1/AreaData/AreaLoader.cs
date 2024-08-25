// <copyright file="AreaLoader.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1.AreaData;

using System;
using System.Collections.Generic;
using System.Linq;

using HeaderData;

using ObjectData;

using Snes;

using SpriteData;

public class AreaLoader
{
    public AreaLoader(Rom rom, AreaLoaderPointers pointers)
    {
        NumberOfWorlds = rom.ReadByte(pointers.NumberOfWorldsAddress);
        NumberOfAreas = pointers.NumberOfAreas;

        WorldAreaNumberOffsetTable = rom.ReadBytesIndirect(
            pointers.WorldAreaNumberOffsetPointer,
            NumberOfWorlds);

        if (WorldAreaNumberOffsetTable.Any(
            AreaNumber => AreaNumber >= pointers.AreaNumberTableSize))
        {
            throw new ArgumentException(
                "World area number table exceeds max number of areas.");
        }

        AreaNumberTable = rom.ReadBytesIndirect(
            pointers.AreaNumberTablePointer,
            pointers.AreaNumberTableSize);

        ObjectAreaIndexTable = rom.ReadBytesIndirect(
            pointers.ObjectAreaTypeOffsetPointer,
            count: 4);

        // These indices require the index table be built first.
        var objectAreaIndices = AreaNumberTable.Select(
            areaNumber => GetObjectAreaIndex(areaNumber));
        if (objectAreaIndices.Any(areaIndex => areaIndex >= NumberOfAreas))
        {
            throw new ArgumentException("Area data is invalid!");
        }

        SpriteAreaIndexTable = rom.ReadBytesIndirect(
            pointers.SpriteAreaTypeOffsetPointer,
            count: 4);

        var spriteAreaIndices = AreaNumberTable.Select(
            areaNumber => GetSpriteAreaIndex(areaNumber));
        if (spriteAreaIndices.Any(areaIndex => areaIndex >= NumberOfAreas))
        {
            throw new ArgumentException("Area data is invalid!");
        }

        var objectBank = pointers.ObjectLowBytePointer & 0xFF0000;
        var objectLows = rom.ReadBytesIndirect(
            pointers.ObjectLowBytePointer,
            NumberOfAreas);
        var objectHighs = rom.ReadBytesIndirect(
            pointers.ObjectHighBytePointer,
            NumberOfAreas);

        var spriteBank = pointers.SpriteLowBytePointer & 0xFF0000;
        var spriteLows = rom.ReadBytesIndirect(
            pointers.SpriteLowBytePointer,
            NumberOfAreas);
        var spriteHighs = rom.ReadBytesIndirect(
            pointers.SpriteHighBytePointer,
            NumberOfAreas);

        Headers = new AreaHeader[NumberOfAreas];
        AreaObjectData = new AreaObjectCommand[NumberOfAreas][];
        AreaSpriteData = new AreaSpriteCommand[NumberOfAreas][];
        for (var i = 0; i < NumberOfAreas; i++)
        {
            var objectAddress = objectBank | (objectHighs[i] << 8) | objectLows[i];
            Headers[i] = rom.ReadInt16(objectAddress);
            AreaObjectData[i] = GetAreaObjectData(
                rom.EnumerateBytes(objectAddress + 2, 0x10000))
                .ToArray();

            var spriteAddress = spriteBank | (spriteHighs[i] << 8) | spriteLows[i];
            AreaSpriteData[i] = GetAreaSpriteData(rom.EnumerateBytes(spriteAddress, 0x10000))
                .ToArray();
        }

        SortedObjectAreaTypes =
            Enumerable.Range(0, 4).Select(i => (AreaType)i).ToArray();
        Array.Sort(
            SortedObjectAreaTypes,
            (x, y) => ObjectAreaIndexTable[(int)x] - ObjectAreaIndexTable[(int)y]);

        SortedSpriteAreaTypes =
            Enumerable.Range(0, 4).Select(i => (AreaType)i).ToArray();
        Array.Sort(
            SortedSpriteAreaTypes,
            (x, y) => SpriteAreaIndexTable[(int)x] - SpriteAreaIndexTable[(int)y]);

        // The counts should be the same when using sprite or object data. We choose
        // object data.
        AreaObjectCounts = new int[4];
        for (var i = 1; i < 4; i++)
        {
            AreaObjectCounts[i - 1] =
                ObjectAreaIndexTable[(int)SortedObjectAreaTypes[i]] -
                ObjectAreaIndexTable[(int)SortedObjectAreaTypes[i - 1]];
        }

        AreaObjectCounts[3] = NumberOfAreas
            - ObjectAreaIndexTable[(int)SortedObjectAreaTypes[3]];
    }

    public int NumberOfAreas
    {
        get;
    }

    public int NumberOfWorlds
    {
        get;
    }

    public AreaHeader[] Headers
    {
        get;
    }

    public AreaObjectCommand[][] AreaObjectData
    {
        get;
    }

    public AreaSpriteCommand[][] AreaSpriteData
    {
        get;
    }

    private byte[] WorldAreaNumberOffsetTable
    {
        get;
    }

    private byte[] AreaNumberTable
    {
        get;
    }

    private byte[] ObjectAreaIndexTable
    {
        get;
    }

    private AreaType[] SortedObjectAreaTypes
    {
        get;
    }

    private byte[] SpriteAreaIndexTable
    {
        get;
    }

    private AreaType[] SortedSpriteAreaTypes
    {
        get;
    }

    private int[] AreaObjectCounts
    {
        get;
    }

    public static AreaType GetAreaType(int areaNumber)
    {
        return (AreaType)((areaNumber >> 5) & 3);
    }

    /// <summary>
    /// Returns the area number without the <see cref="AreaType"/> bits.
    /// </summary>
    public static int GetReducedAreaNumber(int areaNumber)
    {
        return areaNumber & 0x1F;
    }

    public static int AreaNumberFromAreaType(AreaType areaType, int reducedAreaNumber)
    {
        return reducedAreaNumber | ((int)areaType << 5);
    }

    public bool IsValidAreaNumberForSpriteData(int areaNumber)
    {
        var index = GetSpriteAreaIndex(areaNumber);
        return (uint)index < (uint)NumberOfAreas;
    }

    public bool IsValidAreaNumberForObjectData(int areaNumber)
    {
        var index = GetObjectAreaIndex(areaNumber);
        return (uint)index < (uint)NumberOfAreas;
    }

    public byte GetAreaNumber(int world, int level)
    {
        var worldAreaStart = WorldAreaNumberOffsetTable[world];
        var levelIndex = worldAreaStart + level;
        return (byte)(levelIndex < NumberOfAreas
            ? AreaNumberTable[levelIndex] & 0x7F
            : 0);
    }

    public int GetObjectAreaIndex(int areaNumber)
    {
        var areaType = GetAreaType(areaNumber);
        var reducedAreaNumber = GetReducedAreaNumber(areaNumber);
        var areaTypeIndex = ObjectAreaIndexTable[(int)areaType];

        return reducedAreaNumber + areaTypeIndex;
    }

    public int GetSpriteAreaIndex(int areaNumber)
    {
        var areaType = GetAreaType(areaNumber);
        var reducedAreaNumber = GetReducedAreaNumber(areaNumber);
        var areaTypeIndex = SpriteAreaIndexTable[(int)areaType];

        return reducedAreaNumber + areaTypeIndex;
    }

    public int AreaNumberFromObjectAreaIndex(int objectAreaIndex)
    {
        var areaType = AreaTypeFromObjectAreaIndex(objectAreaIndex);
        var reducedAreaNumber = objectAreaIndex - ObjectAreaIndexTable[(int)areaType];
        return AreaNumberFromAreaType(areaType, reducedAreaNumber);
    }

    public AreaType AreaTypeFromObjectAreaIndex(int objectAreaIndex)
    {
        for (var i = SortedObjectAreaTypes.Length; --i >= 0;)
        {
            if (objectAreaIndex >= ObjectAreaIndexTable[(int)SortedObjectAreaTypes[i]])
            {
                return SortedObjectAreaTypes[i];
            }
        }

        throw new ArgumentOutOfRangeException(
            nameof(objectAreaIndex),
            "Value does not correspond to valid areaType location.");
    }

    public int AreaNumberFromSpriteAreaIndex(int spriteAreaIndex)
    {
        var areaType = AreaTypeFromSpriteAreaIndex(spriteAreaIndex);
        var reducedAreaNumber = spriteAreaIndex - SpriteAreaIndexTable[(int)areaType];
        return AreaNumberFromAreaType(areaType, reducedAreaNumber);
    }

    public AreaType AreaTypeFromSpriteAreaIndex(int spriteAreaIndex)
    {
        for (var i = SortedSpriteAreaTypes.Length; --i >= 0;)
        {
            if (spriteAreaIndex >= SpriteAreaIndexTable[(int)SortedSpriteAreaTypes[i]])
            {
                return SortedSpriteAreaTypes[i];
            }
        }

        throw new ArgumentOutOfRangeException(
            nameof(spriteAreaIndex),
            "Value does not correspond to valid areaType location.");
    }

    public void WriteObjectData(
        int index,
        AreaHeader areaHeader,
        IEnumerable<AreaObjectCommand> objectData)
    {
        Headers[index] = areaHeader;
        AreaObjectData[index] = objectData.ToArray();
    }

    public void WriteSpriteData(
        int index,
        IEnumerable<AreaSpriteCommand> spriteData)
    {
        AreaSpriteData[index] = spriteData.ToArray();
    }

    public void WriteToGameData(Rom rom, AreaLoaderPointers pointers)
    {
        var totalObjectSize = 0;
        var objectData = new byte[NumberOfAreas][];
        var objectOffsets = new int[NumberOfAreas];

        var totalSpriteSize = 0;
        var spriteData = new byte[NumberOfAreas][];
        var spriteOffsets = new int[NumberOfAreas];

        for (var i = 0; i < NumberOfAreas; i++)
        {
            var spriteAreaIndex = i;
            var areaNumber = AreaNumberFromSpriteAreaIndex(spriteAreaIndex);
            var objectAreaIndex = GetObjectAreaIndex(areaNumber);
            var data = AreaObjectData[objectAreaIndex].ToBytes().ToArray();
            objectData[objectAreaIndex] = new byte[2 + data.Length];
            objectData[objectAreaIndex][0] = Headers[objectAreaIndex].Value1;
            objectData[objectAreaIndex][1] = Headers[objectAreaIndex].Value2;
            data.CopyTo(objectData[objectAreaIndex], 2);

            objectOffsets[objectAreaIndex] = totalObjectSize;
            totalObjectSize += objectData[objectAreaIndex].Length;

            spriteData[spriteAreaIndex] =
                AreaSpriteData[spriteAreaIndex].ToBytes().ToArray();
            if (spriteData[spriteAreaIndex].Length > 1 || spriteAreaIndex != 0x0F)
            {
                spriteOffsets[spriteAreaIndex] = totalSpriteSize;
                totalSpriteSize += spriteData[spriteAreaIndex].Length;
            }
            else
            {
                spriteOffsets[spriteAreaIndex] = totalSpriteSize - 1;
            }
        }

        var maxSize = pointers.AreaDataEndPointer - pointers.AreaDataStartPointer;
        if (totalObjectSize + totalSpriteSize > maxSize)
        {
            throw new InvalidOperationException();
        }

        var objectLow = new byte[NumberOfAreas];
        var objectHigh = new byte[NumberOfAreas];
        var spriteLow = new byte[NumberOfAreas];
        var spriteHigh = new byte[NumberOfAreas];
        for (var i = 0; i < NumberOfAreas; i++)
        {
            var spriteAddress = spriteOffsets[i] + pointers.AreaDataStartPointer;
            spriteLow[i] = (byte)spriteAddress;
            spriteHigh[i] = (byte)(spriteAddress >> 8);
            rom.WriteBytes(spriteAddress, spriteData[i]);

            var objectAddress = objectOffsets[i] + totalSpriteSize
                + pointers.AreaDataStartPointer;

            objectLow[i] = (byte)objectAddress;
            objectHigh[i] = (byte)(objectAddress >> 8);
            rom.WriteBytes(objectAddress, objectData[i]);
        }

        rom.WriteBytesIndirect(pointers.SpriteHighBytePointer, spriteHigh);
        rom.WriteBytesIndirect(pointers.SpriteLowBytePointer, spriteLow);
        rom.WriteBytesIndirect(pointers.ObjectHighBytePointer, objectHigh);
        rom.WriteBytesIndirect(pointers.ObjectLowBytePointer, objectLow);

        rom.WriteBytesIndirect(
            pointers.SpriteAreaTypeOffsetPointer,
            SpriteAreaIndexTable);
        rom.WriteBytesIndirect(
            pointers.ObjectAreaTypeOffsetPointer,
            ObjectAreaIndexTable);
        rom.WriteBytesIndirect(
            pointers.AreaNumberTablePointer,
            AreaNumberTable);
        rom.WriteBytesIndirect(
            pointers.WorldAreaNumberOffsetPointer,
            WorldAreaNumberOffsetTable);

        rom.WriteByte(pointers.NumberOfWorldsAddress, NumberOfWorlds);
    }

    private static IEnumerable<AreaObjectCommand> GetAreaObjectData(
        IEnumerable<byte> bytes)
    {
        using var en = bytes.GetEnumerator();
        if (!en.MoveNext())
        {
            throw new ArgumentException("Invalid data format for object data.");
        }

        while (en.Current != AreaObjectCommand.TerminationCode)
        {
            if (AreaObjectCommand.IsThreeByteSpecifier(en.Current))
            {
                var list = new List<byte>(GetBytes(3));
                yield return new AreaObjectCommand(
                    list[0],
                    list[1],
                    list[2]);
            }
            else
            {
                var list = new List<byte>(GetBytes(2));
                yield return new AreaObjectCommand(
                    list[0],
                    list[1]);
            }
        }

        IEnumerable<byte> GetBytes(int size)
        {
            for (var i = 0; i < size; i++)
            {
                yield return en.Current;

                if (!en.MoveNext())
                {
                    throw new ArgumentException("Invalid data format for object data.");
                }
            }
        }
    }

    private static IEnumerable<AreaSpriteCommand> GetAreaSpriteData(
        IEnumerable<byte> bytes)
    {
        using var en = bytes.GetEnumerator();
        if (!en.MoveNext())
        {
            throw new ArgumentException("Invalid data format for sprite data.");
        }

        while (en.Current != AreaSpriteCommand.TerminationCode)
        {
            if (AreaSpriteCommand.IsThreeByteSpecifier(en.Current))
            {
                var list = new List<byte>(GetBytes(3));
                yield return new AreaSpriteCommand(
                    list[0],
                    list[1],
                    list[2]);
            }
            else
            {
                var list = new List<byte>(GetBytes(2));
                yield return new AreaSpriteCommand(
                    list[0],
                    list[1]);
            }
        }

        IEnumerable<byte> GetBytes(int size)
        {
            for (var i = 0; i < size; i++)
            {
                yield return en.Current;

                if (!en.MoveNext())
                {
                    throw new ArgumentException("Invalid data format for sprite data.");
                }
            }
        }
    }
}
