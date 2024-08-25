// <copyright file="IObjectEditor.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Core;

using System;

using Maseya.Smas.Smb1.AreaData.ObjectData;

public interface IObjectEditorView
{
    event EventHandler? AreaPlatformTypeChanged;

    event EventHandler? AreaObjectCommandChanged;

    AreaPlatformType AreaPlatformType { get; set; }

    UIAreaObjectCommand AreaObjectCommand { get; set; }

    bool PromptConfirm();
}
