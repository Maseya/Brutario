namespace Brutario
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    using Smb1;

    using static System.ComponentModel.DesignerSerializationVisibility;

    public class SpriteEditorDialog : DialogProxy, ISpriteEditorView
    {
        public SpriteEditorDialog()
        {
            SpriteEditorForm = new SpriteEditorForm();
            SpriteEditorForm.AreaSpriteCommandChanged +=
                SpriteEditorForm_AreaSpriteCommandChanged;
        }

        public SpriteEditorDialog(IContainer container)
            : this()
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            container.Add(this);
        }

        public event EventHandler AreaSpriteCommandChanged;

        [Browsable(false)]
        [DesignerSerializationVisibility(Hidden)]
        public AreaSpriteCommand AreaSpriteCommand
        {
            get
            {
                return SpriteEditorForm.AreaSpriteCommand;
            }

            set
            {
                SpriteEditorForm.AreaSpriteCommand = value;
            }
        }

        protected override Form BaseForm
        {
            get
            {
                return SpriteEditorForm;
            }
        }

        private SpriteEditorForm SpriteEditorForm
        {
            get;
        }

        private void SpriteEditorForm_AreaSpriteCommandChanged(object sender, EventArgs e)
        {
            AreaSpriteCommandChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
