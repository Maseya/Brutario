namespace Brutario.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Maseya.Smas.Smb1.AreaData.SpriteData;

public class SortedSpriteListEditor :
    IList<UIAreaSpriteCommand>,
    IReadOnlyList<UIAreaSpriteCommand>
{
    public SortedSpriteListEditor()
    {
        Items = new List<UIAreaSpriteCommand>();
    }

    public SortedSpriteListEditor(IEnumerable<AreaSpriteCommand> commands)
    {
        Items = new List<UIAreaSpriteCommand>(GetSortedUICommands(commands));
    }

    public event EventHandler? DataReset;

    public event EventHandler? ItemEdited;

    public event EventHandler? ItemAdded;

    public event EventHandler? ItemRemoved;

    public event EventHandler? DataCleared;

    public int Count
    {
        get
        {
            return Items.Count;
        }
    }

    public UIAreaSpriteCommand this[int index]
    {
        get
        {
            return Items[index];
        }
        set
        {
            _ = Edit(index, value);
        }
    }

    bool ICollection<UIAreaSpriteCommand>.IsReadOnly
    {
        get { return false; }
    }

    private List<UIAreaSpriteCommand> Items
    {
        get;
    }

    public static IEnumerable<AreaSpriteCommand> GetAreaData(
        IEnumerable<byte> bytes)
    {
        using var en = bytes.GetEnumerator();
        if (!en.MoveNext())
        {
            throw new ArgumentException("Area object data ended early.");
        }

        while (en.Current != AreaSpriteCommand.TerminationCode)
        {
            if (AreaSpriteCommand.IsThreeByteSpecifier(en.Current))
            {
                var list = new List<byte>(GetBytes(3));
                yield return new AreaSpriteCommand(
                    list[0],
                    list[1],
                    list[2]);
            }
            else
            {
                var list = new List<byte>(GetBytes(2));
                yield return new AreaSpriteCommand(
                    list[0],
                    list[1]);
            }
        }

        IEnumerable<byte> GetBytes(int size)
        {
            for (var i = 0; i < size; i++)
            {
                yield return en.Current;

                if (!en.MoveNext())
                {
                    throw new ArgumentException("Area object data ended early.");
                }
            }
        }
    }

    public void Reset(IEnumerable<AreaSpriteCommand> commands)
    {
        Reset(GetUICommands(commands));
    }

    public void Reset(IEnumerable<UIAreaSpriteCommand> commands)
    {
        Items.Clear();
        foreach (var item in commands.OrderBy(x => x.X))
        {
            if (item.Command.Code == AreaSpriteCode.ScreenJump)
            {
                continue;
            }

            Items.Add(item);
        }

        DataReset?.Invoke(this, EventArgs.Empty);
    }

    public bool Contains(UIAreaSpriteCommand command)
    {
        return IndexOf(command) >= 0;
    }

    public int IndexOf(UIAreaSpriteCommand item)
    {
        var result = IndexOf(item);
        return result < 0 ? -1 : result;
    }

    public int AtCoords(int x, int y)
    {
        if (Items.Count == 0)
        {
            return -1;
        }

        var command = Items[0];
        command.X = x;
        var index = SearchFirstIndexOf(command);
        if (index == Count)
        {
            return -1;
        }

        for (; index < Items.Count && Items[index].X == x; index++)
        {
            if (Items[index].Y == y)
            {
                return index;
            }
        }

        return -1;
    }

    public int Add(AreaSpriteCommand command, int page)
    {
        return Add(new UIAreaSpriteCommand(command, page));
    }

    public int Add(UIAreaSpriteCommand command)
    {
        var index = AddInternal(command);
        ItemAdded?.Invoke(this, EventArgs.Empty);
        return index;
    }

    void ICollection<UIAreaSpriteCommand>.Add(UIAreaSpriteCommand item)
    {
        _ = Add(item);
    }

    public bool Remove(UIAreaSpriteCommand item)
    {
        var index = IndexOf(item);
        if (index != -1)
        {
            RemoveAt(index);
            return true;
        }

        return false;
    }

    public void RemoveAt(int index)
    {
        Items.RemoveAt(index);
        ItemRemoved?.Invoke(this, EventArgs.Empty);
    }

    public int Edit(int index, UIAreaSpriteCommand newItem)
    {
        Items.RemoveAt(index);
        index = AddInternal(newItem);
        ItemEdited?.Invoke(this, EventArgs.Empty);
        return index;
    }

    public void Clear()
    {
        Items.Clear();
        DataCleared?.Invoke(this, EventArgs.Empty);
    }

    void IList<UIAreaSpriteCommand>.Insert(
        int index,
        Brutario.Core.UIAreaSpriteCommand item)
    {
        Add(item);
    }

    public IEnumerator<UIAreaSpriteCommand> GetEnumerator()
    {
        return Items.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerable<AreaSpriteCommand> GetSpriteData()
    {
        var lastPage = 0;
        foreach (var command in Items)
        {
            var page = command.Page;
            var item = command.Command;
            if (page <= lastPage + 1)
            {
                Debug.Assert(page >= lastPage);

                item.ScreenFlag = page != lastPage;
            }
            else if (page > lastPage + 1)
            {
                yield return new AreaSpriteCommand(0x0F, (byte)(page & 0x1F));
                item.ScreenFlag = false;
            }

            lastPage = page;
            yield return item;
        }
    }

    public byte[] ToByteArray()
    {
        var result = new List<byte>();
        foreach (var command in GetSpriteData())
        {
            result.Add(command.Value1);
            result.Add(command.Value2);
            if (command.IsThreeByteCommand)
            {
                result.Add(command.Value3);
            }
        }

        result.Add(AreaSpriteCommand.TerminationCode);

        return result.ToArray();
    }

    void ICollection<UIAreaSpriteCommand>.CopyTo(
        UIAreaSpriteCommand[] array,
        int index)
    {
        if ((uint)index >= (uint)array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if (index + Count < array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        foreach ((var i, var value) in Enumerable.Range(0, Count).Zip(this))
        {
            array[index + i] = value;
        }
    }

    private static IEnumerable<UIAreaSpriteCommand> GetUICommands(
        IEnumerable<AreaSpriteCommand> data)
    {
        var page = 0;
        foreach (var item in data)
        {
            if (item.ScreenFlag)
            {
                page++;
            }
            else if (item.Code == AreaSpriteCode.ScreenJump)
            {
                page = item.BaseCommand & 0x1F;
                continue;
            }

            yield return new UIAreaSpriteCommand(item, page);
        }
    }

    private static IEnumerable<UIAreaSpriteCommand> GetSortedUICommands(
        IEnumerable<AreaSpriteCommand> data)
    {
        return GetUICommands(data).OrderBy(item => item.X);
    }

    private int AddInternal(UIAreaSpriteCommand item)
    {
        if (item.Command.Code == AreaSpriteCode.ScreenJump)
        {
            return -1;
        }

        var index = SearchLastIndexOf(item);
        Items.Insert(index, item);
        return index;
    }

    private int SearchFirstIndexOf(UIAreaSpriteCommand item)
    {
        var comparer = Comparer<UIAreaSpriteCommand>.Create(
            (x, y) => x.X < y.X ? -1 : +1);
        return ~Items.BinarySearch(item, comparer);
    }

    private int SearchLastIndexOf(UIAreaSpriteCommand item)
    {
        var comparer = Comparer<UIAreaSpriteCommand>.Create(
            (x, y) => x.X <= y.X ? -1 : +1);
        return ~Items.BinarySearch(item, comparer);
    }
}
