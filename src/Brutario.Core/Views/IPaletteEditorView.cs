namespace Brutario.Core.Views;

using System.Drawing;

using Maseya.Smas.Smb1;
using Maseya.Snes;

public interface IPaletteEditorView
{
    ForegroundPalette ForegroundPalette { get; set; }
    BackgroundPalette BackgroundPalette { get; set; }
    SpritePalette SpritePalette { get; set; }

    bool SaveEnabled { get; set; }
    bool UndoEnabled { get; set; }
    bool RedoEnabled { get; set; }

    Size View { get; set; }

    Point SelectedPoint { get; set; }
    int SelectedX
    {
        get { return SelectedPoint.X; }
        set { SelectedPoint = new Point(value, SelectedY); }
    }
    int SelectedY
    {
        get { return SelectedPoint.Y; }
        set { SelectedPoint = new Point(SelectedX, value); }
    }
    int SelectedIndex
    {
        get { return (SelectedY * View.Width) + SelectedX; }
        set { SelectedPoint = new Point(value % View.Width, value / View.Width); }
    }

    Color32BppArgb SelectedColor { get; set; }

    event EventHandler<PathEventArgs>? ImportPalette;
    event EventHandler<PathEventArgs>? ExportPalette;

    event EventHandler? SaveClicked;
    event EventHandler? UndoClicked;
    event EventHandler? RedoClicked;

    event EventHandler? ResetClicked;

    event EventHandler? ForegroundPaletteChanged;
    event EventHandler? BackgroundPaletteChanged;
    event EventHandler? SpritePaletteChanged;

    event EventHandler? ViewSizeChanged;
    event EventHandler? SelectedPointChanged;
    event EventHandler? SelectedColorChanged;
    event EventHandler? SelectedColorEdited;

    event EventHandler<PaletteDrawEventArgs>? DrawPalette;

    void Redraw();
}
