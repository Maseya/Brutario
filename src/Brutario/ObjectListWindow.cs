// <copyright file="ObjectListWindow.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed under GNU
//     Affero General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    public sealed partial class ObjectListWindow : Form
    {
        public ObjectListWindow()
        {
            InitializeComponent();

            Items = new ItemCollection(this);
        }

        public event EventHandler SelectedIndexChanged;

        public event EventHandler EditItem;

        public event EventHandler AddItem_Click;

        public event EventHandler DeleteItem_Click;

        public event EventHandler ClearItems_Click;

        public event EventHandler MoveItemUp_Click;

        public event EventHandler MoveItemDown_Click;

        public int SelectedIndex
        {
            get
            {
                return lvwObjects.SelectedIndices.Count > 0
                    ? lvwObjects.SelectedIndices[0]
                    : -1;
            }

            set
            {
                if ((uint)value < (uint)lvwObjects.Items.Count)
                {
                    lvwObjects.Items[value].Selected = true;
                }
            }
        }

        public ItemCollection Items
        {
            get;
        }

        private void OnSelectedIndexChanged(EventArgs e)
        {
            SelectedIndexChanged?.Invoke(this, e);
        }

        private void OnEditItem(EventArgs e)
        {
            EditItem?.Invoke(this, e);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            AddItem_Click?.Invoke(this, EventArgs.Empty);
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            DeleteItem_Click?.Invoke(this, EventArgs.Empty);
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            ClearItems_Click?.Invoke(this, EventArgs.Empty);
        }

        private void MoveUp_Click(object sender, EventArgs e)
        {
            MoveItemUp_Click?.Invoke(this, EventArgs.Empty);
        }

        private void MoveDown_Click(object sender, EventArgs e)
        {
            MoveItemDown_Click?.Invoke(this, EventArgs.Empty);
        }

        private void Objects_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnEditItem(EventArgs.Empty);
            if (lvwObjects.SelectedItems.Count != 1)
            {
                return;
            }
        }

        private void Objects_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedIndexChanged(EventArgs.Empty);
        }

        private void Objects_ItemSelectionChanged(
            object sender,
            ListViewItemSelectionChangedEventArgs e)
        {
            OnSelectedIndexChanged(EventArgs.Empty);
        }

        public struct Item
        {
            public string Hex { get; set; }
            public int Page { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public string Description { get; set; }
        }

        public class ItemCollection : IList<Item>
        {
            public ItemCollection(ObjectListWindow owner)
            {
                Owner = owner
                    ?? throw new ArgumentNullException(nameof(owner));
            }

            public int Count
            {
                get
                {
                    return BaseItems.Count;
                }
            }

            bool ICollection<Item>.IsReadOnly
            {
                get
                {
                    return false;
                }
            }

            private ObjectListWindow Owner
            {
                get;
            }

            private ListView.ListViewItemCollection BaseItems
            {
                get
                {
                    return Owner.lvwObjects.Items;
                }
            }

            public Item this[int index]
            {
                get
                {
                    return (Item)BaseItems[index].Tag;
                }

                set
                {
                    BaseItems[index].Tag = value;
                    BaseItems[index].SubItems[0].Text = value.Hex;
                    BaseItems[index].SubItems[1].Text = $"{value.Page}";
                    BaseItems[index].SubItems[2].Text = $"{value.X:X1},{value.Y:X1}";
                    BaseItems[index].SubItems[3].Text = value.Description;
                }
            }

            public void SetPage(int index, int page)
            {
                var item = (Item)BaseItems[index].Tag;
                item.Page = page;
                BaseItems[index].Tag = item;
                BaseItems[index].SubItems[1].Text = $"{page}";
            }

            public void Add(Item item)
            {
                Insert(Count, item);
            }

            public void AddRange(IEnumerable<Item> items)
            {
                var result = new List<ListViewItem>(
                    items.Select(item => Create(item)));
                BaseItems.AddRange(result.ToArray());
            }

            public void MoveItem(int oldIndex, int newIndex)
            {
                var change = Math.Sign(newIndex - oldIndex);
                var temp = this[oldIndex];
                for (var i = oldIndex; i != newIndex; i += change)
                {
                    this[i] = this[i + change];
                }

                this[newIndex] = temp;
            }

            public void Insert(int index, Item item)
            {
                _ = BaseItems.Insert(index, Create(item));
            }

            public void Clear()
            {
                BaseItems.Clear();
            }

            public bool Contains(Item item)
            {
                return IndexOf(item) != -1;
            }

            public int IndexOf(Item item)
            {
                for (var i = 0; i < Count; i++)
                {
                    if (item.Equals(this[i]))
                    {
                        return i;
                    }
                }

                return -1;
            }

            public bool Remove(Item item)
            {
                var index = IndexOf(item);
                if (index == -1)
                {
                    return false;
                }

                RemoveAt(index);
                return true;
            }

            public void RemoveAt(int index)
            {
                BaseItems.RemoveAt(index);
            }

            public IEnumerator<Item> GetEnumerator()
            {
                foreach (var item in BaseItems)
                {
                    yield return (Item)(item as ListViewItem).Tag;
                }
            }

            void ICollection<Item>.CopyTo(Item[] dest, int index)
            {
                if (dest is null)
                {
                    throw new ArgumentNullException(nameof(dest));
                }

                if ((uint)(index + Count) < (uint)dest.Length || index < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                for (var i = 0; i < Count; i++)
                {
                    dest[index + i] = this[i];
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private static ListViewItem Create(Item item)
            {
                return new ListViewItem(new string[] {
                    item.Hex,
                    $"{item.Page}",
                    $"{item.X:X1},{item.Y:X1}",
                    item.Description})
                {
                    Tag = item
                };
            }
        }
    }
}
