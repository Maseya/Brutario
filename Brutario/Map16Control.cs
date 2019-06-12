using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.DesignerSerializationVisibility;

namespace Brutario
{
    public class Map16Control : DesignControl
    {
        private Color32BppArgb[] _palette;
        private byte[] _pixelMap;
        private Obj16Tile[] _tiles;
        private int _startTile;

        public Map16Control()
        {
            ClientSize = new Size(0x100, 0x100);

            StartTileIndexes = new List<int>();
            for (var i = 0; i < 0x200; i += 0x100)
            {
                StartTileIndexes.Add(i);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(Hidden)]
        public Color32BppArgb[] Palette
        {
            get
            {
                return _palette;
            }

            set
            {
                if (Palette == value)
                {
                    return;
                }

                _palette = value;
                if (Palette != null && Palette.Length != 0x100)
                {
                    throw new ArgumentException();
                }

                Invalidate();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(Hidden)]
        public byte[] PixelData
        {
            get
            {
                return _pixelMap;
            }

            set
            {
                if (PixelData == value)
                {
                    return;
                }

                _pixelMap = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(Hidden)]
        public Obj16Tile[] Tiles
        {
            get
            {
                return _tiles;
            }

            set
            {
                if (Tiles == value)
                {
                    return;
                }

                _tiles = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(Hidden)]
        public int StartTile
        {
            get
            {
                return _startTile;
            }

            set
            {
                _startTile = value;
                Invalidate();
            }
        }

        private List<int> StartTileIndexes
        {
            get;
        }

        private int StartTileIndexIndex
        {
            get;
            set;
        }

        public Color32BppArgb[] RenderPixels()
        {
            if (Palette is null || PixelData is null || Tiles is null)
            {
                throw new InvalidOperationException();
            }

            var result = new Color32BppArgb[0x100 * 0x100];
            Parallel.For(0, 0x10, RenderRow);

            void RenderRow(int row)
            {
                var rowIndex = StartTile + (row * 0x10);
                var pixelRow = row * 0x10 * 0x100;
                for (var column = 0; column < 0x10; column++)
                {
                    var tile = Tiles[rowIndex + column];
                    var columnIndex = pixelRow + (column * 0x10);
                    for (var i = 0; i < 4; i++)
                    {
                        var tile8 = tile[i];
                        var xFlip = tile8.XFlipped ? 7 : 0;
                        var yFlip = tile8.YFlipped ? 7 : 0;
                        var tileYOffset = (i >> 1) * 8;
                        var tileIndex = columnIndex
                            + ((i & 1) * 8)
                            + (tileYOffset * 0x100);

                        var paletteIndex = tile8.PaletteNumber * 0x10;
                        var pixelIndex = tile8.TileIndex * 0x40;
                        for (var y = 0; y < 8; y++, pixelIndex += 8)
                        {
                            var index = tileIndex + ((y ^ yFlip) * 0x100);
                            var gradientColor = rowIndex + tileYOffset + y;
                            for (var x = 0; x < 8; x++)
                            {
                                var pixel = PixelData[
                                    pixelIndex + (x ^ xFlip)];

                                result[index++] = pixel != 0
                                    ? Palette[paletteIndex + pixel]
                                    : new Color32BppArgb(
                                        0xFF,
                                        0xFF ^ gradientColor,
                                        0xFF,
                                        0xFF);
                            }
                        }
                    }
                }
            }

            return result;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Palette != null && PixelData != null && Tiles != null)
            {
                DrawMap16(e.Graphics);
            }

            base.OnPaint(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageDown)
            {
                StartTileIndexIndex++;
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                StartTileIndexIndex--;
            }

            StartTileIndexIndex %= StartTileIndexes.Count;
            if (StartTileIndexIndex < 0)
            {
                StartTileIndexIndex += StartTileIndexes.Count;
            }

            StartTile = StartTileIndexes[StartTileIndexIndex];

            base.OnKeyDown(e);
        }

        private unsafe void DrawMap16(Graphics graphics)
        {
            fixed (Color32BppArgb* pixels = RenderPixels())
            {
                using (var image = new Bitmap(
                    0x100,
                    0x100,
                    0x400,
                    PixelFormat.Format32bppArgb,
                    (IntPtr)pixels))
                {
                    graphics.DrawImageUnscaled(image, Point.Empty);
                }
            }
        }
    }
}
