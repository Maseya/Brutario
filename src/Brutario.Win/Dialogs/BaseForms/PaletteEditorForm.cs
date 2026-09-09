// <copyright file="PaletteEditorForm.cs" organization="Maseya">
//     Copyright (c) 2026 spel werdz rite. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Win.Dialogs.BaseForms;
using System;
using System.ComponentModel;
using System.Windows.Forms;

using Brutario.Core;
using Brutario.Core.Views;

using Maseya.Smas.Smb1;
using Maseya.Snes;

using static Math;

public partial class PaletteEditorForm : Form, IPaletteEditorView
{
    private Point _selectedPoint;
    private Size _view;
    private Color32BppArgb _selectedColor;

    public PaletteEditorForm()
    {
        InitializeComponent();

        cbxForeground.SelectedIndex =
        cbxBackground.SelectedIndex =
        cbxSprites.SelectedIndex = 0;
    }

    public PaletteEditorForm(IContainer container) : this()
    {
        container.Add(this);
    }

    public bool SaveEnabled
    {
        get
        {
            return tsmSave.Enabled;
        }

        set
        {
            tsmSave.Enabled =
            tsbSave.Enabled = value;
        }
    }

    public bool UndoEnabled
    {
        get
        {
            return tsmUndo.Enabled;
        }

        set
        {
            tsmUndo.Enabled =
            tsbUndo.Enabled = value;
        }
    }

    public bool RedoEnabled
    {
        get
        {
            return tsmRedo.Enabled;
        }

        set
        {
            tsmRedo.Enabled =
            tsbRedo.Enabled = value;
        }
    }

    public ForegroundPalette ForegroundPalette
    {
        get
        {
            return (ForegroundPalette)cbxForeground.SelectedIndex;
        }

        set
        {
            if (!Enum.IsDefined(value))
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
            if (!Enum.IsDefined(value))
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
            if (!Enum.IsDefined(value))
            {
                throw new InvalidEnumArgumentException(
                    nameof(SpritePalette), (int)value, typeof(SpritePalette));
            }

            cbxSprites.SelectedIndex = (int)value;
        }
    }

    public Size View
    {
        get
        {
            return _view;
        }

        set
        {
            if (View == value)
            {
                return;
            }

            _view = value;
            OnViewSizeChanged(EventArgs.Empty);
        }
    }

    public Point SelectedPoint
    {
        get
        {
            return _selectedPoint;
        }

        set
        {
            if (SelectedPoint == value)
            {
                return;
            }

            _selectedPoint = value;
            OnSelectedPointChanged(EventArgs.Empty);
        }
    }

    public int SelectedX
    {
        get { return SelectedPoint.X; }
        set { SelectedPoint = new Point(value, SelectedY); }
    }

    public int SelectedY
    {
        get { return SelectedPoint.Y; }
        set { SelectedPoint = new Point(SelectedX, value); }
    }

    public int SelectedIndex
    {
        get { return (SelectedY * View.Width) + SelectedX; }
        set { SelectedPoint = new Point(value % View.Width, value / View.Width); }
    }

    public Color32BppArgb SelectedColor
    {
        get
        {
            return _selectedColor;
        }

        set
        {
            if (SelectedColor == value)
            {
                return;
            }

            _selectedColor = value;
            OnSelectedColorChanged(EventArgs.Empty);
        }
    }

    private Size Zoom
    {
        get
        {
            return new Size(
                paletteControl.ClientSize.Width / View.Width,
                paletteControl.ClientSize.Height / View.Height);
        }
    }

    public event EventHandler? ForegroundPaletteChanged;
    public event EventHandler? BackgroundPaletteChanged;
    public event EventHandler? SpritePaletteChanged;

    public event EventHandler? SaveClicked;
    public event EventHandler? UndoClicked;
    public event EventHandler? RedoClicked;

    public event EventHandler? ResetClicked;

    public event EventHandler? ViewSizeChanged;
    public event EventHandler? SelectedPointChanged;
    public event EventHandler? SelectedColorChanged;
    public event EventHandler? SelectedColorEdited;
    public event EventHandler<PaletteDrawEventArgs>? DrawPalette;

    public event EventHandler<PathEventArgs>? ImportPalette;
    public event EventHandler<PathEventArgs>? ExportPalette;

    public void Redraw()
    {
        paletteControl.Invalidate();
    }

    protected virtual void OnExportPalette(PathEventArgs e)
    {
        ExportPalette?.Invoke(this, e);
    }

    protected virtual void OnForegroundPaletteChanged(EventArgs e)
    {
        ForegroundPaletteChanged?.Invoke(this, e);
    }

    protected virtual void OnBackgroundPaletteChanged(EventArgs e)
    {
        BackgroundPaletteChanged?.Invoke(this, e);
    }

    protected virtual void OnSpritePaletteChanged(EventArgs e)
    {
        SpritePaletteChanged?.Invoke(this, e);
    }

    protected virtual void OnSaveClicked(EventArgs e)
    {
        try
        {
            SaveClicked?.Invoke(this, e);
        }
        catch (Exception ex)
        {
            exceptionView.Show(ex);
        }
    }

    protected virtual void OnUndoClicked(EventArgs e)
    {
        UndoClicked?.Invoke(this, e);
    }

    protected virtual void OnRedoClicked(EventArgs e)
    {
        RedoClicked?.Invoke(this, e);
    }

    protected virtual void OnReset(EventArgs e)
    {
        ResetClicked?.Invoke(this, e);
    }

    protected virtual void OnViewSizeChanged(EventArgs e)
    {
        ViewSizeChanged?.Invoke(this, e);
    }

    protected virtual void OnSelectedPointChanged(EventArgs e)
    {
        UpdateStatusText();
        SelectedPointChanged?.Invoke(this, e);
    }

    protected virtual void OnSelectedColorChanged(EventArgs e)
    {
        UpdateStatusText();
        SelectedColorChanged?.Invoke(this, e);
    }

    protected virtual void OnSelectedColorEdited(EventArgs e)
    {
        SelectedColorEdited?.Invoke(this, e);
    }

    protected virtual void OnDrawPalette(PaletteDrawEventArgs e)
    {
        DrawPalette?.Invoke(this, e);
    }

    private void UpdateStatusText()
    {
        var coord = $"{SelectedX:X},{SelectedY:X}";
        var snesColor = Color32BppArgb.ToSnesColor(_selectedColor);
        tsslColor.Text = $"{coord}:{SelectedColor.Value & 0xFFFFFF:X6}({snesColor:X4})";
        tsslRed.Text = SelectedColor.R.ToString();
        tssGreen.Text = SelectedColor.G.ToString();
        tsslBlue.Text = SelectedColor.B.ToString();
    }

    private void ImportPalette_Click(object sender, EventArgs e)
    {
        if (importPaletteFileDialog.ShowDialog() == DialogResult.OK)
        {
            ImportPalette?.Invoke(this, new PathEventArgs(importPaletteFileDialog.FileName));
        }
    }

    private void ExportPalette_Click(object sender, EventArgs e)
    {
        if (exportPaletteFileDialog.ShowDialog() == DialogResult.OK)
        {
            ExportPalette?.Invoke(this, new PathEventArgs(exportPaletteFileDialog.FileName));
        }
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

    private void PaletteControl_MouseMove(object sender, MouseEventArgs e)
    {
        var x = Clamp(e.X / Zoom.Width, 0, View.Width);
        var y = Clamp(e.Y / Zoom.Height, 0, View.Height);

        SelectedPoint = new Point(x, y);
    }

    private void PaletteControl_MouseClick(object sender, MouseEventArgs e)
    {
        colorDialog.Color = SelectedColor;
        if (colorDialog.ShowDialog() == DialogResult.OK)
        {
            SelectedColor = (Color32BppArgb)Color.FromArgb(
                SelectedColor.A,
                colorDialog.Color);
            OnSelectedColorEdited(EventArgs.Empty);
        }
    }

    private void PaletteControl_Paint(object sender, PaintEventArgs e)
    {
        var target = new PaletteRenderTarget(e.Graphics, Zoom, View, -1);
        OnDrawPalette(new PaletteDrawEventArgs(target));
    }

    private void Reset_Click(object sender, EventArgs e)
    {
        var result = MessageBox.Show(
            owner: this,
            text: "This will reload all palette data back to what was stored in ROM. This cannot be undone. Proceed?",
            caption: "Reset palette data.",
            buttons: MessageBoxButtons.YesNo,
            icon: MessageBoxIcon.Information);
        if (result == DialogResult.Yes)
        {
            OnReset(EventArgs.Empty);
        }
    }

    private void Undo_Click(object sender, EventArgs e)
    {
        OnUndoClicked(EventArgs.Empty);
    }

    private void Redo_Click(object sender, EventArgs e)
    {
        OnRedoClicked(EventArgs.Empty);
    }

    private void Save_Click(object sender, EventArgs e)
    {
        OnSaveClicked(EventArgs.Empty);
    }

    private void Close_Click(object sender, EventArgs e)
    {
        Hide();
    }

    private void AnimationTimer_Tick(object sender, EventArgs e)
    {

    }

    private void Copy_Click(object sender, EventArgs e)
    {

    }

    private void Paste_Click(object sender, EventArgs e)
    {

    }
}
