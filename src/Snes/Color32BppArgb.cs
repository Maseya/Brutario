// <copyright file="Color32BppArgb.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Snes;

using System;
using System.Drawing;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct Color32BppArgb : IEquatable<Color32BppArgb>
{
    public byte B
    {
        get;
        set;
    }

    public byte G
    {
        get;
        set;
    }

    public byte R
    {
        get;
        set;
    }

    public byte A
    {
        get;
        set;
    }

    public int Value
    {
        get
        {
            return this;
        }
    }

    public Color32BppArgb(int alpha, int red, int green, int blue)
    {
        A = (byte)alpha;
        R = (byte)red;
        G = (byte)green;
        B = (byte)blue;
    }

    public static Color32BppArgb FromSnesColor(int low, int high)
    {
        return FromSnesColor(low | (high << 8));
    }

    public static Color32BppArgb FromSnesColor(int value)
    {
        return new Color32BppArgb(
            Byte.MaxValue,
            ((value >> (5 * 0)) & 0x1F) << 3,
            ((value >> (5 * 1)) & 0x1F) << 3,
            ((value >> (5 * 2)) & 0x1F) << 3);
    }

    public static int ToSnesColor(Color32BppArgb color)
    {
        return (color.R >> 3) | ((color.G >> 3) << 5) | ((color.B >> 3) << 10);
    }

    public static bool operator ==(Color32BppArgb left, Color32BppArgb right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Color32BppArgb left, Color32BppArgb right)
    {
        return !(left == right);
    }

    public static implicit operator int(Color32BppArgb pixel)
    {
        return *(int*)&pixel;
    }

    public static implicit operator Color32BppArgb(int value)
    {
        return *(Color32BppArgb*)&value;
    }

    public static implicit operator Color(Color32BppArgb pixel)
    {
        return Color.FromArgb(pixel);
    }

    public static explicit operator Color32BppArgb(Color color)
    {
        return color.ToArgb();
    }

    public override int GetHashCode()
    {
        return Value;
    }

    public bool Equals(Color32BppArgb other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is Color32BppArgb other && Equals(other);
    }

    public override string ToString()
    {
        return Value.ToString("X6");
    }
}
