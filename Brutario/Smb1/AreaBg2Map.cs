using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brutario.Smb1
{
    public class AreaBg2Map
    {
        public const int TileMapAreaIndexPointer = 0x05916B;

        public const int TileMapTablesPointer = 0x59170;

        public AreaBg2Map(RomData romData)
        {
            RomData = romData
                ?? throw new ArgumentNullException(nameof(romData));
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

        public int TileMapAreaIndexAddress
        {
            get
            {
                return 0x050000 | Rom.ReadInt16(TileMapAreaIndexPointer);
            }
        }

        public int TileMapTablesAddress
        {
            get
            {
                return 0x050000 | Rom.ReadInt16(TileMapTablesPointer);
            }
        }

        public Obj16Tile[] GetTileSet(int areaIndex)
        {
            var result = new Obj16Tile[0x100];
            var resultSizeInBytes = result.Length * Obj16Tile.SizeOf;
            var address = GetTileMapAddress(areaIndex);
            var bytes = Rom.ReadBytes(address, resultSizeInBytes);

            unsafe
            {
                fixed (byte* src = bytes)
                fixed (Obj16Tile* dest = result)
                {
                    Buffer.MemoryCopy(
                        src,
                        dest,
                        resultSizeInBytes,
                        bytes.Length);
                }
            }

            return result;
        }

        public int[] GetTileMap(int areaIndex)
        {
            var result = new int[0x0D00];
            var src = Brutario.Properties.Resources.map16;
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = src[i];
            }

            return result;
        }

        private int GetTileMapAddress(int areaIndex)
        {
            var index = (ushort)Rom.ReadInt16(
                TileMapAreaIndexAddress + (areaIndex << 1));

            var word = (ushort)Rom.ReadInt16(
                TileMapTablesAddress + (index << 1));

            return 0x050000 | word;
        }
    }
}
