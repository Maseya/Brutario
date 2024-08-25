namespace Brutario.Win;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

public partial class AutoSaveForm : Form
{
    public AutoSaveForm()
    {
        InitializeComponent();
    }

    public bool EnableAutoSave
    {
        get
        {
            return chkAutoSave.Checked;
        }

        set
        {
            chkAutoSave.Checked = value;
        }
    }

    public bool EnablePruning
    {
        get
        {
            return chkPrune.Checked;
        }

        set
        {
            chkPrune.Checked = value;
        }
    }

    public TimeSpan AutoSaveInterval
    {
        get
        {
            if (!Int32.TryParse(
                tbxTime.Text,
                CultureInfo.CurrentUICulture,
                out var time))
            {
                return TimeSpan.Zero;
            }

            var conversion = (int)Math.Pow(60, cbxUnits.SelectedIndex);
            return new TimeSpan(0, 0, time * conversion);
        }

        set
        {
            if (value.TotalMinutes < 1)
            {
                cbxUnits.SelectedIndex = 0;
                tbxTime.Text = ((int)value.TotalSeconds)
                    .ToString(CultureInfo.CurrentUICulture);
            }
            else if (value.TotalHours < 1)
            {
                cbxUnits.SelectedIndex = 1;
                tbxTime.Text += ((int)value.TotalMinutes)
                    .ToString(CultureInfo.CurrentUICulture);
            }
            else
            {
                cbxUnits.SelectedIndex = 2;
                tbxTime.Text = ((int)value.TotalHours)
                    .ToString(CultureInfo.CurrentUICulture);
            }
        }
    }

    public TimeSpan PruningInterval
    {
        get
        {
            if (!Int32.TryParse(
                tbxCutoffTime.Text,
                CultureInfo.CurrentUICulture,
                out var time))
            {
                return TimeSpan.Zero;
            }

            switch (cbxCutoffUnits.SelectedIndex)
            {
                case 0:
                    return new TimeSpan(0, time, 0);
                case 1:
                    return new TimeSpan(time, 0, 0);
                case 2:
                    return new TimeSpan(time, 0, 0, 0);
                case 3:
                    return new TimeSpan(7 * time, 0, 0);
                default:
                    Debug.Assert(false);
                    return TimeSpan.Zero;
            }
        }

        set
        {
            if (value.TotalHours < 1)
            {
                cbxCutoffUnits.SelectedIndex = 0;
                tbxCutoffTime.Text = ((int)value.TotalMinutes)
                    .ToString(CultureInfo.CurrentUICulture);
            }
            else if (value.TotalDays < 1)
            {
                cbxCutoffUnits.SelectedIndex = 1;
                tbxCutoffTime.Text += ((int)value.TotalHours)
                    .ToString(CultureInfo.CurrentUICulture);
            }
            else if (value.TotalDays < 7)
            {

                cbxCutoffUnits.SelectedIndex = 2;
                tbxCutoffTime.Text = ((int)value.TotalDays)
                    .ToString(CultureInfo.CurrentUICulture);
            }
            else
            {
                cbxCutoffUnits.SelectedIndex = 3;
                tbxCutoffTime.Text = (7 * (int)value.TotalDays)
                    .ToString(CultureInfo.CurrentUICulture);
            }
        }
    }

    public bool HardCutoff
    {
        get
        {
            return rdbConstant.Checked;
        }

        set
        {
            rdbConstant.Checked = value;
            rdbProgressive.Checked = !value;
        }
    }

    private void UpdateOKEnabled()
    {
        if (EnableAutoSave)
        {
            if (!Int32.TryParse(
                tbxTime.Text,
                CultureInfo.CurrentUICulture,
                out var _))
            {
                btnOK.Enabled = false;
                return;
            }
        }

        if (EnablePruning)
        {
            if (!Int32.TryParse(
                tbxCutoffTime.Text,
                CultureInfo.CurrentUICulture,
                out var _))
            {
                btnOK.Enabled = false;
                return;
            }
        }

        btnOK.Enabled = true;
    }

    private void AutoSave_CheckedChanged(object sender, EventArgs e)
    {
        tbxTime.Enabled =
        cbxUnits.Enabled = chkAutoSave.Checked;
        UpdateOKEnabled();
    }

    private void Prune_CheckedChanged(object sender, EventArgs e)
    {
        tbxCutoffTime.Enabled =
        cbxCutoffUnits.Enabled = chkPrune.Checked;
        UpdateOKEnabled();
    }

    private void AutoSaveForm_Load(object sender, EventArgs e)
    {
    }

    private void Time_TextChanged(object sender, EventArgs e)
    {
        UpdateOKEnabled();
    }
}
