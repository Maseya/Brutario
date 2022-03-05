// <copyright file="AreaLoader.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed under GNU
//     Affero General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Smb1
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class AreaLoader
    {
        public const int DefaultNumberOfAreas = 0x80;

        public AreaLoader()
        {
            WorldAreaStartTable = new byte[0x100];
            AreaNumberTable = new byte[0x100];
            ObjectAreaIndexTable = new byte[4];
            SpriteAreaIndexTable = new byte[4];
            Headers = new AreaHeader[DefaultNumberOfAreas];

            var areaObjectData = new ObjectListEditor[DefaultNumberOfAreas];
            var areaSpriteData = new SpriteListEditor[DefaultNumberOfAreas];
            for (var i = 0; i < DefaultNumberOfAreas; i++)
            {
                areaObjectData[i] = new ObjectListEditor();
                areaSpriteData[i] = new SpriteListEditor();
            }

            AreaObjectData = new ReadOnlyCollection<ObjectListEditor>(areaObjectData);
            AreaSpriteData = new ReadOnlyCollection<SpriteListEditor>(areaSpriteData);

            // Isn't sorted until game data is loaded.
            SortedObjectAreaTypes = new AreaType[4];
            for (var i = 0; i < SortedObjectAreaTypes.Length; i++)
            {
                SortedObjectAreaTypes[i] = (AreaType)i;
            }

            // Isn't sorted until game data is loaded.
            SortedSpriteAreaTypes = new AreaType[4];
            for (var i = 0; i < SortedSpriteAreaTypes.Length; i++)
            {
                SortedSpriteAreaTypes[i] = (AreaType)i;
            }

            AreaObjectCounts = new int[4];
        }

        public AreaLoader(GameData gameData, AreaLoaderPointers pointers)
            : this()
        {
            ReadGameData(gameData, pointers);
        }

        public int NumberOfAreas
        {
            get;
            private set;
        }

        public int NumberOfWorlds
        {
            get;
            private set;
        }

        private int AreaNumberTableSize
        {
            get;
            set;
        }

        public AreaHeader[] Headers
        {
            get;
        }

        public ReadOnlyCollection<ObjectListEditor> AreaObjectData
        {
            get;
        }

        public ReadOnlyCollection<SpriteListEditor> AreaSpriteData
        {
            get;
        }

        private byte[] WorldAreaStartTable
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

        public void ChangeAreaType(int areaNumber, AreaType newAreaType)
        {
            throw new NotImplementedException();

            var oldAreaType = GetAreaType(areaNumber);
            var objectAreaIndex = GetObjectAreaIndex(areaNumber);
            var spriteAreaIndex = GetSpriteAreaIndex(areaNumber);
            var objectData = AreaObjectData[objectAreaIndex];
            var spriteData = AreaSpriteData[spriteAreaIndex];

            AreaObjectCounts[(int)oldAreaType]--;
            var newAreaNumber = ((int)newAreaType << 5) |
                AreaObjectCounts[(int)newAreaType];
            AreaObjectCounts[(int)newAreaType]++;

            for (var i = 0; i < 4; i++)
            {
                if (SortedObjectAreaTypes[i] == oldAreaType)
                {
                    for (i++; i < 4 && SortedObjectAreaTypes[i] <= newAreaType; i++)
                    {
                        ObjectAreaIndexTable[(int)SortedObjectAreaTypes[i]]--;
                    }
                    break;
                }
                else if (SortedObjectAreaTypes[i] == newAreaType)
                {
                    for (i++; i < 4 && SortedObjectAreaTypes[i] <= oldAreaType; i++)
                    {
                        ObjectAreaIndexTable[(int)SortedObjectAreaTypes[i]]++;
                    }
                    break;
                }
            }

            for (var i = 0; i < 4; i++)
            {
                if (SortedSpriteAreaTypes[i] == oldAreaType)
                {
                    for (i++; i < 4 && SortedSpriteAreaTypes[i] <= newAreaType; i++)
                    {
                        SpriteAreaIndexTable[(int)SortedSpriteAreaTypes[i]]--;
                    }
                    break;
                }
                else if (SortedSpriteAreaTypes[i] == newAreaType)
                {
                    for (i++; i < 4 && SortedSpriteAreaTypes[i] <= oldAreaType; i++)
                    {
                        SpriteAreaIndexTable[(int)SortedSpriteAreaTypes[i]]++;
                    }
                    break;
                }
            }

            var newObjectAreaIndex = GetObjectAreaIndex(newAreaNumber);
            var newSpriteAreaIndex = GetSpriteAreaIndex(newAreaNumber);
            if (newObjectAreaIndex < objectAreaIndex)
            {
                for (var i = objectAreaIndex; i > newObjectAreaIndex; i--)
                {
                    AreaObjectData[i].Reset(AreaObjectData[i - 1]);
                }
            }
            else
            {
                for (var i = objectAreaIndex; i < newObjectAreaIndex; i++)
                {
                    AreaObjectData[i].Reset(AreaObjectData[i + 1]);
                }
            }

            if (newSpriteAreaIndex < spriteAreaIndex)
            {
                for (var i = spriteAreaIndex; i > newSpriteAreaIndex; i--)
                {
                    AreaSpriteData[i].Reset(AreaSpriteData[i - 1]);
                }
            }
            else
            {
                for (var i = spriteAreaIndex; i < newSpriteAreaIndex; i++)
                {
                    AreaSpriteData[i].Reset(AreaSpriteData[i + 1]);
                }
            }

            AreaObjectData[newObjectAreaIndex].Reset(objectData);
            AreaSpriteData[newSpriteAreaIndex].Reset(spriteData);
        }

        public void ReadGameData(GameData gameData, AreaLoaderPointers pointers)
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
            NumberOfWorlds = rom.ReadByte(pointers.NumberOfWorldsAddress);
            NumberOfAreas = pointers.NumberOfAreas;
            AreaNumberTableSize = pointers.AreaNumberTableSize;

            rom.ReadBytesIndirect(
                pointers.WorldLevelOffsetPointer,
                new Span<byte>(WorldAreaStartTable, 0, NumberOfWorlds));
            rom.ReadBytesIndirect(
                pointers.AreaIndexTablePointer,
                new Span<byte>(AreaNumberTable, 0, AreaNumberTableSize));
            rom.ReadBytesIndirect(
                pointers.ObjectAreaTypeOffsetPointer,
                ObjectAreaIndexTable);
            rom.ReadBytesIndirect(
                pointers.SpriteAreaTypeOffsetPointer,
                SpriteAreaIndexTable);

            for (var i = 0; i < NumberOfAreas; i++)
            {
                var objectBank = pointers.ObjectLowBytePointer & 0xFF0000;
                var objectAddress = objectBank | rom.ReadInt16IndirectIndexed(
                    pointers.ObjectLowBytePointer, pointers.ObjectHighBytePointer, i);
                Headers[i] = rom.ReadInt16(objectAddress);
                AreaObjectData[i].Reset(
                    ObjectListEditor.GetAreaData(
                        rom.EnumerateBytes(objectAddress + 2)));

                var spriteBank = pointers.SpriteLowBytePointer & 0xFF0000;
                var spriteAddress = spriteBank | rom.ReadInt16IndirectIndexed(
                    pointers.SpriteLowBytePointer, pointers.SpriteHighBytePointer, i);
                AreaSpriteData[i].Reset(
                    SpriteListEditor.GetAreaData(
                        rom.EnumerateBytes(spriteAddress)));
            }

            Array.Sort(
                SortedObjectAreaTypes,
                (x, y) => ObjectAreaIndexTable[(int)x] - ObjectAreaIndexTable[(int)y]);
            Array.Sort(
                SortedSpriteAreaTypes,
                (x, y) => SpriteAreaIndexTable[(int)x] - SpriteAreaIndexTable[(int)y]);

            // The counts should be the same when using sprite or object data. We
            // choose object data.
            for (var i = 0; i < 3; i++)
            {
                AreaObjectCounts[i] =
                    ObjectAreaIndexTable[(int)SortedObjectAreaTypes[i + 1]] -
                    ObjectAreaIndexTable[(int)SortedObjectAreaTypes[i]];
            }
            AreaObjectCounts[3] = NumberOfAreas
                - ObjectAreaIndexTable[(int)SortedObjectAreaTypes[3]];
        }

        public byte GetAreaNumber(int world, int level)
        {
            var worldAreaStart = WorldAreaStartTable[world];
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

            throw new ArgumentOutOfRangeException();
        }

        public int AreaNumberFromSpriteAreaIndex(int spriteAreaIndex)
        {
            var areaType = AreaTypeFromSpriteAreaIndex(spriteAreaIndex);
            var reducedAreaNumber = spriteAreaIndex - SpriteAreaIndexTable[(int)areaType];
            return AreaNumberFromAreaType(areaType, reducedAreaNumber);
        }

        public int AreaNumberFromAreaType(AreaType areaType, int reducedAreaNumber)
        {
            return reducedAreaNumber | ((int)areaType << 5);
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

            throw new ArgumentOutOfRangeException();
        }

        public void WriteObjectData(
            int index,
            AreaHeader areaHeader,
            IEnumerable<AreaObjectCommand> objectData)
        {
            Headers[index] = areaHeader;
            AreaObjectData[index].Reset(objectData);
        }

        public void WriteSpriteData(
            int index,
            IEnumerable<AreaSpriteCommand> spriteData)
        {
            AreaSpriteData[index].Reset(spriteData);
        }

        public void WriteToGameData(GameData gameData, AreaLoaderPointers pointers)
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
                var data = AreaObjectData[objectAreaIndex].ToByteArray();
                objectData[objectAreaIndex] = new byte[2 + data.Length];
                objectData[objectAreaIndex][0] = Headers[objectAreaIndex].Value1;
                objectData[objectAreaIndex][1] = Headers[objectAreaIndex].Value2;
                data.CopyTo(objectData[objectAreaIndex], 2);

                objectOffsets[objectAreaIndex] = totalObjectSize;
                totalObjectSize += objectData[objectAreaIndex].Length;

                spriteData[spriteAreaIndex] =
                    AreaSpriteData[spriteAreaIndex].ToByteArray();
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

            rom.WriteBytesIndirect(pointers.SpriteAreaTypeOffsetPointer, SpriteAreaIndexTable);
            rom.WriteBytesIndirect(pointers.ObjectAreaTypeOffsetPointer, ObjectAreaIndexTable);
            rom.WriteBytesIndirect(pointers.AreaIndexTablePointer, AreaNumberTable);
            rom.WriteBytesIndirect(pointers.WorldLevelOffsetPointer, WorldAreaStartTable);

            rom.WriteByte(pointers.NumberOfWorldsAddress, NumberOfWorlds);
        }
    }
}
