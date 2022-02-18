namespace Brutario.Smb1
{
    using System;

    public class Map16Data
    {
        public Map16Data()
        {
            Data = new Obj16Tile[][]
            {
                new Obj16Tile[0x2B],
                new Obj16Tile[0x38],
                new Obj16Tile[0x0E],
                new Obj16Tile[0x3E],
            };

            IsTileAccessible = new bool[0x100];
            for (var i = 0; i < Data.Length; i++)
            {
                var sourceOffset = i << 6;
                for (var j = 0; j < Data[i].Length; j++)
                {
                    IsTileAccessible[sourceOffset + j] = true;
                }
            }
        }

        public Map16Data(GameData gameData, Map16DataPointers pointers)
            : this()
        {
            ReadGameData(gameData, pointers);
        }

        public bool[] IsTileAccessible
        {
            get;
        }

        private Obj16Tile[][] Data
        {
            get;
        }

        public void ReadGameData(GameData gameData, Map16DataPointers pointers)
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
            for (var i = 0; i < Data.Length; i++)
            {
                unsafe
                {
                    fixed (Obj16Tile* ptr = Data[i])
                    {
                        var dest = new Span<short>(
                                (short*)ptr,
                                Data[i].Length * Obj16Tile.NumberOfTiles);
                        var low = rom.ReadByteIndirectIndexed(
                            pointers.LowBytePointer, i);
                        var high = rom.ReadByteIndirectIndexed(
                            pointers.HighBytePointer, i);
                        var address = (pointers.LowBytePointer & 0xFF0000)
                            | (high << 8) | low;
                        rom.ReadInt16Array(address, dest);
                    }
                }
            }
        }

        public void ReadTiles(Span<Obj16Tile> dest)
        {
            if (dest.Length < 0x100)
            {
                throw new ArgumentException();
            }

            for (var i = 0; i < Data.Length; i++)
            {
                Data[i].CopyTo(dest.Slice(i << 6));
            }
        }

        public void WriteTiles(Span<Obj16Tile> data)
        {
            if (data.Length < 0x100)
            {
                throw new ArgumentException();
            }

            for (var i = 0; i < Data.Length; i++)
            {
                data.Slice(i << 6, Data[i].Length).CopyTo(Data[i]);
            }
        }

        public void WriteToGameData(GameData gameData, Map16DataPointers pointers)
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
            var low = rom.ReadBytesIndirect(pointers.LowBytePointer, Data.Length);
            var high = rom.ReadBytesIndirect(pointers.HighBytePointer, Data.Length);
            for (var i = 0; i < Data.Length; i++)
            {
                unsafe
                {
                    fixed (Obj16Tile* ptr = Data[i])
                    {
                        var src = new Span<short>(
                                (short*)ptr, Data[i].Length *
                                Obj16Tile.NumberOfTiles);
                        var address = (pointers.LowBytePointer & 0xFF0000)
                            | (high[i] << 8) | low[i];
                        rom.WriteInt16Array(address, src);
                    }
                }
            }
        }
    }
}
