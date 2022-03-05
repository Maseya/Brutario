namespace Brutario.Smb1
{
    using System;

    public interface IObjectEditorView
    {
        event EventHandler AreaObjectCommandChanged;

        AreaObjectCommand AreaObjectCommand { get; set; }
    }
}
