namespace Brutario.Smb1
{
    using System;
    using System.Collections.Generic;

    public class AreaLoader
    {
        public const int AreaListPointer = 0x04C03D;
        public const int WorldLevelOffsetPointer = 0x04C035;
        public const int DefaultNumberOfAreas = 0x22;
        public const int NumberOfWorldsAddress = 0x04C026;

        public AreaLoader(RomData romData)
        {
            RomData = romData
                ?? throw new ArgumentNullException(nameof(romData));

            AreaObjectLoader = new AreaObjectLoader(this);
            AreaSpriteLoader = new AreaSpriteLoader(this);
        }

        public RomData RomData
        {
            get;
        }

        public RomIO Rom
        {
            get
            {
                return RomData.Rom;
            }
        }

        public AreaObjectLoader AreaObjectLoader
        {
            get;
        }

        public AreaSpriteLoader AreaSpriteLoader
        {
            get;
        }

        public int AreaListAddress
        {
            get
            {
                return 0x040000 | Rom.ReadInt16(AreaListPointer);
            }
        }

        public int WorldLevelOffsetAddress
        {
            get
            {
                return 0x040000 | Rom.ReadInt16(WorldLevelOffsetPointer);
            }
        }

        public int NumberOfAreas
        {
            get
            {
                return DefaultNumberOfAreas;
            }
        }

        public int NumberOfWorlds
        {
            get
            {
                return Rom.ReadByte(NumberOfWorldsAddress);
            }
        }

        public static AreaType GetAreaType(int areaNumber)
        {
            return (AreaType)((areaNumber >> 5) & 3);
        }

        public byte GetAreaNumber(int world, int level)
        {
            var worldStartLevel = GetWorldStartLevel(world);
            var actualLevel = worldStartLevel + level;
            return (byte)(Rom.ReadByte(AreaListAddress + actualLevel) & 0x7F);
        }

        public byte GetWorldStartLevel(int world)
        {
            return Rom.ReadByte(WorldLevelOffsetAddress + world);
        }

        public IEnumerable<byte> EnumerateAreaNumbers()
        {
            for (var i = 0; i < NumberOfAreas; i++)
            {
                yield return Rom.ReadByte(AreaListAddress + i);
            }
        }
    }
}
