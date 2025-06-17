﻿// <copyright file="UndoEventArgs.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Core;

using System;

public class UndoEventArgs : EventArgs
{
    public UndoEventArgs(UndoElement undoElement)
    {
        UndoElement = undoElement;
    }

    public UndoEventArgs(Action undo, Action redo)
        : this(new UndoElement(undo, redo))
    {
    }

    public UndoElement UndoElement { get; }
}
