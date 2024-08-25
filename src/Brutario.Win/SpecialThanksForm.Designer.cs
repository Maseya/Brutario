// <copyright file="SpecialThanksForm.Designer.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Win
{
    partial class SpecialThanksForm
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
            rtbCredits = new RichTextBox();
            btnOK = new Button();
            SuspendLayout();
            // 
            // rtbCredits
            // 
            rtbCredits.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbCredits.Location = new Point(0, 0);
            rtbCredits.Margin = new Padding(4, 5, 4, 5);
            rtbCredits.Name = "rtbCredits";
            rtbCredits.ReadOnly = true;
            rtbCredits.Size = new Size(871, 1129);
            rtbCredits.TabIndex = 0;
            rtbCredits.Text = "";
            rtbCredits.WordWrap = false;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(756, 1140);
            btnOK.Margin = new Padding(4, 5, 4, 5);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(100, 35);
            btnOK.TabIndex = 1;
            btnOK.Text = "&OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // SpecialThanksForm
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(872, 1194);
            Controls.Add(btnOK);
            Controls.Add(rtbCredits);
            Margin = new Padding(4, 5, 4, 5);
            MinimizeBox = false;
            MinimumSize = new Size(887, 184);
            Name = "SpecialThanksForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Credits";
            Load += SpecialThanksForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbCredits;
        private System.Windows.Forms.Button btnOK;
    }
}
