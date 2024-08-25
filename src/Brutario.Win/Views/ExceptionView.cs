namespace Brutario.Win.Views;

using System;
using System.ComponentModel;

using Controls;

using Core;

public partial class ExceptionView : EditorDialogBase, IExceptionView
{
    private string? _title;

    public ExceptionView()
        : base()
    {
        InitializeComponent();
    }

    public ExceptionView(IContainer container)
        : base(container)
    {
        InitializeComponent();
    }

    public event EventHandler? TitleChanged;

    public string? Title
    {
        get
        {
            return _title;
        }

        set
        {
            if (Title == value)
            {
                return;
            }

            _title = value;
            OnTitleChanged(EventArgs.Empty);
        }
    }

    public void Show(Exception ex)
    {
        Show(ex.Message);
    }

    public void Show(string? message)
    {
        _ = RtlAwareMessageBox.Show(
            Owner,
            message,
            Title,
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
    }

    public bool ShowAndPromptRetry(Exception ex)
    {
        return ShowAndPromptRetry(ex.Message);
    }

    public bool ShowAndPromptRetry(string? message)
    {
        var dialogResult = RtlAwareMessageBox.Show(
            Owner,
            message,
            Title,
            MessageBoxButtons.RetryCancel,
            MessageBoxIcon.Warning);
        return dialogResult == DialogResult.Retry;
    }

    protected virtual void OnTitleChanged(EventArgs e)
    {
        TitleChanged?.Invoke(this, e);
    }
}
