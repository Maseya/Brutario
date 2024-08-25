namespace Brutario.Core;
using System;

public class ItemAddedEventArgs : EventArgs
{
    public ItemAddedEventArgs(int index)
    {
        Index = index;
    }

    public int Index { get; set; }
}
