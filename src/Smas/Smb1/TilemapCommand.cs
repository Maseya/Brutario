// <copyright file="TilemapCommand.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1;

using System;

public struct TilemapCommand : IEquatable<TilemapCommand>
{
    public const int TerminationValue = 0xE3F0;

    private ushort _value;

    private TilemapCommand(int value)
    {
        _value = (ushort)value;
    }

    public int Command04
    {
        get
        {
            return _value;
        }

        set
        {
            _value = (ushort)value;
        }
    }

    public int CommandEF
    {
        get
        {
            return (Command04 & 0x3F0) >> 4;
        }

        set
        {
            Command04 &= ~0x3F0;
            Command04 |= (value << 4) & 0x3F0;
        }
    }

    public int CommandF1
    {
        get
        {
            return Command04 & 0x0F;
        }

        set
        {
            Command04 &= ~0x0F;
            Command04 |= value & 0x0F;
        }
    }

    public int CommandED
    {
        get
        {
            return ((Command04 & 0xE000) | ((Command04 >> 1) & 0x0E00)) >> 8;
        }

        set
        {
            Command04 &= ~0xEE00;
            Command04 |= ((value & 0xE0) | ((value & 0x0E) << 1)) << 8;
        }
    }

    public bool IsTerminationCommand
    {
        get
        {
            return (CommandED & 0xF0) == 0xE0 && CommandEF == 0x3F;
        }
    }

    public static bool operator ==(TilemapCommand left, TilemapCommand right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(TilemapCommand left, TilemapCommand right)
    {
        return !(left == right);
    }

    public static implicit operator int(TilemapCommand command)
    {
        return command.Command04;
    }

    public static implicit operator TilemapCommand(int value)
    {
        return new TilemapCommand(value);
    }

    public bool Equals(TilemapCommand other)
    {
        return Command04.Equals(other.Command04);
    }

    public override bool Equals(object? obj)
    {
        return obj is TilemapCommand other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Command04;
    }

    public override string ToString()
    {
        return Command04.ToString("X4");
    }
}
