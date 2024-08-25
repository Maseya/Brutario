// <copyright file="Program.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Win;

using System;
using System.Windows.Forms;

using Core;

internal static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        var editor = new BrutarioEditor();
        using var form = new MainForm(editor);

        // TODO(swr): This probably does not belong here. The Form.Load event is already
        // used internally, so there are no order guarantees. I could make a new event
        // called within Form.Load and hook to that here. Ideally, I don't think the UI
        // should be capturing command-line logic though. Perhaps a presenter `Ready`
        // event is the way to go. Another caveat is that I want Presenter.Open, not
        // editor.Open. Originally, Presenter was private in MainForm and is now public
        // only for this.
        if (args.Length == 1)
        {
            form.Load += (s, e) => form.Presenter.Open(args[0]);
        }

        // TODO(swr): In a similar note to the above comment, perhaps the presenter
        // should handle the run logic? Not really sure.
        Application.Run(form);
    }
}
