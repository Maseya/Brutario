using System;
using System.Globalization;
using System.Windows.Forms;

namespace Brutario
{
    public partial class MainForm : Form
    {
        private const double FramesPerSecond = 60;

        private const double GameFramesPerSecond = 60;

        private const double RefreshRate = 1000d / FramesPerSecond;

        private AllStarsRomFile _allStarsRomFile;

        public MainForm()
        {
            InitializeComponent();
            if (components is null)
            {
                components = new System.ComponentModel.Container();
            }

            Timer = new System.Timers.Timer();
            components.Add(Timer);
            Timer.Interval = 1000 / 240f;
            Timer.Elapsed += (s, e) => Animate();
            StartTime = DateTime.Now;
            Timer.Start();
        }

        private System.Timers.Timer Timer
        {
            get;
        }

        public AllStarsRomFile AllStarsRomFile
        {
            get
            {
                return _allStarsRomFile;
            }

            private set
            {
                if (AllStarsRomFile == value)
                {
                    return;
                }

                if (AllStarsRomFile != null)
                {
                    AllStarsRomFile.Smb1RomData.AreaNumberChanged -=
                        AreaIndexChanged;
                }

                _allStarsRomFile = value;
                UpdateMenuEnabled();
                if (AllStarsRomFile != null)
                {
                    AllStarsRomFile.Smb1RomData.AreaNumberChanged +=
                        AreaIndexChanged;

                    ttbJumpToArea.Text = Smb1RomData.AreaNumber.ToString("X2");
                }

                OnRomFileChanged(EventArgs.Empty);
            }
        }

        public Smb1.GameData Smb1RomData
        {
            get
            {
                return AllStarsRomFile?.Smb1RomData;
            }
        }

        private DateTime StartTime
        {
            get;
            set;
        }

        private int Frame
        {
            get;
            set;
        }

        private TimeSpan ElapsedTime
        {
            get
            {
                return DateTime.Now - StartTime;
            }
        }

        public void Open()
        {
            if (!IsSavedOrOverwriteUnsaved())
            {
                return;
            }

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                AllStarsRomFile = new AllStarsRomFile(openFileDialog.FileName);
            }
        }

        protected virtual void OnRomFileChanged(EventArgs e)
        {
            LoadPalette();
            gfxControl.PixelData =
            map16Control.PixelData =
            areaControl.PixelData = Smb1RomData.PixelData;
            Smb1RomData?.Animate(0);
            map16Control.Tiles = Smb1RomData.Map16Data;
            LoadBG1();
            LoadBG2();
        }

        private void Animate()
        {
            if (ElapsedTime.Ticks < 10000 * (int)(RefreshRate * (Frame + 1)))
            {
                return;
            }

            Frame++;
            if (Smb1RomData is null)
            {
                return;
            }

            Smb1RomData.Animate((int)(Frame * GameFramesPerSecond / FramesPerSecond));
            gfxControl.Invalidate();
            map16Control.Invalidate();

            areaControl.Sprites = Smb1RomData.AreaSpriteRenderer.GetSprites(
                Smb1RomData.CurrentSpriteData,
                Smb1RomData.CurrentObjectData);

            areaControl.Invalidate();
        }

        private void LoadPalette()
        {
            areaControl.Palette =
            map16Control.Palette =
            gfxControl.Palette =
            paletteControl.Palette = Smb1RomData.CurrentPalette;
        }

        private void LoadBG1()
        {
            areaControl.BG1 = Smb1RomData.RenderScreenTiles(
                Smb1RomData.AreaObjectRenderer.TileMap,
                0x200);

            areaControl.Sprites = Smb1RomData.AreaSpriteRenderer.GetSprites(
                Smb1RomData.CurrentSpriteData,
                Smb1RomData.CurrentObjectData);
        }

        private void LoadBG2()
        {
            areaControl.BG2 = Smb1RomData.RenderBg2Tiles(
                Smb1RomData.AreaBg2Map.GetTileMap(Smb1RomData.AreaIndex),
                0x100);
        }

        private void AreaIndexChanged(object sender, EventArgs e)
        {
            ttbJumpToArea.Text = Smb1RomData.AreaNumber.ToString("X2");
            LoadPalette();
            LoadBG1();
            LoadBG2();
        }

        private bool IsSavedOrOverwriteUnsaved()
        {
            if (AllStarsRomFile is null || !AllStarsRomFile.HasUnsavedChanges)
            {
                return true;
            }

            var dialogResult = MessageBox.Show(
                this,
                "Brutario",
                "You have unsaved changes. Do you wish to save them before opening a new file?",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning);

            switch (dialogResult)
            {
                case DialogResult.Yes:
                    AllStarsRomFile.Save();
                    return true;

                case DialogResult.No:
                    return true;

                case DialogResult.Cancel:
                default:
                    return false;
            }
        }

        private void UpdateMenuEnabled()
        {
            tsbJumpToArea.Enabled =
            ttbJumpToArea.Enabled = AllStarsRomFile != null;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Open_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void JumpToArea_TextChanged(object sender, EventArgs e)
        {
            tsbJumpToArea.Enabled =
                Int32.TryParse(
                    ttbJumpToArea.Text,
                    NumberStyles.HexNumber,
                    CultureInfo.CurrentUICulture,
                    out var areaIndex)
                && areaIndex >= 0;
        }

        private void JumpToArea_Click(object sender, EventArgs e)
        {
            Smb1RomData.AreaNumber = Int32.Parse(
                ttbJumpToArea.Text,
                NumberStyles.HexNumber,
                CultureInfo.CurrentCulture);
        }

        private void JumpToArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tsbJumpToArea.PerformClick();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var viewWidth = ((areaControl.ClientSize.Width - 1) / 8) + 1;
            areaScrollBar.Value = 0;
            areaScrollBar.Maximum = 0x400 - viewWidth;
            areaScrollBar.SmallChange = 1;
            areaScrollBar.LargeChange = 0x20;
        }

        private void AreaScrollBar_ValueChanged(object sender, EventArgs e)
        {
            areaControl.StartX = areaScrollBar.Value;
        }

        private void LoadAreaByLevel_Click(object sender, EventArgs e)
        {
            using var dialog = new AreaSelectorForm
            {
                Smb1RomData = Smb1RomData
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Smb1RomData.AreaNumber = dialog.Area;
            }
        }
    }
}
