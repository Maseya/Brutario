// <copyright file="AreaSelectorForm.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed under
//     GNU Affero General Public License. See LICENSE in project root for full
//     license information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario
{
    using System.Windows.Forms;

    public partial class AreaSelectorForm : Form
    {
        public AreaSelectorForm()
        {
            InitializeComponent();
        }

        public Smb1.GameData Smb1RomData
        {
            get;
            set;
        }

        public int Area
        {
            get;
            set;
        }

        public int World
        {
            get;
            set;
        }

        public int Level
        {
            get;
            set;
        }
    }
}
