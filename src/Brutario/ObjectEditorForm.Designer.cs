namespace Brutario
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
            this.nudLength = new System.Windows.Forms.NumericUpDown();
            this.lblLength = new System.Windows.Forms.Label();
            this.lblObject = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.chkPageFlag = new System.Windows.Forms.CheckBox();
            this.lblY = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.nudY = new System.Windows.Forms.NumericUpDown();
            this.nudX = new System.Windows.Forms.NumericUpDown();
            this.gbxBinary = new System.Windows.Forms.GroupBox();
            this.tbxBinary = new System.Windows.Forms.TextBox();
            this.chkBinary = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
            this.gbxBinary.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(207, 112);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(288, 111);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudLength);
            this.groupBox1.Controls.Add(this.lblLength);
            this.groupBox1.Controls.Add(this.lblObject);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.chkPageFlag);
            this.groupBox1.Controls.Add(this.lblY);
            this.groupBox1.Controls.Add(this.lblX);
            this.groupBox1.Controls.Add(this.nudY);
            this.groupBox1.Controls.Add(this.nudX);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 73);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Object";
            // 
            // nudLength
            // 
            this.nudLength.Location = new System.Drawing.Point(261, 41);
            this.nudLength.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudLength.Name = "nudLength";
            this.nudLength.Size = new System.Drawing.Size(35, 20);
            this.nudLength.TabIndex = 8;
            this.nudLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudLength.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(215, 43);
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
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(50, 40);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(159, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // chkPageFlag
            // 
            this.chkPageFlag.AutoSize = true;
            this.chkPageFlag.Location = new System.Drawing.Point(172, 15);
            this.chkPageFlag.Name = "chkPageFlag";
            this.chkPageFlag.Size = new System.Drawing.Size(74, 17);
            this.chkPageFlag.TabIndex = 4;
            this.chkPageFlag.Text = "Page Flag";
            this.chkPageFlag.UseVisualStyleBackColor = true;
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(91, 16);
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
            this.nudY.Location = new System.Drawing.Point(131, 14);
            this.nudY.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudY.Name = "nudY";
            this.nudY.Size = new System.Drawing.Size(35, 20);
            this.nudY.TabIndex = 1;
            this.nudY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudY.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
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
            this.nudX.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // gbxBinary
            // 
            this.gbxBinary.Controls.Add(this.tbxBinary);
            this.gbxBinary.Controls.Add(this.chkBinary);
            this.gbxBinary.Location = new System.Drawing.Point(12, 91);
            this.gbxBinary.Name = "gbxBinary";
            this.gbxBinary.Size = new System.Drawing.Size(189, 59);
            this.gbxBinary.TabIndex = 3;
            this.gbxBinary.TabStop = false;
            // 
            // tbxBinary
            // 
            this.tbxBinary.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxBinary.Location = new System.Drawing.Point(9, 23);
            this.tbxBinary.MaxLength = 8;
            this.tbxBinary.Name = "tbxBinary";
            this.tbxBinary.Size = new System.Drawing.Size(157, 20);
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
            this.chkBinary.CheckedChanged += new System.EventHandler(this.Binary_TextChanged);
            // 
            // ObjectEditorForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(374, 157);
            this.Controls.Add(this.gbxBinary);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ObjectEditorForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ObjectEditorForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).EndInit();
            this.gbxBinary.ResumeLayout(false);
            this.gbxBinary.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudLength;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lblObject;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox chkPageFlag;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.NumericUpDown nudY;
        private System.Windows.Forms.NumericUpDown nudX;
        private System.Windows.Forms.GroupBox gbxBinary;
        private System.Windows.Forms.TextBox tbxBinary;
        private System.Windows.Forms.CheckBox chkBinary;
    }
}