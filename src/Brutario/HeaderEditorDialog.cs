namespace Brutario
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    using Smb1;

    using static System.ComponentModel.DesignerSerializationVisibility;

    public sealed class HeaderEditorDialog : DialogProxy, IHeaderView
    {
        public HeaderEditorDialog()
        {
            HeaderEditorForm = new HeaderEditorForm();
            HeaderEditorForm.AreaHeaderChanged += HeaderEditorForm_AreaHeaderChanged;
        }

        public HeaderEditorDialog(IContainer container)
            : this()
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            container.Add(this);
        }

        public event EventHandler AreaHeaderChanged;

        [Browsable(false)]
        [DesignerSerializationVisibility(Hidden)]
        public AreaHeader AreaHeader
        {
            get
            {
                return HeaderEditorForm.AreaHeader;
            }

            set
            {
                HeaderEditorForm.AreaHeader = value;
            }
        }

        protected override Form BaseForm
        {
            get
            {
                return HeaderEditorForm;
            }
        }

        private HeaderEditorForm HeaderEditorForm
        {
            get;
        }

        private void HeaderEditorForm_AreaHeaderChanged(object sender, EventArgs e)
        {
            AreaHeaderChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
