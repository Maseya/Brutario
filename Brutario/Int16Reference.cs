using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brutario
{
    public class Int16Reference
    {
        public Int16Reference(RomIO rom, int address)
        {
            if (rom is null)
            {
                throw new ArgumentNullException(nameof(rom));
            }

            Address = address;
            Value = rom.ReadInt16(address);
        }

        public int Address
        {
            get;
            set;
        }

        public int Value
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

            rom.WriteInt16(Address, Value);
        }
    }
}
