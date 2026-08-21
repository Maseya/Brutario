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
        paletteControl = new Brutario.Win.Controls.DesignControl();
        gbxForeground = new GroupBox();
        cbxForeground = new ComboBox();
        gbxBackground = new GroupBox();
        cbxBackground = new ComboBox();
        gbxSprites = new GroupBox();
        cbxSprites = new ComboBox();
        gbxForeground.SuspendLayout();
        gbxBackground.SuspendLayout();
        gbxSprites.SuspendLayout();
        SuspendLayout();
        // 
        // paletteControl
        // 
        paletteControl.BorderStyle = BorderStyle.FixedSingle;
        paletteControl.Location = new Point(0, 0);
        paletteControl.Name = "paletteControl";
        paletteControl.Size = new Size(258, 258);
        paletteControl.TabIndex = 0;
        // 
        // gbxForeground
        // 
        gbxForeground.Controls.Add(cbxForeground);
        gbxForeground.Location = new Point(264, 12);
        gbxForeground.Name = "gbxForeground";
        gbxForeground.Size = new Size(249, 60);
        gbxForeground.TabIndex = 1;
        gbxForeground.TabStop = false;
        gbxForeground.Text = "Foreground";
        // 
        // cbxForeground
        // 
        cbxForeground.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        cbxForeground.FormattingEnabled = true;
        cbxForeground.Items.AddRange(new object[] { "Normal", "Snow (Day)", "Snow (Night)", "Mushroom Island", "Mushroom Island (Warp Zone)", "Underground", "Castle", "Castle (Underwater)" });
        cbxForeground.Location = new Point(6, 26);
        cbxForeground.Name = "cbxForeground";
        cbxForeground.Size = new Size(237, 28);
        cbxForeground.TabIndex = 0;
        cbxForeground.SelectedIndexChanged += Foreground_SelectedIndexChanged;
        // 
        // gbxBackground
        // 
        gbxBackground.Controls.Add(cbxBackground);
        gbxBackground.Location = new Point(264, 78);
        gbxBackground.Name = "gbxBackground";
        gbxBackground.Size = new Size(249, 60);
        gbxBackground.TabIndex = 2;
        gbxBackground.TabStop = false;
        gbxBackground.Text = "Background";
        // 
        // cbxBackground
        // 
        cbxBackground.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        cbxBackground.FormattingEnabled = true;
        cbxBackground.Items.AddRange(new object[] { "Mountains", "One Mountain", "Waterfall", "Goomba Pillars", "Green Peaks", "Orange Peaks", "Snow Peaks", "Starry Night", "Mushroom Island", "Castle Wall", "Bonus Room", "Underwater", "Underground", "Castle", "W8 Castle", "Castle (Underwater)" });
        cbxBackground.Location = new Point(6, 26);
        cbxBackground.Name = "cbxBackground";
        cbxBackground.Size = new Size(237, 28);
        cbxBackground.TabIndex = 0;
        cbxBackground.SelectedIndexChanged += Background_SelectedIndexChanged;
        // 
        // gbxSprites
        // 
        gbxSprites.Controls.Add(cbxSprites);
        gbxSprites.Location = new Point(264, 144);
        gbxSprites.Name = "gbxSprites";
        gbxSprites.Size = new Size(249, 60);
        gbxSprites.TabIndex = 3;
        gbxSprites.TabStop = false;
        gbxSprites.Text = "Sprites";
        // 
        // cbxSprites
        // 
        cbxSprites.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        cbxSprites.FormattingEnabled = true;
        cbxSprites.Items.AddRange(new object[] { "Normal", "Underground", "Castle" });
        cbxSprites.Location = new Point(6, 26);
        cbxSprites.Name = "cbxSprites";
        cbxSprites.Size = new Size(237, 28);
        cbxSprites.TabIndex = 0;
        cbxSprites.SelectedIndexChanged += Sprites_SelectedIndexChanged;
        // 
        // PaletteEditorForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(525, 258);
        Controls.Add(gbxSprites);
        Controls.Add(gbxBackground);
        Controls.Add(gbxForeground);
        Controls.Add(paletteControl);
        Name = "PaletteEditorForm";
        Text = "Palette Editor";
        gbxForeground.ResumeLayout(false);
        gbxBackground.ResumeLayout(false);
        gbxSprites.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion
    private Controls.DesignControl paletteControl;
    private GroupBox gbxForeground;
    private ComboBox cbxForeground;
    private GroupBox gbxBackground;
    private ComboBox cbxBackground;
    private GroupBox gbxSprites;
    private ComboBox cbxSprites;
}