namespace Brutario.Win.Dialogs.BaseForms;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

using Maseya.Smas.Smb1;

using System.Globalization;

public partial class SelectLayerPalettesForm : Form
{
    public SelectLayerPalettesForm()
    {
        InitializeComponent();
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

    public ReadOnlySpan<byte> ForegroundPaletteTable
    {
        get
        {
            _ = TryParseTable(tbxForeground.Text, out var result);
            return result;
        }

        set
        {
            if (value.Length != 5)
            {
                throw new ArgumentException();
            }

            tbxForeground.Text = String.Join(
                ' ',
                value.ToArray().Select(x => x.ToString("X2")));
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
            return (SpritePalette)cbxSprites.SelectedIndex;
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

    private static bool TryParseTable(string text, out byte[] result)
    {
        var tokens = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        result = new byte[tokens.Length];
        for (var i = 0; i < result.Length; i++)
        {
            if (tokens[i].Length != 2)
            {
                return false;
            }

            if (!Byte.TryParse(
                tokens[i],
                NumberStyles.AllowHexSpecifier,
                CultureInfo.InvariantCulture,
                out result[i]))
            {
                return false;
            }
        }
        
        return true;
    }
}
