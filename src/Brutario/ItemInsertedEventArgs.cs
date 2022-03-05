namespace Brutario
{
    using System;

    public class ItemInsertedEventArgs<T> : EventArgs
    {
        public ItemInsertedEventArgs(int index, T item)
        {
            Index = index;
            Item = item;
        }

        public int Index
        {
            get;
            set;
        }

        public T Item
        {
            get;
            set;
        }
    }
}
