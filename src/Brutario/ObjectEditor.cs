namespace Brutario
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    using Smb1;

    using static System.ComponentModel.DesignerSerializationVisibility;

    public sealed partial class ObjectEditor : Component, IObjectEditor
    {
        private IWin32Window _owner;

        public ObjectEditor()
        {
            InitializeComponent();
        }

        public ObjectEditor(IContainer container)
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            container.Add(this);

            InitializeComponent();
        }

        public event EventHandler AreaObjectCommandChanged;

        public event EventHandler OwnerChanged;

        [Browsable(false)]
        [DesignerSerializationVisibility(Hidden)]
        public AreaObjectCommand AreaObjectCommand
        {
            get
            {
                return objectEditorDialog.AreaObjectCommand;
            }

            set
            {
                objectEditorDialog.AreaObjectCommand = value;
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

        public bool TryEditObject(AreaPlatformType areaPlatformType)
        {
            objectEditorDialog.UpdatePlatformTypeDescription(areaPlatformType);
            return objectEditorDialog.ShowDialog(Owner) == DialogResult.OK;
        }

        private void ObjectEditorDialog_AreaObjectCommandChanged(
            object sender,
            EventArgs e)
        {
            AreaObjectCommandChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
