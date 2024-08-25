namespace Brutario.Win;

partial class AutoSaveForm
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
        chkAutoSave = new CheckBox();
        tbxTime = new TextBox();
        cbxUnits = new ComboBox();
        chkPrune = new CheckBox();
        tbxCutoffTime = new TextBox();
        cbxCutoffUnits = new ComboBox();
        gbxPruneMode = new GroupBox();
        rdbProgressive = new RadioButton();
        rdbConstant = new RadioButton();
        gbxPruneMode.SuspendLayout();
        SuspendLayout();
        // 
        // btnOK
        // 
        btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnOK.DialogResult = DialogResult.OK;
        btnOK.Location = new Point(247, 180);
        btnOK.Name = "btnOK";
        btnOK.Size = new Size(94, 29);
        btnOK.TabIndex = 0;
        btnOK.Text = "&OK";
        btnOK.UseVisualStyleBackColor = true;
        // 
        // btnCancel
        // 
        btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(347, 180);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(94, 29);
        btnCancel.TabIndex = 1;
        btnCancel.Text = "&Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        // 
        // chkAutoSave
        // 
        chkAutoSave.AutoSize = true;
        chkAutoSave.Checked = true;
        chkAutoSave.CheckState = CheckState.Checked;
        chkAutoSave.Location = new Point(12, 12);
        chkAutoSave.Name = "chkAutoSave";
        chkAutoSave.Size = new Size(135, 24);
        chkAutoSave.TabIndex = 2;
        chkAutoSave.Text = "Auto save every";
        chkAutoSave.UseVisualStyleBackColor = true;
        chkAutoSave.CheckedChanged += AutoSave_CheckedChanged;
        // 
        // tbxTime
        // 
        tbxTime.Location = new Point(153, 10);
        tbxTime.Name = "tbxTime";
        tbxTime.Size = new Size(73, 27);
        tbxTime.TabIndex = 3;
        tbxTime.TextChanged += Time_TextChanged;
        // 
        // cbxUnits
        // 
        cbxUnits.FormattingEnabled = true;
        cbxUnits.Items.AddRange(new object[] { "seconds", "minutes", "hours" });
        cbxUnits.Location = new Point(232, 10);
        cbxUnits.Name = "cbxUnits";
        cbxUnits.Size = new Size(151, 28);
        cbxUnits.TabIndex = 4;
        // 
        // chkPrune
        // 
        chkPrune.AutoSize = true;
        chkPrune.Checked = true;
        chkPrune.CheckState = CheckState.Checked;
        chkPrune.Location = new Point(12, 46);
        chkPrune.Name = "chkPrune";
        chkPrune.Size = new Size(222, 24);
        chkPrune.TabIndex = 5;
        chkPrune.Text = "Delete auto-saves older than";
        chkPrune.UseVisualStyleBackColor = true;
        chkPrune.CheckedChanged += Prune_CheckedChanged;
        // 
        // tbxCutoffTime
        // 
        tbxCutoffTime.Location = new Point(240, 44);
        tbxCutoffTime.Name = "tbxCutoffTime";
        tbxCutoffTime.Size = new Size(75, 27);
        tbxCutoffTime.TabIndex = 6;
        tbxCutoffTime.TextChanged += Time_TextChanged;
        // 
        // cbxCutoffUnits
        // 
        cbxCutoffUnits.FormattingEnabled = true;
        cbxCutoffUnits.Items.AddRange(new object[] { "minutes", "hours", "days", "weeks" });
        cbxCutoffUnits.Location = new Point(321, 44);
        cbxCutoffUnits.Name = "cbxCutoffUnits";
        cbxCutoffUnits.Size = new Size(116, 28);
        cbxCutoffUnits.TabIndex = 7;
        // 
        // gbxPruneMode
        // 
        gbxPruneMode.Controls.Add(rdbProgressive);
        gbxPruneMode.Controls.Add(rdbConstant);
        gbxPruneMode.Location = new Point(12, 77);
        gbxPruneMode.Name = "gbxPruneMode";
        gbxPruneMode.Size = new Size(409, 91);
        gbxPruneMode.TabIndex = 8;
        gbxPruneMode.TabStop = false;
        gbxPruneMode.Text = "Pruning Mode";
        // 
        // rdbProgressive
        // 
        rdbProgressive.AutoSize = true;
        rdbProgressive.Location = new Point(6, 56);
        rdbProgressive.Name = "rdbProgressive";
        rdbProgressive.Size = new Size(388, 24);
        rdbProgressive.TabIndex = 1;
        rdbProgressive.TabStop = true;
        rdbProgressive.Text = "Retain some saves but delete progressively older ones";
        rdbProgressive.UseVisualStyleBackColor = true;
        // 
        // rdbConstant
        // 
        rdbConstant.AutoSize = true;
        rdbConstant.Location = new Point(6, 26);
        rdbConstant.Name = "rdbConstant";
        rdbConstant.Size = new Size(282, 24);
        rdbConstant.TabIndex = 0;
        rdbConstant.TabStop = true;
        rdbConstant.Text = "Delete all saves older than cutoff date";
        rdbConstant.UseVisualStyleBackColor = true;
        // 
        // AutoSaveForm
        // 
        AcceptButton = btnOK;
        CancelButton = btnCancel;
        ClientSize = new Size(453, 221);
        Controls.Add(gbxPruneMode);
        Controls.Add(cbxCutoffUnits);
        Controls.Add(tbxCutoffTime);
        Controls.Add(chkPrune);
        Controls.Add(cbxUnits);
        Controls.Add(tbxTime);
        Controls.Add(chkAutoSave);
        Controls.Add(btnCancel);
        Controls.Add(btnOK);
        KeyPreview = true;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "AutoSaveForm";
        ShowIcon = false;
        ShowInTaskbar = false;
        Text = "Configure Auto Save";
        Load += AutoSaveForm_Load;
        gbxPruneMode.ResumeLayout(false);
        gbxPruneMode.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.CheckBox chkAutoSave;
    private System.Windows.Forms.TextBox tbxTime;
    private System.Windows.Forms.ComboBox cbxUnits;
    private System.Windows.Forms.CheckBox chkPrune;
    private System.Windows.Forms.TextBox tbxCutoffTime;
    private System.Windows.Forms.GroupBox gbxPruneMode;
    private System.Windows.Forms.RadioButton rdbProgressive;
    private System.Windows.Forms.RadioButton rdbConstant;
    private System.Windows.Forms.ComboBox cbxCutoffUnits;
}