namespace Brutario.Win;
using System.Drawing;

using Brutario.Core;

using Maseya.Snes;

public class PaletteRenderTarget : IPaletteRenderTarget
{
    public PaletteRenderTarget(
        Graphics graphics,
        Size size,
        Size view,
        int selectedIndex)
    {
        Graphics = graphics;
        Size = size;
        View = view;
        SelectedIndex = selectedIndex;
    }

    private Graphics Graphics { get; }

    private Size Size { get; }

    private Size View { get; }

    private int SelectedIndex { get; }

    public void Draw(ReadOnlySpan<Color32BppArgb> palette)
    {
        PaletteRenderer.DrawPalette(
            Graphics,
            palette,
            View,
            Size,
            SelectedIndex);
    }
}
