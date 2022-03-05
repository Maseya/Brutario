namespace Brutario
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Forms;

    using Smb1;

    internal partial class ObjectEditorForm : Form, IObjectEditorView
    {
        public ObjectEditorForm()
        {
            InitializeComponent();

            Codes = AreaObjectCommand.ValidCodes;
            EnumIndexes = new Dictionary<AreaObjectCode, int>();
            for (var i = 0; i < Codes.Count; i++)
            {
                EnumIndexes.Add(Codes[i], i);
                _ = cbxAreaObjectCode.Items.Add(Codes[i].BaseName());
            }

            TerrainMode = TerrainMode.None;
            ForegroundScenery = ForegroundScenery.None;
            BackgroundScenery = BackgroundScenery.None;
        }

        public event EventHandler AreaObjectCommandChanged;

        public AreaObjectCommand AreaObjectCommand
        {
            get
            {
                return UseManualInput ? BinaryCommand : UICommand;
            }

            set
            {
                BinaryCommand = UICommand = value;
                if (IsValidInput)
                {
                    AreaObjectCommandChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool IsValidInput
        {
            get
            {
                return btnOK.Enabled;
            }

            set
            {
                btnOK.Enabled = value;
            }
        }

        private bool UseManualInput
        {
            get
            {
                return chkUseManualInput.Checked;
            }

            set
            {
                chkUseManualInput.Checked = value;
            }
        }

        /// <summary>
        /// Returns true when the UICommand is being updated by its set accessor.
        /// </summary>
        private bool SettingUICommand
        {
            get;
            set;
        }

        private int XPos
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

        private int YPos
        {
            get
            {
                var y = (int)AreaObjectCode >> 8;
                return (y < 0x0C || y == 0x0F) ? (int)nudY.Value : y;
            }

            set
            {
                if (value <= nudY.Maximum)
                {
                    nudY.Value = value;
                }
            }
        }

        private bool PageFlag
        {
            get
            {
                return chkPageFlag.Enabled && chkPageFlag.Checked;
            }

            set
            {
                chkPageFlag.Checked = value;
            }
        }

        private AreaObjectCode AreaObjectCode
        {
            get
            {
                return Codes[
                    cbxAreaObjectCode.SelectedIndex >= 0
                    ? cbxAreaObjectCode.SelectedIndex
                    : 0];
            }

            set
            {
                cbxAreaObjectCode.SelectedIndex = EnumIndexes.TryGetValue(
                    value,
                    out var index) ? index : -1;
            }
        }

        private int Length
        {
            get
            {
                return nudLength.Enabled ? (int)nudLength.Value : 1;
            }

            set
            {
                if (value <= nudLength.Maximum)
                {
                    nudLength.Value = value;
                }
            }
        }

        private bool TerrainModeEnabled
        {
            get
            {
                return cbxTerrainMode.Enabled && cbxTerrainMode.SelectedIndex >= 0;
            }
        }

        private TerrainMode TerrainMode
        {
            get
            {
                return (TerrainMode)(TerrainModeEnabled
                    ? cbxTerrainMode.SelectedIndex
                    : 0);
            }

            set
            {
                cbxTerrainMode.SelectedIndex = (int)value;
            }
        }

        private bool BackgroundSceneryEnabled
        {
            get
            {
                return cbxBackgroundScenery.Enabled
                    && cbxBackgroundScenery.SelectedIndex >= 0;
            }
        }

        private BackgroundScenery BackgroundScenery
        {
            get
            {
                return (BackgroundScenery)(BackgroundSceneryEnabled
                    ? cbxBackgroundScenery.SelectedIndex
                    : 0);
            }

            set
            {
                cbxBackgroundScenery.SelectedIndex = (int)value;
            }
        }

        private bool ForegroundSceneryEnabled
        {
            get
            {
                return cbxForegroundScenery.Enabled
                    && cbxForegroundScenery.SelectedIndex >= 0;
            }
        }

        private ForegroundScenery ForegroundScenery
        {
            get
            {
                return (ForegroundScenery)(ForegroundSceneryEnabled
                    ? cbxForegroundScenery.SelectedIndex
                    : 0);
            }

            set
            {
                cbxForegroundScenery.SelectedIndex = (int)value;
            }
        }

        private AreaObjectCommand UICommand
        {
            get
            {
                var result = default(AreaObjectCommand);
                result.Value1 |= (byte)(XPos << 4);
                switch ((int)AreaObjectCode & 0xF00)
                {
                case 0xE00:
                    result.Value1 |= 0x0E;
                    result.Value2 |= (byte)(((int)AreaObjectCode) & 0x40);
                    result.Value2 |= (byte)ForegroundScenery;
                    result.Value2 |= (byte)TerrainMode;
                    result.Value2 |= (byte)((int)BackgroundScenery << 4);
                    break;

                case 0xF00:
                    result.Value1 |= 0x0F;
                    result.Value2 |= (byte)(YPos << 4);
                    result.Value2 |= (byte)(Length - 1);
                    result.Value3 |= (byte)((int)AreaObjectCode & 0x7F);
                    break;

                default:
                    result.Value1 |= (byte)YPos;
                    result.Value2 |= (byte)((int)AreaObjectCode & 0x7F);
                    result.Value2 |= (byte)(Length - 1);
                    break;
                }

                result.ScreenFlag = PageFlag;
                return result;
            }

            set
            {
                SettingUICommand = true;
                UpdateEnabledControls(value);

                XPos = value.X;
                if (value.HasYCoord)
                {
                    YPos = value.Y;
                }

                PageFlag = value.ScreenFlag;
                AreaObjectCode = value.Code;
                TerrainMode = value.TerrainMode;
                BackgroundScenery = value.BackgroundScenery;
                ForegroundScenery = value.ForegroundScenery;
                Length = 1 + value.Length;

                SettingUICommand = false;
            }
        }

        private AreaObjectCommand BinaryCommand
        {
            get
            {
                _ = TryGetCommand(tbxManualInput.Text, out var result);
                return result;
            }

            set
            {
                tbxManualInput.Text = value.IsThreeByteCommand
                    ? $"{value.Value1:X2} {value.Value2:X2} {value.Value3:X2}"
                    : $"{value.Value1:X2} {value.Value2:X2}";
            }
        }

        private IReadOnlyList<AreaObjectCode> Codes
        {
            get;
        }

        private Dictionary<AreaObjectCode, int> EnumIndexes
        {
            get;
        }

        public void UpdatePlatformTypeDescription(AreaPlatformType areaPlatformType)
        {
            var index = EnumIndexes[AreaObjectCode.AreaSpecificPlatform];
            var code = areaPlatformType.ToObjectCode();
            cbxAreaObjectCode.Items[index] = code.BaseName();
        }

        private static bool TryGetCommand(string text, out AreaObjectCommand command)
        {
            var tokens = text.Split(' ');
            if (tokens.Length != 3 && tokens.Length != 2)
            {
                command = default;
                return false;
            }

            var bytes = new byte[3];
            for (var i = 0; i < tokens.Length; i++)
            {
                if (tokens[i].Length != 2)
                {
                    command = default;
                    return false;
                }

                if (!Byte.TryParse(
                    tokens[i],
                    NumberStyles.HexNumber,
                    CultureInfo.CurrentUICulture,
                    out bytes[i]))
                {
                    command = default;
                    return false;
                }
            }

            command = new AreaObjectCommand(bytes[0], bytes[1], bytes[2]);
            if (!command.IsValid)
            {
                command = default;
                return false;
            }
            return true;
        }

        private void UpdateEnabledControls(AreaObjectCommand value)
        {
            lblY.Enabled =
            nudY.Enabled = value.HasYCoord;

            lblLength.Enabled =
            nudLength.Enabled = value.IsExtendableObject;

            nudLength.Maximum = 1 + value.Code.GetMaxLength();

            lblTerrainMode.Enabled =
            cbxTerrainMode.Enabled =
            lblBackgroundScenery.Enabled =
            cbxBackgroundScenery.Enabled =
                value.Code == AreaObjectCode.BrickAndSceneryChange;

            lblForegroundScenery.Enabled =
            cbxForegroundScenery.Enabled =
                value.Code == AreaObjectCode.ForegroundChange;

            chkPageFlag.Enabled = value.Code != AreaObjectCode.ScreenJump;
        }

        private void UpdateValidInput()
        {
            // If we're using the list and check boxes, then the input is always
            // valid by their restraints. Otherwise, if we're entering the value
            // manually, then we must check that text is valid.
            IsValidInput =
                !UseManualInput || TryGetCommand(tbxManualInput.Text, out var _);
        }

        private void ManualInput_TextChanged(object sender, EventArgs e)
        {
            UpdateValidInput();
            if (!SettingUICommand && IsValidInput && UICommand != BinaryCommand)
            {
                UICommand = BinaryCommand;
                AreaObjectCommandChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void AreaObectCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAreaObjectCode.SelectedIndex == -1 || SettingUICommand)
            {
                return;
            }

            UpdateEnabledControls(UICommand);
            BinaryCommand = UICommand;
            AreaObjectCommandChanged?.Invoke(this, EventArgs.Empty);
        }

        private void Item_ValueChanged(object sender, EventArgs e)
        {
            var control = sender as Control;
            if (control.Enabled && !SettingUICommand && BinaryCommand != UICommand)
            {
                BinaryCommand = UICommand;
                AreaObjectCommandChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void UseManualInput_CheckedChanged(object sender, EventArgs e)
        {
            UpdateValidInput();
        }
    }
}
