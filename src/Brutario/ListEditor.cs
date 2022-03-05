namespace Brutario
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class ListEditor<T> : Collection<T>
    {
        public ListEditor()
        : this(Enumerable.Empty<T>())
        { }

        public ListEditor(IEnumerable<T> items)
            : base(new List<T>(items))
        { }

        public event EventHandler<ItemEditedEventArgs<T>> ItemEdited;

        public event EventHandler<ItemInsertedEventArgs<T>> ItemInserted;

        public event EventHandler<ItemInsertedEventArgs<T>> ItemRemoved;

        public event EventHandler<DataClearedEventArgs<T>> DataCleared;

        public event EventHandler<ItemMovedEventArgs> ItemMoved;

        public event EventHandler<DataResetEventArgs<T>> DataReset;

        public void MoveItem(int oldIndex, int newIndex)
        {
            var change = Math.Sign(newIndex - oldIndex);
            var temp = Items[oldIndex];
            for (var i = oldIndex; i != newIndex; i += change)
            {
                Items[i] = Items[i + change];
            }

            Items[newIndex] = temp;

            var e = new ItemMovedEventArgs(oldIndex, newIndex);
            OnItemMoved(e);
        }

        public void Reset(IEnumerable<T> newData)
        {
            var e = new DataResetEventArgs<T>(Items, newData);
            Items.Clear();
            (Items as List<T>).AddRange(newData);
            OnDataReset(e);
        }

        protected override void SetItem(int index, T item)
        {
            var e = new ItemEditedEventArgs<T>(index, this[index], item);
            base.SetItem(index, item);
            OnItemEdited(e);
        }

        protected override void InsertItem(int index, T item)
        {
            var e = new ItemInsertedEventArgs<T>(index, item);
            base.InsertItem(index, item);
            OnItemInserted(e);
        }

        protected override void RemoveItem(int index)
        {
            var e = new ItemInsertedEventArgs<T>(index, this[index]);
            base.RemoveItem(index);
            OnItemRemoved(e);
        }

        protected override void ClearItems()
        {
            var e = new DataClearedEventArgs<T>(this);
            base.ClearItems();
            OnDataCleared(e);
        }

        protected virtual void OnItemEdited(ItemEditedEventArgs<T> e)
        {
            ItemEdited?.Invoke(this, e);
        }

        protected virtual void OnItemInserted(ItemInsertedEventArgs<T> e)
        {
            ItemInserted?.Invoke(this, e);
        }

        protected virtual void OnItemRemoved(ItemInsertedEventArgs<T> e)
        {
            ItemRemoved?.Invoke(this, e);
        }

        protected virtual void OnDataCleared(DataClearedEventArgs<T> e)
        {
            DataCleared?.Invoke(this, e);
        }

        protected virtual void OnItemMoved(ItemMovedEventArgs e)
        {
            ItemMoved?.Invoke(this, e);
        }

        protected virtual void OnDataReset(DataResetEventArgs<T> e)
        {
            DataReset?.Invoke(this, e);
        }
    }
}
