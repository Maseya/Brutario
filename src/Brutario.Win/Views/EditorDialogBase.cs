// <copyright file="EditorDialogBase.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Win.Views;

using System;
using System.ComponentModel;
using System.Windows.Forms;

public partial class EditorDialogBase : Component
{
    private IWin32Window? _owner;

    public EditorDialogBase()
    {
        InitializeComponent();
    }

    public EditorDialogBase(IContainer container)
    {
        container.Add(this);

        InitializeComponent();
    }

    public event EventHandler? OwnerChanged;

    public IWin32Window? Owner
    {
        get
        {
            return _owner;
        }

        set
        {
            if (Owner == value)
            {
                return;
            }

            _owner = value;
            OnOwnerChanged(EventArgs.Empty);
        }
    }

    protected virtual void OnOwnerChanged(EventArgs e)
    {
        OwnerChanged?.Invoke(this, e);
    }
}
