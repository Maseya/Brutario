namespace Brutario.Core;
using System;

public class PaletteDrawEventArgs : EventArgs
{
    public PaletteDrawEventArgs(IPaletteRenderTarget paletteRenderTarget)
    {
        PaletteRenderTarget = paletteRenderTarget;
    }

    public IPaletteRenderTarget PaletteRenderTarget { get; }
}
