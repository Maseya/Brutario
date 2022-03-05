namespace Brutario
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ItemEditedEventArgs<T> : EventArgs
    {
        public ItemEditedEventArgs(int index, T oldValue, T newValue)
        {
            Index = index;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public int Index
        {
            get;
            set;
        }

        public T OldValue
        {
            get;
            set;
        }

        public T NewValue
        {
            get;
            set;
        }

    }
}
