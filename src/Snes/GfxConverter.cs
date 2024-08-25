// <copyright file="GfxConverter.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Snes;

using System;

public static class GfxConverter
{
    public static int PixelMapSize(int gfxSize)
    {
        var tileCount = gfxSize / 0x20;
        return 0x40 * tileCount;
    }

    public static int PixelMapSize2Bpp(int gfxSize)
    {
        var tileCount = gfxSize / 0x10;
        return 0x40 * tileCount;
    }

    public static byte[] GfxToPixelMap(Span<byte> gfxSrc)
    {
        var tileCount = gfxSrc.Length / 0x20;
        var result = new byte[0x40 * tileCount];
        GfxToPixelMap(gfxSrc, result);
        return result;
    }

    public static void GfxToPixelMap(Span<byte> gfxSrc, Span<byte> pixelDest)
    {
        var tileCount = gfxSrc.Length / 0x20;
        if (pixelDest.Length < 0x40 * tileCount)
        {
            throw new ArgumentException(
                "Destination array is an insufficient size",
                nameof(pixelDest));
        }

        var pixelIndex = 0;
        for (var tileIndex = 0; tileIndex < tileCount; tileIndex++)
        {
            for (var y = 0; y < 8; y++)
            {
                var offset = (tileIndex << 5) + (y << 1);

                var val1 = gfxSrc[offset + 0];
                var val2 = gfxSrc[offset + 1];
                var val3 = gfxSrc[offset + 0 + (2 * 8)];
                var val4 = gfxSrc[offset + 1 + (2 * 8)];

                for (var x = 8; --x >= 0;)
                {
                    pixelDest[pixelIndex++] = (byte)(
                        (((val1 >> x) & 1) << 0) |
                        (((val2 >> x) & 1) << 1) |
                        (((val3 >> x) & 1) << 2) |
                        (((val4 >> x) & 1) << 3));
                }
            }
        }
    }

    public static byte[] PixelMapToGfx(Span<byte> pixelSrc)
    {
        var tileCount = pixelSrc.Length / 0x40;
        var result = new byte[0x20 * tileCount];
        PixelMapToGfx(pixelSrc, result);
        return result;
    }

    public static void PixelMapToGfx(Span<byte> pixelSrc, Span<byte> gfxDest)
    {
        var tileCount = pixelSrc.Length / 0x40;
        if (gfxDest.Length < 0x20 * tileCount)
        {
            throw new ArgumentException(
                "Destination array is an insufficient size",
                nameof(gfxDest));
        }

        for (var i = 0; i < tileCount; i++)
        {
            var pixelIndex = 0;
            for (var y = 0; y < 8; y++)
            {
                byte val1 = 0;
                byte val2 = 0;
                byte val3 = 0;
                byte val4 = 0;
                for (var x = 8; --x >= 0;)
                {
                    var value = pixelSrc[(i * 0x40) + pixelIndex++];
                    val1 |= (byte)(((value >> 0) & 1) << x);
                    val2 |= (byte)(((value >> 1) & 1) << x);
                    val3 |= (byte)(((value >> 2) & 1) << x);
                    val4 |= (byte)(((value >> 3) & 1) << x);
                }

                var offset = (i << 5) + (y << 1);
                gfxDest[offset + 0] = val1;
                gfxDest[offset + 1] = val2;
                gfxDest[offset + 0 + (2 * 8)] = val3;
                gfxDest[offset + 1 + (2 * 8)] = val4;
            }
        }
    }

    public static byte[] Gfx2BppToPixelMap(Span<byte> gfxSrc)
    {
        var tileCount = gfxSrc.Length / 0x10;
        var result = new byte[0x40 * tileCount];
        Gfx2BppToPixelMap(gfxSrc, result);
        return result;
    }

    public static void Gfx2BppToPixelMap(Span<byte> gfxSrc, Span<byte> pixelDest)
    {
        var tileCount = gfxSrc.Length / 0x10;
        if (pixelDest.Length < 0x40 * tileCount)
        {
            throw new ArgumentException(
                "Destination array is an insufficient size",
                nameof(pixelDest));
        }

        var pixelIndex = 0;
        for (var tileIndex = 0; tileIndex < tileCount; tileIndex++)
        {
            for (var y = 0; y < 8; y++)
            {
                var offset = (tileIndex << 4) + (y << 1);

                var val1 = gfxSrc[offset + 0];
                var val2 = gfxSrc[offset + 1];

                for (var x = 8; --x >= 0;)
                {
                    pixelDest[pixelIndex++] = (byte)(
                        (((val1 >> x) & 1) << 0) |
                        (((val2 >> x) & 1) << 1));
                }
            }
        }
    }

    public static byte[] PixelMapToGfx2Bpp(Span<byte> src)
    {
        var tileCount = src.Length / 0x40;
        var result = new byte[tileCount * 0x10];
        PixelMapToGfx2Bpp(src, result);
        return result;
    }

    public static void PixelMapToGfx2Bpp(Span<byte> pixelSrc, Span<byte> gfxDest)
    {
        var tileCount = pixelSrc.Length / 0x40;
        if (gfxDest.Length < 0x10 * tileCount)
        {
            throw new ArgumentException(
                "Destination array is an insufficient size",
                nameof(gfxDest));
        }

        for (var i = 0; i < tileCount; i++)
        {
            var pixelIndex = 0;
            for (var y = 0; y < 8; y++)
            {
                byte val1 = 0;
                byte val2 = 0;
                for (var x = 8; --x >= 0;)
                {
                    var value = pixelSrc[(i * 8 * 8) + pixelIndex++];
                    val1 |= (byte)(((value >> 0) & 1) << x);
                    val2 |= (byte)(((value >> 1) & 1) << x);
                }

                var offset = (i << 4) + (y << 1);
                gfxDest[offset + 0] = val1;
                gfxDest[offset + 1] = val2;
            }
        }
    }
}
