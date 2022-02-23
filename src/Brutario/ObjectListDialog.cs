namespace Brutario
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Forms;

    using Maseya.Controls;

    using Smb1;

    using static System.ComponentModel.DesignerSerializationVisibility;

    public sealed class ObjectListDialog : DialogProxy
    {
        public ObjectListDialog()
            : base()
        {
            ObjectListWindow = new ObjectListWindow();
            ObjectListWindow.SelectedIndexChanged += ObjectListWindow_SelectedIndexChanged;
            ObjectListWindow.EditItem += ObjectListWindow_EditItem;
        }

        public ObjectListDialog(IContainer container)
            : this()
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            container.Add(this);
        }

        public event EventHandler SelectedIndexChanged;

        public event EventHandler EditItem;

        [Browsable(false)]
        [DesignerSerializationVisibility(Hidden)]
        public int SelectedIndex
        {
            get
            {
                return ObjectListWindow.SelectedIndex;
            }
            set
            {
                ObjectListWindow.SelectedIndex = value;
            }
        }

        protected override Form BaseForm
        {
            get
            {
                return ObjectListWindow;
            }
        }

        private ObjectListWindow ObjectListWindow
        {
            get;
        }

        private void ObjectListWindow_EditItem(object sender, EventArgs e)
        {
            SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ObjectListWindow_SelectedIndexChanged(object sender, EventArgs e)
        {
            EditItem?.Invoke(this, e);
        }
    }
}
