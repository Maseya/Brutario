namespace Brutario.Smb1
{
    public class TilemapLoaderPointers
    {
        public static TilemapLoaderPointers Jp10 = new TilemapLoaderPointers(
            baseAddress: 0x058057);

        public static TilemapLoaderPointers Jp11 = new TilemapLoaderPointers(
            baseAddress: 0x058057);

        public static TilemapLoaderPointers Usa = new TilemapLoaderPointers(
            baseAddress: 0x058057);

        public static TilemapLoaderPointers UsaPlusW = new TilemapLoaderPointers(
            baseAddress: 0x058057);

        public static TilemapLoaderPointers Eu = new TilemapLoaderPointers(
            baseAddress: 0x058057);

        public static TilemapLoaderPointers EuPlusW = new TilemapLoaderPointers(
            baseAddress: 0x058057);

        public static TilemapLoaderPointers UsaSmb1 = new TilemapLoaderPointers(
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
}
