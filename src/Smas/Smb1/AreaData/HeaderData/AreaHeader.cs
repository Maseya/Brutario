// <copyright file="AreaHeader.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1.AreaData.HeaderData;

using System;

using ObjectData;

/// <summary>
/// The header data for the current area.
/// </summary>
/// <remarks>
/// An area header defines how the current area should look when first entering it. It
/// defines certain properties like start time, position, scenery, and terrain. Certain
/// objects in the area can modify these properties too, but every area object pointer
/// starts with two bytes that determines the header.
/// </remarks>
public struct AreaHeader : IEquatable<AreaHeader>
{
    /// <summary>
    /// The size, in bytes, of this <see cref="AreaHeader"/>.
    /// </summary>
    public const int SizeOf = sizeof(ushort);

    /// <summary>
    /// Initializes a new instance of the <see cref="AreaHeader"/> struct.
    /// </summary>
    /// <param name="val1">
    /// The first byte of the area header.
    /// </param>
    /// <param name="val2">
    /// The second byte of the area header.
    /// </param>
    public AreaHeader(byte val1, byte val2)
    {
        Value1 = val1;
        Value2 = val2;
    }

    public AreaHeader(int word)
        : this()
    {
        Word = word;
    }

    public AreaHeader(
        StartTime startTime,
        StartYPosition startYPosition,
        ForegroundScenery foregroundScenery,
        AreaPlatformType areaPlatformType,
        BackgroundScenery backgroundScenery,
        TerrainMode terrainMode)
        : this()
    {
        StartTime = startTime;
        StartYPosition = startYPosition;
        ForegroundScenery = foregroundScenery;
        AreaPlatformType = areaPlatformType;
        BackgroundScenery = backgroundScenery;
        TerrainMode = terrainMode;
    }

    public BackgroundColorControl BackgroundColorControl
    {
        get
        {
            var value = Value1 & 0x07;
            return value >= 4 ? (BackgroundColorControl)(value & 4) : 0;
        }

        set
        {
            if (value == BackgroundColorControl.DayTime)
            {
                Value1 &= unchecked((byte)~4);
            }
            else
            {
                Value1 |= 4;
                ForegroundScenery = ForegroundScenery.None;
            }
        }
    }

    /// <summary>
    /// Gets or sets the area foreground scenery.
    /// </summary>
    public ForegroundScenery ForegroundScenery
    {
        get
        {
            var value = Value1 & 7;
            return value >= 4
                ? ForegroundScenery.None
                : (ForegroundScenery)value;
        }

        set
        {
            Value1 &= unchecked((byte)~3);
            if (value != ForegroundScenery.None)
            {
                Value1 |= (byte)((int)value & 3);
                BackgroundColorControl = BackgroundColorControl.DayTime;
            }
        }
    }

    /// <summary>
    /// Gets or sets the miscellaneous platform type to use in the area.
    /// </summary>
    public AreaPlatformType AreaPlatformType
    {
        get
        {
            return (AreaPlatformType)(Value2 >> 6);
        }

        set
        {
            Value2 &= unchecked((byte)~(3 << 6));
            Value2 |= (byte)(((int)value & 3) << 6);
        }
    }

    /// <summary>
    /// Gets or sets the layer 1 scenery to draw at the start of the area.
    /// </summary>
    public BackgroundScenery BackgroundScenery
    {
        get
        {
            return (BackgroundScenery)((Value2 >> 4) & 3);
        }

        set
        {
            Value2 &= unchecked((byte)~(3 << 4));
            Value2 |= (byte)(((int)value & 3) << 4);
        }
    }

    /// <summary>
    /// Gets or sets the players start time when entering the area.
    /// </summary>
    public StartTime StartTime
    {
        get
        {
            return (StartTime)(Value1 >> 6);
        }

        set
        {
            Value1 &= unchecked((byte)~(3 << 6));
            Value1 |= (byte)(((int)value & 3) << 6);
        }
    }

    /// <summary>
    /// Gets or sets the players start Y-position when entering the area.
    /// </summary>
    public StartYPosition StartYPosition
    {
        get
        {
            return (StartYPosition)((Value1 >> 3) & 7);
        }

        set
        {
            Value1 &= unchecked((byte)~(7 << 3));
            Value1 |= (byte)(((int)value & 7) << 3);
        }
    }

    public int StartYPixel
    {
        get
        {
            return StartYPosition switch
            {
                StartYPosition.Y00 => 0x00,
                StartYPosition.Y00FromOtherArea => 0x20,
                StartYPosition.YB0 => 0xB0,
                StartYPosition.Y50 => 0x50,
                StartYPosition.Alt1Y00 => 0x00,
                StartYPosition.Alt2Y00 => 0x00,
                StartYPosition.PipeIntroYB0 => 0xB0,
                StartYPosition.AltPipeIntroYB0 => 0xB0,
                _ => 0x00,
            };
        }
    }

    /// <summary>
    /// Gets or sets the terrain layout to use when starting the area.
    /// </summary>
    public TerrainMode TerrainMode
    {
        get
        {
            return (TerrainMode)(Value2 & 0x0F);
        }

        set
        {
            Value2 &= unchecked((byte)~0x0F);
            Value2 |= (byte)((int)value & 0x0F);
        }
    }

    /// <summary>
    /// Gets or sets the first byte of the area data.
    /// </summary>
    public byte Value1
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the second byte of the area data.
    /// </summary>
    public byte Value2
    {
        get;
        set;
    }

    public int Word
    {
        get
        {
            return Value1 | (Value2 << 8);
        }

        set
        {
            Value1 = (byte)value;
            Value2 = (byte)(value >> 8);
        }
    }

    public static bool operator !=(AreaHeader left, AreaHeader right)
    {
        return !(left == right);
    }

    public static bool operator ==(AreaHeader left, AreaHeader right)
    {
        return left.Equals(right);
    }

    public static implicit operator int(AreaHeader header)
    {
        return header.Word;
    }

    public static implicit operator AreaHeader(int word)
    {
        return new AreaHeader(word);
    }

    public override bool Equals(object? obj)
    {
        return obj is AreaHeader other && Equals(other);
    }

    public bool Equals(AreaHeader other)
    {
        return Value1.Equals(other.Value1) && Value2.Equals(other.Value2);
    }

    public override int GetHashCode()
    {
        return Value1 | (Value2 << 8);
    }

    public override string ToString()
    {
        return $"{Value1:X2} {Value2:X2}";
    }
}
