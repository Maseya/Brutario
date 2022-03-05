namespace Brutario
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    using Smb1;

    public sealed partial class HeaderEditor : Component, IHeaderEditor
    {
        private IWin32Window _owner;

        public HeaderEditor()
        {
            InitializeComponent();
        }

        public HeaderEditor(IContainer container)
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            container.Add(this);

            InitializeComponent();
        }

        public event EventHandler AreaHeaderChanged;

        public event EventHandler OwnerChanged;

        public AreaHeader AreaHeader
        {
            get
            {
                return headerEditorDialog.AreaHeader;
            }

            set
            {
                headerEditorDialog.AreaHeader = value;
            }
        }

        public IWin32Window Owner
        {
            get
            {
                return _owner;
            }

            set
            {
                if (Owner == value)
                {
                    return;
                }

                _owner = value;
                OwnerChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool TryEditAreaHeader()
        {
            return headerEditorDialog.ShowDialog(Owner) == DialogResult.OK;
        }

        private void HeaderEditorDialog_AreaHeaderChanged(
            object sender,
            EventArgs e)
        {
            AreaHeaderChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
