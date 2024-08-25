namespace Brutario.Win.Views;

using System.ComponentModel;
using System.Windows.Forms;

public class SaveFileNameSelector : FileNameSelectorBase
{
    public SaveFileNameSelector() : base()
    {
        SaveFileDialog = new SaveFileDialog();
    }

    public SaveFileNameSelector(IContainer container)
        : base(container)
    {
        SaveFileDialog = new SaveFileDialog();
    }

    public SaveFileDialog SaveFileDialog { get; }

    protected override FileDialog FileDialog
    {
        get
        {
            return SaveFileDialog;
        }
    }
}
