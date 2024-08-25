﻿namespace Maseya.Snes;

public static class MathHelper
{
    /// <summary>
    /// Calculates the smallest power of 2 that is an upper-bound for a given integer.
    /// </summary>
    /// <param name="x">
    /// The integer to upper-bound.
    /// </param>
    /// <returns>
    /// An integer that is the smallest power of 2 that upper-bounds <paramref
    /// name="x"/>.
    /// </returns>
    public static int BitCeil(int x)
    {
        // check for the set bits
        x |= x >> 1;
        x |= x >> 2;
        x |= x >> 4;
        x |= x >> 8;
        x |= x >> 16;

        // Then we remove all but the top bit by xor'ing the string of 1's with that
        // string of 1's shifted one to the left, and we end up with just the one top
        // bit followed by 0's.
        return x ^ (x >> 1);
    }
}
