namespace Maseya;
using System.Collections.Generic;

public class RedBlackTreeNode<TKey, TValue>
{
    internal RedBlackTreeNode(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }

    internal RedBlackTreeNode(
        TKey key,
        TValue value,
        RedBlackTreeNode<TKey, TValue> parent,
        Direction direction)
        : this(key, value)
    {
        Parent = parent;
        Color = Color.Red;
        if (parent != null)
        {
            parent[direction] = this;
            if (direction == Direction.Left)
            {
                Prev = parent.Prev;
                if (Prev != null)
                {
                    Prev.Next = this;
                }

                Next = parent;
                parent.Prev = this;
            }
            else
            {
                Next = parent.Next;
                if (Next != null)
                {
                    Next.Prev = this;
                }

                Prev = parent;
                parent.Next = this;
            }
        }
    }

    public TKey Key { get; internal set; }

    public TValue Value { get; set; }

    public RedBlackTreeNode<TKey, TValue>? Left { get; internal set; }

    public RedBlackTreeNode<TKey, TValue>? Right { get; internal set; }

    public RedBlackTreeNode<TKey, TValue>? Parent { get; internal set; }

    public RedBlackTreeNode<TKey, TValue>? Prev { get; internal set; }

    public RedBlackTreeNode<TKey, TValue>? Next { get; internal set; }

    public KeyValuePair<TKey, TValue> KeyValuePair
    {
        get
        {
            return new KeyValuePair<TKey, TValue>(Key, Value);
        }

        internal set
        {
            Key = value.Key;
            Value = value.Value;
        }
    }

    internal Color Color { get; set; }

    internal RedBlackTreeNode<TKey, TValue>? this[Direction direction]
    {
        get
        {
            return direction == Direction.Left
                ? Left
                : Right;
        }

        set
        {
            if (direction == Direction.Left)
            {
                Left = value;
            }
            else
            {
                Right = value;
            }
        }
    }

    public override string ToString()
    {
        return $"{{{Key}, {Value}}}";
    }
}
