namespace Brutario
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class DataResetEventArgs<T> : EventArgs
    {
        public DataResetEventArgs(IEnumerable<T> oldData, IEnumerable<T> newData)
        {
            OldData = new Collection<T>(new List<T>(oldData));
            NewData = new Collection<T>(new List<T>(newData));
        }

        public Collection<T> OldData
        {
            get;
        }

        public Collection<T> NewData
        {
            get;
        }
    }
}
