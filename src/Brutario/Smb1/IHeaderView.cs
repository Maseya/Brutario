namespace Brutario.Smb1
{
    using System;

    public interface IHeaderView
    {
        event EventHandler AreaHeaderChanged;

        AreaHeader AreaHeader { get; set; }
    }
}
