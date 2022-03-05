// <copyright file="MainForm.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed under GNU
//     Affero General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
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
            areaControl.MouseWheel += AreaControl_MouseWheel;

            Timer = new System.Timers.Timer
            {
                Interval = RefreshRate
            };
            Timer.Elapsed += (s, e) => Animate();

            if (components is null)
            {
                components = new Container();
            }

            components.Add(Timer);

            objectListDialog.Owner = this;
        }

        private AllStarsRomFile AllStarsRomFile
        {
            get
            {
                return _allStarsRomFile;
            }

            set
            {
                if (AllStarsRomFile == value)
                {
                    return;
                }

                if (AllStarsRomFile != null)
                {
                    AllStarsRomFile.Smb1GameData.AreaNumberChanged -=
                        Smb1RomData_AreaNumberChanged;
                    AllStarsRomFile.Smb1GameData.AreaHeaderChanged -=
                        Smb1RomData_AreaHeaderChanged;
                    AllStarsRomFile.Smb1GameData.PlayerChanged -=
                        Smb1RomData_PlayerChanged;
                    AllStarsRomFile.Smb1GameData.PlayerStateChanged -=
                        Smb1RomData_PlayerStateChanged;

                    AllStarsRomFile.Smb1GameData.ObjectData.ItemEdited -=
                        ObjectData_ItemEdited;
                    AllStarsRomFile.Smb1GameData.ObjectData.ItemInserted -=
                        ObjectData_ItemInserted;
                    AllStarsRomFile.Smb1GameData.ObjectData.ItemMoved -=
                        ObjectData_ItemMoved;
                    AllStarsRomFile.Smb1GameData.ObjectData.ItemRemoved -=
                        ObjectData_ItemRemoved;
                    AllStarsRomFile.Smb1GameData.ObjectData.DataReset -=
                        ObjectData_DataReset;
                    AllStarsRomFile.Smb1GameData.ObjectData.DataCleared -=
                        ObjectData_DataCleared;

                    AllStarsRomFile.Smb1GameData.SpriteData.ItemEdited -=
                        SpriteData_ItemEdited;
                    AllStarsRomFile.Smb1GameData.SpriteData.ItemInserted -=
                        SpriteData_ItemInserted;
                    AllStarsRomFile.Smb1GameData.SpriteData.ItemMoved -=
                        SpriteData_ItemMoved;
                    AllStarsRomFile.Smb1GameData.SpriteData.ItemRemoved -=
                        SpriteData_ItemRemoved;
                    AllStarsRomFile.Smb1GameData.SpriteData.DataReset -=
                        SpriteData_DataReset;
                    AllStarsRomFile.Smb1GameData.SpriteData.DataCleared -=
                        SpriteData_DataCleared;
                }

                _allStarsRomFile = value;
                if (AllStarsRomFile != null)
                {
                    AllStarsRomFile.Smb1GameData.AreaNumberChanged +=
                        Smb1RomData_AreaNumberChanged;
                    AllStarsRomFile.Smb1GameData.AreaHeaderChanged +=
                        Smb1RomData_AreaHeaderChanged;
                    AllStarsRomFile.Smb1GameData.PlayerChanged +=
                        Smb1RomData_PlayerChanged;
                    AllStarsRomFile.Smb1GameData.PlayerStateChanged +=
                        Smb1RomData_PlayerStateChanged;

                    AllStarsRomFile.Smb1GameData.ObjectData.ItemEdited +=
                        ObjectData_ItemEdited;
                    AllStarsRomFile.Smb1GameData.ObjectData.ItemInserted +=
                        ObjectData_ItemInserted;
                    AllStarsRomFile.Smb1GameData.ObjectData.ItemMoved +=
                        ObjectData_ItemMoved;
                    AllStarsRomFile.Smb1GameData.ObjectData.ItemRemoved +=
                        ObjectData_ItemRemoved;
                    AllStarsRomFile.Smb1GameData.ObjectData.DataReset +=
                        ObjectData_DataReset;
                    AllStarsRomFile.Smb1GameData.ObjectData.DataCleared +=
                        ObjectData_DataCleared;

                    AllStarsRomFile.Smb1GameData.SpriteData.ItemEdited +=
                        SpriteData_ItemEdited;
                    AllStarsRomFile.Smb1GameData.SpriteData.ItemInserted +=
                        SpriteData_ItemInserted;
                    AllStarsRomFile.Smb1GameData.SpriteData.ItemMoved +=
                        SpriteData_ItemMoved;
                    AllStarsRomFile.Smb1GameData.SpriteData.ItemRemoved +=
                        SpriteData_ItemRemoved;
                    AllStarsRomFile.Smb1GameData.SpriteData.DataReset +=
                        SpriteData_DataReset;
                    AllStarsRomFile.Smb1GameData.SpriteData.DataCleared +=
                        SpriteData_DataCleared;
                }

                OnRomFileChanged(EventArgs.Empty);
            }
        }

        private Smb1.GameData Smb1GameData
        {
            get
            {
                return AllStarsRomFile?.Smb1GameData;
            }
        }

        private Smb1.Player Player
        {
            get
            {
                return tsrbMario.Checked ? Smb1.Player.Mario : Smb1.Player.Luigi;
            }

            set
            {
                tsrbMario.Checked = value == Smb1.Player.Mario;
                tsrbLuigi.Checked = value == Smb1.Player.Luigi;
            }
        }

        private Smb1.PlayerState PlayerState
        {
            get
            {
                return tsrbSmall.Checked
                    ? Smb1.PlayerState.Small
                    : tsrbBig.Checked
                    ? Smb1.PlayerState.Big
                    : Smb1.PlayerState.Fire;
            }

            set
            {
                tsrbSmall.Checked = value == Smb1.PlayerState.Small;
                tsrbBig.Checked = value == Smb1.PlayerState.Big;
                tsrbFire.Checked = value == Smb1.PlayerState.Fire;
            }
        }

        private System.Timers.Timer Timer
        {
            get;
        }

        private bool BeginMouseMove
        {
            get;
            set;
        }

        private Point StartMousePoint
        {
            get;
            set;
        }

        private int SavedPage
        {
            get;
            set;
        }

        private DateTime StartTime
        {
            get;
            set;
        }

        private bool SpriteMode
        {
            get
            {
                return tsmSpriteMode.Checked;
            }

            set
            {
                tsmSpriteMode.Checked = value;
            }
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

        public void Open(string path)
        {
            AllStarsRomFile = new AllStarsRomFile(path);
        }

        protected virtual void OnRomFileChanged(EventArgs e)
        {
            SuspendLayout();
            UpdateMenuEnabled();
            if (!(Smb1GameData is null))
            {
                Player = Smb1GameData.Player;
                PlayerState = Smb1GameData.PlayerState;
                ResetDataListWindowContents();
                ttbJumpToArea.Text = Smb1GameData.AreaNumber.ToString("X2");
                Smb1GameData.UpdateAnimatedPixelData(0);

                StartTime = DateTime.Now;
                Timer.Start();
            }
            else
            {
                Timer.Stop();
            }

            ResumeLayout();
        }

        private void Open()
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

        private void SaveAs()
        {
            saveFileDialog.FileName = AllStarsRomFile.Path;
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                SaveAs(saveFileDialog.FileName);
            }
        }

        private void SaveAs(string path)
        {
            AllStarsRomFile.Path = path;
            Save();
        }

        private void Save()
        {
            Smb1GameData.WriteArea();
            Smb1GameData.Save();

            File.WriteAllBytes(AllStarsRomFile.Path, Smb1GameData.Rom.Data);
        }

        private void EditCurrentItem()
        {
            if (SpriteMode)
            {
                EditSprite();
            }
            else
            {
                EditObject();
            }
        }

        private void ResetDataListWindowContents()
        {
            objectListDialog.Items.Clear();
            if (SpriteMode)
            {
                var pages = Smb1GameData.SpriteData.EnumeratePositions().Select(
                    p => p.x >> 4);
                var items = Smb1GameData.SpriteData.Zip(
                    pages, (c, p) => CreateItem(c, p));
                objectListDialog.Items.AddRange(items);
            }
            else
            {
                var pages = Smb1GameData.ObjectData.EnumeratePositions().Select(
                    p => p.x >> 4);
                var items = Smb1GameData.ObjectData.Zip(
                    pages, (c, p) => CreateItem(c, p));
                objectListDialog.Items.AddRange(items);
            }
        }

        private void AddObject()
        {
            var index = objectListDialog.SelectedIndex + 1;

            var oldCommand = new Smb1.AreaObjectCommand();
            if ((uint)(index - 1) < (uint)Smb1GameData.ObjectData.Count)
            {
                oldCommand = Smb1GameData.ObjectData[index - 1];
            }

            Smb1GameData.ObjectData.Insert(index, oldCommand);
            objectListDialog.SelectedIndex = index;

            using var dialog = new ObjectEditorForm();
            dialog.AreaObjectCommand = oldCommand;
            dialog.UpdatePlatformTypeDescription(Smb1GameData.AreaHeader.AreaPlatformType);
            dialog.AreaObjectCommandChanged += Dialog_AreaObjectCommandChanged;
            switch (dialog.ShowDialog(owner: this))
            {
            case DialogResult.OK:
                break;

            default:
                objectListDialog.SelectedIndex = index - 1;
                Smb1GameData.ObjectData.RemoveAt(index);
                break;
            }
        }

        private void AddSprite()
        {
            var index = objectListDialog.SelectedIndex + 1;

            var oldCommand = new Smb1.AreaSpriteCommand();
            if ((uint)(index - 1) < (uint)Smb1GameData.SpriteData.Count)
            {
                oldCommand = Smb1GameData.SpriteData[index - 1];
            }

            Smb1GameData.SpriteData.Insert(index, oldCommand);
            objectListDialog.SelectedIndex = index;

            using var dialog = new SpriteEditorForm();
            dialog.AreaSpriteCommand = oldCommand;
            dialog.AreaSpriteCommandChanged += Dialog_AreaSpriteCommandChanged;
            switch (dialog.ShowDialog(owner: this))
            {
            case DialogResult.OK:
                break;

            default:
                objectListDialog.SelectedIndex = index - 1;
                Smb1GameData.SpriteData.RemoveAt(index);
                break;
            }
        }

        private void Animate()
        {
            if (Smb1GameData is null)
            {
                return;
            }

            Smb1GameData.UpdateAnimatedPixelData(Frame);
            areaControl.Invalidate();
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

            objectListDialog.Visible = AllStarsRomFile != null;
        }

        private void EditObject()
        {
            if (objectListDialog.SelectedIndex == -1)
            {
                return;
            }

            var oldCommand = Smb1GameData.ObjectData[objectListDialog.SelectedIndex];
            using var dialog = new ObjectEditorForm();
            dialog.AreaObjectCommand = Smb1GameData.ObjectData[
                objectListDialog.SelectedIndex];
            dialog.UpdatePlatformTypeDescription(Smb1GameData.AreaHeader.AreaPlatformType);
            dialog.AreaObjectCommandChanged += Dialog_AreaObjectCommandChanged;
            switch (dialog.ShowDialog(owner: this))
            {
            case DialogResult.OK:
                break;

            default:
                Smb1GameData.ObjectData[objectListDialog.SelectedIndex] = oldCommand;
                Smb1GameData.RenderAreaTilemap();
                break;
            }
        }

        private void EditSprite()
        {
            if (objectListDialog.SelectedIndex == -1)
            {
                return;
            }

            var oldCommand = Smb1GameData.SpriteData[objectListDialog.SelectedIndex];
            using var dialog = new SpriteEditorForm();
            dialog.AreaSpriteCommand = Smb1GameData.SpriteData[
                objectListDialog.SelectedIndex];
            dialog.AreaSpriteCommandChanged += Dialog_AreaSpriteCommandChanged;
            switch (dialog.ShowDialog(owner: this))
            {
            case DialogResult.OK:
                break;

            default:
                Smb1GameData.SpriteData[objectListDialog.SelectedIndex] = oldCommand;
                break;
            }
        }

        private void UpdateObjectPages()
        {
            var pages = new List<int>(
                Smb1GameData.ObjectData.EnumeratePositions().Select(p => p.x >> 4));
            for (var i = 0; i < pages.Count; i++)
            {
                objectListDialog.Items.SetPage(i, pages[i]);
            }
        }

        private void UpdateSpritePages()
        {
            var pages = new List<int>(
                Smb1GameData.SpriteData.EnumeratePositions().Select(p => p.x >> 4));
            for (var i = 0; i < pages.Count; i++)
            {
                objectListDialog.Items.SetPage(i, pages[i]);
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

        private void Open_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void SaveAs_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Save();
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
                    out var areaNumber)
                && Smb1GameData.IsValidAreaNumber(areaNumber);
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

        private void AreaScrollBar_ValueChanged(object sender, EventArgs e)
        {
            areaControl.Invalidate();
        }

        private void LoadAreaByLevel_Click(object sender, EventArgs e)
        {
        }

        private void ExportTileData_Click(object sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog();
            dialog.DefaultExt = ".bin";
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                File.WriteAllBytes(dialog.FileName, Smb1GameData.ExportTileMap());
            }
        }

        private void ViewObjectListWindow_CheckedChanged(object sender, EventArgs e)
        {
            objectListDialog.Visible = tsmViewObjectListWindow.Checked;
        }

        private void EditHeader_Click(object sender, EventArgs e)
        {
            var oldHeader = Smb1GameData.AreaHeader;
            var dialog = new HeaderEditorForm
            {
                AreaHeader = Smb1GameData.AreaHeader
            };

            dialog.AreaHeaderChanged +=
                (s, e) => Smb1GameData.AreaHeader = dialog.AreaHeader;

            switch (dialog.ShowDialog(owner: this))
            {
            case DialogResult.OK:
                break;

            default:
                Smb1GameData.AreaHeader = oldHeader;
                break;
            }
        }

        private void Dialog_AreaSpriteCommandChanged(object sender, EventArgs e)
        {
            var dialog = sender as SpriteEditorForm;
            Smb1GameData.SpriteData[objectListDialog.SelectedIndex] =
                dialog.AreaSpriteCommand;
        }

        private void Dialog_AreaObjectCommandChanged(object sender, EventArgs e)
        {
            var dialog = sender as ObjectEditorForm;
            Smb1GameData.ObjectData[objectListDialog.SelectedIndex] =
                dialog.AreaObjectCommand;
        }

        private void SpriteMode_CheckedChanged(object sender, EventArgs e)
        {
            ResetDataListWindowContents();
            areaControl.Invalidate();
        }

        private void Smb1RomData_PlayerChanged(object sender, EventArgs e)
        {
            Player = Smb1GameData.Player;
            Smb1GameData.ReloadPalette();
        }

        private void Smb1RomData_PlayerStateChanged(object sender, EventArgs e)
        {
            PlayerState = Smb1GameData.PlayerState;
            Smb1GameData.ReloadPalette();
        }

        private void Smb1RomData_AreaHeaderChanged(object sender, EventArgs e)
        {
            Smb1GameData.RenderAreaTilemap();
            areaControl.Invalidate();
        }

        private void Smb1RomData_AreaNumberChanged(object sender, EventArgs e)
        {
            ttbJumpToArea.Text = Smb1GameData.AreaNumber.ToString("X2");
            ResetDataListWindowContents();
            areaScrollBar.Value = 0;
        }

        private void Player_CheckedChanged(object sender, EventArgs e)
        {
            Smb1GameData.Player = Player;
        }

        private void PlayerState_CheckedChanged(object sender, EventArgs e)
        {
            Smb1GameData.PlayerState = PlayerState;
        }

        private void AreaControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || BeginMouseMove)
            {
                return;
            }

            var loc = new Point((e.X >> 4) + areaScrollBar.Value, e.Y >> 4);
            objectListDialog.SelectedIndex = SpriteMode
                ? Smb1GameData.SpriteData.GetIndex(loc.X, loc.Y)
                : Smb1GameData.ObjectData.GetIndex(loc.X, loc.Y - 2);
            areaControl.Invalidate();
            BeginMouseMove = objectListDialog.SelectedIndex != -1;
            if (BeginMouseMove)
            {
                StartMousePoint = new Point(loc.X, loc.Y);
                SavedPage = StartMousePoint.X >> 4;
            }
        }

        private void AreaControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditCurrentItem();
        }

        private void AreaControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || !BeginMouseMove)
            {
                return;
            }

            objectListDialog.SelectedIndex = Smb1GameData.MoveObject(
                objectListDialog.SelectedIndex,
                new Point(e.X >> 4, (e.Y >> 4) - 2));
        }

        private void AreaControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                BeginMouseMove = false;
            }
        }

        private void AreaControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (objectListDialog.SelectedIndex != -1)
            {
                if (e.Delta < 0)
                {
                    ObjectListWindow_MoveItemDown_Click(sender, e);
                }
                else if (e.Delta > 0)
                {
                    ObjectListWindow_MoveItemUp_Click(sender, e);
                }
            }
        }

        private void ObjectListWindow_SelectedIndexChanged(object sender, EventArgs e)
        {
            areaControl.Invalidate();
        }

        private void ObjectListWindow_EditItem(object sender, EventArgs e)
        {
            EditCurrentItem();
        }

        private void ObjectListWindow_AddItem_Click(object sender, EventArgs e)
        {
            if (SpriteMode)
            {
                AddSprite();
            }
            else
            {
                AddObject();
            }
        }

        private void ObjectListWindow_DeleteItem_Click(object sender, EventArgs e)
        {
            var index = objectListDialog.SelectedIndex;
            if (SpriteMode && (uint)index < (uint)Smb1GameData.SpriteData.Count)
            {
                Smb1GameData.SpriteData.RemoveAt(index);
                objectListDialog.SelectedIndex = index;
            }
            else if ((uint)index < (uint)Smb1GameData.ObjectData.Count)
            {
                Smb1GameData.ObjectData.RemoveAt(index);
                objectListDialog.SelectedIndex = index;
            }
        }

        private void ObjectListWindow_ClearItems_Click(object sender, EventArgs e)
        {
            if (SpriteMode)
            {
                Smb1GameData.SpriteData.Clear();
            }
            else
            {
                Smb1GameData.ObjectData.Clear();
            }
        }

        private void ObjectListWindow_MoveItemUp_Click(object sender, EventArgs e)
        {
            var index = objectListDialog.SelectedIndex;
            if (SpriteMode && (uint)(index - 1) < (uint)Smb1GameData.SpriteData.Count)
            {
                Smb1GameData.SpriteData.MoveItem(index, index - 1);
                objectListDialog.SelectedIndex = index - 1;
            }
            else if ((uint)(index - 1) < (uint)Smb1GameData.ObjectData.Count)
            {
                Smb1GameData.ObjectData.MoveItem(index, index - 1);
                objectListDialog.SelectedIndex = index - 1;
            }
        }

        private void ObjectListWindow_MoveItemDown_Click(object sender, EventArgs e)
        {
            var index = objectListDialog.SelectedIndex;
            if (SpriteMode && (uint)index < (uint)(Smb1GameData.SpriteData.Count - 1))
            {
                Smb1GameData.SpriteData.MoveItem(index, index + 1);
                objectListDialog.SelectedIndex = index + 1;
            }
            else if ((uint)index < (uint)(Smb1GameData.ObjectData.Count - 1))
            {
                Smb1GameData.ObjectData.MoveItem(index, index + 1);
                objectListDialog.SelectedIndex = index + 1;
            }
        }

        private void ObjectListDialog_VisibleChanged(object sender, EventArgs e)
        {
            tsmViewObjectListWindow.Checked = objectListDialog.Visible;
        }

        private void ObjectData_DataCleared(
            object sender,
            DataClearedEventArgs<Smb1.AreaObjectCommand> e)
        {
            if (!SpriteMode)
            {
                objectListDialog.Items.Clear();
                Smb1GameData.RenderAreaTilemap();
            }
        }

        private void ObjectData_DataReset(
            object sender,
            DataResetEventArgs<Smb1.AreaObjectCommand> e)
        {
            if (!SpriteMode)
            {
                ResetDataListWindowContents();
                Smb1GameData.RenderAreaTilemap();
            }
        }

        private void ObjectData_ItemRemoved(
            object sender,
            ItemInsertedEventArgs<Smb1.AreaObjectCommand> e)
        {
            if (!SpriteMode)
            {
                objectListDialog.Items.RemoveAt(e.Index);
                UpdateObjectPages();
                Smb1GameData.RenderAreaTilemap();
            }
        }

        private void ObjectData_ItemMoved(
            object sender,
            ItemMovedEventArgs e)
        {
            if (!SpriteMode)
            {
                objectListDialog.Items.MoveItem(e.OldIndex, e.NewIndex);
                UpdateObjectPages();
                Smb1GameData.RenderAreaTilemap();
            }
        }

        private void ObjectData_ItemInserted(
            object sender,
            ItemInsertedEventArgs<Smb1.AreaObjectCommand> e)
        {
            if (!SpriteMode)
            {
                objectListDialog.Items.Insert(e.Index, CreateItem(e.Item, 0));
                UpdateObjectPages();
                Smb1GameData.RenderAreaTilemap();
            }
        }

        private void ObjectData_ItemEdited(
            object sender,
            ItemEditedEventArgs<Smb1.AreaObjectCommand> e)
        {
            if (!SpriteMode)
            {
                objectListDialog.Items[e.Index] = CreateItem(e.NewValue, 0);
                UpdateObjectPages();
                Smb1GameData.RenderAreaTilemap();
            }
        }

        private void SpriteData_DataCleared(
            object sender,
            DataClearedEventArgs<Smb1.AreaSpriteCommand> e)
        {
            if (SpriteMode)
            {
                objectListDialog.Items.Clear();
                Smb1GameData.RenderAreaTilemap();
            }
        }

        private void SpriteData_DataReset(
            object sender,
            DataResetEventArgs<Smb1.AreaSpriteCommand> e)
        {
            if (SpriteMode)
            {
                ResetDataListWindowContents();
                UpdateSpritePages();
                Smb1GameData.RenderAreaTilemap();
            }
        }

        private void SpriteData_ItemRemoved(
            object sender,
            ItemInsertedEventArgs<Smb1.AreaSpriteCommand> e)
        {
            if (SpriteMode)
            {
                objectListDialog.Items.RemoveAt(e.Index);
                UpdateSpritePages();
                Smb1GameData.RenderAreaTilemap();
            }
        }

        private void SpriteData_ItemMoved(
            object sender,
            ItemMovedEventArgs e)
        {
            if (SpriteMode)
            {
                objectListDialog.Items.MoveItem(e.OldIndex, e.NewIndex);
                UpdateSpritePages();
                Smb1GameData.RenderAreaTilemap();
            }
        }

        private void SpriteData_ItemInserted(
            object sender,
            ItemInsertedEventArgs<Smb1.AreaSpriteCommand> e)
        {
            if (SpriteMode)
            {
                objectListDialog.Items.Insert(e.Index, CreateItem(e.Item, 0));
                UpdateSpritePages();
                Smb1GameData.RenderAreaTilemap();
            }
        }

        private void SpriteData_ItemEdited(
            object sender,
            ItemEditedEventArgs<Smb1.AreaSpriteCommand> e)
        {
            if (SpriteMode)
            {
                objectListDialog.Items[e.Index] = CreateItem(e.NewValue, 0);
                UpdateSpritePages();
                Smb1GameData.RenderAreaTilemap();
            }
        }

        private ObjectListWindow.Item CreateItem(
            Smb1.AreaObjectCommand command,
            int page)
        {
            return new ObjectListWindow.Item
            {
                Description = command.FullName(
                AllStarsRomFile.Smb1GameData.AreaHeader.AreaPlatformType),
                Hex = $"{command.Value1:X2} {command.Value2:X2}"
                    + (command.IsThreeByteCommand
                        ? $"{command.Value3:X2}"
                        : String.Empty),
                X = command.X,
                Y = command.Y,
                Page = page
            };
        }

        private ObjectListWindow.Item CreateItem(
            Smb1.AreaSpriteCommand command,
            int page)
        {
            return new ObjectListWindow.Item
            {
                Description = command.FullName,
                Hex = $"{command.Value1:X2} {command.Value2:X2}"
                    + (command.IsThreeByteCommand
                        ? $"{command.Value3:X2}"
                        : String.Empty),
                X = command.X,
                Y = command.Y,
                Page = page
            };
        }

        private void AreaControl_Paint(object sender, PaintEventArgs e)
        {
            if (Smb1GameData is null)
            {
                return;
            }

            Smb1.AreaPixelRenderer.DrawArea(
                e.Graphics,
                new Color32BppArgb(0xFF, 0, 0, 0),
                Smb1GameData.Palette,
                Smb1GameData.PixelData,
                Smb1GameData.BG1,
                Smb1GameData.EnumerateSprites(Frame),
                areaScrollBar.Value,
                areaControl.Size,
                SpriteMode
                    ? Smb1GameData.GetSpriteRectangles().ToArray()
                    : Smb1GameData.GetObjectRectangles().ToArray(),
                objectListDialog.SelectedIndex,
                Color.Blue,
                Color.White,
                Color.LightGreen);
        }
    }
}
