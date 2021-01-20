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
    public class GfxControl : DesignControl
    {
        private Color32BppArgb[] _palette;
        private byte[] _pixelMap;
        private int _paletteIndex;
        private int _startTile;

        public GfxControl()
        {
            ClientSize = new Size(0x100, 0x100);
            PaletteIndexes = new List<int>();
            for (var i = 0; i < 0x10; i += 4)
            {
                PaletteIndexes.Add(i);
            }

            for (var i = 0x10; i < 0x100; i += 0x10)
            {
                PaletteIndexes.Add(i);
            }

            StartTileIndexes = new List<int>();
            for (var i = 0; i < 0xA00; i += 0x100)
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
        public int PaletteIndex
        {
            get
            {
                return _paletteIndex;
            }

            set
            {
                if (PaletteIndex == value)
                {
                    return;
                }

                if (value < 0 || value > 0xF0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _paletteIndex = value;
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

        private List<int> PaletteIndexes
        {
            get;
        }

        private int PaletteIndexIndex
        {
            get;
            set;
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
            if (Palette is null || PixelData is null)
            {
                throw new InvalidOperationException();
            }

            var result = new Color32BppArgb[0x80 * 0x80];
            var palette = new Color32BppArgb[0x10];
            Array.Copy(
                sourceArray: Palette,
                sourceIndex: PaletteIndex,
                destinationArray: palette,
                destinationIndex: 0,
                length: Math.Min(0x10, Palette.Length - PaletteIndex));

            Parallel.For(0, 0x10, RenderRow);

            void RenderRow(int row)
            {
                var pixelRow = row * 8 * 0x80;
                var pixelIndex = (StartTile + (row * 0x10)) * 0x40;
                for (var column = 0; column < 0x10; column++)
                {
                    var columnIndex = pixelRow + (column * 8);
                    for (var y = 0; y < 8; y++)
                    {
                        var index = columnIndex + (y * 0x80);
                        for (var x = 0; x < 8; x++)
                        {
                            var pixel = PixelData[pixelIndex++];
                            result[index++] = palette[pixel];
                        }
                    }
                }
            }

            return Upscale.Simple(result, 0x80, 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Palette != null && PixelData != null)
            {
                DrawGfx(e.Graphics);
            }

            base.OnPaint(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            PaletteIndexIndex += Math.Sign(e.Delta);
            PaletteIndexIndex %= PaletteIndexes.Count;
            if (PaletteIndexIndex < 0)
            {
                PaletteIndexIndex += PaletteIndexes.Count;
            }

            PaletteIndex = PaletteIndexes[PaletteIndexIndex];

            base.OnMouseWheel(e);
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

        private unsafe void DrawGfx(Graphics graphics)
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
