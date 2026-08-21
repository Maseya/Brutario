namespace Brutario.Win.Views;
using System;
using System.ComponentModel;

using Brutario.Core.Views;
using Brutario.Win.Dialogs;
using Brutario.Win.Dialogs.BaseForms;

using Maseya.Smas.Smb1;

public partial class PaletteEditor : EditorDialogBase, IPaletteEditorView
{
    public PaletteEditor()
    {
        InitializeComponent();
    }

    public PaletteEditor(IContainer container)
        : base(container)
    {
        {
            InitializeComponent();
        }
    }

    public ForegroundPalette ForegroundPalette
    {
        get
        {
            return paletteEditorDialog.ForegroundPalette;
        }

        set
        {
            paletteEditorDialog.ForegroundPalette = value;
        }
    }

    public BackgroundPalette BackgroundPalette
    {
        get
        {
            return paletteEditorDialog.BackgroundPalette;
        }

        set
        {
            paletteEditorDialog.BackgroundPalette = value;
        }
    }

    public SpritePalette SpritePalette
    {
        get
        {
            return paletteEditorDialog.SpritePalette;
        }

        set
        {
            paletteEditorDialog.SpritePalette = value;
        }
    }

    public event EventHandler? ForegroundPaletteChanged;
    public event EventHandler? BackgroundPaletteChanged;
    public event EventHandler? SpritePaletteChanged;

    public bool Prompt()
    {
        return paletteEditorDialog.ShowDialog(Owner) == DialogResult.OK;
    }
}
