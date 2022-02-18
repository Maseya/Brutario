namespace Brutario.Smb1
{
    public class AreaLoaderPointers
    {
        public static readonly AreaLoaderPointers Jp10 = new AreaLoaderPointers(
            baseAddress: 0x04C026);

        public static readonly AreaLoaderPointers Jp11 = new AreaLoaderPointers(
            baseAddress: 0x04C026);

        public static readonly AreaLoaderPointers Usa = new AreaLoaderPointers(
            baseAddress: 0x04C026);

        public static readonly AreaLoaderPointers UsaPlusW = new AreaLoaderPointers(
            baseAddress: 0x04C026);

        public static readonly AreaLoaderPointers Eu = new AreaLoaderPointers(
            baseAddress: 0x04C026);

        public static readonly AreaLoaderPointers EuPlusW = new AreaLoaderPointers(
            baseAddress: 0x04C026);

        public static readonly AreaLoaderPointers UsaSmb1 = new AreaLoaderPointers(
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
            int areaDataEndPointer)
        {
            NumberOfWorldsAddress = numberOfWorldsAddress;
            WorldLevelOffsetPointer = worldLevelOffsetPointer;
            AreaIndexTablePointer = areaIndexTablePointer;
            SpriteAreaTypeOffsetPointer = spriteAreaTypeOffsetPointer;
            SpriteLowBytePointer = spriteLowBytePointer;
            SpriteHighBytePointer = spriteHighBytePointer;
            ObjectAreaTypeOffsetPointer = objectAreaTypeOffsetPointer;
            ObjectLowBytePointer = objectLowBytePointer;
            ObjectHighBytePointer = objectHighBytePointer;
            AreaDataStartPointer = areaDataStartPointer;
            AreaDataEndPointer = areaDataEndPointer;
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

        public int WorldLevelOffsetPointer { get; }

        public int AreaIndexTablePointer { get; }

        public int SpriteAreaTypeOffsetPointer { get; }

        public int SpriteLowBytePointer { get; }

        public int SpriteHighBytePointer { get; }

        public int ObjectAreaTypeOffsetPointer { get; }

        public int ObjectLowBytePointer { get; }

        public int ObjectHighBytePointer { get; }

        public int AreaDataStartPointer { get; }

        public int AreaDataEndPointer { get; }
    }
}
