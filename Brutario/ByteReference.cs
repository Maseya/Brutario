using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brutario
{
    public class ByteReference
    {
        public ByteReference(RomIO rom, int address)
        {
            if (rom is null)
            {
                throw new ArgumentNullException(nameof(rom));
            }

            Address = address;
            Value = rom.ReadByte(address);
        }

        public int Address
        {
            get;
            set;
        }

        public byte Value
        {
            get;
            set;
        }

        public void WriteToRom(RomIO rom)
        {
            if (rom is null)
            {
                throw new ArgumentNullException(nameof(rom));
            }

            rom.WriteByte(Address, Value);
        }
    }
}
