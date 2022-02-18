namespace Brutario
{
    partial class ObjectListWindow
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
            this.lvwObjects = new System.Windows.Forms.ListView();
            this.chdHex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdPage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdPosition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvwObjects
            // 
            this.lvwObjects.AutoArrange = false;
            this.lvwObjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chdHex,
            this.chdPage,
            this.chdPosition,
            this.chdType});
            this.lvwObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwObjects.FullRowSelect = true;
            this.lvwObjects.GridLines = true;
            this.lvwObjects.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwObjects.HideSelection = false;
            this.lvwObjects.LabelWrap = false;
            this.lvwObjects.Location = new System.Drawing.Point(0, 0);
            this.lvwObjects.Name = "lvwObjects";
            this.lvwObjects.ShowGroups = false;
            this.lvwObjects.Size = new System.Drawing.Size(496, 334);
            this.lvwObjects.TabIndex = 0;
            this.lvwObjects.UseCompatibleStateImageBehavior = false;
            this.lvwObjects.View = System.Windows.Forms.View.Details;
            this.lvwObjects.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwObjects_MouseDoubleClick);
            // 
            // chdHex
            // 
            this.chdHex.Text = "Hex";
            this.chdHex.Width = 57;
            // 
            // chdPage
            // 
            this.chdPage.Text = "Page";
            this.chdPage.Width = 63;
            // 
            // chdPosition
            // 
            this.chdPosition.Text = "Position";
            this.chdPosition.Width = 62;
            // 
            // chdType
            // 
            this.chdType.Text = "Type";
            this.chdType.Width = 310;
            // 
            // ObjectListWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 334);
            this.Controls.Add(this.lvwObjects);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(512, 373);
            this.Name = "ObjectListWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Object List";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwObjects;
        private System.Windows.Forms.ColumnHeader chdHex;
        private System.Windows.Forms.ColumnHeader chdPage;
        private System.Windows.Forms.ColumnHeader chdPosition;
        private System.Windows.Forms.ColumnHeader chdType;
    }
}