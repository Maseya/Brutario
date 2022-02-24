namespace Brutario
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Forms;

    using Brutario.Smb1;

    public partial class SpriteEditorForm : Form
    {
        public SpriteEditorForm()
        {
            InitializeComponent();

            Codes = new List<AreaSpriteCode>();
            EnumIndexes = new Dictionary<AreaSpriteCode, int>();
            foreach (var code in Enum.GetValues(typeof(AreaSpriteCode)))
            {
                EnumIndexes.Add((AreaSpriteCode)code, Codes.Count);
                Codes.Add((AreaSpriteCode)code);
                _ = cbxAreaSpriteCode.Items.Add(
                    BaseName((AreaSpriteCode)code));
            }
        }

        public event EventHandler AreaSpriteCommandChanged;

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

        public AreaSpriteCommand AreaSpriteCommand
        {
            get
            {
                return UseManual ? BinaryCommand : UICommand;
            }

            set
            {
                BinaryCommand = UICommand = value;
                if (btnOK.Enabled)
                {
                    AreaSpriteCommandChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool Assigning
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
                return (int)nudY.Value;
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
                return chkPageFlag.Checked;
            }

            set
            {
                chkPageFlag.Checked = value;
            }
        }

        private AreaSpriteCode AreaSpriteCode
        {
            get
            {
                return Codes[
                    cbxAreaSpriteCode.SelectedIndex >= 0
                    ? cbxAreaSpriteCode.SelectedIndex
                    : 0];
            }

            set
            {
                cbxAreaSpriteCode.SelectedIndex = EnumIndexes.TryGetValue(
                    value,
                    out var index)
                    ? index
                    : -1;
            }
        }

        private int Page
        {
            get
            {
                return (int)nudPage.Value;
            }

            set
            {
                if (value <= nudPage.Maximum)
                {
                    nudPage.Value = value;
                }
            }
        }

        private int World
        {
            get
            {
                return (int)nudWorld.Value;
            }

            set
            {
                if (value <= nudWorld.Maximum)
                {
                    nudWorld.Value = value;
                }
            }
        }

        private bool HardFlag
        {
            get
            {
                return chkHardFlag.Checked;
            }

            set
            {
                chkHardFlag.Checked = value;
            }
        }

        private int AreaNumber
        {
            get
            {
                return Int32.TryParse(
                    tbxAreaNumber.Text,
                    NumberStyles.HexNumber,
                    CultureInfo.CurrentUICulture,
                    out var result)
                    ? result
                    : -1;
            }

            set
            {
                tbxAreaNumber.Text = $"{value:X2}";
            }
        }

        private AreaSpriteCommand UICommand
        {
            get
            {
                var result = default(AreaSpriteCommand);
                result.Value1 |= (byte)(XPos << 4);
                switch (AreaSpriteCode)
                {
                case AreaSpriteCode.AreaPointer:
                    result.Value1 |= 0x0E;
                    result.Value2 |= (byte)(AreaNumber & 0x7F);
                    result.Value3 |= (byte)((World - 1) << 5);
                    result.Value3 |= (byte)((Page - 1) & 0x1F);
                    break;

                case AreaSpriteCode.ScreenSkip:
                    result.Value1 |= 0x0F;
                    result.Value2 |= (byte)((Page - 1) & 0x1F);
                    break;

                default:
                    result.Value1 |= (byte)YPos;
                    result.Value2 |= (byte)((int)AreaSpriteCode & 0x3F);
                    if (HardFlag)
                    {
                        result.Value2 |= 0x40;
                    }
                    break;
                }

                if (PageFlag)
                {
                    result.Value2 |= 0x80;
                }

                return result;
            }

            set
            {
                if (!EnumIndexes.ContainsKey(value.Code))
                {
                    return;
                }

                Assigning = true;
                UpdateAccess(value);

                XPos = value.X;
                PageFlag = value.ScreenFlag;
                AreaSpriteCode = value.Code;
                switch (value.Code)
                {
                case AreaSpriteCode.AreaPointer:
                    Page = 1 + (value.Value3 & 0x1F);
                    World = 1 + value.WorldLimit;
                    AreaNumber = value.AreaNumber;
                    break;

                case AreaSpriteCode.ScreenSkip:
                    Page = 1 + (value.Value2 & 0x1F);
                    break;

                default:
                    YPos = value.Y;
                    HardFlag = value.HardWorldFlag;
                    break;
                }

                Assigning = false;
            }
        }

        private AreaSpriteCommand BinaryCommand
        {
            get
            {
                _ = TryGetCommand(tbxBinary.Text, out var result);
                return result;
            }

            set
            {
                tbxBinary.Text = value.Size == 3
                    ? $"{value.Value1:X2} {value.Value2:X2} {value.Value3:X2}"
                    : $"{value.Value1:X2} {value.Value2:X2}";
            }
        }

        private List<AreaSpriteCode> Codes
        {
            get;
        }

        private Dictionary<AreaSpriteCode, int> EnumIndexes
        {
            get;
        }

        private static bool TryGetCommand(string text, out AreaSpriteCommand command)
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
                if (tokens[i].Length != 2)
                {
                    return false;
                }

                if (!Byte.TryParse(
                        tokens[i],
                        NumberStyles.HexNumber,
                        CultureInfo.CurrentUICulture,
                        out bytes[i]))
                {
                    return false;
                }
            }

            if (bytes[0] == 0xFF)
            {
                return false;
            }

            if ((bytes[0] & 0x0F) == 0x0E && tokens.Length == 2)
            {
                return false;
            }

            if ((bytes[0] & 0x0F) != 0x0E && tokens.Length == 3)
            {
                return false;
            }

            command = new AreaSpriteCommand(bytes[0], bytes[1], bytes[2]);
            return true;
        }

        private static string BaseName(
            AreaSpriteCode code)
        {
            switch (code)
            {
            case AreaSpriteCode.AreaPointer:
                return "Transition Command";

            case AreaSpriteCode.GreenKoopaTroopa:
                return "Koopa Troopa (Green)";

            case AreaSpriteCode.RedKoopaTroopa:
                return "Koopa Troopa (Red; Walks off floors)";
            case AreaSpriteCode.BuzzyBeetle:
                return "Buzzy Beetle";

            case AreaSpriteCode.RedKoopaTroopa2:
                return "Koopa Troopa (Red; Stays on floors)";
            case AreaSpriteCode.GreenKoopaTroopa2:
                return "Koopa Troopa (Green; Walks in place)";
            case AreaSpriteCode.HammerBros:
                return "Hammer Bros.";

            case AreaSpriteCode.Goomba:
                return "Goomba";

            case AreaSpriteCode.Blooper:
                return "Squid";

            case AreaSpriteCode.BulletBill:
                return "Bullet Bill";

            case AreaSpriteCode.YellowKoopaParatroopa:
                return "Yellow Koopa Paratroopa (Flies in place)";

            case AreaSpriteCode.GreenCheepCheep:
                return "Green Cheep-Cheep";

            case AreaSpriteCode.RedCheepCheep:
                return "Red Cheep-Cheep";

            case AreaSpriteCode.Podoboo:
                return "Podoboo";

            case AreaSpriteCode.PiranhaPlant:
                return "Piranha Plant";

            case AreaSpriteCode.GreenKoopaParatroopa:
                return "Green Koopa Paratroopa (Leaping)";

            case AreaSpriteCode.RedKoopaParatroopa:
                return "Red Koopa Paratroopa (Flies vertically)";

            case AreaSpriteCode.GreenKoopaParatroopa2:
                return "Green Koopa Paratroopa (Flies horizontally)";

            case AreaSpriteCode.Lakitu:
                return "Lakitu";

            case AreaSpriteCode.Spiny:
                return "Spiny (undefined walk speed)";

            case AreaSpriteCode.RedFlyingCheepCheep:
                return "Red Flying Cheep-Cheep";

            case AreaSpriteCode.BowsersFire:
                return "Bowser's Fire (generator)";

            case AreaSpriteCode.Fireworks:
                return "Single Firework";

            case AreaSpriteCode.BulletBillOrCheepCheeps:
                return "Generator (Bullet Bill or Cheep-Cheeps)";

            case AreaSpriteCode.FireBarClockwise:
                return "Fire Bar (Clockwise)";

            case AreaSpriteCode.FastFireBarClockwise:
                return "Fire Bar (Fast; Clockwise)";
            case AreaSpriteCode.FireBarCounterClockwise:
                return "Fire Bar (Counter-Clockwise)";

            case AreaSpriteCode.FastFireBarCounterClockwise:
                return "Fire Bar (Fast; Counter-Clockwise)";
            case AreaSpriteCode.LongFireBarClockwise:
                return "Long Fire Bar (Fast; Clockwise)";
            case AreaSpriteCode.BalanceRopeLift:
                return "Rope for Lift Balance";

            case AreaSpriteCode.LiftDownThenUp:
                return "Lift (Down, then up)";

            case AreaSpriteCode.LiftUp:
                return "Lift (Up)";

            case AreaSpriteCode.LiftDown:
                return "Lift (Down)";

            case AreaSpriteCode.LiftLeftThenRight:
                return "Lift (Left, then right)";

            case AreaSpriteCode.LiftFalling:
                return "Lift (Falling)";

            case AreaSpriteCode.LiftRight:
                return "Lift (Right)";

            case AreaSpriteCode.ShortLiftUp:
                return "Short Lift (Up)";

            case AreaSpriteCode.ShortLiftDown:
                return "Short Lift (Down)";

            case AreaSpriteCode.Bowser:
                return "Bowser: King of the Koopa";

            case AreaSpriteCode.WarpZoneCommand:
                return "Command: Load Warp Zone";

            case AreaSpriteCode.ToadOrPrincess:
                return "Toad or Princess";

            case AreaSpriteCode.TwoGoombasY10:
                return "Two Goombas (Y=10)";

            case AreaSpriteCode.ThreeGoombasY10:
                return "Three Goombas (Y=10)";

            case AreaSpriteCode.TwoGoombasY6:
                return "Two Goombas (Y=6)";

            case AreaSpriteCode.ThreeGoombasY6:
                return "Three Goombas (Y=6)";

            case AreaSpriteCode.TwoGreenKoopasY10:
                return "Two Green Koopa Troopas (Y=10)";

            case AreaSpriteCode.ThreeGreenKoopasY10:
                return "Three Green Koopa Troopas (Y=10)";

            case AreaSpriteCode.TwoGreenKoopasY6:
                return "Two Green Koopa Troopas (Y=6)";

            case AreaSpriteCode.ThreeGreenKoopasY6:
                return "Three Green Koopa Troopas (Y=6)";

            case AreaSpriteCode.ScreenSkip:
                return "Page Skip";

            default:
                break;
            }

            return "Unknown code";
        }

        private void UpdateAccess(AreaSpriteCommand value)
        {
            lblY.Enabled =
            nudY.Enabled =
            chkHardFlag.Enabled = value.Y <= 0x0D;

            lblPage.Enabled =
            nudPage.Enabled = value.Y > 0x0D;

            lblWorld.Enabled =
            nudWorld.Enabled =
            lblAreaNumber.Enabled =
            tbxAreaNumber.Enabled = value.Code == AreaSpriteCode.AreaPointer;
        }

        private void AreaNumber_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = !tbxAreaNumber.Enabled || AreaNumber != -1;
        }

        private void Binary_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = !UseManual || TryGetCommand(tbxBinary.Text, out var _);
            if (!Assigning && btnOK.Enabled && UICommand != BinaryCommand)
            {
                UICommand = BinaryCommand;
                AreaSpriteCommandChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Item_ValueChanged(object sender, EventArgs e)
        {
            if ((sender as Control).Enabled && !Assigning && BinaryCommand != UICommand)
            {
                BinaryCommand = UICommand;
                if (btnOK.Enabled)
                {
                    AreaSpriteCommandChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void AreaSpriteCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAreaSpriteCode.SelectedIndex == -1 || Assigning)
            {
                return;
            }

            UpdateAccess(UICommand);
            BinaryCommand = UICommand;
            if (btnOK.Enabled)
            {
                AreaSpriteCommandChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Binary_CheckedChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = !UseManual || TryGetCommand(tbxBinary.Text, out var _);
        }
    }
}
