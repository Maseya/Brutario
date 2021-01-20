namespace Brutario.Smb1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AreaObjectLoader
    {
        public const int LowBytePointer = 0x04C093;
        public const int HighBytePointer = 0x04C098;
        public const int AreaTypeOffsetPointer = 0x04C073;

        public AreaObjectLoader(AreaLoader areaLoader)
        {
            AreaLoader = areaLoader
                ?? throw new ArgumentNullException(nameof(areaLoader));
        }

        public AreaLoader AreaLoader
        {
            get;
        }

        public GameData RomData
        {
            get
            {
                return AreaLoader.RomData;
            }
        }

        public RomIO Rom
        {
            get
            {
                return RomData.Rom;
            }
        }

        public int LowByteAddress
        {
            get
            {
                return 0x040000 | Rom.ReadInt16(LowBytePointer);
            }
        }

        public int HighByteAddress
        {
            get
            {
                return 0x040000 | Rom.ReadInt16(HighBytePointer);
            }
        }

        public int AreaTypeOffsetAddress
        {
            get
            {
                return 0x040000 | Rom.ReadInt16(AreaTypeOffsetPointer);
            }
        }

        public int GetAreaIndex(int areaNumber)
        {
            var areaType = AreaLoader.GetAreaType(areaNumber);
            var reducedAreaNumber = areaNumber & 0x1F;
            var areaTypeIndex = Rom.ReadByte(
                AreaTypeOffsetAddress + (int)areaType);

            return reducedAreaNumber + areaTypeIndex;
        }

        public int GetAreaAddress(int areaNumber)
        {
            var areaIndex = GetAreaIndex(areaNumber);
            return 0x040000
                | (Rom.ReadByte(HighByteAddress + areaIndex) << 8)
                | Rom.ReadByte(LowByteAddress + areaIndex);
        }

        public AreaHeader GetAreaHeader(int snesAddress)
        {
            var value = Rom.ReadInt16(snesAddress);
            return new AreaHeader((byte)value, (byte)(value >> 8));
        }

        public IEnumerable<AreaObjectCommand> GetAreaData(int snesAddress)
        {
            var index = Rom.SnesToPc(snesAddress);
            return AreaObjectCommand.GetAreaData(Rom.Data.Skip(index));
        }

        public void WriteAreaData(
            int snesAddress,
            IEnumerable<AreaObjectCommand> data)
        {
            var bytes = new List<byte>(
                AreaObjectCommand.GetAreaByteData(data));

            bytes.CopyTo(Rom.Data, Rom.SnesToPc(snesAddress));
        }
    }
}
