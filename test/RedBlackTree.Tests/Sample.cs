namespace RedBlackTree.Tests;
using System;
using System.Collections.Generic;
using System.Linq;

public static class Sample
{
    public static IEnumerable<int> RandomSample(int count, bool distinct)
    {
        return RandomSample(count, new Random(), distinct);
    }

    public static IEnumerable<int> RandomSample(int count, int seed, bool distinct)
    {
        return RandomSample(count, new Random(seed), distinct);
    }

    public static IEnumerable<int> RandomSample(
        int count,
        Random random,
        bool distinct)
    {
        if (!distinct)
        {
            for (var i = 0; i < count; i++)
            {
                yield return random.Next(count);
            }
        }
        else
        {
            var indexes = Enumerable.Range(0, count).ToArray();
            for (var i = 0; i < count; i++)
            {
                var index = random.Next(count - i);
                yield return indexes[index];

                (indexes[index], indexes[count - i - 1]) =
                    (indexes[count - i - 1], indexes[index]);
            }
        }
    }
}
