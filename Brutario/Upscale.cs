using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brutario
{
    public static class Upscale
    {
        public static Color32BppArgb[] Simple(Color32BppArgb[] pixels, int width, int scale)
        {
            var scalePixels = InitializeUpscale(pixels, width, scale);
            var height = pixels.Length / width;
            var scaleWidth = width * scale;
            Parallel.For(0, height, RenderRow);

            void RenderRow(int row)
            {
                var index = row * width;
                var scaleIndex = index * scale * scale;
                for (var x = 0; x < width; x++)
                {
                    var pixel = pixels[index++];
                    for (var i = 0; i < scale; i++, scaleIndex++)
                    {
                        for (var j = 0; j < scale; j++)
                        {
                            scalePixels[scaleIndex + (j * scaleWidth)] = pixel;
                        }
                    }
                }
            }

            return scalePixels;
        }

        public static Color32BppArgb[] Epx(Color32BppArgb[] pixels, int width)
        {
            var result = Simple(pixels, width, 2);
            var height = pixels.Length / width;
            var scaleWidth = width * 2;

            // Top-left corner
            WritePixel(0, 0, false, CompareBD(0), false, false);

            // Top edge
            var x = 1;
            for (; x < width - 1; x++)
            {
                WritePixel(
                    x,
                    x << 1,
                    false,
                    CompareBD(x),
                    CompareDC(x),
                    false);
            }

            // Top-right corner
            WritePixel(
                x,
                x << 1,
                false,
                false,
                CompareDC(x),
                false);

            // Center
            var srcRow = 0;
            var destRow = 0;
            for (var y = 1; y < height - 1; y++)
            {
                // Left edge
                srcRow += width;
                destRow += width << 2;
                WritePixel(
                    srcRow,
                    destRow,
                    CompareAB(srcRow),
                    CompareBD(srcRow),
                    false,
                    false);

                // Center
                for (x = 1; x < width - 1; x++)
                {
                    var i = srcRow + x;
                    WritePixel(
                        i,
                        destRow + (x << 1),
                        CompareAB(i),
                        CompareBD(i),
                        CompareDC(i),
                        CompareCA(i));
                }

                // Right edge
                WritePixel(
                    srcRow + x,
                    destRow + (x << 1),
                    false,
                    false,
                    CompareDC(srcRow + x),
                    CompareDC(srcRow + x));
            }

            // Bottom-left corner
            WritePixel(
                srcRow,
                destRow,
                CompareCA(srcRow),
                false,
                false,
                false);

            // Bottom edge
            x = 1;
            for (; x < width - 1; x++)
            {
                var i = srcRow + x;
                WritePixel(
                    i,
                    destRow + (x << 1),
                    CompareCA(i),
                    CompareBD(i),
                    false,
                    false);
            }

            // Bottom-right corner
            WritePixel(
                srcRow + x,
                destRow + (x << 1),
                false,
                CompareBD(srcRow + x),
                false,
                false);

            return result;

            bool CompareAB(int index)
            {
                return pixels[index - width] == pixels[index + 1];
            }

            bool CompareBD(int index)
            {
                return pixels[index + 1] == pixels[index + width];
            }

            bool CompareDC(int index)
            {
                return pixels[index + width] == pixels[index - 1];
            }

            bool CompareCA(int index)
            {
                return pixels[index - 1] == pixels[index - width];
            }

            void WritePixel(
                int srcIndex,
                int destIndex,
                bool compareAB,
                bool compareBD,
                bool compareDC,
                bool compareCA)
            {
                if (compareCA && !compareDC && !compareAB)
                {
                    result[destIndex] = pixels[srcIndex - width];
                }

                if (compareAB && !compareCA && !compareBD)
                {
                    result[destIndex + 1] = pixels[srcIndex + 1];
                }

                if (compareDC && !compareBD && !compareCA)
                {
                    result[destIndex + scaleWidth] = pixels[srcIndex - 1];
                }

                if (compareBD && !compareAB && !compareDC)
                {
                    result[destIndex + scaleWidth + 1] =
                        pixels[srcIndex + width];
                }
            }
        }

        private static Color32BppArgb[] InitializeUpscale(
                    Color32BppArgb[] pixels,
            int width,
            int scale)
        {
            if (pixels is null)
            {
                throw new ArgumentNullException(nameof(pixels));
            }

            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }

            if (pixels.Length % width != 0)
            {
                throw new ArgumentException();
            }
            return new Color32BppArgb[pixels.Length * scale * scale];
        }

        private static int GetResult(
            Color32BppArgb a,
            Color32BppArgb b,
            Color32BppArgb c,
            Color32BppArgb d)
        {
            var x = 0;
            var y = 0;
            var r = 0;

            if (a == c)
            {
                x += 1;
            }
            else if (b == c)
            {
                y += 1;
            }

            if (a == d)
            {
                x += 1;
            }
            else if (b == d)
            {
                y += 1;
            }

            if (x <= 1)
            {
                r += 1;
            }

            if (y <= 1)
            {
                r -= 1;
            }

            return r;
        }
    }
}
