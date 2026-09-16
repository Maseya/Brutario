// <copyright file="ObjectEditorForm.Designer.cs" organization="Maseya">
//     Copyright (c) 2026 spel werdz rite. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Win.Dialogs.BaseForms
{
    partial class ObjectEditorForm
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
            btnOK = new Button();
            btnCancel = new Button();
            groupBox1 = new GroupBox();
            lblPage = new Label();
            nudPage = new NumericUpDown();
            lblForegroundScenery = new Label();
            cbxBackgroundScenery = new ComboBox();
            cbxForegroundScenery = new ComboBox();
            lblBackgroundScenery = new Label();
            cbxTerrainMode = new ComboBox();
            lblTerrainMode = new Label();
            nudLength = new NumericUpDown();
            lblLength = new Label();
            lblObject = new Label();
            cbxAreaObjectCode = new ComboBox();
            lblY = new Label();
            lblX = new Label();
            nudY = new NumericUpDown();
            nudX = new NumericUpDown();
            gbxBinary = new GroupBox();
            tbxManualInput = new TextBox();
            chkUseManualInput = new CheckBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudX).BeginInit();
            gbxBinary.SuspendLayout();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(295, 215);
            btnOK.Margin = new Padding(4, 3, 4, 3);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(88, 27);
            btnOK.TabIndex = 0;
            btnOK.Text = "&OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(390, 215);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 27);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(lblPage);
            groupBox1.Controls.Add(nudPage);
            groupBox1.Controls.Add(lblForegroundScenery);
            groupBox1.Controls.Add(cbxBackgroundScenery);
            groupBox1.Controls.Add(cbxForegroundScenery);
            groupBox1.Controls.Add(lblBackgroundScenery);
            groupBox1.Controls.Add(cbxTerrainMode);
            groupBox1.Controls.Add(lblTerrainMode);
            groupBox1.Controls.Add(nudLength);
            groupBox1.Controls.Add(lblLength);
            groupBox1.Controls.Add(lblObject);
            groupBox1.Controls.Add(cbxAreaObjectCode);
            groupBox1.Controls.Add(lblY);
            groupBox1.Controls.Add(lblX);
            groupBox1.Controls.Add(nudY);
            groupBox1.Controls.Add(nudX);
            groupBox1.Location = new Point(14, 14);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(463, 171);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Object";
            // 
            // lblPage
            // 
            lblPage.AutoSize = true;
            lblPage.Location = new Point(106, 18);
            lblPage.Margin = new Padding(4, 0, 4, 0);
            lblPage.Name = "lblPage";
            lblPage.Size = new Size(33, 15);
            lblPage.TabIndex = 23;
            lblPage.Text = "Page";
            // 
            // nudPage
            // 
            nudPage.Location = new Point(150, 16);
            nudPage.Margin = new Padding(4, 3, 4, 3);
            nudPage.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            nudPage.Name = "nudPage";
            nudPage.Size = new Size(41, 23);
            nudPage.TabIndex = 22;
            nudPage.TextAlign = HorizontalAlignment.Center;
            nudPage.ValueChanged += Item_ValueChanged;
            // 
            // lblForegroundScenery
            // 
            lblForegroundScenery.AutoSize = true;
            lblForegroundScenery.Location = new Point(7, 143);
            lblForegroundScenery.Margin = new Padding(4, 0, 4, 0);
            lblForegroundScenery.Name = "lblForegroundScenery";
            lblForegroundScenery.Size = new Size(69, 15);
            lblForegroundScenery.TabIndex = 21;
            lblForegroundScenery.Text = "Foreground";
            // 
            // cbxBackgroundScenery
            // 
            cbxBackgroundScenery.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbxBackgroundScenery.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxBackgroundScenery.FormattingEnabled = true;
            cbxBackgroundScenery.Items.AddRange(new object[] { "Nothing", "Clouds", "Mountain", "Fence" });
            cbxBackgroundScenery.Location = new Point(96, 108);
            cbxBackgroundScenery.Margin = new Padding(4, 3, 4, 3);
            cbxBackgroundScenery.Name = "cbxBackgroundScenery";
            cbxBackgroundScenery.Size = new Size(360, 23);
            cbxBackgroundScenery.TabIndex = 20;
            cbxBackgroundScenery.SelectedIndexChanged += Item_ValueChanged;
            // 
            // cbxForegroundScenery
            // 
            cbxForegroundScenery.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbxForegroundScenery.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxForegroundScenery.FormattingEnabled = true;
            cbxForegroundScenery.Items.AddRange(new object[] { "None", "Underwater", "Castle Wall (Unused)", "Over Water", "Night (Unused)", "Snow (Unused)", "Night and Snow (Unused)", "Castle (unused)" });
            cbxForegroundScenery.Location = new Point(96, 140);
            cbxForegroundScenery.Margin = new Padding(4, 3, 4, 3);
            cbxForegroundScenery.Name = "cbxForegroundScenery";
            cbxForegroundScenery.Size = new Size(360, 23);
            cbxForegroundScenery.TabIndex = 19;
            cbxForegroundScenery.SelectedIndexChanged += Item_ValueChanged;
            // 
            // lblBackgroundScenery
            // 
            lblBackgroundScenery.AutoSize = true;
            lblBackgroundScenery.Location = new Point(7, 112);
            lblBackgroundScenery.Margin = new Padding(4, 0, 4, 0);
            lblBackgroundScenery.Name = "lblBackgroundScenery";
            lblBackgroundScenery.Size = new Size(48, 15);
            lblBackgroundScenery.TabIndex = 18;
            lblBackgroundScenery.Text = "Scenery";
            // 
            // cbxTerrainMode
            // 
            cbxTerrainMode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbxTerrainMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxTerrainMode.FormattingEnabled = true;
            cbxTerrainMode.Items.AddRange(new object[] { "None", "2-tile-high floor with no ceiling", "2-tile-high floor with 1-tile-high ceiling", "2-tile-high floor with 3-tile-high ceiling", "2-tile-high floor with 4-tile-high ceiling", "2-tile-high floor with 8-tile-high ceiling", "5-tile-high floor with 1-tile-high ceiling", "5-tile-high floor with 3-tile-high ceiling", "5-tile-high floor with 4-tile-high ceiling", "6-tile-high floor with 1-tile-high ceiling", "No floor with 1-tile-high ceiling", "6-tile-high floor with 4-tile-high ceiling", "9-tile-high floor with 1-tile-high ceiling", "2-tile-high floor with 1-tile-high ceiling and 5 tiles in the middle", "2-tile-high floor with 1-tile-high ceiling and 4 tiles in the middle", "Floor tiles everywhere" });
            cbxTerrainMode.Location = new Point(96, 77);
            cbxTerrainMode.Margin = new Padding(4, 3, 4, 3);
            cbxTerrainMode.Name = "cbxTerrainMode";
            cbxTerrainMode.Size = new Size(360, 23);
            cbxTerrainMode.TabIndex = 17;
            cbxTerrainMode.SelectedIndexChanged += Item_ValueChanged;
            // 
            // lblTerrainMode
            // 
            lblTerrainMode.AutoSize = true;
            lblTerrainMode.Location = new Point(7, 81);
            lblTerrainMode.Margin = new Padding(4, 0, 4, 0);
            lblTerrainMode.Name = "lblTerrainMode";
            lblTerrainMode.Size = new Size(76, 15);
            lblTerrainMode.TabIndex = 16;
            lblTerrainMode.Text = "Terrain Mode";
            // 
            // nudLength
            // 
            nudLength.Location = new Point(415, 16);
            nudLength.Margin = new Padding(4, 3, 4, 3);
            nudLength.Maximum = new decimal(new int[] { 15, 0, 0, 0 });
            nudLength.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudLength.Name = "nudLength";
            nudLength.Size = new Size(41, 23);
            nudLength.TabIndex = 8;
            nudLength.TextAlign = HorizontalAlignment.Center;
            nudLength.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudLength.ValueChanged += Item_ValueChanged;
            // 
            // lblLength
            // 
            lblLength.AutoSize = true;
            lblLength.Location = new Point(362, 18);
            lblLength.Margin = new Padding(4, 0, 4, 0);
            lblLength.Name = "lblLength";
            lblLength.Size = new Size(44, 15);
            lblLength.TabIndex = 7;
            lblLength.Text = "Length";
            // 
            // lblObject
            // 
            lblObject.AutoSize = true;
            lblObject.Location = new Point(7, 50);
            lblObject.Margin = new Padding(4, 0, 4, 0);
            lblObject.Name = "lblObject";
            lblObject.Size = new Size(42, 15);
            lblObject.TabIndex = 6;
            lblObject.Text = "Object";
            // 
            // cbxAreaObjectCode
            // 
            cbxAreaObjectCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbxAreaObjectCode.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxAreaObjectCode.FormattingEnabled = true;
            cbxAreaObjectCode.Location = new Point(58, 46);
            cbxAreaObjectCode.Margin = new Padding(4, 3, 4, 3);
            cbxAreaObjectCode.Name = "cbxAreaObjectCode";
            cbxAreaObjectCode.Size = new Size(397, 23);
            cbxAreaObjectCode.TabIndex = 5;
            cbxAreaObjectCode.SelectedIndexChanged += AreaObectCode_SelectedIndexChanged;
            // 
            // lblY
            // 
            lblY.AutoSize = true;
            lblY.Location = new Point(198, 18);
            lblY.Margin = new Padding(4, 0, 4, 0);
            lblY.Name = "lblY";
            lblY.Size = new Size(36, 15);
            lblY.TabIndex = 3;
            lblY.Text = "Y pos";
            // 
            // lblX
            // 
            lblX.AutoSize = true;
            lblX.Location = new Point(7, 18);
            lblX.Margin = new Padding(4, 0, 4, 0);
            lblX.Name = "lblX";
            lblX.Size = new Size(36, 15);
            lblX.TabIndex = 2;
            lblX.Text = "X pos";
            // 
            // nudY
            // 
            nudY.Location = new Point(245, 16);
            nudY.Margin = new Padding(4, 3, 4, 3);
            nudY.Maximum = new decimal(new int[] { 11, 0, 0, 0 });
            nudY.Name = "nudY";
            nudY.Size = new Size(41, 23);
            nudY.TabIndex = 1;
            nudY.TextAlign = HorizontalAlignment.Center;
            nudY.ValueChanged += Item_ValueChanged;
            // 
            // nudX
            // 
            nudX.Location = new Point(58, 16);
            nudX.Margin = new Padding(4, 3, 4, 3);
            nudX.Maximum = new decimal(new int[] { 15, 0, 0, 0 });
            nudX.Name = "nudX";
            nudX.Size = new Size(41, 23);
            nudX.TabIndex = 0;
            nudX.TextAlign = HorizontalAlignment.Center;
            nudX.ValueChanged += Item_ValueChanged;
            // 
            // gbxBinary
            // 
            gbxBinary.Controls.Add(tbxManualInput);
            gbxBinary.Controls.Add(chkUseManualInput);
            gbxBinary.Location = new Point(14, 192);
            gbxBinary.Margin = new Padding(4, 3, 4, 3);
            gbxBinary.Name = "gbxBinary";
            gbxBinary.Padding = new Padding(4, 3, 4, 3);
            gbxBinary.Size = new Size(219, 68);
            gbxBinary.TabIndex = 3;
            gbxBinary.TabStop = false;
            // 
            // tbxManualInput
            // 
            tbxManualInput.CharacterCasing = CharacterCasing.Upper;
            tbxManualInput.Location = new Point(10, 27);
            tbxManualInput.Margin = new Padding(4, 3, 4, 3);
            tbxManualInput.MaxLength = 8;
            tbxManualInput.Name = "tbxManualInput";
            tbxManualInput.Size = new Size(201, 23);
            tbxManualInput.TabIndex = 1;
            tbxManualInput.WordWrap = false;
            tbxManualInput.TextChanged += ManualInput_TextChanged;
            // 
            // chkUseManualInput
            // 
            chkUseManualInput.AutoSize = true;
            chkUseManualInput.Location = new Point(10, 0);
            chkUseManualInput.Margin = new Padding(4, 3, 4, 3);
            chkUseManualInput.Name = "chkUseManualInput";
            chkUseManualInput.Size = new Size(136, 19);
            chkUseManualInput.TabIndex = 0;
            chkUseManualInput.Text = "Enter value manually";
            chkUseManualInput.UseVisualStyleBackColor = true;
            chkUseManualInput.CheckedChanged += UseManualInput_CheckedChanged;
            // 
            // ObjectEditorForm
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(491, 273);
            Controls.Add(gbxBinary);
            Controls.Add(groupBox1);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(452, 312);
            Name = "ObjectEditorForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Object Editor";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLength).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudY).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudX).EndInit();
            gbxBinary.ResumeLayout(false);
            gbxBinary.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudLength;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lblObject;
        private System.Windows.Forms.ComboBox cbxAreaObjectCode;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.NumericUpDown nudY;
        private System.Windows.Forms.NumericUpDown nudX;
        private System.Windows.Forms.GroupBox gbxBinary;
        private System.Windows.Forms.TextBox tbxManualInput;
        private System.Windows.Forms.CheckBox chkUseManualInput;
        private System.Windows.Forms.ComboBox cbxTerrainMode;
        private System.Windows.Forms.Label lblTerrainMode;
        private System.Windows.Forms.ComboBox cbxForegroundScenery;
        private System.Windows.Forms.Label lblBackgroundScenery;
        private System.Windows.Forms.Label lblForegroundScenery;
        private System.Windows.Forms.ComboBox cbxBackgroundScenery;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.NumericUpDown nudPage;
    }
}
