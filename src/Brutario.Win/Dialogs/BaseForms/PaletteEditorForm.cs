namespace Brutario.Win.Dialogs.BaseForms;
using System;
using System.ComponentModel;
using System.Windows.Forms;

using Maseya.Smas.Smb1;

public partial class PaletteEditorForm : Form
{
    public PaletteEditorForm()
    {
        InitializeComponent();

        cbxForeground.SelectedIndex =
        cbxBackground.SelectedIndex =
        cbxSprites.SelectedIndex = 0;
    }

    public ForegroundPalette ForegroundPalette
    {
        get
        {
            return (ForegroundPalette)cbxForeground.SelectedIndex;
        }

        set
        {
            if (!Enum.IsDefined(typeof(ForegroundPalette), value))
            {
                throw new InvalidEnumArgumentException(
                    nameof(ForegroundPalette), (int)value, typeof(ForegroundPalette));
            }

            cbxForeground.SelectedIndex = (int)value;
        }
    }

    public BackgroundPalette BackgroundPalette
    {
        get
        {
            return (BackgroundPalette)cbxBackground.SelectedIndex;
        }

        set
        {
            if (!Enum.IsDefined(typeof(BackgroundPalette), value))
            {
                throw new InvalidEnumArgumentException(
                    nameof(BackgroundPalette), (int)value, typeof(BackgroundPalette));
            }

            cbxBackground.SelectedIndex = (int)value;
        }
    }

    public SpritePalette SpritePalette
    {
        get
        {
            return (SpritePalette)cbxBackground.SelectedIndex;
        }

        set
        {
            if (!Enum.IsDefined(typeof(SpritePalette), value))
            {
                throw new InvalidEnumArgumentException(
                    nameof(SpritePalette), (int)value, typeof(SpritePalette));
            }

            cbxSprites.SelectedIndex = (int)value;
        }
    }

    public event EventHandler? ForegroundPaletteChanged;
    public event EventHandler? BackgroundPaletteChanged;
    public event EventHandler? SpritePaletteChanged;

    protected virtual void OnForegroundPaletteChanged(EventArgs e)
    {
        ForegroundPaletteChanged?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnBackgroundPaletteChanged(EventArgs e)
    {
        BackgroundPaletteChanged?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnSpritePaletteChanged(EventArgs e)
    {
        SpritePaletteChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Foreground_SelectedIndexChanged(object sender, EventArgs e)
    {
        OnForegroundPaletteChanged(EventArgs.Empty);
    }

    private void Background_SelectedIndexChanged(object sender, EventArgs e)
    {
        OnBackgroundPaletteChanged(EventArgs.Empty);
    }

    private void Sprites_SelectedIndexChanged(object sender, EventArgs e)
    {
        OnSpritePaletteChanged(EventArgs.Empty);
    }
}
