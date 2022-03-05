namespace Brutario.Smb1
{
    using System;

    public interface ISpriteEditor
    {
        AreaSpriteCommand AreaSpriteCommand
        {
            get;
            set;
        }

        event EventHandler AreaSpriteCommandChanged;

        bool TryEditSprite();
    }
}
