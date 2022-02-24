// <copyright file="MainForm.Designer.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.tsmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLoadArea = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExportTileData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEditHeader = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSpriteMode = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmViewObjectListWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPlayer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmMario = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLuigi = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPlayerState = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmStateSmall = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmStateBig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmStateFire = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tslJumpToArea = new System.Windows.Forms.ToolStripLabel();
            this.ttbJumpToArea = new System.Windows.Forms.ToolStripTextBox();
            this.tsbJumpToArea = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLoadAreaByLevel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCut = new System.Windows.Forms.ToolStripButton();
            this.tsbCopy = new System.Windows.Forms.ToolStripButton();
            this.tsbPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.areaScrollBar = new System.Windows.Forms.HScrollBar();
            this.areaControl = new Brutario.AreaControl();
            this.mnuMain.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmFile,
            this.tsmLevel,
            this.tsmView});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1384, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // tsmFile
            // 
            this.tsmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmOpen,
            this.toolStripSeparator1,
            this.tsmExit});
            this.tsmFile.Name = "tsmFile";
            this.tsmFile.Size = new System.Drawing.Size(37, 20);
            this.tsmFile.Text = "&File";
            // 
            // tsmOpen
            // 
            this.tsmOpen.Name = "tsmOpen";
            this.tsmOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmOpen.Size = new System.Drawing.Size(146, 22);
            this.tsmOpen.Text = "&Open";
            this.tsmOpen.Click += new System.EventHandler(this.Open_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // tsmExit
            // 
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(146, 22);
            this.tsmExit.Text = "E&xit";
            this.tsmExit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // tsmLevel
            // 
            this.tsmLevel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmLoadArea,
            this.tsmExportTileData,
            this.tsmEditHeader,
            this.tsmSpriteMode});
            this.tsmLevel.Name = "tsmLevel";
            this.tsmLevel.Size = new System.Drawing.Size(46, 20);
            this.tsmLevel.Text = "Level";
            // 
            // tsmLoadArea
            // 
            this.tsmLoadArea.Name = "tsmLoadArea";
            this.tsmLoadArea.Size = new System.Drawing.Size(156, 22);
            this.tsmLoadArea.Text = "Load Area";
            // 
            // tsmExportTileData
            // 
            this.tsmExportTileData.Name = "tsmExportTileData";
            this.tsmExportTileData.Size = new System.Drawing.Size(156, 22);
            this.tsmExportTileData.Text = "Export Tile Data";
            this.tsmExportTileData.Click += new System.EventHandler(this.ExportTileData_Click);
            // 
            // tsmEditHeader
            // 
            this.tsmEditHeader.Name = "tsmEditHeader";
            this.tsmEditHeader.Size = new System.Drawing.Size(156, 22);
            this.tsmEditHeader.Text = "Edit Header";
            this.tsmEditHeader.Click += new System.EventHandler(this.EditHeader_Click);
            // 
            // tsmSpriteMode
            // 
            this.tsmSpriteMode.CheckOnClick = true;
            this.tsmSpriteMode.Name = "tsmSpriteMode";
            this.tsmSpriteMode.Size = new System.Drawing.Size(156, 22);
            this.tsmSpriteMode.Text = "Sprite Mode";
            this.tsmSpriteMode.CheckedChanged += new System.EventHandler(this.SpriteMode_CheckedChanged);
            // 
            // tsmView
            // 
            this.tsmView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmViewObjectListWindow,
            this.tsmPlayer,
            this.tsmPlayerState});
            this.tsmView.Name = "tsmView";
            this.tsmView.Size = new System.Drawing.Size(44, 20);
            this.tsmView.Text = "&View";
            // 
            // tsmViewObjectListWindow
            // 
            this.tsmViewObjectListWindow.CheckOnClick = true;
            this.tsmViewObjectListWindow.Name = "tsmViewObjectListWindow";
            this.tsmViewObjectListWindow.Size = new System.Drawing.Size(135, 22);
            this.tsmViewObjectListWindow.Text = "&Object List";
            this.tsmViewObjectListWindow.CheckedChanged += new System.EventHandler(this.ViewObjectListWindow_CheckedChanged);
            // 
            // tsmPlayer
            // 
            this.tsmPlayer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmMario,
            this.tsmLuigi});
            this.tsmPlayer.Name = "tsmPlayer";
            this.tsmPlayer.Size = new System.Drawing.Size(135, 22);
            this.tsmPlayer.Text = "&Player";
            // 
            // tsmMario
            // 
            this.tsmMario.Name = "tsmMario";
            this.tsmMario.Size = new System.Drawing.Size(105, 22);
            this.tsmMario.Text = "&Mario";
            this.tsmMario.Click += new System.EventHandler(this.Player_Click);
            // 
            // tsmLuigi
            // 
            this.tsmLuigi.Name = "tsmLuigi";
            this.tsmLuigi.Size = new System.Drawing.Size(105, 22);
            this.tsmLuigi.Text = "&Luigi";
            this.tsmLuigi.Click += new System.EventHandler(this.Player_Click);
            // 
            // tsmPlayerState
            // 
            this.tsmPlayerState.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmStateSmall,
            this.tsmStateBig,
            this.tsmStateFire});
            this.tsmPlayerState.Name = "tsmPlayerState";
            this.tsmPlayerState.Size = new System.Drawing.Size(135, 22);
            this.tsmPlayerState.Text = "Player &State";
            // 
            // tsmStateSmall
            // 
            this.tsmStateSmall.Name = "tsmStateSmall";
            this.tsmStateSmall.Size = new System.Drawing.Size(103, 22);
            this.tsmStateSmall.Text = "&Small";
            this.tsmStateSmall.Click += new System.EventHandler(this.PlayerState_Click);
            // 
            // tsmStateBig
            // 
            this.tsmStateBig.Name = "tsmStateBig";
            this.tsmStateBig.Size = new System.Drawing.Size(103, 22);
            this.tsmStateBig.Text = "&Big";
            this.tsmStateBig.Click += new System.EventHandler(this.PlayerState_Click);
            // 
            // tsmStateFire
            // 
            this.tsmStateFire.Name = "tsmStateFire";
            this.tsmStateFire.Size = new System.Drawing.Size(103, 22);
            this.tsmStateFire.Text = "&Fire";
            this.tsmStateFire.Click += new System.EventHandler(this.PlayerState_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "sfc";
            this.openFileDialog.Filter = "ROM Image (*.sfc;*.smc)|*.smc;*.sfc|All files|*.*";
            this.openFileDialog.Title = "Open Super Mario All-Stars ROM";
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpen,
            this.tsbSave,
            this.toolStripSeparator,
            this.tslJumpToArea,
            this.ttbJumpToArea,
            this.tsbJumpToArea,
            this.toolStripSeparator4,
            this.tsbLoadAreaByLevel,
            this.toolStripSeparator3,
            this.tsbCut,
            this.tsbCopy,
            this.tsbPaste,
            this.toolStripSeparator2,
            this.tsbHelp});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1384, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbOpen.Text = "&Open";
            this.tsbOpen.Click += new System.EventHandler(this.Open_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Enabled = false;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Text = "&Save";
            this.tsbSave.Click += new System.EventHandler(this.Save_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // tslJumpToArea
            // 
            this.tslJumpToArea.Name = "tslJumpToArea";
            this.tslJumpToArea.Size = new System.Drawing.Size(93, 22);
            this.tslJumpToArea.Text = "Jump to area: 0x";
            // 
            // ttbJumpToArea
            // 
            this.ttbJumpToArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ttbJumpToArea.Enabled = false;
            this.ttbJumpToArea.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ttbJumpToArea.Name = "ttbJumpToArea";
            this.ttbJumpToArea.Size = new System.Drawing.Size(100, 25);
            this.ttbJumpToArea.Text = "0";
            this.ttbJumpToArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.JumpToArea_KeyDown);
            this.ttbJumpToArea.TextChanged += new System.EventHandler(this.JumpToArea_TextChanged);
            // 
            // tsbJumpToArea
            // 
            this.tsbJumpToArea.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbJumpToArea.Enabled = false;
            this.tsbJumpToArea.Image = ((System.Drawing.Image)(resources.GetObject("tsbJumpToArea.Image")));
            this.tsbJumpToArea.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbJumpToArea.Name = "tsbJumpToArea";
            this.tsbJumpToArea.Size = new System.Drawing.Size(23, 22);
            this.tsbJumpToArea.Text = "Jump to area";
            this.tsbJumpToArea.Click += new System.EventHandler(this.JumpToArea_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbLoadAreaByLevel
            // 
            this.tsbLoadAreaByLevel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLoadAreaByLevel.Enabled = false;
            this.tsbLoadAreaByLevel.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadAreaByLevel.Image")));
            this.tsbLoadAreaByLevel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadAreaByLevel.Name = "tsbLoadAreaByLevel";
            this.tsbLoadAreaByLevel.Size = new System.Drawing.Size(23, 22);
            this.tsbLoadAreaByLevel.Text = "Load area by level";
            this.tsbLoadAreaByLevel.Click += new System.EventHandler(this.LoadAreaByLevel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbCut
            // 
            this.tsbCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCut.Enabled = false;
            this.tsbCut.Image = ((System.Drawing.Image)(resources.GetObject("tsbCut.Image")));
            this.tsbCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCut.Name = "tsbCut";
            this.tsbCut.Size = new System.Drawing.Size(23, 22);
            this.tsbCut.Text = "C&ut";
            // 
            // tsbCopy
            // 
            this.tsbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCopy.Enabled = false;
            this.tsbCopy.Image = ((System.Drawing.Image)(resources.GetObject("tsbCopy.Image")));
            this.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopy.Name = "tsbCopy";
            this.tsbCopy.Size = new System.Drawing.Size(23, 22);
            this.tsbCopy.Text = "&Copy";
            // 
            // tsbPaste
            // 
            this.tsbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPaste.Enabled = false;
            this.tsbPaste.Image = ((System.Drawing.Image)(resources.GetObject("tsbPaste.Image")));
            this.tsbPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPaste.Name = "tsbPaste";
            this.tsbPaste.Size = new System.Drawing.Size(23, 22);
            this.tsbPaste.Text = "&Paste";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbHelp
            // 
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(23, 22);
            this.tsbHelp.Text = "He&lp";
            // 
            // areaScrollBar
            // 
            this.areaScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.areaScrollBar.Location = new System.Drawing.Point(0, 743);
            this.areaScrollBar.Name = "areaScrollBar";
            this.areaScrollBar.Size = new System.Drawing.Size(1384, 17);
            this.areaScrollBar.TabIndex = 4;
            this.areaScrollBar.ValueChanged += new System.EventHandler(this.AreaScrollBar_ValueChanged);
            // 
            // areaControl
            // 
            this.areaControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.areaControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.areaControl.Location = new System.Drawing.Point(0, 52);
            this.areaControl.Name = "areaControl";
            this.areaControl.Size = new System.Drawing.Size(1384, 290);
            this.areaControl.TabIndex = 3;
            this.areaControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.AreaControl_MouseDoubleClick);
            this.areaControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AreaControl_MouseClick);
            this.areaControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AreaControl_MouseMove);
            this.areaControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AreaControl_MouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 761);
            this.Controls.Add(this.areaScrollBar);
            this.Controls.Add(this.areaControl);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.MinimumSize = new System.Drawing.Size(788, 399);
            this.Name = "MainForm";
            this.Text = "Brutario";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem tsmFile;
        private System.Windows.Forms.ToolStripMenuItem tsmOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton tsbCut;
        private System.Windows.Forms.ToolStripButton tsbCopy;
        private System.Windows.Forms.ToolStripButton tsbPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbHelp;
        private System.Windows.Forms.ToolStripLabel tslJumpToArea;
        private System.Windows.Forms.ToolStripTextBox ttbJumpToArea;
        private System.Windows.Forms.ToolStripButton tsbJumpToArea;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private AreaControl areaControl;
        private System.Windows.Forms.HScrollBar areaScrollBar;
        private System.Windows.Forms.ToolStripMenuItem tsmLevel;
        private System.Windows.Forms.ToolStripMenuItem tsmLoadArea;
        private System.Windows.Forms.ToolStripButton tsbLoadAreaByLevel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmExportTileData;
        private System.Windows.Forms.ToolStripMenuItem tsmView;
        private System.Windows.Forms.ToolStripMenuItem tsmViewObjectListWindow;
        private System.Windows.Forms.ToolStripMenuItem tsmEditHeader;
        private System.Windows.Forms.ToolStripMenuItem tsmSpriteMode;
        private System.Windows.Forms.ToolStripMenuItem tsmPlayer;
        private System.Windows.Forms.ToolStripMenuItem tsmMario;
        private System.Windows.Forms.ToolStripMenuItem tsmLuigi;
        private System.Windows.Forms.ToolStripMenuItem tsmPlayerState;
        private System.Windows.Forms.ToolStripMenuItem tsmStateSmall;
        private System.Windows.Forms.ToolStripMenuItem tsmStateBig;
        private System.Windows.Forms.ToolStripMenuItem tsmStateFire;
    }
}

