// <copyright file="PaletteRenderTarget.cs" organization="Maseya">
//     Copyright (c) 2026 spel werdz rite. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

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
