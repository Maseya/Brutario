// <copyright file="HeaderEditorForm.Designer.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Win.Dialogs.BaseForms
{
    partial class HeaderEditorForm
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
            this.lblTime = new System.Windows.Forms.Label();
            this.lblPosition = new System.Windows.Forms.Label();
            this.lblScenery = new System.Windows.Forms.Label();
            this.cbxTime = new System.Windows.Forms.ComboBox();
            this.cbxPosition = new System.Windows.Forms.ComboBox();
            this.cbxForeground = new System.Windows.Forms.ComboBox();
            this.lblForeground = new System.Windows.Forms.Label();
            this.cbxAreaPlatformType = new System.Windows.Forms.ComboBox();
            this.lblAreaPlatformType = new System.Windows.Forms.Label();
            this.cbxBackgroundScenery = new System.Windows.Forms.ComboBox();
            this.lblTerrainMode = new System.Windows.Forms.Label();
            this.cbxTerrainMode = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTime
            //
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(12, 15);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(30, 13);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "Time";
            //
            // lblPosition
            //
            this.lblPosition.AutoSize = true;
            this.lblPosition.Location = new System.Drawing.Point(12, 42);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(44, 13);
            this.lblPosition.TabIndex = 1;
            this.lblPosition.Text = "Position";
            //
            // lblScenery
            //
            this.lblScenery.AutoSize = true;
            this.lblScenery.Location = new System.Drawing.Point(12, 124);
            this.lblScenery.Name = "lblScenery";
            this.lblScenery.Size = new System.Drawing.Size(46, 13);
            this.lblScenery.TabIndex = 4;
            this.lblScenery.Text = "Scenery";
            //
            // cbxTime
            //
            this.cbxTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTime.FormattingEnabled = true;
            this.cbxTime.Items.AddRange(new object[] {
            "Not Set",
            "400",
            "300",
            "200"});
            this.cbxTime.Location = new System.Drawing.Point(115, 12);
            this.cbxTime.Name = "cbxTime";
            this.cbxTime.Size = new System.Drawing.Size(245, 21);
            this.cbxTime.TabIndex = 7;
            this.cbxTime.SelectedIndexChanged += new System.EventHandler(this.Value_SelectedIndexChanged);
            //
            // cbxPosition
            //
            this.cbxPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPosition.FormattingEnabled = true;
            this.cbxPosition.Items.AddRange(new object[] {
            "-1",
            "-1; from another area",
            "10",
            "4",
            "-1",
            "-1",
            "10 (Autowalk)",
            "10 (Autowalk)"});
            this.cbxPosition.Location = new System.Drawing.Point(115, 39);
            this.cbxPosition.Name = "cbxPosition";
            this.cbxPosition.Size = new System.Drawing.Size(245, 21);
            this.cbxPosition.TabIndex = 8;
            this.cbxPosition.SelectedIndexChanged += new System.EventHandler(this.Value_SelectedIndexChanged);
            //
            // cbxForeground
            //
            this.cbxForeground.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxForeground.FormattingEnabled = true;
            this.cbxForeground.Items.AddRange(new object[] {
            "None",
            "Underwater",
            "Castle Wall (Unused)",
            "Over Water",
            "Night (Unused)",
            "Snow (Unused)",
            "Night and Snow (Unused)",
            "Castle (unused)"});
            this.cbxForeground.Location = new System.Drawing.Point(115, 66);
            this.cbxForeground.Name = "cbxForeground";
            this.cbxForeground.Size = new System.Drawing.Size(245, 21);
            this.cbxForeground.TabIndex = 9;
            this.cbxForeground.SelectedIndexChanged += new System.EventHandler(this.Value_SelectedIndexChanged);
            //
            // lblForeground
            //
            this.lblForeground.AutoSize = true;
            this.lblForeground.Location = new System.Drawing.Point(12, 69);
            this.lblForeground.Name = "lblForeground";
            this.lblForeground.Size = new System.Drawing.Size(61, 13);
            this.lblForeground.TabIndex = 10;
            this.lblForeground.Text = "Foreground";
            //
            // cbxAreaPlatformType
            //
            this.cbxAreaPlatformType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAreaPlatformType.FormattingEnabled = true;
            this.cbxAreaPlatformType.Items.AddRange(new object[] {
            "Trees",
            "Mushrooms",
            "Bullet Bill Turrets",
            "Cloud Ground"});
            this.cbxAreaPlatformType.Location = new System.Drawing.Point(115, 93);
            this.cbxAreaPlatformType.Name = "cbxAreaPlatformType";
            this.cbxAreaPlatformType.Size = new System.Drawing.Size(245, 21);
            this.cbxAreaPlatformType.TabIndex = 11;
            this.cbxAreaPlatformType.SelectedIndexChanged += new System.EventHandler(this.Value_SelectedIndexChanged);
            //
            // lblAreaPlatformType
            //
            this.lblAreaPlatformType.AutoSize = true;
            this.lblAreaPlatformType.Location = new System.Drawing.Point(12, 96);
            this.lblAreaPlatformType.Name = "lblAreaPlatformType";
            this.lblAreaPlatformType.Size = new System.Drawing.Size(97, 13);
            this.lblAreaPlatformType.TabIndex = 12;
            this.lblAreaPlatformType.Text = "Area Platform Type";
            //
            // cbxBackgroundScenery
            //
            this.cbxBackgroundScenery.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBackgroundScenery.FormattingEnabled = true;
            this.cbxBackgroundScenery.Items.AddRange(new object[] {
            "Nothing",
            "Clouds",
            "Mountain",
            "Fence"});
            this.cbxBackgroundScenery.Location = new System.Drawing.Point(115, 121);
            this.cbxBackgroundScenery.Name = "cbxBackgroundScenery";
            this.cbxBackgroundScenery.Size = new System.Drawing.Size(245, 21);
            this.cbxBackgroundScenery.TabIndex = 13;
            this.cbxBackgroundScenery.SelectedIndexChanged += new System.EventHandler(this.Value_SelectedIndexChanged);
            //
            // lblTerrainMode
            //
            this.lblTerrainMode.AutoSize = true;
            this.lblTerrainMode.Location = new System.Drawing.Point(12, 152);
            this.lblTerrainMode.Name = "lblTerrainMode";
            this.lblTerrainMode.Size = new System.Drawing.Size(70, 13);
            this.lblTerrainMode.TabIndex = 14;
            this.lblTerrainMode.Text = "Terrain Mode";
            //
            // cbxTerrainMode
            //
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
            this.cbxTerrainMode.Location = new System.Drawing.Point(115, 148);
            this.cbxTerrainMode.Name = "cbxTerrainMode";
            this.cbxTerrainMode.Size = new System.Drawing.Size(245, 21);
            this.cbxTerrainMode.TabIndex = 15;
            this.cbxTerrainMode.SelectedIndexChanged += new System.EventHandler(this.Value_SelectedIndexChanged);
            //
            // btnOK
            //
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(204, 174);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            //
            // btnCancel
            //
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(285, 175);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // HeaderEditorForm
            //
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(372, 210);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbxTerrainMode);
            this.Controls.Add(this.lblTerrainMode);
            this.Controls.Add(this.cbxBackgroundScenery);
            this.Controls.Add(this.lblAreaPlatformType);
            this.Controls.Add(this.cbxAreaPlatformType);
            this.Controls.Add(this.lblForeground);
            this.Controls.Add(this.cbxForeground);
            this.Controls.Add(this.cbxPosition);
            this.Controls.Add(this.cbxTime);
            this.Controls.Add(this.lblScenery);
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.lblTime);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HeaderEditorForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Edit Header";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Label lblScenery;
        private System.Windows.Forms.ComboBox cbxTime;
        private System.Windows.Forms.ComboBox cbxPosition;
        private System.Windows.Forms.ComboBox cbxForeground;
        private System.Windows.Forms.Label lblForeground;
        private System.Windows.Forms.ComboBox cbxAreaPlatformType;
        private System.Windows.Forms.Label lblAreaPlatformType;
        private System.Windows.Forms.ComboBox cbxBackgroundScenery;
        private System.Windows.Forms.Label lblTerrainMode;
        private System.Windows.Forms.ComboBox cbxTerrainMode;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}
