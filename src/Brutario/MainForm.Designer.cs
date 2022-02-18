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
            this.levelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportTileDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEditHeader = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmViewObjectListWindow = new System.Windows.Forms.ToolStripMenuItem();
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
            this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
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
            this.levelToolStripMenuItem,
            this.viewToolStripMenuItem});
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
            // levelToolStripMenuItem
            // 
            this.levelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadAreaToolStripMenuItem,
            this.exportTileDataToolStripMenuItem,
            this.tsmEditHeader});
            this.levelToolStripMenuItem.Name = "levelToolStripMenuItem";
            this.levelToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.levelToolStripMenuItem.Text = "Level";
            // 
            // loadAreaToolStripMenuItem
            // 
            this.loadAreaToolStripMenuItem.Name = "loadAreaToolStripMenuItem";
            this.loadAreaToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.loadAreaToolStripMenuItem.Text = "Load Area";
            // 
            // exportTileDataToolStripMenuItem
            // 
            this.exportTileDataToolStripMenuItem.Name = "exportTileDataToolStripMenuItem";
            this.exportTileDataToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.exportTileDataToolStripMenuItem.Text = "Export Tile Data";
            this.exportTileDataToolStripMenuItem.Click += new System.EventHandler(this.ExportTileData_Click);
            // 
            // tsmEditHeader
            // 
            this.tsmEditHeader.Name = "tsmEditHeader";
            this.tsmEditHeader.Size = new System.Drawing.Size(156, 22);
            this.tsmEditHeader.Text = "Edit Header";
            this.tsmEditHeader.Click += new System.EventHandler(this.EditHeader_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmViewObjectListWindow});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // tsmViewObjectListWindow
            // 
            this.tsmViewObjectListWindow.CheckOnClick = true;
            this.tsmViewObjectListWindow.Name = "tsmViewObjectListWindow";
            this.tsmViewObjectListWindow.Size = new System.Drawing.Size(130, 22);
            this.tsmViewObjectListWindow.Text = "&Object List";
            this.tsmViewObjectListWindow.CheckedChanged += new System.EventHandler(this.ViewObjectListWindow_CheckedChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "sfc";
            this.openFileDialog.Filter = "ROM Image (*.sfc;*.smc)|*.smc;*.sfc|All files|*.*";
            this.openFileDialog.Title = "Open Super Mario All-Stars [U] file";
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
            this.cutToolStripButton,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            this.toolStripSeparator2,
            this.helpToolStripButton});
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
            // tsmSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Enabled = false;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsmSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsmSave";
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
            // cutToolStripButton
            // 
            this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutToolStripButton.Enabled = false;
            this.cutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripButton.Image")));
            this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripButton.Name = "cutToolStripButton";
            this.cutToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.cutToolStripButton.Text = "C&ut";
            // 
            // copyToolStripButton
            // 
            this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolStripButton.Enabled = false;
            this.copyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripButton.Image")));
            this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripButton.Name = "copyToolStripButton";
            this.copyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.copyToolStripButton.Text = "&Copy";
            // 
            // pasteToolStripButton
            // 
            this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteToolStripButton.Enabled = false;
            this.pasteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripButton.Image")));
            this.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripButton.Name = "pasteToolStripButton";
            this.pasteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.pasteToolStripButton.Text = "&Paste";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.helpToolStripButton.Text = "He&lp";
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
            this.areaControl.Sprites = null;
            this.areaControl.TabIndex = 3;
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
        private System.Windows.Forms.ToolStripButton cutToolStripButton;
        private System.Windows.Forms.ToolStripButton copyToolStripButton;
        private System.Windows.Forms.ToolStripButton pasteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolStripLabel tslJumpToArea;
        private System.Windows.Forms.ToolStripTextBox ttbJumpToArea;
        private System.Windows.Forms.ToolStripButton tsbJumpToArea;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private AreaControl areaControl;
        private System.Windows.Forms.HScrollBar areaScrollBar;
        private System.Windows.Forms.ToolStripMenuItem levelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbLoadAreaByLevel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem exportTileDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmViewObjectListWindow;
        private System.Windows.Forms.ToolStripMenuItem tsmEditHeader;
    }
}

