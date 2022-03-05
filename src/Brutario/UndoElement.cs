// <copyright file="UndoElement.cs" company="Public Domain">
//     Copyright (c) 2021 Nelson Garcia. All rights reserved. Licensed under
//     GNU Affero General Public License. See LICENSE in project root for full
//     license information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario
{
    using System;

    public class UndoElement
    {
        public UndoElement(Action undo, Action redo)
        {
            Undo = undo ?? throw new ArgumentNullException(nameof(undo));
            Redo = redo ?? throw new ArgumentNullException(nameof(redo));
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
}
