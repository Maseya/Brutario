// <copyright file="UIAreaObjectCommand.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Core;

using System;
using System.Drawing;

using Maseya.Smas.Smb1.AreaData.ObjectData;

public struct UIAreaObjectCommand : IEquatable<UIAreaObjectCommand>
{
    private AreaObjectCommand command_;

    public UIAreaObjectCommand(AreaObjectCommand command, int page, int z = 0)
    {
        command_ = command;
        Page = page;
        Z = z;
    }

    public AreaObjectCommand Command
    {
        get
        {
            return command_;
        }

        set
        {
            command_ = value;
        }
    }

    public int Page { get; set; }

    public int X
    {
        get
        {
            return Command.X | (Page << 4);
        }

        set
        {
            Page = value >> 4;
            command_.X = value & 0x0F;
        }
    }

    public int Y
    {
        get
        {
            return command_.Y;
        }

        set
        {
            command_.Y = value;
        }
    }

    public Rectangle SelectionRectangle
    {
        get
        {
            return new Rectangle(X << 4, (Y + 2) << 4, 0x10, 0x10);
        }
    }

    public int Z { get; set; }

    public string HexString
    {
        get
        {
            return $"{Page:X2} {Command.HexString}";
        }
    }

    public static bool operator ==(UIAreaObjectCommand left, UIAreaObjectCommand right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(UIAreaObjectCommand left, UIAreaObjectCommand right)
    {
        return !(left == right);
    }

    public bool Equals(UIAreaObjectCommand other)
    {
        return Command.Equals(other.Command)
            && Page.Equals(other.Page)
            && Z.Equals(other.Z);
    }

    public override bool Equals(object? obj)
    {
        return obj is UIAreaObjectCommand other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Command, Page, Z);
    }

    public override string ToString()
    {
        return $"{X}, {Y}, {Z}, {Command.BaseName}";
    }
}
