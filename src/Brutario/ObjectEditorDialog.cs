namespace Brutario
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    using Smb1;

    using static System.ComponentModel.DesignerSerializationVisibility;

    public sealed class ObjectEditorDialog : DialogProxy, IObjectEditorView
    {
        public ObjectEditorDialog()
        {
            ObjectEditorForm = new ObjectEditorForm();
            ObjectEditorForm.AreaObjectCommandChanged +=
                ObjectEditorForm_AreaObjectCommandChanged;
        }

        public ObjectEditorDialog(IContainer container)
            : this()
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            container.Add(this);
        }

        public event EventHandler AreaObjectCommandChanged;

        [Browsable(false)]
        [DesignerSerializationVisibility(Hidden)]
        public AreaObjectCommand AreaObjectCommand
        {
            get
            {
                return ObjectEditorForm.AreaObjectCommand;
            }

            set
            {
                ObjectEditorForm.AreaObjectCommand = value;
            }
        }

        protected override Form BaseForm
        {
            get
            {
                return ObjectEditorForm;
            }
        }

        private ObjectEditorForm ObjectEditorForm
        {
            get;
        }

        public void UpdatePlatformTypeDescription(
            AreaPlatformType areaPlatformType)
        {
            ObjectEditorForm.UpdatePlatformTypeDescription(areaPlatformType);
        }

        private void ObjectEditorForm_AreaObjectCommandChanged(
            object sender,
            EventArgs e)
        {
            AreaObjectCommandChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
