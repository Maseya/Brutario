namespace Brutario.Win;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

using Brutario.Core;

using Maseya.Snes;

public static class PaletteRenderer
{
    public static void DrawPalette(Graphics graphics, in PaletteDrawData drawData)
    {
        DrawPalette(
            graphics,
            drawData.Palette,
            drawData.Size,
            drawData.Zoom,
            drawData.SelectedIndex);
    }

    public static void DrawPalette(
        Graphics graphics,
        ReadOnlySpan<Color32BppArgb> palette,
        Size view,
        Size zoom,
        int selectedIndex)
    {
        var imageWidth = view.Width * zoom.Width;
        var imageHeight = view.Height * zoom.Height;
        using var image = GetImage(palette, view);
        graphics.PixelOffsetMode = PixelOffsetMode.Half;
        graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
        graphics.DrawImage(image, 0, 0, imageWidth, imageHeight);
    }

    public static Bitmap GetImage(
        ReadOnlySpan<Color32BppArgb> palette,
        Size view)
    {
        var pixels = RenderPixels(palette, view);

        unsafe
        {
            fixed (Color32BppArgb* scan0 = pixels)
            {
                return new Bitmap(
                    view.Width,
                    view.Height,
                    view.Width * 4,
                    PixelFormat.Format32bppArgb,
                    (IntPtr)scan0);
            }
        }
    }

    public static Color32BppArgb[] RenderPixels(
        ReadOnlySpan<Color32BppArgb> palette,
        Size view)
    {
        var result = new Color32BppArgb[view.Width * view.Height];
        palette[..Math.Min(result.Length, palette.Length)].CopyTo(result);
        return result;
    }
}
