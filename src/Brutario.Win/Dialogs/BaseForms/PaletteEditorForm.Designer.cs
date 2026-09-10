namespace Brutario.Win.Dialogs.BaseForms;

partial class PaletteEditorForm
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
        components = new System.ComponentModel.Container();
        paletteControl = new Brutario.Win.Controls.DesignControl();
        gbxForeground = new GroupBox();
        cbxForegroundPalette = new ComboBox();
        gbxBackground = new GroupBox();
        cbxBackgroundPalette = new ComboBox();
        gbxSprites = new GroupBox();
        cbxSpritePalette = new ComboBox();
        statusStrip1 = new StatusStrip();
        tsslColor = new ToolStripStatusLabel();
        tsslRed = new ToolStripStatusLabel();
        tssGreen = new ToolStripStatusLabel();
        tsslBlue = new ToolStripStatusLabel();
        menuStrip1 = new MenuStrip();
        fileToolStripMenuItem = new ToolStripMenuItem();
        tsmSave = new ToolStripMenuItem();
        toolStripSeparator1 = new ToolStripSeparator();
        tsmImportPalette = new ToolStripMenuItem();
        tsmExportPalette = new ToolStripMenuItem();
        toolStripSeparator2 = new ToolStripSeparator();
        tsmClose = new ToolStripMenuItem();
        editToolStripMenuItem = new ToolStripMenuItem();
        tsmUndo = new ToolStripMenuItem();
        tsmRedo = new ToolStripMenuItem();
        toolStripSeparator3 = new ToolStripSeparator();
        tsmCopy = new ToolStripMenuItem();
        tsmPaste = new ToolStripMenuItem();
        toolStripSeparator4 = new ToolStripSeparator();
        tsmReset = new ToolStripMenuItem();
        toolStripSeparator7 = new ToolStripSeparator();
        tsmSelectAll = new ToolStripMenuItem();
        toolStrip1 = new ToolStrip();
        tsbSave = new ToolStripButton();
        toolStripSeparator5 = new ToolStripSeparator();
        tsbImportPalette = new ToolStripButton();
        tsbExportPalette = new ToolStripButton();
        toolStripSeparator6 = new ToolStripSeparator();
        tsbUndo = new ToolStripButton();
        tsbRedo = new ToolStripButton();
        toolStripSeparator9 = new ToolStripSeparator();
        tsbCopy = new ToolStripButton();
        tsbPaste = new ToolStripButton();
        toolStripSeparator8 = new ToolStripSeparator();
        tsbReset = new ToolStripButton();
        colorDialog = new ColorDialog();
        importPaletteFileDialog = new OpenFileDialog();
        exportPaletteFileDialog = new SaveFileDialog();
        exceptionView = new Brutario.Win.Views.ExceptionView(components);
        animationTimer = new System.Windows.Forms.Timer(components);
        gbxForeground.SuspendLayout();
        gbxBackground.SuspendLayout();
        gbxSprites.SuspendLayout();
        statusStrip1.SuspendLayout();
        menuStrip1.SuspendLayout();
        toolStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // paletteControl
        // 
        paletteControl.BorderStyle = BorderStyle.FixedSingle;
        paletteControl.Location = new Point(0, 51);
        paletteControl.Margin = new Padding(3, 2, 3, 2);
        paletteControl.Name = "paletteControl";
        paletteControl.Size = new Size(258, 258);
        paletteControl.TabIndex = 0;
        paletteControl.Paint += PaletteControl_Paint;
        paletteControl.MouseClick += PaletteControl_MouseClick;
        paletteControl.MouseMove += PaletteControl_MouseMove;
        // 
        // gbxForeground
        // 
        gbxForeground.Controls.Add(cbxForegroundPalette);
        gbxForeground.Location = new Point(264, 51);
        gbxForeground.Margin = new Padding(3, 2, 3, 2);
        gbxForeground.Name = "gbxForeground";
        gbxForeground.Padding = new Padding(3, 2, 3, 2);
        gbxForeground.Size = new Size(215, 47);
        gbxForeground.TabIndex = 1;
        gbxForeground.TabStop = false;
        gbxForeground.Text = "Foreground";
        // 
        // cbxForeground
        // 
        cbxForegroundPalette.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        cbxForegroundPalette.FormattingEnabled = true;
        cbxForegroundPalette.Items.AddRange(new object[] { "Normal", "Snow (Day)", "Snow (Night)", "Mushroom Island", "Mushroom Island (Warp Zone)", "Underground", "Castle", "Castle (Underwater)" });
        cbxForegroundPalette.Location = new Point(6, 20);
        cbxForegroundPalette.Margin = new Padding(3, 2, 3, 2);
        cbxForegroundPalette.Name = "cbxForeground";
        cbxForegroundPalette.Size = new Size(203, 23);
        cbxForegroundPalette.TabIndex = 0;
        cbxForegroundPalette.SelectedIndexChanged += AreaPalette_SelectedIndexChanged;
        // 
        // gbxBackground
        // 
        gbxBackground.Controls.Add(cbxBackgroundPalette);
        gbxBackground.Location = new Point(264, 102);
        gbxBackground.Margin = new Padding(3, 2, 3, 2);
        gbxBackground.Name = "gbxBackground";
        gbxBackground.Padding = new Padding(3, 2, 3, 2);
        gbxBackground.Size = new Size(215, 47);
        gbxBackground.TabIndex = 2;
        gbxBackground.TabStop = false;
        gbxBackground.Text = "Background";
        // 
        // cbxBackground
        // 
        cbxBackgroundPalette.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        cbxBackgroundPalette.FormattingEnabled = true;
        cbxBackgroundPalette.Items.AddRange(new object[] { "Normal", "Mountains", "Waterfall", "Goomba Pillars", "Green Peaks", "Orange Peaks", "Snow Peaks", "Starry Night", "Mushroom Island", "Castle Wall", "Bonus Room", "Underwater", "Underground", "Castle", "W8 Castle", "Castle (Underwater)" });
        cbxBackgroundPalette.Location = new Point(6, 20);
        cbxBackgroundPalette.Margin = new Padding(3, 2, 3, 2);
        cbxBackgroundPalette.Name = "cbxBackground";
        cbxBackgroundPalette.Size = new Size(203, 23);
        cbxBackgroundPalette.TabIndex = 0;
        cbxBackgroundPalette.SelectedIndexChanged += AreaPalette_SelectedIndexChanged;
        // 
        // gbxSprites
        // 
        gbxSprites.Controls.Add(cbxSpritePalette);
        gbxSprites.Location = new Point(264, 153);
        gbxSprites.Margin = new Padding(3, 2, 3, 2);
        gbxSprites.Name = "gbxSprites";
        gbxSprites.Padding = new Padding(3, 2, 3, 2);
        gbxSprites.Size = new Size(215, 47);
        gbxSprites.TabIndex = 3;
        gbxSprites.TabStop = false;
        gbxSprites.Text = "Sprites";
        // 
        // cbxSprites
        // 
        cbxSpritePalette.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        cbxSpritePalette.FormattingEnabled = true;
        cbxSpritePalette.Items.AddRange(new object[] { "Normal", "Underground", "Castle" });
        cbxSpritePalette.Location = new Point(6, 20);
        cbxSpritePalette.Margin = new Padding(3, 2, 3, 2);
        cbxSpritePalette.Name = "cbxSprites";
        cbxSpritePalette.Size = new Size(203, 23);
        cbxSpritePalette.TabIndex = 0;
        cbxSpritePalette.SelectedIndexChanged += AreaPalette_SelectedIndexChanged;
        // 
        // statusStrip1
        // 
        statusStrip1.Items.AddRange(new ToolStripItem[] { tsslColor, tsslRed, tssGreen, tsslBlue });
        statusStrip1.Location = new Point(0, 309);
        statusStrip1.Name = "statusStrip1";
        statusStrip1.Size = new Size(491, 22);
        statusStrip1.SizingGrip = false;
        statusStrip1.TabIndex = 10;
        statusStrip1.Text = "statusStrip1";
        // 
        // tsslColor
        // 
        tsslColor.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        tsslColor.Name = "tsslColor";
        tsslColor.Size = new Size(119, 17);
        tsslColor.Text = "0,0:000000(0000)";
        // 
        // tsslRed
        // 
        tsslRed.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        tsslRed.ForeColor = Color.Red;
        tsslRed.Name = "tsslRed";
        tsslRed.Size = new Size(28, 17);
        tsslRed.Text = "000";
        // 
        // tssGreen
        // 
        tssGreen.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        tssGreen.ForeColor = Color.Green;
        tssGreen.Name = "tssGreen";
        tssGreen.Size = new Size(28, 17);
        tssGreen.Text = "000";
        // 
        // tsslBlue
        // 
        tsslBlue.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        tsslBlue.ForeColor = Color.Blue;
        tsslBlue.Name = "tsslBlue";
        tsslBlue.Size = new Size(28, 17);
        tsslBlue.Text = "000";
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(491, 24);
        menuStrip1.TabIndex = 11;
        menuStrip1.Text = "menuStrip1";
        // 
        // fileToolStripMenuItem
        // 
        fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tsmSave, toolStripSeparator1, tsmImportPalette, tsmExportPalette, toolStripSeparator2, tsmClose });
        fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        fileToolStripMenuItem.Size = new Size(37, 20);
        fileToolStripMenuItem.Text = "&File";
        // 
        // tsmSave
        // 
        tsmSave.Enabled = false;
        tsmSave.Image = Properties.Resources.floppy_disk_regular;
        tsmSave.Name = "tsmSave";
        tsmSave.ShortcutKeys = Keys.Control | Keys.S;
        tsmSave.Size = new Size(211, 22);
        tsmSave.Text = "&Save";
        tsmSave.Click += Save_Click;
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        toolStripSeparator1.Size = new Size(208, 6);
        // 
        // tsmImportPalette
        // 
        tsmImportPalette.Image = Properties.Resources.file_import_solid;
        tsmImportPalette.Name = "tsmImportPalette";
        tsmImportPalette.ShortcutKeys = Keys.Control | Keys.Alt | Keys.I;
        tsmImportPalette.Size = new Size(211, 22);
        tsmImportPalette.Text = "&Import Palette";
        tsmImportPalette.Click += ImportPalette_Click;
        // 
        // tsmExportPalette
        // 
        tsmExportPalette.Image = Properties.Resources.file_export_solid;
        tsmExportPalette.Name = "tsmExportPalette";
        tsmExportPalette.ShortcutKeys = Keys.Control | Keys.Alt | Keys.X;
        tsmExportPalette.Size = new Size(211, 22);
        tsmExportPalette.Text = "E&xport Palette";
        tsmExportPalette.Click += ExportPalette_Click;
        // 
        // toolStripSeparator2
        // 
        toolStripSeparator2.Name = "toolStripSeparator2";
        toolStripSeparator2.Size = new Size(208, 6);
        // 
        // tsmClose
        // 
        tsmClose.Name = "tsmClose";
        tsmClose.ShortcutKeys = Keys.Control | Keys.F4;
        tsmClose.Size = new Size(211, 22);
        tsmClose.Text = "&Close";
        tsmClose.Click += Close_Click;
        // 
        // editToolStripMenuItem
        // 
        editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tsmUndo, tsmRedo, toolStripSeparator3, tsmCopy, tsmPaste, toolStripSeparator4, tsmReset, toolStripSeparator7, tsmSelectAll });
        editToolStripMenuItem.Name = "editToolStripMenuItem";
        editToolStripMenuItem.Size = new Size(39, 20);
        editToolStripMenuItem.Text = "&Edit";
        // 
        // tsmUndo
        // 
        tsmUndo.Enabled = false;
        tsmUndo.Image = Properties.Resources.rotate_left_solid;
        tsmUndo.Name = "tsmUndo";
        tsmUndo.ShortcutKeys = Keys.Control | Keys.Z;
        tsmUndo.Size = new Size(180, 22);
        tsmUndo.Text = "&Undo";
        tsmUndo.Click += Undo_Click;
        // 
        // tsmRedo
        // 
        tsmRedo.Enabled = false;
        tsmRedo.Image = Properties.Resources.rotate_right_solid;
        tsmRedo.Name = "tsmRedo";
        tsmRedo.ShortcutKeys = Keys.Control | Keys.Y;
        tsmRedo.Size = new Size(180, 22);
        tsmRedo.Text = "&Redo";
        tsmRedo.Click += Redo_Click;
        // 
        // toolStripSeparator3
        // 
        toolStripSeparator3.Name = "toolStripSeparator3";
        toolStripSeparator3.Size = new Size(177, 6);
        // 
        // tsmCopy
        // 
        tsmCopy.Image = Properties.Resources.copy_solid;
        tsmCopy.Name = "tsmCopy";
        tsmCopy.ShortcutKeys = Keys.Control | Keys.C;
        tsmCopy.Size = new Size(180, 22);
        tsmCopy.Text = "&Copy";
        tsmCopy.Click += Copy_Click;
        // 
        // tsmPaste
        // 
        tsmPaste.Enabled = false;
        tsmPaste.Image = Properties.Resources.paste_solid;
        tsmPaste.Name = "tsmPaste";
        tsmPaste.ShortcutKeys = Keys.Control | Keys.V;
        tsmPaste.Size = new Size(180, 22);
        tsmPaste.Text = "&Paste";
        tsmPaste.Click += Paste_Click;
        // 
        // toolStripSeparator4
        // 
        toolStripSeparator4.Name = "toolStripSeparator4";
        toolStripSeparator4.Size = new Size(177, 6);
        // 
        // tsmReset
        // 
        tsmReset.Image = Properties.Resources.arrows_rotate_solid;
        tsmReset.Name = "tsmReset";
        tsmReset.ShortcutKeys = Keys.Control | Keys.Shift | Keys.R;
        tsmReset.Size = new Size(180, 22);
        tsmReset.Text = "&Reset";
        tsmReset.Click += Reset_Click;
        // 
        // toolStripSeparator7
        // 
        toolStripSeparator7.Name = "toolStripSeparator7";
        toolStripSeparator7.Size = new Size(177, 6);
        // 
        // tsmSelectAll
        // 
        tsmSelectAll.Name = "tsmSelectAll";
        tsmSelectAll.ShortcutKeys = Keys.Control | Keys.A;
        tsmSelectAll.Size = new Size(180, 22);
        tsmSelectAll.Text = "Select &All";
        // 
        // toolStrip1
        // 
        toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
        toolStrip1.Items.AddRange(new ToolStripItem[] { tsbSave, toolStripSeparator5, tsbImportPalette, tsbExportPalette, toolStripSeparator6, tsbUndo, tsbRedo, toolStripSeparator9, tsbCopy, tsbPaste, toolStripSeparator8, tsbReset });
        toolStrip1.Location = new Point(0, 24);
        toolStrip1.Name = "toolStrip1";
        toolStrip1.Size = new Size(491, 25);
        toolStrip1.TabIndex = 12;
        toolStrip1.Text = "toolStrip1";
        // 
        // tsbSave
        // 
        tsbSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
        tsbSave.Enabled = false;
        tsbSave.Image = Properties.Resources.floppy_disk_regular;
        tsbSave.ImageTransparentColor = Color.Magenta;
        tsbSave.Name = "tsbSave";
        tsbSave.Size = new Size(23, 22);
        tsbSave.Text = "Save";
        tsbSave.Click += Save_Click;
        // 
        // toolStripSeparator5
        // 
        toolStripSeparator5.Name = "toolStripSeparator5";
        toolStripSeparator5.Size = new Size(6, 25);
        // 
        // tsbImportPalette
        // 
        tsbImportPalette.DisplayStyle = ToolStripItemDisplayStyle.Image;
        tsbImportPalette.Image = Properties.Resources.file_import_solid;
        tsbImportPalette.ImageTransparentColor = Color.Magenta;
        tsbImportPalette.Name = "tsbImportPalette";
        tsbImportPalette.Size = new Size(23, 22);
        tsbImportPalette.Text = "Import Palette";
        tsbImportPalette.Click += ImportPalette_Click;
        // 
        // tsbExportPalette
        // 
        tsbExportPalette.DisplayStyle = ToolStripItemDisplayStyle.Image;
        tsbExportPalette.Image = Properties.Resources.file_export_solid;
        tsbExportPalette.ImageTransparentColor = Color.Magenta;
        tsbExportPalette.Name = "tsbExportPalette";
        tsbExportPalette.Size = new Size(23, 22);
        tsbExportPalette.Text = "Export Palette";
        tsbExportPalette.Click += ExportPalette_Click;
        // 
        // toolStripSeparator6
        // 
        toolStripSeparator6.Name = "toolStripSeparator6";
        toolStripSeparator6.Size = new Size(6, 25);
        // 
        // tsbUndo
        // 
        tsbUndo.DisplayStyle = ToolStripItemDisplayStyle.Image;
        tsbUndo.Enabled = false;
        tsbUndo.Image = Properties.Resources.rotate_left_solid;
        tsbUndo.ImageTransparentColor = Color.Magenta;
        tsbUndo.Name = "tsbUndo";
        tsbUndo.Size = new Size(23, 22);
        tsbUndo.Text = "Undo";
        tsbUndo.Click += Undo_Click;
        // 
        // tsbRedo
        // 
        tsbRedo.DisplayStyle = ToolStripItemDisplayStyle.Image;
        tsbRedo.Enabled = false;
        tsbRedo.Image = Properties.Resources.rotate_right_solid;
        tsbRedo.ImageTransparentColor = Color.Magenta;
        tsbRedo.Name = "tsbRedo";
        tsbRedo.Size = new Size(23, 22);
        tsbRedo.Text = "Redo";
        tsbRedo.Click += Redo_Click;
        // 
        // toolStripSeparator9
        // 
        toolStripSeparator9.Name = "toolStripSeparator9";
        toolStripSeparator9.Size = new Size(6, 25);
        // 
        // tsbCopy
        // 
        tsbCopy.DisplayStyle = ToolStripItemDisplayStyle.Image;
        tsbCopy.Image = Properties.Resources.copy_solid;
        tsbCopy.ImageTransparentColor = Color.Magenta;
        tsbCopy.Name = "tsbCopy";
        tsbCopy.Size = new Size(23, 22);
        tsbCopy.Text = "Copy";
        tsbCopy.Click += Copy_Click;
        // 
        // tsbPaste
        // 
        tsbPaste.DisplayStyle = ToolStripItemDisplayStyle.Image;
        tsbPaste.Enabled = false;
        tsbPaste.Image = Properties.Resources.paste_solid;
        tsbPaste.ImageTransparentColor = Color.Magenta;
        tsbPaste.Name = "tsbPaste";
        tsbPaste.Size = new Size(23, 22);
        tsbPaste.Text = "Paste";
        tsbPaste.Click += Paste_Click;
        // 
        // toolStripSeparator8
        // 
        toolStripSeparator8.Name = "toolStripSeparator8";
        toolStripSeparator8.Size = new Size(6, 25);
        // 
        // tsbReset
        // 
        tsbReset.DisplayStyle = ToolStripItemDisplayStyle.Image;
        tsbReset.Image = Properties.Resources.arrows_rotate_solid;
        tsbReset.ImageTransparentColor = Color.Magenta;
        tsbReset.Name = "tsbReset";
        tsbReset.Size = new Size(23, 22);
        tsbReset.Text = "Reset";
        tsbReset.Click += Reset_Click;
        // 
        // colorDialog
        // 
        colorDialog.FullOpen = true;
        // 
        // importPaletteFileDialog
        // 
        importPaletteFileDialog.DefaultExt = "tpl";
        importPaletteFileDialog.Filter = "All Palette Files (*.tpl;*.pal;*.rpf)|*.tpl;*.pal;*.rpf|Tile Layer PRO Palette Files (*.tpl)|*.tpl|Raw palette file|*.rpf|24-bit Palette Files (*.pal)|*.pal|All Files (*.*)|*.*";
        importPaletteFileDialog.Title = "Import Palette";
        // 
        // exportPaletteFileDialog
        // 
        exportPaletteFileDialog.DefaultExt = "tpl";
        exportPaletteFileDialog.Filter = "All Palette Files (*.tpl;*.pal;*.rpf)|*.tpl;*.pal;*.rpf|Tile Layer PRO Palette Files (*.tpl)|*.tpl|Raw palette file|*.rpf|24-bit Palette Files (*.pal)|*.pal|All Files (*.*)|*.*";
        exportPaletteFileDialog.Title = "Export Palette";
        // 
        // exceptionView
        // 
        exceptionView.Owner = this;
        exceptionView.Title = "Palette Editor";
        // 
        // animationTimer
        // 
        animationTimer.Interval = 3;
        animationTimer.Tick += AnimationTimer_Tick;
        // 
        // PaletteEditorForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(491, 331);
        Controls.Add(toolStrip1);
        Controls.Add(statusStrip1);
        Controls.Add(menuStrip1);
        Controls.Add(gbxSprites);
        Controls.Add(gbxBackground);
        Controls.Add(gbxForeground);
        Controls.Add(paletteControl);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MainMenuStrip = menuStrip1;
        Margin = new Padding(3, 2, 3, 2);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "PaletteEditorForm";
        ShowIcon = false;
        ShowInTaskbar = false;
        Text = "Palette Editor";
        gbxForeground.ResumeLayout(false);
        gbxBackground.ResumeLayout(false);
        gbxSprites.ResumeLayout(false);
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        toolStrip1.ResumeLayout(false);
        toolStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private Controls.DesignControl paletteControl;
    private GroupBox gbxForeground;
    private ComboBox cbxForegroundPalette;
    private GroupBox gbxBackground;
    private ComboBox cbxBackgroundPalette;
    private GroupBox gbxSprites;
    private ComboBox cbxSpritePalette;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel tsslColor;
    private ToolStripStatusLabel tsslRed;
    private ToolStripStatusLabel tssGreen;
    private ToolStripStatusLabel tsslBlue;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem editToolStripMenuItem;
    private ToolStripMenuItem tsmUndo;
    private ToolStripMenuItem tsmRedo;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripMenuItem tsmSelectAll;
    private ToolStripMenuItem tsmSave;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem tsmImportPalette;
    private ToolStripMenuItem tsmExportPalette;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem tsmClose;
    private ToolStripMenuItem tsmReset;
    private ToolStripMenuItem tsmCopy;
    private ToolStripMenuItem tsmPaste;
    private ToolStripSeparator toolStripSeparator7;
    private ToolStrip toolStrip1;
    private ToolStripButton tsbSave;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripButton tsbImportPalette;
    private ToolStripButton tsbExportPalette;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripButton tsbCopy;
    private ToolStripSeparator toolStripSeparator8;
    private ToolStripButton tsbUndo;
    private ToolStripButton tsbRedo;
    private ToolStripSeparator toolStripSeparator9;
    private ToolStripButton tsbPaste;
    private ToolStripButton tsbReset;
    private ColorDialog colorDialog;
    private OpenFileDialog importPaletteFileDialog;
    private SaveFileDialog exportPaletteFileDialog;
    private Views.ExceptionView exceptionView;
    private System.Windows.Forms.Timer animationTimer;
}