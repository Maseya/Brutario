// <copyright file="SpriteEditorForm.Designer.cs" organization="Maseya">
//     Copyright (c) 2026 spel werdz rite. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Win.Dialogs.BaseForms
{
    partial class SpriteEditorForm
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
            gbxBinary = new GroupBox();
            tbxManualInput = new TextBox();
            chkUseManualInput = new CheckBox();
            btnCancel = new Button();
            btnOK = new Button();
            groupBox1 = new GroupBox();
            lblPage = new Label();
            nudPage = new NumericUpDown();
            tbxAreaNumber = new TextBox();
            lblAreaNumber = new Label();
            nudWorld = new NumericUpDown();
            lblWorld = new Label();
            lblDestPage = new Label();
            nudDestPage = new NumericUpDown();
            chkHardFlag = new CheckBox();
            lblObject = new Label();
            cbxAreaSpriteCode = new ComboBox();
            lblY = new Label();
            lblX = new Label();
            nudY = new NumericUpDown();
            nudX = new NumericUpDown();
            gbxBinary.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudWorld).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDestPage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudX).BeginInit();
            SuspendLayout();
            // 
            // gbxBinary
            // 
            gbxBinary.Controls.Add(tbxManualInput);
            gbxBinary.Controls.Add(chkUseManualInput);
            gbxBinary.Location = new Point(14, 128);
            gbxBinary.Margin = new Padding(4);
            gbxBinary.Name = "gbxBinary";
            gbxBinary.Padding = new Padding(4);
            gbxBinary.Size = new Size(220, 68);
            gbxBinary.TabIndex = 6;
            gbxBinary.TabStop = false;
            // 
            // tbxManualInput
            // 
            tbxManualInput.CharacterCasing = CharacterCasing.Upper;
            tbxManualInput.Location = new Point(10, 26);
            tbxManualInput.Margin = new Padding(4);
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
            chkUseManualInput.Margin = new Padding(4);
            chkUseManualInput.Name = "chkUseManualInput";
            chkUseManualInput.Size = new Size(136, 19);
            chkUseManualInput.TabIndex = 0;
            chkUseManualInput.Text = "Enter value manually";
            chkUseManualInput.UseVisualStyleBackColor = true;
            chkUseManualInput.CheckedChanged += UseManualInput_CheckedChanged;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(335, 152);
            btnCancel.Margin = new Padding(4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 26);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(241, 152);
            btnOK.Margin = new Padding(4);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(88, 26);
            btnOK.TabIndex = 4;
            btnOK.Text = "&OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblPage);
            groupBox1.Controls.Add(nudPage);
            groupBox1.Controls.Add(tbxAreaNumber);
            groupBox1.Controls.Add(lblAreaNumber);
            groupBox1.Controls.Add(nudWorld);
            groupBox1.Controls.Add(lblWorld);
            groupBox1.Controls.Add(lblDestPage);
            groupBox1.Controls.Add(nudDestPage);
            groupBox1.Controls.Add(chkHardFlag);
            groupBox1.Controls.Add(lblObject);
            groupBox1.Controls.Add(cbxAreaSpriteCode);
            groupBox1.Controls.Add(lblY);
            groupBox1.Controls.Add(lblX);
            groupBox1.Controls.Add(nudY);
            groupBox1.Controls.Add(nudX);
            groupBox1.Location = new Point(14, 14);
            groupBox1.Margin = new Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4);
            groupBox1.Size = new Size(409, 107);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Object";
            // 
            // lblPage
            // 
            lblPage.AutoSize = true;
            lblPage.Location = new Point(106, 19);
            lblPage.Margin = new Padding(4, 0, 4, 0);
            lblPage.Name = "lblPage";
            lblPage.Size = new Size(33, 15);
            lblPage.TabIndex = 29;
            lblPage.Text = "Page";
            // 
            // nudPage
            // 
            nudPage.Location = new Point(150, 16);
            nudPage.Margin = new Padding(4);
            nudPage.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            nudPage.Name = "nudPage";
            nudPage.Size = new Size(41, 23);
            nudPage.TabIndex = 28;
            nudPage.TextAlign = HorizontalAlignment.Center;
            nudPage.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudPage.ValueChanged += Item_ValueChanged;
            // 
            // tbxAreaNumber
            // 
            tbxAreaNumber.CharacterCasing = CharacterCasing.Upper;
            tbxAreaNumber.Location = new Point(368, 76);
            tbxAreaNumber.Margin = new Padding(4);
            tbxAreaNumber.MaxLength = 2;
            tbxAreaNumber.Name = "tbxAreaNumber";
            tbxAreaNumber.Size = new Size(33, 23);
            tbxAreaNumber.TabIndex = 2;
            tbxAreaNumber.Text = "25";
            tbxAreaNumber.TextAlign = HorizontalAlignment.Right;
            tbxAreaNumber.WordWrap = false;
            tbxAreaNumber.TextChanged += AreaNumber_TextChanged;
            // 
            // lblAreaNumber
            // 
            lblAreaNumber.AutoSize = true;
            lblAreaNumber.Location = new Point(280, 80);
            lblAreaNumber.Margin = new Padding(4, 0, 4, 0);
            lblAreaNumber.Name = "lblAreaNumber";
            lblAreaNumber.Size = new Size(78, 15);
            lblAreaNumber.TabIndex = 27;
            lblAreaNumber.Text = "Area Number";
            // 
            // nudWorld
            // 
            nudWorld.Location = new Point(176, 77);
            nudWorld.Margin = new Padding(4);
            nudWorld.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            nudWorld.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudWorld.Name = "nudWorld";
            nudWorld.Size = new Size(41, 23);
            nudWorld.TabIndex = 26;
            nudWorld.TextAlign = HorizontalAlignment.Center;
            nudWorld.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudWorld.ValueChanged += Item_ValueChanged;
            // 
            // lblWorld
            // 
            lblWorld.AutoSize = true;
            lblWorld.Location = new Point(129, 80);
            lblWorld.Margin = new Padding(4, 0, 4, 0);
            lblWorld.Name = "lblWorld";
            lblWorld.Size = new Size(39, 15);
            lblWorld.TabIndex = 25;
            lblWorld.Text = "World";
            // 
            // lblDestPage
            // 
            lblDestPage.AutoSize = true;
            lblDestPage.Location = new Point(7, 80);
            lblDestPage.Margin = new Padding(4, 0, 4, 0);
            lblDestPage.Name = "lblDestPage";
            lblDestPage.Size = new Size(59, 15);
            lblDestPage.TabIndex = 24;
            lblDestPage.Text = "Dest Page";
            // 
            // nudDestPage
            // 
            nudDestPage.Location = new Point(80, 77);
            nudDestPage.Margin = new Padding(4);
            nudDestPage.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            nudDestPage.Name = "nudDestPage";
            nudDestPage.Size = new Size(41, 23);
            nudDestPage.TabIndex = 23;
            nudDestPage.TextAlign = HorizontalAlignment.Center;
            nudDestPage.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudDestPage.ValueChanged += Item_ValueChanged;
            // 
            // chkHardFlag
            // 
            chkHardFlag.AutoSize = true;
            chkHardFlag.Location = new Point(318, 17);
            chkHardFlag.Margin = new Padding(4);
            chkHardFlag.Name = "chkHardFlag";
            chkHardFlag.Size = new Size(77, 19);
            chkHardFlag.TabIndex = 22;
            chkHardFlag.Text = "Hard Flag";
            chkHardFlag.UseVisualStyleBackColor = true;
            chkHardFlag.CheckedChanged += Item_ValueChanged;
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
            // cbxAreaSpriteCode
            // 
            cbxAreaSpriteCode.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxAreaSpriteCode.FormattingEnabled = true;
            cbxAreaSpriteCode.Location = new Point(59, 46);
            cbxAreaSpriteCode.Margin = new Padding(4);
            cbxAreaSpriteCode.Name = "cbxAreaSpriteCode";
            cbxAreaSpriteCode.Size = new Size(343, 23);
            cbxAreaSpriteCode.TabIndex = 5;
            cbxAreaSpriteCode.SelectedIndexChanged += AreaSpriteCode_SelectedIndexChanged;
            // 
            // lblY
            // 
            lblY.AutoSize = true;
            lblY.Location = new Point(199, 19);
            lblY.Margin = new Padding(4, 0, 4, 0);
            lblY.Name = "lblY";
            lblY.Size = new Size(36, 15);
            lblY.TabIndex = 3;
            lblY.Text = "Y pos";
            // 
            // lblX
            // 
            lblX.AutoSize = true;
            lblX.Location = new Point(7, 19);
            lblX.Margin = new Padding(4, 0, 4, 0);
            lblX.Name = "lblX";
            lblX.Size = new Size(36, 15);
            lblX.TabIndex = 2;
            lblX.Text = "X pos";
            // 
            // nudY
            // 
            nudY.Location = new Point(245, 16);
            nudY.Margin = new Padding(4);
            nudY.Maximum = new decimal(new int[] { 11, 0, 0, 0 });
            nudY.Name = "nudY";
            nudY.Size = new Size(41, 23);
            nudY.TabIndex = 1;
            nudY.TextAlign = HorizontalAlignment.Center;
            nudY.ValueChanged += Item_ValueChanged;
            // 
            // nudX
            // 
            nudX.Location = new Point(59, 16);
            nudX.Margin = new Padding(4);
            nudX.Maximum = new decimal(new int[] { 15, 0, 0, 0 });
            nudX.Name = "nudX";
            nudX.Size = new Size(41, 23);
            nudX.TabIndex = 0;
            nudX.TextAlign = HorizontalAlignment.Center;
            nudX.ValueChanged += Item_ValueChanged;
            // 
            // SpriteEditorForm
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(437, 210);
            Controls.Add(groupBox1);
            Controls.Add(gbxBinary);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SpriteEditorForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Sprite Editor";
            gbxBinary.ResumeLayout(false);
            gbxBinary.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudWorld).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDestPage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudY).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudX).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox gbxBinary;
        private System.Windows.Forms.TextBox tbxManualInput;
        private System.Windows.Forms.CheckBox chkUseManualInput;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblObject;
        private System.Windows.Forms.ComboBox cbxAreaSpriteCode;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.NumericUpDown nudY;
        private System.Windows.Forms.NumericUpDown nudX;
        private System.Windows.Forms.TextBox tbxAreaNumber;
        private System.Windows.Forms.Label lblAreaNumber;
        private System.Windows.Forms.NumericUpDown nudWorld;
        private System.Windows.Forms.Label lblWorld;
        private System.Windows.Forms.Label lblDestPage;
        private System.Windows.Forms.NumericUpDown nudDestPage;
        private System.Windows.Forms.CheckBox chkHardFlag;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.NumericUpDown nudPage;
    }
}
