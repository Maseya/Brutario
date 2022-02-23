// <copyright file="MainForm.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed under
//     GNU Affero General Public License. See LICENSE in project root for full
//     license information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.IO;
    using System.Windows.Forms;

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
                components = new Container();
            }

            ObjectListWindow = new ObjectListWindow
            {
                Owner = this
            };
            ObjectListWindow.SelectedIndexChanged += ObjectListWindow_SelectedIndexChanged;
            ObjectListWindow.EditItem += ObjectListWindow_EditItem;
            ObjectListWindow.FormClosing += ChildToolWindow_FormClosing;
            components.Add(ObjectListWindow);

            Timer = new System.Timers.Timer();
            components.Add(Timer);
            Timer.Interval = RefreshRate;
            Timer.Elapsed += (s, e) => Animate();
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
                        AreaNumberChanged;
                    AllStarsRomFile.Smb1RomData.AreaHeaderChanged -=
                        Smb1RomData_AreaHeaderChanged;
                }

                _allStarsRomFile = value;
                if (AllStarsRomFile != null)
                {
                    AllStarsRomFile.Smb1RomData.AreaNumberChanged +=
                        AreaNumberChanged;
                    AllStarsRomFile.Smb1RomData.AreaHeaderChanged +=
                        Smb1RomData_AreaHeaderChanged;
                }

                OnRomFileChanged(EventArgs.Empty);
            }
        }

        public Smb1.GameData Smb1GameData
        {
            get
            {
                return AllStarsRomFile?.Smb1RomData;
            }
        }

        private ObjectListWindow ObjectListWindow
        {
            get;
        }

        private System.Timers.Timer Timer
        {
            get;
        }

        private DateTime StartTime
        {
            get;
            set;
        }

        private int Frame
        {
            get
            {
                return (int)(ElapsedTime.TotalSeconds * GameFramesPerSecond);
            }
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
                Open(openFileDialog.FileName);
            }
        }

        public void Open(string path)
        {
            AllStarsRomFile = new AllStarsRomFile(path);
        }

        public void LoadBG1()
        {
            areaControl.BG1 = Smb1GameData.ReadBG1Tiles();
            LoadSprites();

            areaControl.ObjectRectangles = Smb1GameData.GetObjectRectangles().ToArray();
            areaControl.SpriteRectangles = Smb1GameData.GetSpriteRectangles().ToArray();
        }

        protected virtual void OnRomFileChanged(EventArgs e)
        {
            SuspendLayout();
            UpdateMenuEnabled();
            ObjectListWindow.Items.Clear();
            if (!(Smb1GameData is null))
            {
                ObjectListWindow.Items.AddRange(Smb1GameData.ObjectData);
                ttbJumpToArea.Text = Smb1GameData.AreaNumber.ToString("X2");
                Smb1GameData.UpdateAnimatedPixelData(0);
                LoadPalette();
                areaControl.PixelData = Smb1GameData.PixelData;
                LoadBG1();

                StartTime = DateTime.Now;
                Timer.Start();
            }
            else
            {
                Timer.Stop();
            }
            ResumeLayout();
        }

        private void Animate()
        {
            if (Smb1GameData is null)
            {
                return;
            }

            Smb1GameData.UpdateAnimatedPixelData(Frame);
            LoadSprites();

            areaControl.Invalidate();
        }

        private void LoadPalette()
        {
            areaControl.Palette = Smb1GameData.Palette;
        }

        private void LoadSprites()
        {
            lock (areaControl.Sprites)
            {
                areaControl.Sprites.Clear();
                areaControl.Sprites.AddRange(Smb1GameData.EnumerateSprites(Frame));
            }
        }

        private void AreaNumberChanged(object sender, EventArgs e)
        {
            SuspendLayout();
            ttbJumpToArea.Text = Smb1GameData.AreaNumber.ToString("X2");
            ObjectListWindow.Items.Clear();
            ObjectListWindow.Items.AddRange(Smb1GameData.ObjectData);
            areaScrollBar.Value = 0;
            LoadPalette();
            LoadBG1();
            ResumeLayout();
        }

        private void ChildToolWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                ObjectListWindow.Visible =
                tsmViewObjectListWindow.Checked = false;
                e.Cancel = true;
            }
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
            tsbSave.Enabled =
            tsbJumpToArea.Enabled =
            ttbJumpToArea.Enabled = AllStarsRomFile != null;

            ObjectListWindow.Visible = AllStarsRomFile != null;
        }

        private void Open_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
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
            Smb1GameData.AreaNumber = Int32.Parse(
                ttbJumpToArea.Text,
                NumberStyles.HexNumber,
                CultureInfo.CurrentCulture);
        }

        private void JumpToArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tsbJumpToArea.PerformClick();
                e.SuppressKeyPress = true;
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
                Smb1RomData = Smb1GameData
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Smb1GameData.AreaNumber = dialog.Area;
            }
        }

        private void ExportTileData_Click(object sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog();
            dialog.DefaultExt = ".bin";
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var data = new byte[0x20 * 0x10 * 0x0D];
                for (var i = 0; i < data.Length; i++)
                {
                    data[i] = (byte)Smb1GameData.AreaObjectRenderer.TileMap[i + (2 * 0x20 * 0x10)];
                }

                File.WriteAllBytes(dialog.FileName, data);
            }
        }

        private void ViewObjectListWindow_CheckedChanged(object sender, EventArgs e)
        {
            ObjectListWindow.Visible = tsmViewObjectListWindow.Checked;
        }

        private void EditHeader_Click(object sender, EventArgs e)
        {
            var dialog = new HeaderEditorForm
            {
                AreaHeader = Smb1GameData.AreaHeader
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                Smb1GameData.AreaHeader = dialog.AreaHeader;
                Smb1GameData.RenderAreaTilemap();
                LoadBG1();
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Smb1GameData.WriteArea();
            Smb1GameData.Save();

            File.WriteAllBytes("Test ROM.sfc", Smb1GameData.Rom.Data);
        }

        private void AreaControl_MouseClick(object sender, MouseEventArgs e)
        {
            var loc = new Point(e.X + (areaControl.StartX << 3), e.Y);
            ObjectListWindow.SelectedIndex = Smb1GameData.GetObjectIndex(loc);
            areaControl.Invalidate();
        }

        public void EditCurrentItem()
        {
            if (ObjectListWindow.SelectedIndex == -1)
            {
                return;
            }

            var oldCommand = Smb1GameData.ObjectData[ObjectListWindow.SelectedIndex];
            using var dialog = new ObjectEditorForm();
            dialog.AreaObjectCommand = Smb1GameData.ObjectData[
                ObjectListWindow.SelectedIndex];
            dialog.UseManual = true;
            dialog.AreaPlatformType = Smb1GameData.AreaHeader.AreaPlatformType;
            dialog.AreaObjectCommandChanged += Dialog_AreaObjectCommandChanged;
            switch (dialog.ShowDialog(owner: this))
            {
            case DialogResult.OK:
                ObjectListWindow.Items[ObjectListWindow.SelectedIndex] =
                    dialog.AreaObjectCommand;
                break;
            default:
                Smb1GameData.ObjectData[ObjectListWindow.SelectedIndex] = oldCommand;
                Smb1GameData.RenderAreaTilemap();
                LoadBG1();
                break;
            }
        }

        private void Smb1RomData_AreaHeaderChanged(object sender, EventArgs e)
        {
            ObjectListWindow.AreaPlatformType =
                Smb1GameData.AreaHeader.AreaPlatformType;
        }

        private void Dialog_AreaObjectCommandChanged(object sender, EventArgs e)
        {
            var dialog = sender as ObjectEditorForm;
            Smb1GameData.ObjectData[ObjectListWindow.SelectedIndex] =
                dialog.AreaObjectCommand;
            Smb1GameData.RenderAreaTilemap();
            LoadBG1();
        }

        private void AreaControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditCurrentItem();
        }

        private void ObjectListWindow_SelectedIndexChanged(object sender, EventArgs e)
        {
            areaControl.ObjectIndex = ObjectListWindow.SelectedIndex;
        }

        private void ObjectListWindow_EditItem(object sender, EventArgs e)
        {
            EditCurrentItem();
        }
    }
}
