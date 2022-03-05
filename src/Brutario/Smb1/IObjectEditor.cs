namespace Brutario.Smb1
{
    using System;

    public interface IObjectEditor
    {
        event EventHandler AreaObjectCommandChanged;

        AreaObjectCommand AreaObjectCommand { get; set; }

        bool TryEditObject(AreaPlatformType areaPlatformType);
    }
}
