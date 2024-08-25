namespace RedBlackTree.Tests;

using System;
using System.Collections.Generic;
using System.Linq;

using Maseya;

using Xunit;

using static Sample;

public class TestRedBlackTree
{
    [Fact]
    public void TreeStressTests()
    {
        var tree = new RedBlackTree<int, int>();
        for (var count = 0; count < 50; count++)
        {
            for (var seed = 0; seed < 500; seed++)
            {
                var random = new Random(seed);
                var sample = RandomSample(
                    count,
                    random: random
,
                    distinct: true).ToArray();

                for (var i = 0; i < count; i++)
                {
                    tree.Add(sample[i], i);
                    Assert.Equal(i + 1, tree.Count);
                    AssertTree(tree);
                }

                Assert.Equal(count, tree.Count);

                var en = RandomSample(count, random: random, distinct: true)
                    .Zip(Enumerable.Range(0, count));
                foreach ((var key, var i) in en)
                {
                    if (!tree.Remove(key))
                    {
                        throw new InvalidOperationException();
                    }

                    Assert.Equal(count - 1 - i, tree.Count);
                    AssertTree(tree);
                }

                Assert.Empty(tree);
            }
        }
    }

    [Fact]
    public void TestLowerBound()
    {
        var random = new Random(0);
        for (var i = 0; i < 1000; i++)
        {
            var tree = new RedBlackTree<int, int>();
            for (var j = 0; j < random.Next(1000); j++)
            {
                tree.Add(random.Next(1000), j);
            }

            var key = random.Next(1000);
            var lower = tree.LowerBound(key);
            if (lower is not null)
            {
                Assert.True(lower.Key >= key);
                Assert.True(lower.Prev is null || lower.Prev.Key < key);
            }
            else
            {
                Assert.True(tree.Root is null || tree.Root.Max().Key < key);
            }
        }
    }

    [Fact]
    public void TestUpperBound()
    {
        var random = new Random(0);
        for (var i = 0; i < 1000; i++)
        {
            var tree = new RedBlackTree<int, int>();
            for (var j = 0; j < random.Next(1000); j++)
            {
                tree.Add(random.Next(1000), j);
            }

            var key = random.Next(1000);
            var lower = tree.UpperBound(key);
            if (lower is not null)
            {
                Assert.True(lower.Key > key);
                Assert.True(lower.Prev is null || lower.Prev.Key <= key);
            }
            else
            {
                Assert.True(tree.Root is null || tree.Root.Max().Key <= key);
            }
        }
    }

    [Fact]
    public void TestNext()
    {
        var tree = new RedBlackTree<int, int>();
        for (var count = 0; count < 50; count++)
        {
            for (var seed = 0; seed < 100; seed++)
            {
                var random = new Random(seed);
                var sample = RandomSample(
                    count,
                    random: random
,
                    distinct: true).ToArray();

                for (var i = 0; i < count; i++)
                {
                    tree.Add(sample[i], i);
                    AssertSuccessors(tree);
                }

                foreach (var key in RandomSample(count, random: random, distinct: true))
                {
                    _ = tree.Remove(key);
                    AssertSuccessors(tree);
                }
            }
        }
    }

    private static void AssertTree<TKey, TValue>(RedBlackTree<TKey, TValue> tree)
    {
        Assert.True(tree.Select(x => x.Key).IsSorted());

        Assert.Equal(expected: tree.Root.CountNodes(), actual: tree.Count);

        Assert.True(tree.Root.IsValidTree());

        Assert.True(tree.Root.IsBalanced());
    }

    private static void AssertSuccessors<TKey, TValue>(RedBlackTree<TKey, TValue> tree)
    {
        if (tree.Root is null)
        {
            return;
        }

        var nodes = tree.Root.InOrderTraversal().ToArray();
        for (var j = 0; j < nodes.Length; j++)
        {
            if (j > 0)
            {
                Assert.Equal(nodes[j - 1], nodes[j].Prev);
            }
            else
            {
                Assert.Null(nodes[j].Prev);
            }

            if (j < nodes.Length - 1)
            {
                Assert.Equal(nodes[j + 1], nodes[j].Next);
            }
            else
            {
                Assert.Null(nodes[j].Next);
            }
        }
    }
}
