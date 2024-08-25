﻿namespace Brutario.Core;

using System;
using System.Collections.Generic;
using System.Drawing;

using Maseya.Snes;

public readonly ref struct DrawData
{
    public DrawData(
        Color32BppArgb bgColor,
        ReadOnlySpan<Color32BppArgb> palette,
        ReadOnlySpan<byte> pixelData,
        ReadOnlySpan<ObjTile> bg1,
        IEnumerable<Sprite> sprites,
        int startX,
        Size size,
        IEnumerable<Rectangle> rectangles,
        int selectedIndex,
        Color separatorColor,
        Color passiveColor,
        Color selectColor)
    {
        BgColor = bgColor;
        Palette = palette;
        PixelData = pixelData;
        Bg1 = bg1;
        Sprites = sprites;
        StartX = startX;
        Size = size;
        Rectangles = rectangles;
        SelectedIndex = selectedIndex;
        SeparatorColor = separatorColor;
        PassiveColor = passiveColor;
        SelectColor = selectColor;
    }

    public Color32BppArgb BgColor { get; }

    public ReadOnlySpan<Color32BppArgb> Palette { get; }

    public ReadOnlySpan<byte> PixelData { get; }

    public ReadOnlySpan<ObjTile> Bg1 { get; }

    public IEnumerable<Sprite> Sprites { get; }

    public int StartX { get; }

    public Size Size { get; }

    public IEnumerable<Rectangle> Rectangles { get; }

    public int SelectedIndex { get; }

    public Color SeparatorColor { get; }

    public Color PassiveColor { get; }

    public Color SelectColor { get; }
}
