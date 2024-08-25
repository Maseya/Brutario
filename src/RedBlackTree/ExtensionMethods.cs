namespace Maseya;
using System.Collections.Generic;
using System.Linq;

public static class ExtensionMethods
{
    public static RedBlackTreeNode<TKey, TValue> Min<TKey, TValue>(
        this RedBlackTreeNode<TKey, TValue> node)
    {
        var result = node;
        while (result.Left != null)
        {
            result = result.Left;
        }

        return result;
    }

    public static RedBlackTreeNode<TKey, TValue> Max<TKey, TValue>(
        this RedBlackTreeNode<TKey, TValue> node)
    {
        var result = node;
        while (result.Right != null)
        {
            result = result.Right;
        }

        return result;
    }

    public static IEnumerable<RedBlackTreeNode<TKey, TValue>>
        InOrderTraversal<TKey, TValue>(this RedBlackTreeNode<TKey, TValue>? node)
    {
        var nodes = Enumerable.Empty<RedBlackTreeNode<TKey, TValue>>();
        if (node is not null)
        {
            nodes = nodes.Concat(InOrderTraversal(node.Left));
            nodes = nodes.Concat(Enumerable.Repeat(node, 1));
            nodes = nodes.Concat(InOrderTraversal(node.Right));
        }

        return nodes;
    }

    public static IEnumerable<RedBlackTreeNode<TKey, TValue>>
        ForwardTraversal<TKey, TValue>(this RedBlackTreeNode<TKey, TValue>? node)
    {
        while (node is not null)
        {
            yield return node;
            node = node.Next;
        }
    }

    public static bool IsBlack<TKey, TValue>(this RedBlackTreeNode<TKey, TValue>? node)
    {
        return node == null || node.Color == Color.Black;
    }

    public static bool IsRed<TKey, TValue>(this RedBlackTreeNode<TKey, TValue>? node)
    {
        return !node.IsBlack();
    }

    internal static Direction Reverse(this Direction direction)
    {
        return (Direction)(1 - (int)direction);
    }
}
