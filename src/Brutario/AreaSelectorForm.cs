using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brutario
{
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
