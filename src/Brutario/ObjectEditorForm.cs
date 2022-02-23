namespace Brutario
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Forms;

    using Smb1;

    public sealed partial class ObjectEditorForm : Form
    {
        private AreaPlatformType _areaPlatformType;

        public ObjectEditorForm()
        {
            InitializeComponent();

            Codes = new List<AreaObjectCode>();
            EnumIndexes = new Dictionary<AreaObjectCode, int>();
            foreach (var code in Enum.GetValues(typeof(AreaObjectCode)))
            {
                EnumIndexes.Add((AreaObjectCode)code, Codes.Count);
                Codes.Add((AreaObjectCode)code);
                _ = cbxAreaObjectCode.Items.Add(
                    BaseName((AreaObjectCode)code, AreaPlatformType.Trees));
            }

            TerrainMode = TerrainMode.None;
            ForegroundScenery = ForegroundScenery.None;
            BackgroundScenery = BackgroundScenery.None;
        }

        public event EventHandler AreaPlatformTypeChanged;

        public event EventHandler AreaObjectCommandChanged;

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

        public AreaPlatformType AreaPlatformType
        {
            get
            {
                return _areaPlatformType;
            }

            set
            {
                if (value == AreaPlatformType)
                {
                    return;
                }

                _areaPlatformType = value;
                var index = EnumIndexes[AreaObjectCode.AreaSpecificPlatform];
                cbxAreaObjectCode.Items[index] = BaseName(
                    AreaObjectCode.AreaSpecificPlatform,
                    value);
                AreaPlatformTypeChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public AreaObjectCommand AreaObjectCommand
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
                    AreaObjectCommandChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool Assigning
        {
            get;
            set;
        }

        private AreaObjectCommand UICommand
        {
            get
            {
                var result = default(AreaObjectCommand);
                result.Value1 |= (byte)(XPos << 4);
                switch ((int)AreaObjectCode & 0xF00)
                {
                case 0xC00:
                    result.Value1 |= 0x0C;
                    result.Value2 |= (byte)((int)AreaObjectCode & 0x7F);
                    result.Value2 |= (byte)(Length - 1);
                    break;

                case 0xD00:
                    result.Value1 |= 0x0D;
                    result.Value2 |= (byte)((int)AreaObjectCode & 0x7F);
                    if (Codes[cbxAreaObjectCode.SelectedIndex] == AreaObjectCode.ScreenSkip)
                    {
                        result.Value2 |= (byte)(Length - 1);
                    }
                    break;

                case 0xE00:
                    result.Value1 |= 0x0E;
                    if (AreaObjectCode == AreaObjectCode.BrickAndSceneryChange)
                    {
                        result.Value2 |= (byte)TerrainMode;
                        result.Value2 |= (byte)((int)BackgroundScenery << 4);
                    }
                    else
                    {
                        result.Value2 |= 0x40;
                        result.Value2 |= (byte)ForegroundScenery;
                    }
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
                    if ((int)AreaObjectCode >= 0x10)
                    {
                        result.Value2 |= (byte)(Length - 1);
                    }
                    break;
                }

                if (PageFlag)
                {
                    if (result.Size == 3)
                    {
                        result.Value3 |= 0x80;
                    }
                    else
                    {
                        result.Value2 |= 0x80;
                    }
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
                var y = value.Value1 & 0x0F;
                if (y == 0x0F || y < 0x0C)
                {
                    YPos = value.Y;
                }

                PageFlag = value.ScreenFlag;
                AreaObjectCode = value.Code;

                if (value.Code == AreaObjectCode.BrickAndSceneryChange)
                {
                    TerrainMode = (TerrainMode)(value.BaseCommand & 0x0F);
                    BackgroundScenery = (BackgroundScenery)((value.BaseCommand >> 4) & 3);
                }

                if (value.Code == AreaObjectCode.ForegroundChange)
                {
                    ForegroundScenery = (ForegroundScenery)(value.Parameter & 7);
                }

                if (value.Code == AreaObjectCode.ScreenSkip)
                {
                    Length = 1 + (value.Value2 & 0x1F);
                }
                else if (value.Code == AreaObjectCode.EnterablePipe
                    || value.Code == AreaObjectCode.UnenterablePipe)
                {
                    Length = 1 + (AreaObjectCommand.Parameter & 7);
                }
                else if (((int)value.Code >= 0x10 && (int)value.Code < 0xD00)
                    || ((int)value.Code >= 0xF00))
                {
                    Length = 1 + AreaObjectCommand.Parameter;
                }

                Assigning = false;
            }
        }

        private AreaObjectCommand BinaryCommand
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

        private AreaObjectCode AreaObjectCode
        {
            get
            {
                return Codes[
                    cbxAreaObjectCode.SelectedIndex >= 0 ? cbxAreaObjectCode.SelectedIndex : 0];
            }

            set
            {
                cbxAreaObjectCode.SelectedIndex = EnumIndexes.TryGetValue(value, out var index)
                    ? index
                    : -1;
            }
        }

        private int Length
        {
            get
            {
                return (int)nudLength.Value;
            }

            set
            {
                if (value <= nudLength.Maximum)
                {
                    nudLength.Value = value;
                }
            }
        }

        private TerrainMode TerrainMode
        {
            get
            {
                return (TerrainMode)(cbxTerrainMode.SelectedIndex >= 0
                    ? cbxTerrainMode.SelectedIndex
                    : 0);
            }

            set
            {
                cbxTerrainMode.SelectedIndex = (int)value;
            }
        }

        private BackgroundScenery BackgroundScenery
        {
            get
            {
                return (BackgroundScenery)(cbxBackgroundScenery.SelectedIndex >= 0
                    ? cbxBackgroundScenery.SelectedIndex
                    : 0);
            }

            set
            {
                cbxBackgroundScenery.SelectedIndex = (int)value;
            }
        }

        private ForegroundScenery ForegroundScenery
        {
            get
            {
                return (ForegroundScenery)(cbxForegroundScenery.SelectedIndex >= 0
                    ? cbxForegroundScenery.SelectedIndex
                    : 0);
            }

            set
            {
                cbxForegroundScenery.SelectedIndex = (int)value;
            }
        }

        private List<AreaObjectCode> Codes
        {
            get;
        }

        private Dictionary<AreaObjectCode, int> EnumIndexes
        {
            get;
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

            if (bytes[0] == 0xFD)
            {
                return false;
            }

            if ((bytes[0] & 0x0F) == 0x0F && tokens.Length == 2)
            {
                return false;
            }

            if ((bytes[0] & 0x0F) != 0x0F && tokens.Length == 3)
            {
                return false;
            }

            command = new AreaObjectCommand(bytes[0], bytes[1], bytes[2]);
            return true;
        }

        private static int GetMaxLength(AreaObjectCode code)
        {
            switch (code)
            {
            case AreaObjectCode.ScreenSkip:
                return 0x1F;

            case AreaObjectCode.EnterablePipe:
            case AreaObjectCode.UnenterablePipe:
                return 7;

            case AreaObjectCode.Staircase:
                return 8;

            case AreaObjectCode.Castle:
                return 7;
            }

            return IsExtendableObject(code) ? 0x0F : 0;
        }

        private static bool IsExtendableObject(AreaObjectCode code)
        {
            switch (code)
            {
            case AreaObjectCode.QuestionBlockPowerup:
            case AreaObjectCode.QuestionBlockCoin:
            case AreaObjectCode.HiddenBlockCoin:
            case AreaObjectCode.HiddenBlock1UP:
            case AreaObjectCode.BrickPowerup:
            case AreaObjectCode.BrickBeanstalk:
            case AreaObjectCode.BrickStar:
            case AreaObjectCode.Brick10Coins:
            case AreaObjectCode.Brick1UP:
            case AreaObjectCode.SidewaysPipe:
            case AreaObjectCode.UsedBlock:
            case AreaObjectCode.SpringBoard:
            case AreaObjectCode.JPipe:
            case AreaObjectCode.FlagPole:
            case AreaObjectCode.Empty:
            case AreaObjectCode.Empty2:
                return false;

            case AreaObjectCode.AreaSpecificPlatform:
            case AreaObjectCode.HorizontalBricks:
            case AreaObjectCode.HorizontalStones:
            case AreaObjectCode.HorizontalCoins:
            case AreaObjectCode.VerticalBricks:
            case AreaObjectCode.VerticalStones:
            case AreaObjectCode.UnenterablePipe:
            case AreaObjectCode.EnterablePipe:
            case AreaObjectCode.Hole:
            case AreaObjectCode.BalanceHorizontalRope:
            case AreaObjectCode.BridgeV7:
            case AreaObjectCode.BridgeV8:
            case AreaObjectCode.BridgeV10:
            case AreaObjectCode.HoleWithWaterOrLava:
            case AreaObjectCode.HorizontalQuestionBlocksV3:
            case AreaObjectCode.HorizontalQuestionBlocksV7:
            case AreaObjectCode.ScreenSkip:
                return true;

            case AreaObjectCode.AltJPipe:
            case AreaObjectCode.AltFlagPole:
            case AreaObjectCode.BowserAxe:
            case AreaObjectCode.RopeForAxe:
            case AreaObjectCode.BowserBridge:
            case AreaObjectCode.ScrollStopWarpZone:
            case AreaObjectCode.ScrollStop:
            case AreaObjectCode.AltScrollStop:
            case AreaObjectCode.RedCheepCheepFlying:
            case AreaObjectCode.BulletBillGenerator:
            case AreaObjectCode.StopGenerator:
            case AreaObjectCode.LoopCommand:
            case AreaObjectCode.BrickAndSceneryChange:
            case AreaObjectCode.ForegroundChange:
                return false;

            case AreaObjectCode.RopeForLift:
            case AreaObjectCode.PulleyRope:
                return true;

            case AreaObjectCode.EmptyTile:
                return false;

            case AreaObjectCode.Castle:
            case AreaObjectCode.CastleCeilingCap:
            case AreaObjectCode.Staircase:
            case AreaObjectCode.CastleStairs:
            case AreaObjectCode.CastleRectangularCeilingTiles:
            case AreaObjectCode.CastleFloorRightEdge:
            case AreaObjectCode.CastleFloorLeftEdge:
            case AreaObjectCode.CastleFloorLeftWall:
            case AreaObjectCode.CastleFloorRightWall:
            case AreaObjectCode.VerticalSeaBlocks:
            case AreaObjectCode.ExtendableJPipe:
            case AreaObjectCode.VerticalBalls:
                return true;

            default:
                return false;
            }
        }

        private static string BaseName(
            AreaObjectCode code,
            AreaPlatformType areaPlatformType)
        {
            switch (code)
            {
            case AreaObjectCode.QuestionBlockPowerup:
                return "Question Block (Powerup)";

            case AreaObjectCode.QuestionBlockCoin:
                return "Question Block (Coin)";

            case AreaObjectCode.HiddenBlockCoin:
                return "Hidden Block (Coin)";

            case AreaObjectCode.HiddenBlock1UP:
                return "Hidden Block (1UP)";

            case AreaObjectCode.BrickPowerup:
                return "Brick (Powerup)";

            case AreaObjectCode.BrickBeanstalk:
                return "Brick (Beanstalk)";

            case AreaObjectCode.BrickStar:
                return "Brick (Star)";

            case AreaObjectCode.Brick10Coins:
                return "Brick (10 Coins)";

            case AreaObjectCode.Brick1UP:
                return "Brick (1UP)";

            case AreaObjectCode.SidewaysPipe:
                return "Sideways Pipe Cap";

            case AreaObjectCode.UsedBlock:
                return "Used Block";

            case AreaObjectCode.SpringBoard:
                return "Spring Board";

            case AreaObjectCode.JPipe:
            case AreaObjectCode.AltJPipe:
                return "J-Pipe";

            case AreaObjectCode.FlagPole:
            case AreaObjectCode.AltFlagPole:
                return "Flag Pole";

            case AreaObjectCode.Empty:
            case AreaObjectCode.Empty2:
                return "Nothing";

            case AreaObjectCode.AreaSpecificPlatform:
                return areaPlatformType switch
                {
                    AreaPlatformType.Trees => "Tree Top Platform",
                    AreaPlatformType.Mushrooms => "Mushroom Platform",
                    AreaPlatformType.BulletBillTurrets => "Bullet Bill Shooter",
                    AreaPlatformType.CloudGround => "Cloud Ground",
                    _ => "Unknown Area Platform Type",
                };

            case AreaObjectCode.HorizontalBricks:
                return "Horizontal Bricks";

            case AreaObjectCode.HorizontalStones:
                return "Horizontal Blocks";

            case AreaObjectCode.HorizontalCoins:
                return "Horizontal Coins";

            case AreaObjectCode.VerticalBricks:
                return "Vertical Bricks";

            case AreaObjectCode.VerticalStones:
                return "Vertical Blocks";

            case AreaObjectCode.UnenterablePipe:
                return "Unenterable Pipe";

            case AreaObjectCode.EnterablePipe:
                return "Enterable Pipe";

            case AreaObjectCode.Hole:
                return "Hole";

            case AreaObjectCode.BalanceHorizontalRope:
                return "Pulley Platforms";

            case AreaObjectCode.BridgeV7:
                return "Rope Bridge (Y=7)";

            case AreaObjectCode.BridgeV8:
                return "Rope Bridge (Y=8)";

            case AreaObjectCode.BridgeV10:
                return "Rope Bridge (Y=10)";

            case AreaObjectCode.HoleWithWaterOrLava:
                return "Hole with water or lava";

            case AreaObjectCode.HorizontalQuestionBlocksV3:
                return "Row of Coin Blocks (Y=3)";

            case AreaObjectCode.HorizontalQuestionBlocksV7:
                return "Row of Coin Blocks (Y=7)";

            case AreaObjectCode.ScreenSkip:
                return "Screen Skip";

            case AreaObjectCode.BowserAxe:
                return "Bowser Axe";

            case AreaObjectCode.RopeForAxe:
                return "Rope For Axe";

            case AreaObjectCode.BowserBridge:
                return "Bowser Bridge";

            case AreaObjectCode.ScrollStopWarpZone:
                return "Scroll Stop (Warp Zone)";

            case AreaObjectCode.ScrollStop:
            case AreaObjectCode.AltScrollStop:
                return "Scroll Stop";

            case AreaObjectCode.RedCheepCheepFlying:
                return "Generator: Red flying cheep-cheeps";

            case AreaObjectCode.BulletBillGenerator:
                return "Generator: Bullet Bills";

            case AreaObjectCode.StopGenerator:
                return "Stop Generator (also stops Lakitus)";

            case AreaObjectCode.LoopCommand:
                return "Screen Loop Command";

            case AreaObjectCode.BrickAndSceneryChange:
                return "Brick and scenery change";

            case AreaObjectCode.ForegroundChange:
                return "Foreground Change";

            case AreaObjectCode.RopeForLift:
                return "Rope for platform lifts";

            case AreaObjectCode.PulleyRope:
                return "Rope for pulley platforms";

            case AreaObjectCode.EmptyTile:
                return "Empty tile";

            case AreaObjectCode.Castle:
                return "Castle";

            case AreaObjectCode.CastleCeilingCap:
                return "Castle Object: Ceiling Cap Tile";

            case AreaObjectCode.Staircase:
                return "Staircase";

            case AreaObjectCode.CastleStairs:
                return "Castle Object: Descending Stairs";

            case AreaObjectCode.CastleRectangularCeilingTiles:
                return "Castle Object: Rectangular Ceiling Tiles";

            case AreaObjectCode.CastleFloorRightEdge:
                return "Castle Object: Right-Facing Wall To Floor";

            case AreaObjectCode.CastleFloorLeftEdge:
                return "Castle Object: Left-Facing Wall To Floor";

            case AreaObjectCode.CastleFloorLeftWall:
                return "Castle Object: Left-Facing Wall";

            case AreaObjectCode.CastleFloorRightWall:
                return "Castle Object: Right-Facing Wall";

            case AreaObjectCode.VerticalSeaBlocks:
                return "Vertical Sea Blocks";

            case AreaObjectCode.ExtendableJPipe:
                return "Extendable J-Pipe";

            case AreaObjectCode.VerticalBalls:
                return "Vertical Climbing Balls";
            }

            return String.Empty;
        }

        private void UpdateAccess(AreaObjectCommand value)
        {
            var y = value.Value1 & 0x0F;
            lblY.Enabled =
            nudY.Enabled = y == 0x0F || y < 0x0C;

            lblTerrainMode.Enabled =
            cbxTerrainMode.Enabled =
            lblBackgroundScenery.Enabled =
            cbxBackgroundScenery.Enabled =
                value.Code == AreaObjectCode.BrickAndSceneryChange;

            lblForegroundScenery.Enabled =
            cbxForegroundScenery.Enabled =
                value.Code == AreaObjectCode.ForegroundChange;

            if (value.Code == AreaObjectCode.ScreenSkip)
            {
                lblY.Enabled =
                nudY.Enabled = false;
                lblLength.Enabled =
                nudLength.Enabled = true;
                nudLength.Maximum = 0x20;
            }
            else if (((int)value.Code >= 0x10 && (int)value.Code < 0xD00)
                || ((int)value.Code >= 0xF00))
            {
                lblY.Enabled =
                nudY.Enabled =
                lblLength.Enabled =
                nudLength.Enabled = true;
                nudLength.Maximum = 1 + GetMaxLength(value.Code);
            }
            else
            {
                lblLength.Enabled =
                nudLength.Enabled = false;
            }
        }

        private void Binary_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = !UseManual || TryGetCommand(tbxBinary.Text, out var _);
            if (!Assigning && btnOK.Enabled && UICommand != BinaryCommand)
            {
                UICommand = BinaryCommand;
                AreaObjectCommandChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void AreaObectCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAreaObjectCode.SelectedIndex == -1 || Assigning)
            {
                return;
            }

            UpdateAccess(UICommand);
            BinaryCommand = UICommand;
            if (btnOK.Enabled)
            {
                AreaObjectCommandChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Item_ValueChanged(object sender, EventArgs e)
        {
            if ((sender as Control).Enabled && !Assigning && BinaryCommand != UICommand)
            {
                BinaryCommand = UICommand;
                if (btnOK.Enabled)
                {
                    AreaObjectCommandChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
