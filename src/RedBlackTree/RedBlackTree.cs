namespace Maseya;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

public sealed class RedBlackTree<TKey, TValue> :
    IDictionary<TKey, TValue>
{
    public RedBlackTree(IComparer<TKey>? comparer = null)
    {
        Comparer = comparer ?? Comparer<TKey>.Default;
        Keys = new KeyCollection(this);
        Values = new ValueCollection(this);
    }

    public RedBlackTreeNode<TKey, TValue>? Root { get; private set; }

    public bool IsEmpty { get { return Root is null; } }

    public int Count { get; private set; }

    public IComparer<TKey> Comparer { get; }

    public KeyCollection Keys { get; }

    public ValueCollection Values { get; }

    ICollection<TKey> IDictionary<TKey, TValue>.Keys { get { return Keys; } }

    ICollection<TValue> IDictionary<TKey, TValue>.Values { get { return Values; } }

    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly { get { return false; } }

    public TValue this[TKey key]
    {
        get
        {
            return TryGetValue(key, out var value)
                ? value
                : throw new KeyNotFoundException();
        }

        set
        {
            var node = FindKey(key);
            if (node is null)
            {
                Add(key, value);
            }
            else
            {
                node.Value = value;
            }
        }
    }

    private RedBlackTreeNode<TKey, TValue>? this[RedBlackTreeNode<TKey, TValue> node]
    {
        set
        {
            if (value is not null)
            {
                value.Parent = node.Parent;
                value.Color = Color.Black;
            }

            if (node.Parent is null)
            {
                Root = value;
            }
            else
            {
                var parent = node.Parent;
                var direction = GetDirection(node);
                parent[direction] = value;
            }
        }
    }

    public bool ContainsKey(TKey key)
    {
        return FindKey(key) is not null;
    }

    public bool ContainsValue(TValue value, IEqualityComparer<TValue>? comparer = null)
    {
        return FindValue(value, comparer) is not null;
    }

    public bool Contains(
        TKey key,
        TValue value,
        IEqualityComparer<TValue>? comparer = null)
    {
        return Find(key, value, comparer) is not null;
    }

    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        var node = FindKey(key);
        if (node is null)
        {
            value = default;
            return false;
        }

        value = node.Value;
        return true;
    }

    public RedBlackTreeNode<TKey, TValue>? FindKey(TKey key)
    {
        var node = Root;
        while (node is not null)
        {
            var comparison = Comparer.Compare(key, node.Key);
            if (comparison == 0)
            {
                break;
            }

            node = comparison < 0 ? node.Left : node.Right;
        }

        return node;
    }

    public RedBlackTreeNode<TKey, TValue>? LowerBound(TKey key)
    {
        var node = Root;
        RedBlackTreeNode<TKey, TValue>? result = null;
        while (node is not null)
        {
            var comparison = Comparer.Compare(key, node.Key);
            if (comparison <= 0)
            {
                result = node;
                node = node.Left;
            }
            else
            {
                node = node.Right;
            }
        }

        return result;
    }

    public RedBlackTreeNode<TKey, TValue>? UpperBound(TKey key)
    {
        var node = Root;
        RedBlackTreeNode<TKey, TValue>? result = null;
        while (node is not null)
        {
            var comparison = Comparer.Compare(key, node.Key);
            if (comparison < 0)
            {
                result = node;
                node = node.Left;
            }
            else
            {
                node = node.Right;
            }
        }

        return result;
    }

    public RedBlackTreeNode<TKey, TValue>? FindValue(
        TValue value,
        IEqualityComparer<TValue>? comparer)
    {
        comparer ??= EqualityComparer<TValue>.Default;
        return Root
            .InOrderTraversal()
            .FirstOrDefault(x => comparer.Equals(x.Value, value));
    }

    public RedBlackTreeNode<TKey, TValue>? Find(
        TKey key,
        TValue value,
        IEqualityComparer<TValue>? comparer = null)
    {
        var first = LowerBound(key);
        if (first == null)
        {
            return null;
        }

        comparer ??= EqualityComparer<TValue>.Default;
        return first
            .ForwardTraversal()
            .TakeWhile(x => Comparer.Compare(x.Key, key) == 0)
            .FirstOrDefault(x => comparer.Equals(x.Value, value));
    }

    public void Add(TKey key, TValue value)
    {
        Count++;
        if (Root is null)
        {
            // When setting root node, it is guaranteed to be the only node in the
            // tree. So we do not need to balance it or set any connecting nodes.
            Root = new RedBlackTreeNode<TKey, TValue>(key, value);
            return;
        }

        var parent = Root;
        Direction direction;
        {
        _loop:
            direction = Comparer.Compare(key, parent.Key) < 0
                ? Direction.Left
                : Direction.Right;
            var next = parent[direction];
            if (next is not null)
            {
                parent = next;
                goto _loop;
            }
        }

        var node = new RedBlackTreeNode<TKey, TValue>(key, value, parent, direction);
        BalanceInsertedNode(node);
    }

    public bool Remove(TKey key)
    {
        return Remove(FindKey(key));
    }

    public bool Remove(
        TKey key,
        TValue value,
        IEqualityComparer<TValue>? comparer = null)
    {
        return Remove(Find(key, value, comparer));
    }

    public void Clear()
    {
        Root = null;
        Count = 0;
    }

    public KeyValuePair<TKey, TValue> Min()
    {
        return Root is not null
            ? Root.Min().KeyValuePair
            : throw new InvalidOperationException("Tree contains no elements.");
    }

    public KeyValuePair<TKey, TValue> Max()
    {
        return Root is not null
            ? Root.Max().KeyValuePair
            : throw new InvalidOperationException("Tree contains no elements.");
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return Root.InOrderTraversal().Select(x => x.KeyValuePair).GetEnumerator();
    }

    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(
        KeyValuePair<TKey, TValue> item)
    {
        return Contains(item.Key, item.Value);
    }

    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
    {
        Add(item.Key, item.Value);
    }

    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
    {
        return Remove(item.Key, item.Value);
    }

    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(
        KeyValuePair<TKey, TValue>[] array,
        int arrayIndex)
    {
        if ((uint)arrayIndex >= (uint)array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        }

        if (arrayIndex + Count > array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        }

        foreach (var kvp in this)
        {
            array[arrayIndex++] = kvp;
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private static Direction GetDirection(RedBlackTreeNode<TKey, TValue> node)
    {
        return node == node.Parent!.Left
            ? Direction.Left
            : Direction.Right;
    }

    private static RedBlackTreeNode<TKey, TValue> LeftRotate(
        RedBlackTreeNode<TKey, TValue> node)
    {
        var child = node.Right!;

        node.Right = child.Left;
        if (node.Right is not null)
        {
            node.Right.Parent = node;
        }

        child.Left = node;
        child.Parent = node.Parent;
        node.Parent = child;
        if (child.Parent is not null)
        {
            var direction = node == child.Parent.Left
                ? Direction.Left
                : Direction.Right;
            child.Parent[direction] = child;
        }

        return child;
    }

    private static RedBlackTreeNode<TKey, TValue> RightRotate(
        RedBlackTreeNode<TKey, TValue> node)
    {
        var child = node.Left!;

        node.Left = child.Right;
        if (node.Left is not null)
        {
            node.Left.Parent = node;
        }

        child.Right = node;
        child.Parent = node.Parent;
        node.Parent = child;
        if (child.Parent is not null)
        {
            var direction = node == child.Parent.Left
                ? Direction.Left
                : Direction.Right;
            child.Parent[direction] = child;
        }

        return child;
    }

    private bool Remove(RedBlackTreeNode<TKey, TValue>? node)
    {
        if (node is null)
        {
            return false;
        }

        Count--;

        // If the node has two children, we swap the node with its successor, which is
        // guaranteed to either be a leaf or have only a right child. If it had a left
        // child, it would contradict this node actually be the successor. This breaks
        // the well-ordering of the collection only temporarily until the active node
        // is removed.
        if (node.Left is not null && node.Right is not null)
        {
            (node.Next!.Key, node.Key) = (node.Key, node.Next.Key);
            (node.Next.Value, node.Value) = (node.Value, node.Next.Value);
            node = node.Next;
        }

        if (node.Prev != null)
        {
            node.Prev.Next = node.Next;
        }

        if (node.Next != null)
        {
            node.Next.Prev = node.Prev;
        }

        // From now on, the node has either zero or one children.

        // If the node has exactly one child, then the node must be black and the child
        // must be red. Otherwise, there would be a black violation along the null
        // child path. In such a case, we simply remove the active node and replace it
        // with the child. We must repaint the child node black to guarantee no red or
        // black violations occur, ensuring the tree remains balanced.
        if (node.Left is not null)
        {
            this[node] = node.Left;
        }
        else if (node.Right is not null)
        {
            this[node] = node.Right;
        }
        else if (node == Root)
        {
            // If the node is the root and has no children, then it can simply be removed.
            this[node] = null;
        }
        else
        {
            var parent = node.Parent!;
            var direction = GetDirection(node);
            this[node] = null;

            // Finally, we end up at the non-trivial case of a black leaf node.
            if (node.IsBlack())
            {
                BalanceDeletedNode(parent, direction);
            }
        }

        return true;
    }

    private RedBlackTreeNode<TKey, TValue> Rotate(
        RedBlackTreeNode<TKey, TValue> node,
        Direction direction)
    {
        var result = direction == Direction.Left
            ? LeftRotate(node)
            : RightRotate(node);

        // Check if we need to update the root node after the rotation.
        if (result.Parent is null)
        {
            Root = result;
        }

        return result;
    }

    private void BalanceInsertedNode(RedBlackTreeNode<TKey, TValue> node)
    {
        // Loop invariants:
        // * The current node is red at the start of each iteration
        // * Requirement 3 (no parent-child relationship has two red nodes) is satisfied
        // everywhere except for possibly the current node and its parent.
        // * All other RB tree properties are satisfied everywhere.
        {
        _loop:
            var parent = node.Parent;

            // Case 1: The parent node is black. Case 3: The active node is the root.
            // These cases satisfy requirement 3, making the whole tree RB-balanced.
            if (parent.IsBlack())
            {
                return;
            }

            // Moving forward, the parent node is guaranteed to be red.
            var grandparent = parent!.Parent;

            // Case 4: The parent is red and is the root node.
            if (grandparent is null)
            {
                // The node and its parent are both red, causing a red-violation.
                // However, the parent is also the root, so we can change its color to
                // black, fixing the violation.
                parent.Color = Color.Black;
                return;
            }

            // Moving forward, parent is red and has a non-null grandparent.
            var uncle = parent == grandparent.Left
                ? grandparent.Right
                : grandparent.Left;

            // Case 2: The parent and uncle nodes are both red
            if (uncle.IsRed())
            {
                // We recolor the parent black to fix the red-violation between the
                // node and its parent.
                parent.Color = Color.Black;

                // However, now there's a black a violation from the grandparent to the
                // current node since we introduced a new black node, so we recolor the
                // grandparent node to red.
                grandparent.Color = Color.Red;

                // But now there is a black-violation from the grandparent to the
                // uncle, and a red-violation since they're both red now, so we color
                // the uncle black, which fixes both violations.
                uncle!.Color = Color.Black;

                // This makes the subtree balanced, but the grandparent may be in
                // violation now if its parent is also red. We set it as the active
                // node and restart the loop.
                node = grandparent;
                goto _loop;
            }

            // From now on, the uncle is black.

            // Case 5: The node is an inner grandchild.
            var direction = GetDirection(parent);
            if (direction != GetDirection(node))
            {
                // After rotating, we fall through to case 6.
                parent = Rotate(parent, direction);
            }

            // Case 6: The node is now an outer grandchild. Since the parent is red and
            // the tree was balanced up to now before inserting, the grandparent must
            // be black.

            // If we change the parent color to black, then the red violation between
            // active node and parent goes away.
            parent.Color = Color.Black;

            // However, now a black violation occurs from grandparent to active node as
            // we introduced a new black node. So we set the grandparent to red.
            grandparent.Color = Color.Red;

            // This does not add any red violations from grandparent to uncle since
            // uncle is black, but this does cause a black-violation since the path
            // from grandparent to uncle lost a black node. We can balance this with a
            // rotation on the grandparent.
            _ = Rotate(grandparent, direction.Reverse());

            // This makes the tree full balanced, so we are done.
            return;
        }
    }

    private void BalanceDeletedNode(
        RedBlackTreeNode<TKey, TValue> parent,
        Direction direction)
    {
        // The first iteration now syncs with the regular loop, which we express as a
        // goto loop. The loop has the following invariants: The black height of the
        // active node is one less than the other nodes, meaning there is always a
        // black violation. All other balancing requirements are satisfied.
        {
        _loop:
            // During any iteration, the parent node may be red or black. However, the
            // sibling may never be null as that would imply a black violation existed
            // before the (black) active node was removed.
            var sibling = parent[direction.Reverse()]!;

            // We get the nephews, which can possibly be null.
            var distantNephew = sibling[direction.Reverse()];
            var closeNephew = sibling[direction];

            // Case 3: The sibling is red. This implies that the sibling its parent and
            // children must all be black.
            if (sibling.IsRed())
            {
                // The goal is to transform this into either case 4, 5, or 6, which all
                // require the sibling node be black.
                sibling.Color = Color.Black;

                // However, this introduces a black violation along the sibling
                // subtree, so we must change the parent color to red.
                parent.Color = Color.Red;

                // But if the grandparent is also red, then we create a red violation
                // outside of our parent subtree. We can fix this issue by rotating the
                // parent node.
                _ = Rotate(parent, direction);
                goto _loop;
            }

            // From now on, the sibling node is black.

            // Case 4: The parent is red. This implies that both nephews must be black.
            if (parent.IsRed() && distantNephew.IsBlack() && closeNephew.IsBlack())
            {
                // If we swap the colors of the parent and sibling nodes, this doesn't
                // affect the black height of any paths going through the sibling
                // subtree, nor does it introduce any red violations. It does, however,
                // add one to the black height toward the delete node, which balances
                // the tree.
                parent.Color = Color.Black;
                sibling.Color = Color.Red;

                // Because the tree is balanced, we are done.
                return;
            }

            // From now on, the parent is black.

            if (distantNephew.IsBlack())
            {
                if (closeNephew.IsBlack())
                {
                    // Case 1: Parent, sibling, and nephews are all black. If we
                    // repaint the sibling node to red, this balances the parent
                    // subtree after removal. But now the whole parent subtree is in
                    // black violation with the rest of the tree (if parent is not the root).
                    sibling.Color = Color.Red;

                    // Case 2: The parent is the root.
                    if (parent.Parent is null)
                    {
                        // This implies that one black node has been removed from every
                        // path, decreasing the total black height of the tree by 1,
                        // and removing the black violation of the tree.
                        return;
                    }

                    direction = GetDirection(parent);
                    parent = parent.Parent!;

                    // Otherwise, we go to the next iteration.
                    goto _loop;
                }

                // Case 5: The sibling node is black and the close nephew is red and
                // the distant nephew is black. Our goal is to transform this into case
                // 6, where the distant nephew is red.

                closeNephew!.Color = Color.Black;

                sibling.Color = Color.Red;

                // Finally, we do the actual rotation.
                distantNephew = sibling;
                sibling = Rotate(sibling, direction.Reverse());
            }

            // Case 6: The sibling node is black and the distant nephew is red. We do
            // not care about the color of the close nephew.

            // If we swap the colors of the sibling and parent nodes, they do preserve
            // the black heights of all paths through the sibling subtree, introducing
            // no black violations. Plus, making the parent node black will not
            // introduce any red violations either.
            sibling.Color = parent.Color;
            parent.Color = Color.Black;

            // If we rotate the parent node, then the sibling becomes the new parent,
            // increasing the black height to the deleted node by 1, which fixes its
            // black violation.
            _ = Rotate(parent, direction);

            // However, the distant nephew subtree lost its black parent, introducing a
            // black violation here. But we can fix this by making the node black.
            distantNephew!.Color = Color.Black;

            // This balances the entire tree, and we are done.
            return;
        }
    }

    public sealed class KeyCollection : ICollection<TKey>, IReadOnlyCollection<TKey>
    {
        internal KeyCollection(RedBlackTree<TKey, TValue> tree)
        {
            Tree = tree;
        }

        public int Count
        {
            get
            {
                return Tree.Count;
            }
        }

        bool ICollection<TKey>.IsReadOnly { get { return true; } }

        private RedBlackTree<TKey, TValue> Tree { get; }

        public bool Contains(TKey value)
        {
            return Tree.ContainsKey(value);
        }

        public void CopyTo(TKey[] array, int arrayIndex)
        {
            if ((uint)arrayIndex >= (uint)array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }

            if (arrayIndex + Count > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }

            foreach (var key in this)
            {
                array[arrayIndex++] = key;
            }
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            return Tree.Select(x => x.Key).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void ICollection<TKey>.Add(TKey item)
        {
            throw new NotSupportedException();
        }

        void ICollection<TKey>.Clear()
        {
            throw new NotSupportedException();
        }

        bool ICollection<TKey>.Remove(TKey item)
        {
            throw new NotSupportedException();
        }
    }

    public sealed class ValueCollection : ICollection<TValue>
    {
        internal ValueCollection(RedBlackTree<TKey, TValue> tree)
        {
            Tree = tree;
        }

        public int Count
        {
            get
            {
                return Tree.Count;
            }
        }

        bool ICollection<TValue>.IsReadOnly { get { return true; } }

        private RedBlackTree<TKey, TValue> Tree { get; }

        public bool Contains(TValue value)
        {
            return Tree.ContainsValue(value);
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            if ((uint)arrayIndex >= (uint)array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }

            if (arrayIndex + Count > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }

            foreach (var value in this)
            {
                array[arrayIndex++] = value;
            }
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return Tree.Select(x => x.Value).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void ICollection<TValue>.Add(TValue item)
        {
            throw new NotSupportedException();
        }

        void ICollection<TValue>.Clear()
        {
            throw new NotSupportedException();
        }

        bool ICollection<TValue>.Remove(TValue item)
        {
            throw new NotSupportedException();
        }
    }
}
