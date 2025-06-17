﻿// <copyright file="ObjectEditor.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Win.Views;

using System;
using System.ComponentModel;
using System.Windows.Forms;

using Core;

using Maseya.Smas.Smb1.AreaData.ObjectData;

using static System.ComponentModel.DesignerSerializationVisibility;

public sealed partial class ObjectEditor : EditorDialogBase, IObjectEditorView
{
    public ObjectEditor()
        : base()
    {
        InitializeComponent();
    }

    public ObjectEditor(IContainer container)
        : base(container)
    {
        InitializeComponent();
    }

    public event EventHandler? AreaPlatformTypeChanged;

    public event EventHandler? AreaObjectCommandChanged;

    [Browsable(false)]
    [DesignerSerializationVisibility(Hidden)]
    public AreaPlatformType AreaPlatformType
    {
        get
        {
            return objectEditorDialog.AreaPlatformType;
        }

        set
        {
            objectEditorDialog.AreaPlatformType = value;
        }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(Hidden)]
    public UIAreaObjectCommand AreaObjectCommand
    {
        get
        {
            return objectEditorDialog.AreaObjectCommand;
        }

        set
        {
            objectEditorDialog.AreaObjectCommand = value;
        }
    }

    public bool PromptConfirm()
    {
        return objectEditorDialog.ShowDialog(Owner) == DialogResult.OK;
    }

    private void ObjectEditorDialog_AreaPlatformTypeChanged(
        object? sender,
        EventArgs e)
    {
        AreaPlatformTypeChanged?.Invoke(this, EventArgs.Empty);
    }

    private void ObjectEditorDialog_AreaObjectCommandChanged(
        object? sender,
        EventArgs e)
    {
        AreaObjectCommandChanged?.Invoke(this, EventArgs.Empty);
    }
}
