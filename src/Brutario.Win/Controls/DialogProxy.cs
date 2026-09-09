// <copyright file="DialogProxy.cs" organization="Maseya">
//     Copyright (c) 2026 spel werdz rite. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Win.Controls;

using System.ComponentModel;
using System.Windows.Forms;

/// <summary>
/// Provides a simplified representation of forms that are intended to only be handled
/// as dialog windows. This is an abstract class.
/// </summary>
/// <remarks>
/// Many windows <see cref="Form"/> classes are intended to be used as dialog boxes.
/// These classes usually do not intend to make public the many properties and methods
/// that a form exposes. This class is therefore used to make public only the essential
/// parameters that an application developer intends to make visible. The base
/// <see cref="Form"/> is kept internal so inheritors can select which properties,
/// methods, and events should be visible.
/// <para/>
/// This class was inspired by the design of <see cref=" OpenFileDialog"/>, <see
/// cref="SaveFileDialog"/>, and <see cref=" FolderBrowserDialog"/>.
/// </remarks>
[ToolboxItem(true)]
[DesignTimeVisible(true)]
public abstract class DialogProxy : Component
{
    private bool _hideOnUserClose;
    private bool _hideEventAdded  = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="DialogProxy"/> class.
    /// </summary>
    protected DialogProxy()
    {
    }

    protected DialogProxy(IContainer container)
        : this()
    {
        container.Add(this);
    }

    /// <summary>
    /// Occurs when the user clicks the Help button in the dialog box.
    /// </summary>
    public event HelpEventHandler? HelpRequested;

    /// <summary>
    /// Gets or sets a value indicating whether the Help button is displayed in the
    /// dialog box.
    /// </summary>
    public bool ShowHelp
    {
        get
        {
            return BaseForm.HelpButton;
        }

        set
        {
            BaseForm.HelpButton = value;
        }
    }

    /// <summary>
    /// Gets or sets the dialog box title.
    /// </summary>
    public string Title
    {
        get
        {
            return BaseForm.Text;
        }

        set
        {
            BaseForm.Text = value;
        }
    }

    /// <summary>
    /// Gets or sets an object that contains data about the control.
    /// </summary>
    [Localizable(false)]
    [Bindable(true)]
    [DefaultValue(null)]
    [TypeConverter(typeof(StringConverter))]
    public object? Tag
    {
        get
        {
            return BaseForm?.Tag;
        }

        set
        {
            BaseForm.Tag = value;
        }
    }

    public Form? Owner
    {
        get
        {
            return BaseForm.Owner;
        }

        set
        {
            BaseForm.Owner = value;
        }
    }

    public bool Visible
    {
        get
        {
            return BaseForm.Visible;
        }

        set
        {
            BaseForm.Visible = value;
        }
    }

    /// <summary>
    /// Gets or sets a value that determines whether the base form will mark itself as
    /// hidden when closed by the user, rather than actually closing itself.
    /// </summary>
    public bool HideOnUserClose
    {
        get
        {
            return _hideOnUserClose;
        }
        set
        {
            // We cannot add the event on construction, as the base form will not yet
            // have been assigned.
            if (!_hideEventAdded)
            {
                BaseForm.FormClosing += BaseForm_FormClosing;
                _hideEventAdded = true;
            }

            _hideOnUserClose = value;
        }
    }

    /// <summary>
    /// Gets the <see cref="Form"/> to use for modal dialog operations.
    /// </summary>
    protected abstract Form BaseForm
    {
        get;
    }

    /// <summary>
    /// Runs a common dialog box with a specified owner or default if none is given.
    /// </summary>
    /// <param name="owner">
    /// An <see cref="IWin32Window"/> that represents the top-level windows that will
    /// own the modal dialog box, or <see langword="null"/> to specify the currently
    /// active window of your application.
    /// </param>
    /// <returns>
    /// The <see cref="DialogResult"/> that this dialog box returns when it closes.
    /// </returns>
    public DialogResult ShowDialog(IWin32Window? owner = null)
    {
        return BaseForm.ShowDialog(owner);
    }

    /// <summary>
    /// Shows the base window with a specified owner or default if none is given.
    /// </summary>
    /// <param name="owner">
    /// An <see cref="IWin32Window"/> that represents the top-level windows that will
    /// own the modal dialog box, or <see langword="null"/> to specify the currently
    /// active window of your application.
    /// </param>
    public void Show(IWin32Window? owner = null)
    {
        BaseForm.Show(owner);
    }

    /// <summary>
    /// Raises the <see cref="HelpRequested"/> event.
    /// </summary>
    /// <param name="e">
    /// A <see cref="HelpEventArgs"/> that contains the event data.
    /// </param>
    protected virtual void OnHelpRequested(HelpEventArgs e)
    {
        HelpRequested?.Invoke(this, e);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref=" DialogProxy"/> and
    /// optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">
    /// <see langword="true"/> to released both managed and unmanaged resources; <see
    /// langword="false"/> to release only unmanaged resources.
    /// </param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            BaseForm.Dispose();
        }

        base.Dispose(disposing);
    }

    private void BaseForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        switch (e.CloseReason)
        {
            case CloseReason.UserClosing:
                if (HideOnUserClose)
                {
                    e.Cancel = true;
                    BaseForm.Hide();
                }

                break;
        }
    }
}
