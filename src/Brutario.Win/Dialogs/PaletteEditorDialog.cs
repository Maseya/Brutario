namespace Brutario.Win.Dialogs;
using System;
using System.ComponentModel;
using System.Windows.Forms;

using Brutario.Win.Controls;
using Brutario.Win.Dialogs.BaseForms;

using Maseya.Smas.Smb1;

public class PaletteEditorDialog : DialogProxy
{
    public PaletteEditorDialog()
    {
        PaletteEditorForm = new PaletteEditorForm();
        PaletteEditorForm.ForegroundPaletteChanged +=
            (_, _) => ForegroundPaletteChanged?.Invoke(this, EventArgs.Empty);
        PaletteEditorForm.BackgroundPaletteChanged +=
            (_, _) => BackgroundPaletteChanged?.Invoke(this, EventArgs.Empty);
        PaletteEditorForm.SpritePaletteChanged +=
            (_, _) => SpritePaletteChanged?.Invoke(this, EventArgs.Empty);
    }

    public PaletteEditorDialog(IContainer container)
        : this()
    {
        container.Add(this);
    }

    public ForegroundPalette ForegroundPalette
    {
        get
        {
            return PaletteEditorForm.ForegroundPalette;
        }

        set
        {
            PaletteEditorForm.ForegroundPalette = value;
        }
    }

    public BackgroundPalette BackgroundPalette
    {
        get
        {
            return PaletteEditorForm.BackgroundPalette;
        }

        set
        {
            PaletteEditorForm.BackgroundPalette = value;
        }
    }

    public SpritePalette SpritePalette
    {
        get
        {
            return PaletteEditorForm.SpritePalette;
        }

        set
        {
            PaletteEditorForm.SpritePalette = value;
        }
    }

    public event EventHandler? ForegroundPaletteChanged;
    public event EventHandler? BackgroundPaletteChanged;
    public event EventHandler? SpritePaletteChanged;

    protected override Form BaseForm
    {
        get
        {
            return PaletteEditorForm;
        }
    }

    private PaletteEditorForm PaletteEditorForm
    {
        get;
    }
}
