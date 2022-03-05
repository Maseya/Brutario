namespace Brutario.Smb1
{
    using System;

    public interface IHeaderEditor
    {
        event EventHandler AreaHeaderChanged;

        AreaHeader AreaHeader { get; set; }

        bool TryEditAreaHeader();
    }
}
