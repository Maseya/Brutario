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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tslJumpToArea = new System.Windows.Forms.ToolStripLabel();
            this.ttbJumpToArea = new System.Windows.Forms.ToolStripTextBox();
            this.tsbJumpToArea = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.areaScrollBar = new System.Windows.Forms.HScrollBar();
            this.areaControl = new Brutario.AreaControl();
            this.map16Control = new Brutario.Map16Control();
            this.gfxControl = new Brutario.GfxControl();
            this.paletteControl = new Brutario.PaletteControl();
            this.mnuMain.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmFile});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(772, 24);
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
            this.saveToolStripButton,
            this.toolStripSeparator,
            this.tslJumpToArea,
            this.ttbJumpToArea,
            this.tsbJumpToArea,
            this.toolStripSeparator3,
            this.cutToolStripButton,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            this.toolStripSeparator2,
            this.helpToolStripButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(772, 25);
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
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Enabled = false;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // tslJumpToArea
            // 
            this.tslJumpToArea.Name = "tslJumpToArea";
            this.tslJumpToArea.Size = new System.Drawing.Size(92, 22);
            this.tslJumpToArea.Text = "Jump to area: 0x";
            // 
            // ttbJumpToArea
            // 
            this.ttbJumpToArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ttbJumpToArea.Enabled = false;
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
            this.areaScrollBar.Location = new System.Drawing.Point(0, 889);
            this.areaScrollBar.Name = "areaScrollBar";
            this.areaScrollBar.Size = new System.Drawing.Size(772, 17);
            this.areaScrollBar.TabIndex = 4;
            this.areaScrollBar.ValueChanged += new System.EventHandler(this.AreaScrollBar_ValueChanged);
            // 
            // areaControl
            // 
            this.areaControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.areaControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.areaControl.Location = new System.Drawing.Point(0, 53);
            this.areaControl.Name = "areaControl";
            this.areaControl.Size = new System.Drawing.Size(772, 578);
            this.areaControl.Sprites = null;
            this.areaControl.TabIndex = 3;
            // 
            // map16Control
            // 
            this.map16Control.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.map16Control.Location = new System.Drawing.Point(514, 630);
            this.map16Control.Name = "map16Control";
            this.map16Control.Size = new System.Drawing.Size(258, 258);
            this.map16Control.TabIndex = 2;
            // 
            // gfxControl
            // 
            this.gfxControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gfxControl.Location = new System.Drawing.Point(257, 630);
            this.gfxControl.Name = "gfxControl";
            this.gfxControl.Size = new System.Drawing.Size(258, 258);
            this.gfxControl.TabIndex = 1;
            // 
            // paletteControl
            // 
            this.paletteControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paletteControl.Location = new System.Drawing.Point(0, 630);
            this.paletteControl.Name = "paletteControl";
            this.paletteControl.Size = new System.Drawing.Size(258, 258);
            this.paletteControl.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 907);
            this.Controls.Add(this.areaScrollBar);
            this.Controls.Add(this.areaControl);
            this.Controls.Add(this.map16Control);
            this.Controls.Add(this.gfxControl);
            this.Controls.Add(this.paletteControl);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.MinimumSize = new System.Drawing.Size(788, 658);
            this.Name = "MainForm";
            this.Text = "Brutario";
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
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
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
        private PaletteControl paletteControl;
        private GfxControl gfxControl;
        private Map16Control map16Control;
        private AreaControl areaControl;
        private System.Windows.Forms.HScrollBar areaScrollBar;
    }
}

