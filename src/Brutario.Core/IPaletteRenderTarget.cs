namespace Brutario.Core;

using Maseya.Snes;

public interface IPaletteRenderTarget
{
    void Draw(ReadOnlySpan<Color32BppArgb> palette);
}
