// <copyright file="UndoElement.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Core;

using System;

public class UndoElement
{
    public UndoElement(Action undo, Action redo)
    {
        Undo = undo;
        Redo = redo;
    }

    public Action Undo
    {
        get;
    }

    public Action Redo
    {
        get;
    }
}
