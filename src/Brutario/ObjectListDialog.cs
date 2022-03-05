namespace Brutario
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    using static System.ComponentModel.DesignerSerializationVisibility;
    using static Brutario.ObjectListWindow;

    public class ObjectListDialog : DialogProxy
    {
        public ObjectListDialog()
            : base()
        {
            ObjectListWindow = new ObjectListWindow();
            ObjectListWindow.SelectedIndexChanged += ObjectListWindow_SelectedIndexChanged;
            ObjectListWindow.EditItem += ObjectListWindow_EditItem;
            ObjectListWindow.AddItem_Click += ObjectListWindow_AddItem_Click;
            ObjectListWindow.DeleteItem_Click += ObjectListWindow_DeleteItem_Click;
            ObjectListWindow.ClearItems_Click += ObjectListWindow_ClearItems_Click;
            ObjectListWindow.MoveItemUp_Click += ObjectListWindow_MoveItemUp_Click;
            ObjectListWindow.MoveItemDown_Click += ObjectListWindow_MoveItemDown_Click;
            ObjectListWindow.FormClosing += ObjectListWindow_FormClosing;
            ObjectListWindow.VisibleChanged += ObjectListWindow_VisibleChanged;
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

        public event EventHandler AddItem_Click;

        public event EventHandler DeleteItem_Click;

        public event EventHandler ClearItems_Click;

        public event EventHandler MoveItemUp_Click;

        public event EventHandler MoveItemDown_Click;

        public event EventHandler VisibleChanged;

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

        [Browsable(false)]
        [DesignerSerializationVisibility(Hidden)]
        public ItemCollection Items
        {
            get
            {
                return ObjectListWindow.Items;
            }
        }

        public bool Visible
        {
            get
            {
                return ObjectListWindow.Visible;
            }

            set
            {
                ObjectListWindow.Visible = value;
            }
        }

        public Form Owner
        {
            get
            {
                return ObjectListWindow.Owner;
            }

            set
            {
                ObjectListWindow.Owner = value;
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

        private void ObjectListWindow_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ObjectListWindow_EditItem(object sender, EventArgs e)
        {
            EditItem?.Invoke(this, EventArgs.Empty);
        }

        private void ObjectListWindow_AddItem_Click(object sender, EventArgs e)
        {
            AddItem_Click?.Invoke(this, EventArgs.Empty);
        }

        private void ObjectListWindow_DeleteItem_Click(object sender, EventArgs e)
        {
            DeleteItem_Click?.Invoke(this, EventArgs.Empty);
        }

        private void ObjectListWindow_ClearItems_Click(object sender, EventArgs e)
        {
            ClearItems_Click?.Invoke(this, EventArgs.Empty);
        }

        private void ObjectListWindow_MoveItemUp_Click(object sender, EventArgs e)
        {
            MoveItemUp_Click?.Invoke(this, EventArgs.Empty);
        }

        private void ObjectListWindow_MoveItemDown_Click(object sender, EventArgs e)
        {
            MoveItemDown_Click?.Invoke(this, EventArgs.Empty);
        }

        private void ObjectListWindow_VisibleChanged(object sender, EventArgs e)
        {
            VisibleChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ObjectListWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Owner != null && e.CloseReason == CloseReason.UserClosing)
            {
                ObjectListWindow.Visible = false;
                e.Cancel = true;
            }
        }
    }
}
