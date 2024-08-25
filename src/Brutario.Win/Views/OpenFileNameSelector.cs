namespace Brutario.Win.Views;

using System.ComponentModel;
using System.Windows.Forms;

public class OpenFileNameSelector : FileNameSelectorBase
{
    public OpenFileNameSelector()
        : base()
    {
        OpenFileDialog = new OpenFileDialog();
    }

    public OpenFileNameSelector(IContainer container)
        : base(container)
    {
        OpenFileDialog = new OpenFileDialog();
    }

    public OpenFileDialog OpenFileDialog { get; }

    protected override FileDialog FileDialog
    {
        get
        {
            return OpenFileDialog;
        }
    }
}
