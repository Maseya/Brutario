namespace Brutario
{
    using Smb1;
    using System;
    using System.Globalization;
    using System.Windows.Forms;

    public partial class ObjectEditorForm : Form
    {
        public ObjectEditorForm()
        {
            InitializeComponent();
        }

        public int XPos
        {
            get
            {
                return (int)nudX.Value;
            }

            set
            {
                nudX.Value = value;
            }
        }

        public int YPos
        {
            get
            {
                return (int)nudY.Value;
            }

            set
            {
                nudY.Value = value;
            }
        }

        public bool PageFlag
        {
            get
            {
                return chkPageFlag.Checked;
            }

            set
            {
                chkPageFlag.Checked = value;
            }
        }

        public int Length
        {
            get
            {
                return (int)nudLength.Value;
            }

            set
            {
                nudLength.Value = value;
            }
        }

        public bool UseManual
        {
            get
            {
                return chkBinary.Checked;
            }

            set
            {
                chkBinary.Checked = value;
            }
        }

        public AreaObjectCommand AreaObjectCommand
        {
            get
            {
                if (UseManual)
                {
                    _ = TryGetCommand(tbxBinary.Text, out var result);
                    return result;
                }
                else
                {
                    return default;
                }
            }

            set
            {
                tbxBinary.Text = value.Size == 3
                    ? $"{value.Value1:X2} {value.Value2:X2} {value.Value3:X2}"
                    : $"{value.Value1:X2} {value.Value2:X2}";
            }
        }

        private static bool TryGetCommand(string text, out AreaObjectCommand command)
        {
            command = default;
            var tokens = text.Split(' ');
            if (tokens.Length != 3 && tokens.Length != 2)
            {
                return false;
            }

            var bytes = new byte[3];
            for (var i = 0; i < tokens.Length; i++)
            {
                if (!Byte.TryParse(
                    tokens[i],
                    NumberStyles.HexNumber,
                    CultureInfo.CurrentUICulture,
                    out bytes[i]))
                {
                    return false;
                }
            }

            command = new AreaObjectCommand(bytes[0], bytes[1], bytes[2]);
            return true;
        }

        private void Binary_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = !chkBinary.Checked ||
                TryGetCommand(tbxBinary.Text, out var _);
        }
    }
}
