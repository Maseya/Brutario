namespace Brutario.Smb1
{
    public class Map16DataPointers
    {
        public static Map16DataPointers Jp10 = new Map16DataPointers(
            baseAddress: 0x039298);

        public static Map16DataPointers Jp11 = new Map16DataPointers(
            baseAddress: 0x0392A5);

        public static Map16DataPointers Usa = new Map16DataPointers(
            baseAddress: 0x03927D);

        public static Map16DataPointers UsaPlusW = new Map16DataPointers(
            baseAddress: 0x03928A);

        public static Map16DataPointers Eu = new Map16DataPointers(
            baseAddress: 0x03929B);

        public static Map16DataPointers EuPlusW = new Map16DataPointers(
            baseAddress: 0x03929B);

        public static Map16DataPointers UsaSmb1 = new Map16DataPointers(
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
        {
        }

        public int LowBytePointer
        {
            get;
        }

        public int HighBytePointer
        {
            get;
        }
    }
}
