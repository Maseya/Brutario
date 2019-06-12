using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brutario.Smb1
{
    public class TilemapLoader
    {
        public const int TilemapDataIndexPointer = 0x058057;

        public const int TilemapDataPointer = 0x058060;

        public TilemapLoader(RomData romData)
        {
            RomData = romData
                ?? throw new ArgumentNullException(nameof(romData));

            Layer2Tilemap = new int[0xD00 >> 1];

            BackgroundGenerationCommands = new Action[0x0D]
            {
                () => Layer2TilemapIndex++,
                EnableHdmaGradient,
                EnableHdmaWaving,
                UnknownCommand03,
                SetTilemapIndex,
                FillTopAreaTilemap,
                FillUndergroundRockPattern,
                FillUnderwaterTopAreaTilemap,
                FillWaterFallRockPattern,
                () => EnableLayer3 = true,
                GenerateWaterfallTiles,
                SetSpecialTilemapIndex,
                GenerateGoombaPillars,
            };
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

        public int[] Layer2Tilemap
        {
            get;
        }

        public byte TileSetIndex
        {
            get;
            private set;
        }

        public int TilemapDataIndexAddress
        {
            get
            {
                return 0x050000 | Rom.ReadInt16(TilemapDataIndexPointer);
            }
        }

        public int TilemapDataAddress
        {
            get
            {
                return 0x050000 | Rom.ReadInt16(TilemapDataPointer);
            }
        }

        public bool EnableLayer3
        {
            get;
            private set;
        }

        private int Layer2TilemapIndex
        {
            get;
            set;
        }

        private Action[] BackgroundGenerationCommands
        {
            get;
        }

        private TilemapCommand CurrentTilemapCommand
        {
            get;
            set;
        }

        public void LoadTilemap(int areaIndex)
        {
            EnableLayer3 = false;
            Array.Clear(Layer2Tilemap, 0, Layer2Tilemap.Length);

            var tilemapDataIndex = Rom.ReadInt16(
                TilemapDataIndexAddress + (areaIndex << 1));

            var address = TilemapDataAddress + tilemapDataIndex;

            while (true)
            {
                CurrentTilemapCommand = Rom.ReadInt16(address);
                address += 2;
                if ((CurrentTilemapCommand.CommandED & 0xF0) == 0xE0)
                {
                    if (CurrentTilemapCommand.CommandEF == 0x3F)
                    {
                        Layer2Tilemap[++Layer2TilemapIndex] = 0xFFFF;
                        LoadTilemapTiles();
                        break;
                    }
                    else
                    {
                        BackgroundGenerationCommands[
                            CurrentTilemapCommand.CommandEF]();
                    }
                }
                else
                {
                }
            }
        }

        private void LoadTilemapTiles()
        {
        }

        private void EnableHdmaGradient()
        {
        }

        private void EnableHdmaWaving()
        {
        }

        private void UnknownCommand03()
        {
        }

        private void SetTilemapIndex()
        {
            TileSetIndex = (byte)CurrentTilemapCommand.CommandF1;
        }

        private void FillTopAreaTilemap()
        {
        }

        private void FillUndergroundRockPattern()
        {
        }

        private void FillUnderwaterTopAreaTilemap()
        {
        }

        private void FillWaterFallRockPattern()
        {
        }

        private void GenerateWaterfallTiles()
        {
        }

        private void SetSpecialTilemapIndex()
        {
            Layer2TilemapIndex = CurrentTilemapCommand.CommandF1 | 0x10;
        }

        private void GenerateGoombaPillars()
        {
        }
    }
}
