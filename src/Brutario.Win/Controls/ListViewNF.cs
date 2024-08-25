// <copyright file="ListViewNF.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Win.Controls;

using System.Windows.Forms;

public class ListViewNF : ListView
{
    public ListViewNF()
    {
        DoubleBuffered = true;
    }
}
