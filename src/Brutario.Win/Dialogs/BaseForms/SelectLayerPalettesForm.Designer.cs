// <copyright file="SelectLayerPalettesForm.Designer.cs" organization="Maseya">
//     Copyright (c) 2026 spel werdz rite. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Win.Dialogs.BaseForms;

partial class SelectLayerPalettesForm
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
        var resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectLayerPalettesForm));
        gbxSprites = new GroupBox();
        textBox3 = new TextBox();
        cbxSprites = new ComboBox();
        gbxBackground = new GroupBox();
        textBox2 = new TextBox();
        cbxBackground = new ComboBox();
        gbxForeground = new GroupBox();
        tbxForeground = new TextBox();
        cbxForeground = new ComboBox();
        btnOK = new Button();
        btnCancel = new Button();
        btnApply = new Button();
        label1 = new Label();
        gbxSprites.SuspendLayout();
        gbxBackground.SuspendLayout();
        gbxForeground.SuspendLayout();
        SuspendLayout();
        // 
        // gbxSprites
        // 
        gbxSprites.Controls.Add(textBox3);
        gbxSprites.Controls.Add(cbxSprites);
        gbxSprites.Location = new Point(12, 171);
        gbxSprites.Margin = new Padding(3, 2, 3, 2);
        gbxSprites.Name = "gbxSprites";
        gbxSprites.Padding = new Padding(3, 2, 3, 2);
        gbxSprites.Size = new Size(237, 76);
        gbxSprites.TabIndex = 6;
        gbxSprites.TabStop = false;
        gbxSprites.Text = "Sprites";
        // 
        // textBox3
        // 
        textBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        textBox3.Font = new Font("Consolas", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
        textBox3.Location = new Point(6, 48);
        textBox3.MaxLength = 23;
        textBox3.Name = "textBox3";
        textBox3.ReadOnly = true;
        textBox3.Size = new Size(225, 23);
        textBox3.TabIndex = 2;
        textBox3.Text = "00 00 00 00 00 00 00 00";
        // 
        // cbxSprites
        // 
        cbxSprites.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cbxSprites.FormattingEnabled = true;
        cbxSprites.Items.AddRange(new object[] { "Normal", "Underground", "Castle" });
        cbxSprites.Location = new Point(6, 20);
        cbxSprites.Margin = new Padding(3, 2, 3, 2);
        cbxSprites.Name = "cbxSprites";
        cbxSprites.Size = new Size(225, 23);
        cbxSprites.TabIndex = 0;
        // 
        // gbxBackground
        // 
        gbxBackground.Controls.Add(textBox2);
        gbxBackground.Controls.Add(cbxBackground);
        gbxBackground.Location = new Point(12, 91);
        gbxBackground.Margin = new Padding(3, 2, 3, 2);
        gbxBackground.Name = "gbxBackground";
        gbxBackground.Padding = new Padding(3, 2, 3, 2);
        gbxBackground.Size = new Size(237, 76);
        gbxBackground.TabIndex = 5;
        gbxBackground.TabStop = false;
        gbxBackground.Text = "Background";
        // 
        // textBox2
        // 
        textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        textBox2.Font = new Font("Consolas", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
        textBox2.Location = new Point(6, 48);
        textBox2.MaxLength = 8;
        textBox2.Name = "textBox2";
        textBox2.ReadOnly = true;
        textBox2.Size = new Size(225, 23);
        textBox2.TabIndex = 2;
        textBox2.Text = "00 00 00";
        // 
        // cbxBackground
        // 
        cbxBackground.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cbxBackground.FormattingEnabled = true;
        cbxBackground.Items.AddRange(new object[] { "Mountains", "One Mountain", "Waterfall", "Goomba Pillars", "Green Peaks", "Orange Peaks", "Snow Peaks", "Starry Night", "Mushroom Island", "Castle Wall", "Bonus Room", "Underwater", "Underground", "Castle", "W8 Castle", "Castle (Underwater)" });
        cbxBackground.Location = new Point(6, 20);
        cbxBackground.Margin = new Padding(3, 2, 3, 2);
        cbxBackground.Name = "cbxBackground";
        cbxBackground.Size = new Size(225, 23);
        cbxBackground.TabIndex = 0;
        // 
        // gbxForeground
        // 
        gbxForeground.Controls.Add(tbxForeground);
        gbxForeground.Controls.Add(cbxForeground);
        gbxForeground.Location = new Point(12, 11);
        gbxForeground.Margin = new Padding(3, 2, 3, 2);
        gbxForeground.Name = "gbxForeground";
        gbxForeground.Padding = new Padding(3, 2, 3, 2);
        gbxForeground.Size = new Size(237, 76);
        gbxForeground.TabIndex = 4;
        gbxForeground.TabStop = false;
        gbxForeground.Text = "Foreground";
        // 
        // tbxForeground
        // 
        tbxForeground.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        tbxForeground.Font = new Font("Consolas", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
        tbxForeground.Location = new Point(6, 48);
        tbxForeground.MaxLength = 14;
        tbxForeground.Name = "tbxForeground";
        tbxForeground.ReadOnly = true;
        tbxForeground.Size = new Size(225, 23);
        tbxForeground.TabIndex = 1;
        tbxForeground.Text = "00 00 00 00 00";
        // 
        // cbxForeground
        // 
        cbxForeground.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cbxForeground.FormattingEnabled = true;
        cbxForeground.Items.AddRange(new object[] { "Normal", "Snow (Day)", "Snow (Night)", "Mushroom Island", "Mushroom Island (Warp Zone)", "Underground", "Castle", "Castle (Underwater)" });
        cbxForeground.Location = new Point(6, 20);
        cbxForeground.Margin = new Padding(3, 2, 3, 2);
        cbxForeground.Name = "cbxForeground";
        cbxForeground.Size = new Size(225, 23);
        cbxForeground.TabIndex = 0;
        // 
        // btnOK
        // 
        btnOK.DialogResult = DialogResult.OK;
        btnOK.Location = new Point(12, 252);
        btnOK.Name = "btnOK";
        btnOK.Size = new Size(75, 23);
        btnOK.TabIndex = 7;
        btnOK.Text = "&OK";
        btnOK.UseVisualStyleBackColor = true;
        // 
        // btnCancel
        // 
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(93, 252);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(75, 23);
        btnCancel.TabIndex = 8;
        btnCancel.Text = "&Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        // 
        // btnApply
        // 
        btnApply.Location = new Point(174, 252);
        btnApply.Name = "btnApply";
        btnApply.Size = new Size(75, 23);
        btnApply.TabIndex = 9;
        btnApply.Text = "&Apply";
        btnApply.UseVisualStyleBackColor = true;
        // 
        // label1
        // 
        label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        label1.Location = new Point(255, 11);
        label1.Name = "label1";
        label1.Size = new Size(330, 267);
        label1.TabIndex = 10;
        label1.Text = resources.GetString("label1.Text");
        // 
        // SelectLayerPalettesForm
        // 
        AcceptButton = btnOK;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(597, 287);
        Controls.Add(label1);
        Controls.Add(btnApply);
        Controls.Add(btnCancel);
        Controls.Add(btnOK);
        Controls.Add(gbxSprites);
        Controls.Add(gbxBackground);
        Controls.Add(gbxForeground);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "SelectLayerPalettesForm";
        ShowIcon = false;
        ShowInTaskbar = false;
        Text = "Select Layer Palettes";
        gbxSprites.ResumeLayout(false);
        gbxSprites.PerformLayout();
        gbxBackground.ResumeLayout(false);
        gbxBackground.PerformLayout();
        gbxForeground.ResumeLayout(false);
        gbxForeground.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private GroupBox gbxSprites;
    private ComboBox cbxSprites;
    private GroupBox gbxBackground;
    private ComboBox cbxBackground;
    private GroupBox gbxForeground;
    private ComboBox cbxForeground;
    private TextBox tbxForeground;
    private TextBox textBox3;
    private TextBox textBox2;
    private Button btnOK;
    private Button btnCancel;
    private Button btnApply;
    private Label label1;
}