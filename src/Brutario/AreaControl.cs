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
    public class AreaControl : DesignControl
    {
        private Color32BppArgb[] _palette;
        private byte[] _pixelMap;
        private ObjTile[] _bg1;
        private ObjTile[] _bg2;
        private int _startX;

        [Browsable(false)]
        [DesignerSerializationVisibility(Hidden)]
        public int StartX
        {
            get
            {
                return _startX;
            }

            set
            {
                if (StartX == value)
                {
                    return;
                }

                _startX = value;
                Invalidate();
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
        public ObjTile[] BG1
        {
            get
            {
                return _bg1;
            }

            set
            {
                if (BG1 == value)
                {
                    return;
                }

                _bg1 = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(Hidden)]
        public ObjTile[] BG2
        {
            get
            {
                return _bg2;
            }

            set
            {
                if (BG2 == value)
                {
                    return;
                }

                _bg2 = value;
                Invalidate();
            }
        }

        public IList<Sprite> Sprites
        {
            get;
            set;
        }

        public Color32BppArgb[] RenderPixels()
        {
            if (Palette is null || PixelData is null || BG1 is null)
            {
                throw new InvalidOperationException();
            }

            var startTime = DateTime.Now;
            var viewWidth = ((ClientSize.Width - 1) / 8) + 1;
            var imageWidth = viewWidth * 8;

            var result = new Color32BppArgb[imageWidth * 0xF0];
            var bgColor = new Color32BppArgb(0xFF, 0, 0, 0);

            Parallel.For(0, result.Length, i => result[i] = bgColor);

            //Parallel.For(0, 0x1E, row => RenderBg2Row(row, LayerPriority.Priority0));
            Parallel.ForEach(Sprites, sprite => RenderSprite(sprite, LayerPriority.Priority0));
            Parallel.For(0, 0x1E, row => RenderRow(row, LayerPriority.Priority0));
            Parallel.ForEach(Sprites, sprite => RenderSprite(sprite, LayerPriority.Priority2));
            Parallel.For(0, 0x1E, row => RenderRow(row, LayerPriority.Priority1));
            Parallel.ForEach(Sprites, sprite => RenderSprite(sprite, LayerPriority.Priority3));

            var end = DateTime.Now;
            var elspaded = end - startTime;

            Parent.Text = $"{elspaded.Milliseconds} ms";

            return result;

            void RenderRow(int row, LayerPriority layerPriority)
            {
                var rowIndex = row * 0x400;
                var pixelRow = row * 8 * imageWidth;
                for (var column = 0; column < viewWidth; column++, pixelRow += 8)
                {
                    var tile8 = BG1[rowIndex + column + StartX];
                    if (tile8.Priority != layerPriority)
                    {
                        continue;
                    }

                    var xFlip = tile8.XFlipped ? 7 : 0;
                    var yFlip = tile8.YFlipped ? 7 : 0;
                    var paletteIndex = tile8.PaletteNumber * 0x10;
                    var pixelIndex = tile8.TileIndex * 0x40;

                    for (var y = 0; y < 8; y++, pixelIndex += 8)
                    {
                        var index = pixelRow + ((y ^ yFlip) * imageWidth);
                        for (var x = 0; x < 8; x++, index++)
                        {
                            var pixel = PixelData[
                                pixelIndex + (x ^ xFlip)];

                            if (pixel != 0)
                            {
                                result[index] = Palette[paletteIndex + pixel];
                            }
                        }
                    }
                }
            }

            void RenderBg2Row(int row, LayerPriority layerPriority)
            {
                var rowIndex = row * 0x200;
                var pixelRow = row * 8 * imageWidth;
                for (var column = 0; column < viewWidth; column++, pixelRow += 8)
                {
                    var tile8 = BG2[rowIndex + column + (StartX >> 1)];
                    if (tile8.Priority != layerPriority)
                    {
                        continue;
                    }

                    var xFlip = tile8.XFlipped ? 7 : 0;
                    var yFlip = tile8.YFlipped ? 7 : 0;
                    var paletteIndex = tile8.PaletteNumber * 0x10;
                    var pixelIndex = tile8.TileIndex * 0x40;

                    for (var y = 0; y < 8; y++, pixelIndex += 8)
                    {
                        var index = pixelRow + ((y ^ yFlip) * imageWidth);
                        if (column != 0 && (StartX & 1) != 0)
                        {
                            index -= 4;
                        }

                        for (var x = 0; x < 8; x++, index++)
                        {
                            if (column == 0 && x == 0 && (StartX & 1) != 0)
                            {
                                x += 4;
                            }

                            var pixel = PixelData[
                                pixelIndex + (x ^ xFlip)];

                            if (pixel != 0)
                            {
                                result[index] = Palette[paletteIndex + pixel];
                            }
                        }
                    }
                }
            }

            void RenderSprite(Sprite sprite, LayerPriority layerPriority)
            {
                var spriteX = sprite.X - (StartX * 8);
                var firstX = 0;
                if (spriteX < 0)
                {
                    firstX = -spriteX;
                    if (firstX >= 8)
                    {
                        return;
                    }
                }

                if (sprite.Y < 0)
                {
                    return;
                }

                if (spriteX + 8 > imageWidth)
                {
                    return;
                }

                if (sprite.Y + 8 >= 0xF0)
                {
                    return;
                }

                var tile8 = sprite.Tile;
                if (tile8.Priority != layerPriority)
                {
                    return;
                }

                var xFlip = tile8.XFlipMask;
                var yFlip = tile8.YFlipMask;
                var paletteIndex = (tile8.PaletteIndex * 0x10) + 0x80;
                var pixelIndex = (tile8.TileIndex + 0x300) * 0x40;
                var func = TransformPixel(sprite.TileProperties);
                var pixelRow = (sprite.Y * imageWidth) + sprite.X - (StartX * 8);

                for (var y = 0; y < 8; y++, pixelIndex += 8)
                {
                    var index = pixelRow + ((y ^ yFlip) * imageWidth);
                    for (var x = firstX; x < 8; x++, index++)
                    {
                        var pixel = PixelData[
                            pixelIndex + (x ^ xFlip)];

                        if (pixel != 0)
                        {
                            result[index] = func(
                                Palette[paletteIndex + pixel],
                                result[index]);
                        }
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Palette != null && PixelData != null && BG1 != null)
            {
                DrawMap16(e.Graphics);
            }

            base.OnPaint(e);
        }

        private Func<Color32BppArgb, Color32BppArgb, Color32BppArgb> TransformPixel(TileProperties tileProperties)
        {
            switch (tileProperties)
            {
                case TileProperties.Invert:
                    return (pixel, bottom) =>
                    {
                        pixel.R ^= 0xFF;
                        pixel.G ^= 0xFF;
                        pixel.B ^= 0xFF;
                        return pixel;
                    };
                case TileProperties.Red:
                    return (pixel, bottom) =>
                    {
                        pixel.R = 0xFF;
                        return pixel;
                    };
                case TileProperties.Green:
                    return (pixel, bottom) =>
                    {
                        pixel.G = 0xFF;
                        return pixel;
                    };
                case TileProperties.Blue:
                    return (pixel, bottom) =>
                    {
                        pixel.B = 0xFF;
                        return pixel;
                    };
                case TileProperties.Yellow:
                    return (pixel, bottom) =>
                    {
                        pixel.R = 0xFF;
                        pixel.G = 0xFF;
                        return pixel;
                    };
                case TileProperties.Magenta:
                    return (pixel, bottom) =>
                    {
                        pixel.R = 0xFF;
                        pixel.B = 0xFF;
                        return pixel;
                    };
                case TileProperties.Cyan:
                    return (pixel, bottom) =>
                    {
                        pixel.G = 0xFF;
                        pixel.B = 0xFF;
                        return pixel;
                    };
                case TileProperties.Transparent:
                    return (pixel, bottom) =>
                    {
                        pixel.R = (byte)((pixel.R + (1 * bottom.R)) >> 1);
                        pixel.G = (byte)((pixel.G + (1 * bottom.G)) >> 1);
                        pixel.B = (byte)((pixel.B + (1 * bottom.B)) >> 1);
                        return pixel;
                    };
                case TileProperties.None:
                default:
                    return (pixel, bottom) => pixel;
            }
        }

        private Obj16Tile OffsetTile16(Obj16Tile tile)
        {
            tile.TopLeft = OffsetTile8(tile.TopLeft);
            tile.TopRight = OffsetTile8(tile.TopRight);
            tile.BottomLeft = OffsetTile8(tile.BottomLeft);
            tile.BottomRight = OffsetTile8(tile.BottomRight);
            return tile;
        }

        private ObjTile OffsetTile8(ObjTile tile)
        {
            tile.TileIndex += 4;
            return tile;
        }

        private unsafe void DrawMap16(Graphics graphics)
        {
            var bitmapPixels = RenderPixels();
            var imageWidth = bitmapPixels.Length / 0xF0;
            var scale = 2;
            var scaleWidth = imageWidth * scale;
            fixed (Color32BppArgb* pixels = Upscale.Simple(bitmapPixels, imageWidth, scale))
            {
                using (var image = new Bitmap(
                    scaleWidth,
                    0xF0 * scale,
                    scaleWidth * 4,
                    PixelFormat.Format32bppArgb,
                    (IntPtr)pixels))
                {
                    graphics.DrawImageUnscaled(image, Point.Empty);
                }
            }
        }
    }
}
