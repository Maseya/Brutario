namespace Brutario
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    using Brutario.Smb1;

    using static System.ComponentModel.DesignerSerializationVisibility;

    public sealed partial class SpriteEditor : Component, ISpriteEditor
    {
        IWin32Window _owner;

        public SpriteEditor()
        {
            InitializeComponent();
        }

        public SpriteEditor(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public event EventHandler AreaSpriteCommandChanged;

        public event EventHandler OwnerChanged;

        [Browsable(false)]
        [DesignerSerializationVisibility(Hidden)]
        public AreaSpriteCommand AreaSpriteCommand
        {
            get
            {
                return spriteEditorDialog.AreaSpriteCommand;
            }

            set
            {
                spriteEditorDialog.AreaSpriteCommand = value;
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

        public bool TryEditSprite()
        {
            return spriteEditorDialog.ShowDialog(Owner) == DialogResult.OK;
        }

        private void SpriteEditorDialog_AreaSpriteCommandChanged(
            object sender,
            EventArgs e)
        {
            AreaSpriteCommandChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
