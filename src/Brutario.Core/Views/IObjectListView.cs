namespace Brutario.Core.Views;
using System.Collections.Generic;

using Maseya.Smas.Smb1.AreaData.ObjectData;

public interface IObjectListView
{
    event EventHandler? AreaPlatformTypeChanged;

    event EventHandler? SelectedIndexChanged;

    event EventHandler? EditItem;

    event EventHandler? AddItem_Click;

    event EventHandler? DeleteItem_Click;

    event EventHandler? ClearItems_Click;

    event EventHandler? MoveItemDown_Click;

    event EventHandler? MoveItemUp_Click;

    AreaPlatformType AreaPlatformType { get; set; }

    int SelectedIndex { get; set; }

    IList<UIAreaObjectCommand> Items { get; }
}
