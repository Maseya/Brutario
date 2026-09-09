namespace Brutario.Core.Presenters;
using System;
using System.Drawing;

using Brutario.Core.Models;
using Brutario.Core.Views;

using Maseya.Smas.Smb1;

public class PaletteEditorPresenter
{
    public PaletteEditorPresenter(
        PaletteEditorModel paletteEditorModel,
        IPaletteEditorView paletteEditorView)
    {
        PaletteEditorModel = paletteEditorModel;
        PaletteEditorModel.Saved += PaletteEditorModel_Saved;
        PaletteEditorModel.PaletteChanged += PaletteEditorModel_PaletteChanged;
        PaletteEditorModel.ForegroundPaletteChanged += PaletteEditorModel_ForegroundPaletteChanged;
        PaletteEditorModel.BackgroundPaletteChanged += PaletteEditorModel_BackgroundPaletteChanged;
        PaletteEditorModel.SpritePaletteChanged += PaletteEditorModel_SpritePaletteChanged;
        PaletteEditorModel.HasUnsavedChangesChanged += PaletteEditorModel_HasUnsavedChangesChanged;
        PaletteEditorModel.UndoElementAdded += PaletteEditorModel_UndoElementAdded;
        PaletteEditorModel.UndoComplete += PaletteEditorModel_UndoComplete;
        PaletteEditorModel.RedoComplete += PaletteEditorModel_RedoComplete;
        PaletteEditorModel.HistoryCleared += PaletteEditorModel_HistoryCleared;

        PaletteEditorView = paletteEditorView;
        PaletteEditorView.View = new Size(
            width: PaletteData.ColorsPerRow,
            height: PaletteData.RowsPerPalette);
        PaletteEditorView.ForegroundPaletteChanged +=
            PaletteEditorView_ForegroundPaletteChanged;
        PaletteEditorView.BackgroundPaletteChanged +=
            PaletteEditorView_BackgroundPaletteChanged;
        PaletteEditorView.SpritePaletteChanged +=
            PaletteEditorView_SpritePaletteChanged;
        PaletteEditorView.SaveClicked += PaletteEditorView_SaveClicked;
        PaletteEditorView.UndoClicked += PaletteEditorView_UndoClicked;
        PaletteEditorView.RedoClicked += PaletteEditorView_RedoClicked;
        PaletteEditorView.ResetClicked += PaletteEditorView_ResetClicked;
        PaletteEditorView.DrawPalette += PaletteEditorView_DrawPalette;
        PaletteEditorView.SelectedPointChanged +=
            PaletteEditorView_SelectedPointChanged;
        PaletteEditorView.SelectedColorEdited +=
            PaletteEditorView_SelectedColorEdited;
        PaletteEditorView.ImportPalette += PaletteEditorView_ImportPalette;
        PaletteEditorView.ExportPalette += PaletteEditorView_ExportPalette;
    }

    private PaletteEditorModel PaletteEditorModel { get; }

    private IPaletteEditorView PaletteEditorView { get; }

    private void PaletteEditorModel_ForegroundPaletteChanged(object? sender, EventArgs e)
    {
        PaletteEditorView.ForegroundPalette = PaletteEditorModel.ForegroundPalette;
    }

    private void PaletteEditorModel_BackgroundPaletteChanged(object? sender, EventArgs e)
    {
        PaletteEditorView.BackgroundPalette = PaletteEditorModel.BackgroundPalette;
    }

    private void PaletteEditorModel_SpritePaletteChanged(object? sender, EventArgs e)
    {
        PaletteEditorView.SpritePalette = PaletteEditorModel.SpritePalette;
    }

    private void PaletteEditorView_ForegroundPaletteChanged(object? sender, EventArgs e)
    {
        PaletteEditorModel.ForegroundPalette = PaletteEditorView.ForegroundPalette;
    }

    private void PaletteEditorView_BackgroundPaletteChanged(object? sender, EventArgs e)
    {
        PaletteEditorModel.BackgroundPalette = PaletteEditorView.BackgroundPalette;
    }

    private void PaletteEditorView_SpritePaletteChanged(object? sender, EventArgs e)
    {
        PaletteEditorModel.SpritePalette = PaletteEditorView.SpritePalette;
    }

    private void PaletteEditorModel_PaletteChanged(object? sender, EventArgs e)
    {
        PaletteEditorView.SaveEnabled = true;
        PaletteEditorView.Redraw();
    }

    private void PaletteEditorView_DrawPalette(object? sender, PaletteDrawEventArgs e)
    {
        e.PaletteRenderTarget.Draw(PaletteEditorModel.GetCurrentPalette());
    }

    private void PaletteEditorView_SelectedPointChanged(object? sender, EventArgs e)
    {
        PaletteEditorView.SelectedColor =
            PaletteEditorModel.GetCurrentPalette()[PaletteEditorView.SelectedIndex];
    }

    private void PaletteEditorView_SelectedColorEdited(object? sender, EventArgs e)
    {
        PaletteEditorModel.EditColor(
            PaletteEditorView.SelectedIndex,
            PaletteEditorView.SelectedColor);
    }

    private void PaletteEditorView_ImportPalette(object? sender, PathEventArgs e)
    {
        PaletteEditorModel.ImportPalette(e.Path);
    }

    private void PaletteEditorView_ExportPalette(object? sender, PathEventArgs e)
    {
        PaletteEditorModel.ExportPalette(e.Path);
    }

    private void PaletteEditorView_UndoClicked(object? sender, EventArgs e)
    {
        PaletteEditorModel.Undo();
    }

    private void PaletteEditorView_RedoClicked(object? sender, EventArgs e)
    {
        PaletteEditorModel.Redo();
    }

    private void PaletteEditorView_ResetClicked(object? sender, EventArgs e)
    {
        PaletteEditorModel.Reset();
    }

    private void PaletteEditorModel_HasUnsavedChangesChanged(object? sender, EventArgs e)
    {
        PaletteEditorView.SaveEnabled = PaletteEditorModel.HasUnsavedChanges;
    }

    private void SetUndoRedoEnabled()
    {
        PaletteEditorView.UndoEnabled = PaletteEditorModel.CanUndo;
        PaletteEditorView.RedoEnabled = PaletteEditorModel.CanRedo;
    }

    private void PaletteEditorModel_Saved(object? sender, EventArgs e)
    {
        PaletteEditorView.SaveEnabled = false;
    }

    private void PaletteEditorView_SaveClicked(object? sender, EventArgs e)
    {
        PaletteEditorModel.Save();
    }

    private void PaletteEditorModel_UndoElementAdded(object? sender, EventArgs e)
    {
        SetUndoRedoEnabled();
    }

    private void PaletteEditorModel_UndoComplete(object? sender, EventArgs e)
    {
        SetUndoRedoEnabled();
    }

    private void PaletteEditorModel_RedoComplete(object? sender, EventArgs e)
    {
        SetUndoRedoEnabled();
    }

    private void PaletteEditorModel_HistoryCleared(object? sender, EventArgs e)
    {
        SetUndoRedoEnabled();
    }
}
