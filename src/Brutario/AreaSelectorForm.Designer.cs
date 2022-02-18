// <copyright file="AreaSelectorForm.Designer.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario
{
    partial class AreaSelectorForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Bonus Room");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Level 1", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Bonus Room");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Main Area", new System.Windows.Forms.TreeNode[] {
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Level 2", new System.Windows.Forms.TreeNode[] {
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Level 3");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Level 4");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("World 1", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode5,
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("World 2");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("World 3");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("World 4");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("World 5");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("World 6");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("World 7");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("World 8");
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.areaControl1 = new Brutario.AreaControl();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "B0";
            treeNode1.Text = "Bonus Room";
            treeNode2.Name = "L1";
            treeNode2.Text = "Level 1";
            treeNode3.Name = "B0";
            treeNode3.Text = "Bonus Room";
            treeNode4.Name = "L2.5";
            treeNode4.Text = "Main Area";
            treeNode5.Name = "Level 2";
            treeNode5.Text = "Level 2";
            treeNode6.Name = "L3";
            treeNode6.Text = "Level 3";
            treeNode7.Name = "L4";
            treeNode7.Text = "Level 4";
            treeNode8.Name = "W1";
            treeNode8.Text = "World 1";
            treeNode9.Name = "W2";
            treeNode9.Text = "World 2";
            treeNode10.Name = "W3";
            treeNode10.Text = "World 3";
            treeNode11.Name = "W4";
            treeNode11.Text = "World 4";
            treeNode12.Name = "W5";
            treeNode12.Text = "World 5";
            treeNode13.Name = "W6";
            treeNode13.Text = "World 6";
            treeNode14.Name = "W7";
            treeNode14.Text = "World 7";
            treeNode15.Name = "W8";
            treeNode15.Text = "World 8";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15});
            this.treeView1.Size = new System.Drawing.Size(242, 258);
            this.treeView1.TabIndex = 0;
            // 
            // areaControl1
            // 
            this.areaControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.areaControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.areaControl1.Location = new System.Drawing.Point(241, 0);
            this.areaControl1.Name = "areaControl1";
            this.areaControl1.Size = new System.Drawing.Size(514, 258);
            this.areaControl1.TabIndex = 1;
            // 
            // AreaSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 258);
            this.Controls.Add(this.areaControl1);
            this.Controls.Add(this.treeView1);
            this.Name = "AreaSelectorForm";
            this.Text = "AreaSelector";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private AreaControl areaControl1;
    }
}