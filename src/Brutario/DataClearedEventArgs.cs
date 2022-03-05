namespace Brutario
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class DataClearedEventArgs<T> : EventArgs
    {
        public DataClearedEventArgs(IEnumerable<T> oldData)
        {
            if (oldData is null)
            {
                throw new ArgumentNullException(nameof(oldData));
            }

            OldData = new Collection<T>(new List<T>(oldData));
        }

        public Collection<T> OldData
        {
            get;
        }
    }
}
