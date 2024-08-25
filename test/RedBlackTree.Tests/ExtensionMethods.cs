namespace RedBlackTree.Tests;
using System.Collections.Generic;
using System.Linq;

using Maseya;

public static class ExtensionMethods
{
    public static bool IsSorted<T>(
        this IEnumerable<T> collection,
        IComparer<T>? comparer = null)
    {
        comparer ??= Comparer<T>.Default;
        return collection.Zip(collection.Skip(1), comparer.Compare).All(x => x <= 0);
    }

    public static int CountNodes<TKey, TValue>(
        this RedBlackTreeNode<TKey, TValue>? node)
    {
        return node is not null
            ? 1 + node.Left.CountNodes() + node.Right.CountNodes()
            : 0;
    }

    public static bool IsValidTree<TKey, TValue>(
        this RedBlackTreeNode<TKey, TValue>? node)
    {
        if (node is null)
        {
            return true;
        }

        if (node.Parent is not null)
        {
            if (node.Parent.Left != node && node.Parent.Right != node)
            {
                return false;
            }
        }

        return node.Left.IsValidTree() && node.Right.IsValidTree();
    }

    public static bool IsBalanced<TKey, TValue>(
        this RedBlackTreeNode<TKey, TValue>? node)
    {
        var blackHeight = node.BlackHeight();
        return IsBalanced(node, 0, blackHeight);
    }

    private static int BlackHeight<TKey, TValue>(
        this RedBlackTreeNode<TKey, TValue>? node)
    {
        var result = 0;
        while (node != null)
        {
            if (node.IsBlack())
            {
                result++;
            }

            node = node.Left;
        }

        return result;
    }

    private static bool IsBalanced<TKey, TValue>(
        RedBlackTreeNode<TKey, TValue>? node,
        int currentHeight,
        int blackHeight)
    {
        if (node is null)
        {
            // Forbid black violation
            return currentHeight == blackHeight;
        }

        if (node.IsBlack())
        {
            currentHeight++;
        }
        else if (node.Parent.IsRed())
        {
            // Forbid red-violations.
            return false;
        }

        return IsBalanced(node.Left, currentHeight, blackHeight)
            && IsBalanced(node.Right, currentHeight, blackHeight);
    }
}
