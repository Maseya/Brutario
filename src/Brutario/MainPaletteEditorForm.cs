// <copyright file="MainPaletteEditorForm.cs" company="Public Domain">
//     Copyright (c) 2020 Nelson Garcia. All rights reserved. Licensed under
//     GNU Affero General Public License. See LICENSE in project root for full
//     license information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Brutario.Smb1;
    using static System.ComponentModel.DesignerSerializationVisibility;

    public partial class MainPaletteEditorForm : Form
    {
        private PaletteData _paletteData;

        private int _areaIndex;

        public MainPaletteEditorForm()
        {
            InitializeComponent();
        }

        public event EventHandler PaletteDataChanged;

        public event EventHandler AreaIndexChanged;

        public string Status
        {
            get
            {
                return tssMain.Text;
            }

            set
            {
                tssMain.Text = value;
            }
        }

        public PaletteData PaletteData
        {
            get
            {
                return _paletteData;
            }

            set
            {
                if (PaletteData == value)
                {
                    return;
                }

                _paletteData = value;
                OnPaletteDataChanged(EventArgs.Empty);
            }
        }

        public int AreaIndex
        {
            get
            {
                return _areaIndex;
            }

            set
            {
                if (AreaIndex == value)
                {
                    return;
                }

                _areaIndex = value;
                OnAreaIndexChanged(EventArgs.Empty);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(Hidden)]
        public Color32BppArgb[] Palette
        {
            get
            {
                return paletteControl.Palette;
            }

            set
            {
                paletteControl.Palette = value;
            }
        }

        protected virtual void OnAreaIndexChanged(EventArgs e)
        {
            AreaIndexChanged?.Invoke(this, e);
        }

        protected virtual void OnPaletteDataChanged(EventArgs e)
        {
            PaletteDataChanged?.Invoke(this, e);
        }
    }
}
