// <copyright file="IHeaderEditor.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Core;

using System;

using Maseya.Smas.Smb1.AreaData.HeaderData;

public interface IHeaderEditorView
{
    event EventHandler? AreaHeaderChanged;

    AreaHeader AreaHeader { get; set; }

    bool Prompt();
}
