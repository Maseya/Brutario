namespace Brutario.Smb1
{
    using System;

    public interface ISpriteEditorView
    {
        AreaSpriteCommand AreaSpriteCommand
        {
            get;
            set;
        }

        event EventHandler AreaSpriteCommandChanged;
    }
}
