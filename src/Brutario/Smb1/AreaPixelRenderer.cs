namespace Brutario.Smb1
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Threading.Tasks;

    public static class AreaPixelRenderer
    {
        private delegate Color32BppArgb PixelTransform(
            Color32BppArgb top,
            Color32BppArgb bottom);

        public static unsafe void DrawArea(
            Graphics graphics,
            Color32BppArgb bgColor,
            Color32BppArgb[] palette,
            byte[] pixelData,
            ObjTile[] bg1,
            IEnumerable<Sprite> sprites,
            int startX,
            Size size,
            IList<Rectangle> rectangles,
            int selectedIndex,
            Color separatorColor,
            Color passiveColor,
            Color selectColor)
        {
            using var image = GetPixels(
                bgColor,
                palette,
                pixelData,
                bg1,
                sprites,
                startX,
                size);

            using var imageGraphics = Graphics.FromImage(image);
            DrawPageSeparators(imageGraphics, startX, image.Size, separatorColor);
            DrawItemRectangle(
                imageGraphics,
                startX,
                rectangles,
                selectedIndex,
                selectColor,
                passiveColor);

            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.DrawImage(image, 0, 0, image.Width, image.Height);
        }

        public static void DrawItemRectangle(
            Graphics graphics,
            int startX,
            IList<Rectangle> rectangles,
            int selectedIndex,
            Color passiveColor,
            Color selectColor)
        {
            if (rectangles is null)
            {
                throw new ArgumentNullException(nameof(rectangles));
            }

            using var selectBrush = new SolidBrush(Color.FromArgb(0x7F, selectColor));
            using var passiveBrush = new SolidBrush(Color.FromArgb(0x7F, passiveColor));
            for (var i = 0; i < rectangles.Count; i++)
            {
                var rectangle = rectangles[i];
                rectangle.X -= startX * 8;
                graphics.FillRectangle(
                    i == selectedIndex ? selectBrush : passiveBrush,
                    rectangle);
            }
        }

        public static void DrawPageSeparators(
            Graphics graphics,
            int startX,
            Size size,
            Color color)
        {
            if (graphics is null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            var offset = (startX * 8) & 0xFF;
            using var pen = new Pen(color);
            for (var pageX = 0xFF - offset; pageX < size.Width; pageX += 0x100)
            {
                graphics.DrawLine(pen, pageX, 0, pageX, size.Height);
            }
        }

        public static Bitmap GetPixels(
            Color32BppArgb bgColor,
            Color32BppArgb[] palette,
            byte[] pixelData,
            ObjTile[] bg1,
            IEnumerable<Sprite> sprites,
            int startX,
            Size size)
        {
            var viewWidth = ((size.Width - 1) / 8) + 1;
            var imageWidth = viewWidth * 8;
            var pixels = RenderPixels(
                bgColor,
                palette,
                pixelData,
                bg1,
                sprites,
                startX,
                size);

            unsafe
            {
                fixed (Color32BppArgb* scan0 = pixels)
                {
                    return new Bitmap(
                        imageWidth,
                        size.Height,
                        imageWidth * 4,
                        PixelFormat.Format32bppArgb,
                        (IntPtr)scan0);
                }
            }
        }

        public static Color32BppArgb[] RenderPixels(
            Color32BppArgb bgColor,
            Color32BppArgb[] palette,
            byte[] pixelData,
            ObjTile[] bg1,
            IEnumerable<Sprite> sprites,
            int startX,
            Size size)
        {
            if (palette is null)
            {
                throw new ArgumentNullException(nameof(palette));
            }

            if (pixelData is null)
            {
                throw new ArgumentNullException(nameof(pixelData));
            }

            if (bg1 is null)
            {
                throw new ArgumentNullException(nameof(bg1));
            }

            if (sprites is null)
            {
                throw new ArgumentNullException(nameof(sprites));
            }

            var viewWidth = ((size.Width - 1) / 8) + 1;
            var imageWidth = viewWidth * 8;

            var result = new Color32BppArgb[imageWidth * size.Height];

            sprites = sprites.Where(IsInFrame);

            // Set the BG color to the area.
            _ = Parallel.For(
                fromInclusive: 0,
                toExclusive: result.Length,
                body: i => result[i] = bgColor);

            _ = Parallel.ForEach(
                source: sprites.Where(
                    sprite => HasLayer(sprite, LayerPriority.Priority0)),
                body: sprite => RenderSprite(sprite));
            _ = Parallel.For(
                fromInclusive: 0,
                toExclusive: size.Height / 8,
                body: row => RenderRow(row, LayerPriority.Priority0));

            _ = Parallel.ForEach(
                source: sprites.Where(
                    sprite => HasLayer(sprite, LayerPriority.Priority2)),
                body: sprite => RenderSprite(sprite));
            _ = Parallel.For(
                fromInclusive: 0,
                toExclusive: size.Height / 8,
                body: row => RenderRow(row, LayerPriority.Priority1));

            _ = Parallel.ForEach(
                source: sprites.Where(
                    sprite => HasLayer(sprite, LayerPriority.Priority3)),
                body: sprite => RenderSprite(sprite));

            _ = Parallel.ForEach(
                source: sprites.Where(sprite => HasLayer(sprite, (LayerPriority)4)),
                body: sprite => RenderSprite(sprite));

            return result;

            void RenderRow(int row, LayerPriority layerPriority)
            {
                var darken = row > 0x1D;
                var rowIndex = Math.Min(row, 0x1C + (row & 1)) * 0x400;
                var pixelRow = row * 8 * imageWidth;
                for (var column = 0; column < viewWidth; column++, pixelRow += 8)
                {
                    var tile8 = bg1[rowIndex + column + startX];
                    if (tile8.Priority != layerPriority)
                    {
                        continue;
                    }

                    var xFlip = tile8.XFlipped ? 7 : 0;
                    var yFlip = tile8.YFlipped ? 7 : 0;
                    var paletteIndex = tile8.PaletteIndex * 0x10;
                    var pixelIndex = tile8.TileIndex * 0x40;

                    for (var y = 0; y < 8; y++, pixelIndex += 8)
                    {
                        var index = pixelRow + ((y ^ yFlip) * imageWidth);
                        for (var x = 0; x < 8; x++, index++)
                        {
                            var pixel = pixelData[
                                pixelIndex + (x ^ xFlip)];

                            if (pixel != 0)
                            {
                                var color = palette[paletteIndex + pixel];
                                if (darken)
                                {
                                    color.R /= 2;
                                    color.G /= 2;
                                    color.B /= 2;
                                }
                                result[index] = color;
                            }
                        }
                    }
                }
            }

            bool IsInFrame(Sprite sprite)
            {
                var spriteX = sprite.X - (startX * 8);
                return spriteX >= -8
                    && sprite.Y >= 0
                    && spriteX + 8 <= imageWidth
                    && sprite.Y + 8 < size.Height;
            }

            bool HasLayer(Sprite sprite, LayerPriority layerPriority)
            {
                return (sprite.Tile.Priority - 2) / 3 == (int)layerPriority;
            }

            void RenderSprite(Sprite sprite)
            {
                var spriteX = sprite.X - (startX * 8);
                var firstX = spriteX >= 0 ? 0 : -spriteX;

                var tile8 = sprite.Tile;

                var xFlip = tile8.FlipX ? 7 : 0;
                var yFlip = tile8.FlipY ? 7 : 0;
                var paletteIndex = tile8.PaletteIndex * 0x10;
                var pixelIndex = tile8.TileIndex * 0x40;
                var func = TransformPixel(sprite.TileProperties);
                var pixelRow = (sprite.Y * imageWidth) + sprite.X - (startX * 8);

                for (var y = 0; y < 8; y++, pixelIndex += 8)
                {
                    var index = pixelRow + ((y ^ yFlip) * imageWidth);
                    for (var x = firstX; x < 8; x++, index++)
                    {
                        var pixel = pixelData[pixelIndex + (x ^ xFlip)];

                        if (pixel != 0)
                        {
                            result[index] = func(
                                palette[paletteIndex + pixel],
                                result[index]);
                        }
                    }
                }
            }
        }

        private static PixelTransform TransformPixel(TileProperties tileProperties)
        {
            return tileProperties switch
            {
                TileProperties.Invert => (pixel, bottom) =>
                {
                    pixel.R ^= 0xFF;
                    pixel.G ^= 0xFF;
                    pixel.B ^= 0xFF;
                    return pixel;
                }
                ,
                TileProperties.Red => (pixel, bottom) =>
                {
                    pixel.R = 0xFF;
                    return pixel;
                }
                ,
                TileProperties.Green => (pixel, bottom) =>
                {
                    pixel.G = 0xFF;
                    return pixel;
                }
                ,
                TileProperties.Blue => (pixel, bottom) =>
                {
                    pixel.B = 0xFF;
                    return pixel;
                }
                ,
                TileProperties.Yellow => (pixel, bottom) =>
                {
                    pixel.R = 0xFF;
                    pixel.G = 0xFF;
                    return pixel;
                }

                ,
                TileProperties.Magenta => (pixel, bottom) =>
                {
                    pixel.R = 0xFF;
                    pixel.B = 0xFF;
                    return pixel;
                }
                ,
                TileProperties.Cyan => (pixel, bottom) =>
                {
                    pixel.G = 0xFF;
                    pixel.B = 0xFF;
                    return pixel;
                }
                ,
                TileProperties.Transparent => (pixel, bottom) =>
                {
                    pixel.R = (byte)((pixel.R + (1 * bottom.R)) >> 1);
                    pixel.G = (byte)((pixel.G + (1 * bottom.G)) >> 1);
                    pixel.B = (byte)((pixel.B + (1 * bottom.B)) >> 1);
                    return pixel;
                }
                ,
                _ => (pixel, bottom) => pixel,
            };
        }
    }
}
