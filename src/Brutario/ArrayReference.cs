using System;
using System.Collections.ObjectModel;

namespace Brutario
{
    public class ArrayReference
    {
        private readonly byte[] _bytes;

        public ArrayReference(
            RomIO rom,
            int address,
            int count,
            bool crossBanks = true)
        {
            if (rom is null)
            {
                throw new ArgumentNullException(nameof(rom));
            }

            Address = address;
            CrossBanks = crossBanks;

            _bytes = rom.ReadBytes(address, count, crossBanks);
            Bytes = new Collection<byte>(_bytes);
        }

        public int Address
        {
            get;
            set;
        }

        public bool CrossBanks
        {
            get;
            set;
        }

        public Collection<byte> Bytes
        {
            get;
        }

        public void WriteToRom(RomIO rom)
        {
            if (rom is null)
            {
                throw new ArgumentNullException(nameof(rom));
            }

            rom.WriteBytes(_bytes, Address, CrossBanks);
        }
    }
}
