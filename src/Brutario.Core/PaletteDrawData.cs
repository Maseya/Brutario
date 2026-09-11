namespace Brutario.Core;
using System;
using System.Drawing;

using Maseya.Snes;

public readonly ref struct PaletteDrawData
{
    public PaletteDrawData(
        ReadOnlySpan<Color32BppArgb> palette,
        Size size,
        Size zoom,
        int selectedIndex)
    {
        Palette = palette;
        Size = size;
        Zoom = zoom;
        SelectedIndex = selectedIndex;
    }

    public ReadOnlySpan<Color32BppArgb> Palette { get; }

    public Size Size { get; }

    public Size Zoom { get; }

    public int SelectedIndex { get; }
}
