namespace Brutario
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
            this.gbxBinary = new System.Windows.Forms.GroupBox();
            this.tbxBinary = new System.Windows.Forms.TextBox();
            this.chkBinary = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxAreaNumber = new System.Windows.Forms.TextBox();
            this.lblAreaNumber = new System.Windows.Forms.Label();
            this.nudWorld = new System.Windows.Forms.NumericUpDown();
            this.lblWorld = new System.Windows.Forms.Label();
            this.lblPage = new System.Windows.Forms.Label();
            this.nudPage = new System.Windows.Forms.NumericUpDown();
            this.chkHardFlag = new System.Windows.Forms.CheckBox();
            this.lblObject = new System.Windows.Forms.Label();
            this.cbxAreaSpriteCode = new System.Windows.Forms.ComboBox();
            this.chkPageFlag = new System.Windows.Forms.CheckBox();
            this.lblY = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.nudY = new System.Windows.Forms.NumericUpDown();
            this.nudX = new System.Windows.Forms.NumericUpDown();
            this.gbxBinary.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWorld)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxBinary
            // 
            this.gbxBinary.Controls.Add(this.tbxBinary);
            this.gbxBinary.Controls.Add(this.chkBinary);
            this.gbxBinary.Location = new System.Drawing.Point(12, 111);
            this.gbxBinary.Name = "gbxBinary";
            this.gbxBinary.Size = new System.Drawing.Size(188, 59);
            this.gbxBinary.TabIndex = 6;
            this.gbxBinary.TabStop = false;
            // 
            // tbxBinary
            // 
            this.tbxBinary.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxBinary.Location = new System.Drawing.Point(9, 23);
            this.tbxBinary.MaxLength = 8;
            this.tbxBinary.Name = "tbxBinary";
            this.tbxBinary.Size = new System.Drawing.Size(173, 20);
            this.tbxBinary.TabIndex = 1;
            this.tbxBinary.WordWrap = false;
            this.tbxBinary.TextChanged += new System.EventHandler(this.Binary_TextChanged);
            // 
            // chkBinary
            // 
            this.chkBinary.AutoSize = true;
            this.chkBinary.Location = new System.Drawing.Point(9, 0);
            this.chkBinary.Name = "chkBinary";
            this.chkBinary.Size = new System.Drawing.Size(124, 17);
            this.chkBinary.TabIndex = 0;
            this.chkBinary.Text = "Enter value manually";
            this.chkBinary.UseVisualStyleBackColor = true;
            this.chkBinary.CheckedChanged += new System.EventHandler(this.Binary_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(287, 131);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(206, 131);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxAreaNumber);
            this.groupBox1.Controls.Add(this.lblAreaNumber);
            this.groupBox1.Controls.Add(this.nudWorld);
            this.groupBox1.Controls.Add(this.lblWorld);
            this.groupBox1.Controls.Add(this.lblPage);
            this.groupBox1.Controls.Add(this.nudPage);
            this.groupBox1.Controls.Add(this.chkHardFlag);
            this.groupBox1.Controls.Add(this.lblObject);
            this.groupBox1.Controls.Add(this.cbxAreaSpriteCode);
            this.groupBox1.Controls.Add(this.chkPageFlag);
            this.groupBox1.Controls.Add(this.lblY);
            this.groupBox1.Controls.Add(this.lblX);
            this.groupBox1.Controls.Add(this.nudY);
            this.groupBox1.Controls.Add(this.nudX);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 93);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Object";
            // 
            // tbxAreaNumber
            // 
            this.tbxAreaNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxAreaNumber.Location = new System.Drawing.Point(315, 66);
            this.tbxAreaNumber.MaxLength = 2;
            this.tbxAreaNumber.Name = "tbxAreaNumber";
            this.tbxAreaNumber.Size = new System.Drawing.Size(29, 20);
            this.tbxAreaNumber.TabIndex = 2;
            this.tbxAreaNumber.Text = "25";
            this.tbxAreaNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbxAreaNumber.WordWrap = false;
            this.tbxAreaNumber.TextChanged += new System.EventHandler(this.AreaNumber_TextChanged);
            // 
            // lblAreaNumber
            // 
            this.lblAreaNumber.AutoSize = true;
            this.lblAreaNumber.Location = new System.Drawing.Point(240, 69);
            this.lblAreaNumber.Name = "lblAreaNumber";
            this.lblAreaNumber.Size = new System.Drawing.Size(69, 13);
            this.lblAreaNumber.TabIndex = 27;
            this.lblAreaNumber.Text = "Area Number";
            // 
            // nudWorld
            // 
            this.nudWorld.Location = new System.Drawing.Point(132, 67);
            this.nudWorld.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudWorld.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWorld.Name = "nudWorld";
            this.nudWorld.Size = new System.Drawing.Size(35, 20);
            this.nudWorld.TabIndex = 26;
            this.nudWorld.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudWorld.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWorld.ValueChanged += new System.EventHandler(this.Item_ValueChanged);
            // 
            // lblWorld
            // 
            this.lblWorld.AutoSize = true;
            this.lblWorld.Location = new System.Drawing.Point(91, 69);
            this.lblWorld.Name = "lblWorld";
            this.lblWorld.Size = new System.Drawing.Size(35, 13);
            this.lblWorld.TabIndex = 25;
            this.lblWorld.Text = "World";
            // 
            // lblPage
            // 
            this.lblPage.AutoSize = true;
            this.lblPage.Location = new System.Drawing.Point(6, 69);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(32, 13);
            this.lblPage.TabIndex = 24;
            this.lblPage.Text = "Page";
            // 
            // nudPage
            // 
            this.nudPage.Location = new System.Drawing.Point(50, 67);
            this.nudPage.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nudPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPage.Name = "nudPage";
            this.nudPage.Size = new System.Drawing.Size(35, 20);
            this.nudPage.TabIndex = 23;
            this.nudPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPage.ValueChanged += new System.EventHandler(this.Item_ValueChanged);
            // 
            // chkHardFlag
            // 
            this.chkHardFlag.AutoSize = true;
            this.chkHardFlag.Location = new System.Drawing.Point(192, 15);
            this.chkHardFlag.Name = "chkHardFlag";
            this.chkHardFlag.Size = new System.Drawing.Size(72, 17);
            this.chkHardFlag.TabIndex = 22;
            this.chkHardFlag.Text = "Hard Flag";
            this.chkHardFlag.UseVisualStyleBackColor = true;
            this.chkHardFlag.CheckedChanged += new System.EventHandler(this.Item_ValueChanged);
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
            // cbxAreaSpriteCode
            // 
            this.cbxAreaSpriteCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAreaSpriteCode.FormattingEnabled = true;
            this.cbxAreaSpriteCode.Location = new System.Drawing.Point(50, 40);
            this.cbxAreaSpriteCode.Name = "cbxAreaSpriteCode";
            this.cbxAreaSpriteCode.Size = new System.Drawing.Size(294, 21);
            this.cbxAreaSpriteCode.TabIndex = 5;
            this.cbxAreaSpriteCode.SelectedIndexChanged += new System.EventHandler(this.AreaSpriteCode_SelectedIndexChanged);
            // 
            // chkPageFlag
            // 
            this.chkPageFlag.AutoSize = true;
            this.chkPageFlag.Location = new System.Drawing.Point(270, 15);
            this.chkPageFlag.Name = "chkPageFlag";
            this.chkPageFlag.Size = new System.Drawing.Size(74, 17);
            this.chkPageFlag.TabIndex = 4;
            this.chkPageFlag.Text = "Page Flag";
            this.chkPageFlag.UseVisualStyleBackColor = true;
            this.chkPageFlag.CheckedChanged += new System.EventHandler(this.Item_ValueChanged);
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(92, 16);
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
            this.nudY.Location = new System.Drawing.Point(132, 14);
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
            // SpriteEditorForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(374, 182);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbxBinary);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpriteEditorForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Sprite Editor";
            this.gbxBinary.ResumeLayout(false);
            this.gbxBinary.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWorld)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxBinary;
        private System.Windows.Forms.TextBox tbxBinary;
        private System.Windows.Forms.CheckBox chkBinary;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblObject;
        private System.Windows.Forms.ComboBox cbxAreaSpriteCode;
        private System.Windows.Forms.CheckBox chkPageFlag;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.NumericUpDown nudY;
        private System.Windows.Forms.NumericUpDown nudX;
        private System.Windows.Forms.TextBox tbxAreaNumber;
        private System.Windows.Forms.Label lblAreaNumber;
        private System.Windows.Forms.NumericUpDown nudWorld;
        private System.Windows.Forms.Label lblWorld;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.NumericUpDown nudPage;
        private System.Windows.Forms.CheckBox chkHardFlag;
    }
}