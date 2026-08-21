namespace Brutario.Core.Views;

using Maseya.Smas.Smb1;

public interface IPaletteEditorView
{
    ForegroundPalette ForegroundPalette
    {
        get; set;
    }

    public BackgroundPalette BackgroundPalette
    {
        get; set;
    }

    public SpritePalette SpritePalette
    {
        get; set;
    }

    event EventHandler? ForegroundPaletteChanged;
    event EventHandler? BackgroundPaletteChanged;
    event EventHandler? SpritePaletteChanged;

    bool Prompt();
}
