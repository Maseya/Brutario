// <copyright file="ObjectEditorForm.Designer.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblForegroundScenery = new System.Windows.Forms.Label();
            this.cbxBackgroundScenery = new System.Windows.Forms.ComboBox();
            this.cbxForegroundScenery = new System.Windows.Forms.ComboBox();
            this.lblBackgroundScenery = new System.Windows.Forms.Label();
            this.cbxTerrainMode = new System.Windows.Forms.ComboBox();
            this.lblTerrainMode = new System.Windows.Forms.Label();
            this.nudLength = new System.Windows.Forms.NumericUpDown();
            this.lblLength = new System.Windows.Forms.Label();
            this.lblObject = new System.Windows.Forms.Label();
            this.cbxAreaObjectCode = new System.Windows.Forms.ComboBox();
            this.lblY = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.nudY = new System.Windows.Forms.NumericUpDown();
            this.nudX = new System.Windows.Forms.NumericUpDown();
            this.gbxBinary = new System.Windows.Forms.GroupBox();
            this.tbxManualInput = new System.Windows.Forms.TextBox();
            this.chkUseManualInput = new System.Windows.Forms.CheckBox();
            this.lblPage = new System.Windows.Forms.Label();
            this.nudPage = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
            this.gbxBinary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPage)).BeginInit();
            this.SuspendLayout();
            //
            // btnOK
            //
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(253, 186);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            //
            // btnCancel
            //
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(334, 186);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblPage);
            this.groupBox1.Controls.Add(this.nudPage);
            this.groupBox1.Controls.Add(this.lblForegroundScenery);
            this.groupBox1.Controls.Add(this.cbxBackgroundScenery);
            this.groupBox1.Controls.Add(this.cbxForegroundScenery);
            this.groupBox1.Controls.Add(this.lblBackgroundScenery);
            this.groupBox1.Controls.Add(this.cbxTerrainMode);
            this.groupBox1.Controls.Add(this.lblTerrainMode);
            this.groupBox1.Controls.Add(this.nudLength);
            this.groupBox1.Controls.Add(this.lblLength);
            this.groupBox1.Controls.Add(this.lblObject);
            this.groupBox1.Controls.Add(this.cbxAreaObjectCode);
            this.groupBox1.Controls.Add(this.lblY);
            this.groupBox1.Controls.Add(this.lblX);
            this.groupBox1.Controls.Add(this.nudY);
            this.groupBox1.Controls.Add(this.nudX);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(397, 148);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Object";
            //
            // lblForegroundScenery
            //
            this.lblForegroundScenery.AutoSize = true;
            this.lblForegroundScenery.Location = new System.Drawing.Point(6, 124);
            this.lblForegroundScenery.Name = "lblForegroundScenery";
            this.lblForegroundScenery.Size = new System.Drawing.Size(61, 13);
            this.lblForegroundScenery.TabIndex = 21;
            this.lblForegroundScenery.Text = "Foreground";
            //
            // cbxBackgroundScenery
            //
            this.cbxBackgroundScenery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxBackgroundScenery.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBackgroundScenery.FormattingEnabled = true;
            this.cbxBackgroundScenery.Items.AddRange(new object[] {
            "Nothing",
            "Clouds",
            "Mountain",
            "Fence"});
            this.cbxBackgroundScenery.Location = new System.Drawing.Point(82, 94);
            this.cbxBackgroundScenery.Name = "cbxBackgroundScenery";
            this.cbxBackgroundScenery.Size = new System.Drawing.Size(309, 21);
            this.cbxBackgroundScenery.TabIndex = 20;
            this.cbxBackgroundScenery.SelectedIndexChanged += new System.EventHandler(this.Item_ValueChanged);
            //
            // cbxForegroundScenery
            //
            this.cbxForegroundScenery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxForegroundScenery.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxForegroundScenery.FormattingEnabled = true;
            this.cbxForegroundScenery.Items.AddRange(new object[] {
            "None",
            "Underwater",
            "Castle Wall (Unused)",
            "Over Water",
            "Night (Unused)",
            "Snow (Unused)",
            "Night and Snow (Unused)",
            "Castle (unused)"});
            this.cbxForegroundScenery.Location = new System.Drawing.Point(82, 121);
            this.cbxForegroundScenery.Name = "cbxForegroundScenery";
            this.cbxForegroundScenery.Size = new System.Drawing.Size(309, 21);
            this.cbxForegroundScenery.TabIndex = 19;
            this.cbxForegroundScenery.SelectedIndexChanged += new System.EventHandler(this.Item_ValueChanged);
            //
            // lblBackgroundScenery
            //
            this.lblBackgroundScenery.AutoSize = true;
            this.lblBackgroundScenery.Location = new System.Drawing.Point(6, 97);
            this.lblBackgroundScenery.Name = "lblBackgroundScenery";
            this.lblBackgroundScenery.Size = new System.Drawing.Size(46, 13);
            this.lblBackgroundScenery.TabIndex = 18;
            this.lblBackgroundScenery.Text = "Scenery";
            //
            // cbxTerrainMode
            //
            this.cbxTerrainMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxTerrainMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTerrainMode.FormattingEnabled = true;
            this.cbxTerrainMode.Items.AddRange(new object[] {
            "None",
            "2-tile-high floor with no ceiling",
            "2-tile-high floor with 1-tile-high ceiling",
            "2-tile-high floor with 3-tile-high ceiling",
            "2-tile-high floor with 4-tile-high ceiling",
            "2-tile-high floor with 8-tile-high ceiling",
            "5-tile-high floor with 1-tile-high ceiling",
            "5-tile-high floor with 3-tile-high ceiling",
            "5-tile-high floor with 4-tile-high ceiling",
            "6-tile-high floor with 1-tile-high ceiling",
            "No floor with 1-tile-high ceiling",
            "6-tile-high floor with 4-tile-high ceiling",
            "9-tile-high floor with 1-tile-high ceiling",
            "2-tile-high floor with 1-tile-high ceiling and 5 tiles in the middle",
            "2-tile-high floor with 1-tile-high ceiling and 4 tiles in the middle",
            "Floor tiles everywhere"});
            this.cbxTerrainMode.Location = new System.Drawing.Point(82, 67);
            this.cbxTerrainMode.Name = "cbxTerrainMode";
            this.cbxTerrainMode.Size = new System.Drawing.Size(309, 21);
            this.cbxTerrainMode.TabIndex = 17;
            this.cbxTerrainMode.SelectedIndexChanged += new System.EventHandler(this.Item_ValueChanged);
            //
            // lblTerrainMode
            //
            this.lblTerrainMode.AutoSize = true;
            this.lblTerrainMode.Location = new System.Drawing.Point(6, 70);
            this.lblTerrainMode.Name = "lblTerrainMode";
            this.lblTerrainMode.Size = new System.Drawing.Size(70, 13);
            this.lblTerrainMode.TabIndex = 16;
            this.lblTerrainMode.Text = "Terrain Mode";
            //
            // nudLength
            //
            this.nudLength.Location = new System.Drawing.Point(356, 14);
            this.nudLength.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLength.Name = "nudLength";
            this.nudLength.Size = new System.Drawing.Size(35, 20);
            this.nudLength.TabIndex = 8;
            this.nudLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLength.ValueChanged += new System.EventHandler(this.Item_ValueChanged);
            //
            // lblLength
            //
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(310, 16);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(40, 13);
            this.lblLength.TabIndex = 7;
            this.lblLength.Text = "Length";
            //
            // lblObject
            //
            this.lblObject.AutoSize = true;
            this.lblObject.Location = new System.Drawing.Point(6, 43);
            this.lblObject.Name = "lblObject";
            this.lblObject.Size = new System.Drawing.Size(38, 13);
            this.lblObject.TabIndex = 6;
            this.lblObject.Text = "Object";
            //
            // cbxAreaObjectCode
            //
            this.cbxAreaObjectCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxAreaObjectCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAreaObjectCode.FormattingEnabled = true;
            this.cbxAreaObjectCode.Location = new System.Drawing.Point(50, 40);
            this.cbxAreaObjectCode.Name = "cbxAreaObjectCode";
            this.cbxAreaObjectCode.Size = new System.Drawing.Size(341, 21);
            this.cbxAreaObjectCode.TabIndex = 5;
            this.cbxAreaObjectCode.SelectedIndexChanged += new System.EventHandler(this.AreaObectCode_SelectedIndexChanged);
            //
            // lblY
            //
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(170, 16);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(34, 13);
            this.lblY.TabIndex = 3;
            this.lblY.Text = "Y pos";
            //
            // lblX
            //
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(6, 16);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(34, 13);
            this.lblX.TabIndex = 2;
            this.lblX.Text = "X pos";
            //
            // nudY
            //
            this.nudY.Location = new System.Drawing.Point(210, 14);
            this.nudY.Maximum = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.nudY.Name = "nudY";
            this.nudY.Size = new System.Drawing.Size(35, 20);
            this.nudY.TabIndex = 1;
            this.nudY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudY.ValueChanged += new System.EventHandler(this.Item_ValueChanged);
            //
            // nudX
            //
            this.nudX.Location = new System.Drawing.Point(50, 14);
            this.nudX.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudX.Name = "nudX";
            this.nudX.Size = new System.Drawing.Size(35, 20);
            this.nudX.TabIndex = 0;
            this.nudX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudX.ValueChanged += new System.EventHandler(this.Item_ValueChanged);
            //
            // gbxBinary
            //
            this.gbxBinary.Controls.Add(this.tbxManualInput);
            this.gbxBinary.Controls.Add(this.chkUseManualInput);
            this.gbxBinary.Location = new System.Drawing.Point(12, 166);
            this.gbxBinary.Name = "gbxBinary";
            this.gbxBinary.Size = new System.Drawing.Size(188, 59);
            this.gbxBinary.TabIndex = 3;
            this.gbxBinary.TabStop = false;
            //
            // tbxManualInput
            //
            this.tbxManualInput.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxManualInput.Location = new System.Drawing.Point(9, 23);
            this.tbxManualInput.MaxLength = 8;
            this.tbxManualInput.Name = "tbxManualInput";
            this.tbxManualInput.Size = new System.Drawing.Size(173, 20);
            this.tbxManualInput.TabIndex = 1;
            this.tbxManualInput.WordWrap = false;
            this.tbxManualInput.TextChanged += new System.EventHandler(this.ManualInput_TextChanged);
            //
            // chkUseManualInput
            //
            this.chkUseManualInput.AutoSize = true;
            this.chkUseManualInput.Location = new System.Drawing.Point(9, 0);
            this.chkUseManualInput.Name = "chkUseManualInput";
            this.chkUseManualInput.Size = new System.Drawing.Size(124, 17);
            this.chkUseManualInput.TabIndex = 0;
            this.chkUseManualInput.Text = "Enter value manually";
            this.chkUseManualInput.UseVisualStyleBackColor = true;
            this.chkUseManualInput.CheckedChanged += new System.EventHandler(this.UseManualInput_CheckedChanged);
            //
            // lblPage
            //
            this.lblPage.AutoSize = true;
            this.lblPage.Location = new System.Drawing.Point(91, 16);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(32, 13);
            this.lblPage.TabIndex = 23;
            this.lblPage.Text = "Page";
            //
            // nudPage
            //
            this.nudPage.Location = new System.Drawing.Point(129, 14);
            this.nudPage.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.nudPage.Name = "nudPage";
            this.nudPage.Size = new System.Drawing.Size(35, 20);
            this.nudPage.TabIndex = 22;
            this.nudPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPage.ValueChanged += new System.EventHandler(this.Item_ValueChanged);
            //
            // ObjectEditorForm
            //
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(421, 237);
            this.Controls.Add(this.gbxBinary);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(390, 276);
            this.Name = "ObjectEditorForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Object Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).EndInit();
            this.gbxBinary.ResumeLayout(false);
            this.gbxBinary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPage)).EndInit();
            this.ResumeLayout(false);

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
