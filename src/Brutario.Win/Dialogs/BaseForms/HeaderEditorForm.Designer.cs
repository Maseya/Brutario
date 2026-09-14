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
            lblTime = new Label();
            lblPosition = new Label();
            lblScenery = new Label();
            cbxTime = new ComboBox();
            cbxPosition = new ComboBox();
            cbxForeground = new ComboBox();
            lblForeground = new Label();
            cbxAreaPlatformType = new ComboBox();
            lblAreaPlatformType = new Label();
            cbxBackgroundScenery = new ComboBox();
            lblTerrainMode = new Label();
            cbxTerrainMode = new ComboBox();
            btnOK = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Location = new Point(14, 17);
            lblTime.Margin = new Padding(4, 0, 4, 0);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(33, 15);
            lblTime.TabIndex = 0;
            lblTime.Text = "Time";
            // 
            // lblPosition
            // 
            lblPosition.AutoSize = true;
            lblPosition.Location = new Point(14, 48);
            lblPosition.Margin = new Padding(4, 0, 4, 0);
            lblPosition.Name = "lblPosition";
            lblPosition.Size = new Size(50, 15);
            lblPosition.TabIndex = 1;
            lblPosition.Text = "Position";
            // 
            // lblScenery
            // 
            lblScenery.AutoSize = true;
            lblScenery.Location = new Point(14, 143);
            lblScenery.Margin = new Padding(4, 0, 4, 0);
            lblScenery.Name = "lblScenery";
            lblScenery.Size = new Size(48, 15);
            lblScenery.TabIndex = 4;
            lblScenery.Text = "Scenery";
            // 
            // cbxTime
            // 
            cbxTime.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxTime.FormattingEnabled = true;
            cbxTime.Items.AddRange(new object[] { "Not Set", "400", "300", "200" });
            cbxTime.Location = new Point(134, 14);
            cbxTime.Margin = new Padding(4, 3, 4, 3);
            cbxTime.Name = "cbxTime";
            cbxTime.Size = new Size(285, 23);
            cbxTime.TabIndex = 7;
            cbxTime.SelectedIndexChanged += Value_SelectedIndexChanged;
            // 
            // cbxPosition
            // 
            cbxPosition.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxPosition.FormattingEnabled = true;
            cbxPosition.Items.AddRange(new object[] { "-1", "-1; from another area", "10", "4", "-1", "-1", "10 (Autowalk)", "10 (Autowalk)" });
            cbxPosition.Location = new Point(134, 45);
            cbxPosition.Margin = new Padding(4, 3, 4, 3);
            cbxPosition.Name = "cbxPosition";
            cbxPosition.Size = new Size(285, 23);
            cbxPosition.TabIndex = 8;
            cbxPosition.SelectedIndexChanged += Value_SelectedIndexChanged;
            // 
            // cbxForeground
            // 
            cbxForeground.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxForeground.FormattingEnabled = true;
            cbxForeground.Items.AddRange(new object[] { "None", "Underwater", "Castle Wall (Unused)", "Over Water", "Night (Unused)", "Snow (Unused)", "Night and Snow (Unused)", "Castle (unused)" });
            cbxForeground.Location = new Point(134, 76);
            cbxForeground.Margin = new Padding(4, 3, 4, 3);
            cbxForeground.Name = "cbxForeground";
            cbxForeground.Size = new Size(285, 23);
            cbxForeground.TabIndex = 9;
            cbxForeground.SelectedIndexChanged += Value_SelectedIndexChanged;
            // 
            // lblForeground
            // 
            lblForeground.AutoSize = true;
            lblForeground.Location = new Point(14, 80);
            lblForeground.Margin = new Padding(4, 0, 4, 0);
            lblForeground.Name = "lblForeground";
            lblForeground.Size = new Size(69, 15);
            lblForeground.TabIndex = 10;
            lblForeground.Text = "Foreground";
            // 
            // cbxAreaPlatformType
            // 
            cbxAreaPlatformType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxAreaPlatformType.FormattingEnabled = true;
            cbxAreaPlatformType.Items.AddRange(new object[] { "Trees", "Mushrooms", "Bullet Bill Turrets", "Cloud Ground" });
            cbxAreaPlatformType.Location = new Point(134, 107);
            cbxAreaPlatformType.Margin = new Padding(4, 3, 4, 3);
            cbxAreaPlatformType.Name = "cbxAreaPlatformType";
            cbxAreaPlatformType.Size = new Size(285, 23);
            cbxAreaPlatformType.TabIndex = 11;
            cbxAreaPlatformType.SelectedIndexChanged += Value_SelectedIndexChanged;
            // 
            // lblAreaPlatformType
            // 
            lblAreaPlatformType.AutoSize = true;
            lblAreaPlatformType.Location = new Point(14, 111);
            lblAreaPlatformType.Margin = new Padding(4, 0, 4, 0);
            lblAreaPlatformType.Name = "lblAreaPlatformType";
            lblAreaPlatformType.Size = new Size(107, 15);
            lblAreaPlatformType.TabIndex = 12;
            lblAreaPlatformType.Text = "Area Platform Type";
            // 
            // cbxBackgroundScenery
            // 
            cbxBackgroundScenery.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxBackgroundScenery.FormattingEnabled = true;
            cbxBackgroundScenery.Items.AddRange(new object[] { "Nothing", "Clouds", "Mountain", "Fence" });
            cbxBackgroundScenery.Location = new Point(134, 140);
            cbxBackgroundScenery.Margin = new Padding(4, 3, 4, 3);
            cbxBackgroundScenery.Name = "cbxBackgroundScenery";
            cbxBackgroundScenery.Size = new Size(285, 23);
            cbxBackgroundScenery.TabIndex = 13;
            cbxBackgroundScenery.SelectedIndexChanged += Value_SelectedIndexChanged;
            // 
            // lblTerrainMode
            // 
            lblTerrainMode.AutoSize = true;
            lblTerrainMode.Location = new Point(14, 175);
            lblTerrainMode.Margin = new Padding(4, 0, 4, 0);
            lblTerrainMode.Name = "lblTerrainMode";
            lblTerrainMode.Size = new Size(76, 15);
            lblTerrainMode.TabIndex = 14;
            lblTerrainMode.Text = "Terrain Mode";
            // 
            // cbxTerrainMode
            // 
            cbxTerrainMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxTerrainMode.FormattingEnabled = true;
            cbxTerrainMode.Items.AddRange(new object[] { "None", "2-tile-high floor with no ceiling", "2-tile-high floor with 1-tile-high ceiling", "2-tile-high floor with 3-tile-high ceiling", "2-tile-high floor with 4-tile-high ceiling", "2-tile-high floor with 8-tile-high ceiling", "5-tile-high floor with 1-tile-high ceiling", "5-tile-high floor with 3-tile-high ceiling", "5-tile-high floor with 4-tile-high ceiling", "6-tile-high floor with 1-tile-high ceiling", "No floor with 1-tile-high ceiling", "6-tile-high floor with 4-tile-high ceiling", "9-tile-high floor with 1-tile-high ceiling", "2-tile-high floor with 1-tile-high ceiling and 5 tiles in the middle", "2-tile-high floor with 1-tile-high ceiling and 4 tiles in the middle", "Floor tiles everywhere" });
            cbxTerrainMode.Location = new Point(134, 171);
            cbxTerrainMode.Margin = new Padding(4, 3, 4, 3);
            cbxTerrainMode.Name = "cbxTerrainMode";
            cbxTerrainMode.Size = new Size(285, 23);
            cbxTerrainMode.TabIndex = 15;
            cbxTerrainMode.SelectedIndexChanged += Value_SelectedIndexChanged;
            // 
            // btnOK
            // 
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(238, 201);
            btnOK.Margin = new Padding(4, 3, 4, 3);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(88, 27);
            btnOK.TabIndex = 16;
            btnOK.Text = "&OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(332, 202);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 27);
            btnCancel.TabIndex = 17;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // HeaderEditorForm
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(434, 242);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(cbxTerrainMode);
            Controls.Add(lblTerrainMode);
            Controls.Add(cbxBackgroundScenery);
            Controls.Add(lblAreaPlatformType);
            Controls.Add(cbxAreaPlatformType);
            Controls.Add(lblForeground);
            Controls.Add(cbxForeground);
            Controls.Add(cbxPosition);
            Controls.Add(cbxTime);
            Controls.Add(lblScenery);
            Controls.Add(lblPosition);
            Controls.Add(lblTime);
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "HeaderEditorForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Edit Header";
            ResumeLayout(false);
            PerformLayout();

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
