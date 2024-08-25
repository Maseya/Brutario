// <copyright file="SpecialThanksForm.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Win;

using System.Windows.Forms;

public partial class SpecialThanksForm : Form
{
    public SpecialThanksForm()
    {
        InitializeComponent();
    }

    private void SpecialThanksForm_Load(object sender, EventArgs e)
    {
        var dir = Path.GetDirectoryName(Application.ExecutablePath);
        var path = Path.Combine(dir!, "Credits.rtf");
        if (!File.Exists(path))
        {
            rtbCredits.Text = "Oops the Credits file is missing";
            return;
        }

        rtbCredits.LoadFile(path);
    }
}
