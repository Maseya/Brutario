using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using static System.ComponentModel.DesignerSerializationVisibility;

namespace Brutario
{
    public class PaletteControl : DesignControl
    {
        private Color32BppArgb[] _palette;

        public PaletteControl()
        {
            ClientSize = new Size(0x100, 0x100);
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

        public Color32BppArgb[] RenderPixels()
        {
            if (Palette is null)
            {
                throw new InvalidOperationException();
            }

            var result = new Color32BppArgb[0x100 * 0x100];
            for (var row = 0; row < 0x10; row++)
            {
                var pixelRow = row * 0x10 * 0x100;
                for (var column = 0; column < 0x10; column++)
                {
                    var columnIndex = pixelRow + (column * 0x10);
                    var color = Palette[(row * 0x10) + column];
                    for (var y = 0; y < 0x10; y++)
                    {
                        var index = columnIndex + (y * 0x100);
                        for (var x = 0; x < 0x10; x++)
                        {
                            result[index++] = color;
                        }
                    }
                }
            }

            return result;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Palette != null)
            {
                DrawPalette(e.Graphics);
            }

            base.OnPaint(e);
        }

        private unsafe void DrawPalette(Graphics graphics)
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
