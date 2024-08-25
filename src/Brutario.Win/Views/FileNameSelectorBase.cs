namespace Brutario.Win.Views;

using System;
using System.ComponentModel;

using Core;

public abstract class FileNameSelectorBase : EditorDialogBase, IFileNameSelector
{
    public FileNameSelectorBase()
    {
    }

    public FileNameSelectorBase(IContainer container)
    {
        container.Add(this);
    }

    public event EventHandler? FileNameChanged;

    public string? FileName
    {
        get
        {
            return FileDialog.FileName;
        }

        set
        {
            if (FileName == value)
            {
                return;
            }

            FileDialog.FileName = value;
            OnFileNameChanged(EventArgs.Empty);
        }
    }

    protected abstract FileDialog FileDialog
    {
        get;
    }

    public PromptResult Prompt()
    {
        return FileDialog.ShowDialog() switch
        {
            DialogResult.OK => PromptResult.Yes,
            _ => PromptResult.No,
        };
    }

    protected virtual void OnFileNameChanged(EventArgs e)
    {
        FileNameChanged?.Invoke(this, e);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            FileDialog.Dispose();
        }

        base.Dispose(disposing);
    }
}
