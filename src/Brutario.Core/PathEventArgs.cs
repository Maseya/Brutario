namespace Brutario.Core;
using System;

public class PathEventArgs : EventArgs
{
    public PathEventArgs(string path)
    {
        Path = path;
    }

    public string Path { get; set; }
}
