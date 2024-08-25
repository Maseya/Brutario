namespace Brutario.Core;
using System;

public class ObjectEditedEventArgs : EventArgs
{
    public ObjectEditedEventArgs(
        int oldIndex,
        int newIndex,
        UIAreaObjectCommand oldCommand,
        UIAreaObjectCommand newCommand)
    {
        OldIndex = oldIndex;
        NewIndex = newIndex;
        OldCommand = oldCommand;
        NewCommand = newCommand;
    }

    public int OldIndex { get; set; }

    public int NewIndex { get; set; }

    public UIAreaObjectCommand OldCommand { get; set; }

    public UIAreaObjectCommand NewCommand { get; set; }
}
