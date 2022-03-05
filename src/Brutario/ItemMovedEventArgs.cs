namespace Brutario
{
    using System;

    public class ItemMovedEventArgs : EventArgs
    {
        public ItemMovedEventArgs(int oldIndex, int newIndex)
        {
            OldIndex = oldIndex;
            NewIndex = newIndex;
        }

        public int OldIndex
        {
            get;
            set;
        }

        public int NewIndex
        {
            get;
            set;
        }
    }
}
