namespace Maseya.Smas.Smb1;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

public struct AreaPalette : IEquatable<AreaPalette>
{
    private ForegroundPalette foregroundPalette_;
    private BackgroundPalette backgroundPalette_;
    private SpritePalette spritePalette_;

    public AreaPalette(
        ForegroundPalette foregroundPalette,
        BackgroundPalette backgroundPalette,
        SpritePalette spritePalette)
    {
        ForegroundPalette = foregroundPalette;
        BackgroundPalette = backgroundPalette;
        SpritePalette = spritePalette;
    }

    public ForegroundPalette ForegroundPalette
    {
        readonly get
        {
            return foregroundPalette_;
        }

        set
        {
            if (!Enum.IsDefined(value))
            {
                throw new InvalidEnumArgumentException(
                    nameof(ForegroundPalette),
                    (int)value,
                    typeof(ForegroundPalette));
            }

            foregroundPalette_ = value;
        }
    }
    public BackgroundPalette BackgroundPalette
    {
        readonly get
        {
            return backgroundPalette_;
        }

        set
        {
            if (!Enum.IsDefined(value))
            {
                throw new InvalidEnumArgumentException(
                    nameof(BackgroundPalette),
                    (int)value,
                    typeof(BackgroundPalette));
            }

            backgroundPalette_ = value;
        }
    }
    public SpritePalette SpritePalette
    {
        readonly get
        {
            return spritePalette_;
        }

        set
        {
            if (!Enum.IsDefined(value))
            {
                throw new InvalidEnumArgumentException(
                    nameof(SpritePalette),
                    (int)value,
                    typeof(SpritePalette));
            }

            spritePalette_ = value;
        }
    }

    public static bool operator !=(AreaPalette left, AreaPalette right)
    {
        return !(left == right);
    }

    public static bool operator ==(AreaPalette left, AreaPalette right)
    {
        return left.Equals(right);
    }

    public readonly bool Equals(AreaPalette other)
    {
        return ForegroundPalette == other.ForegroundPalette
            && BackgroundPalette == other.BackgroundPalette
            && SpritePalette == other.SpritePalette;
    }

    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is AreaPalette other && Equals(other);
    }

    public override readonly int GetHashCode()
    {
        return HashCode.Combine(
            ForegroundPalette,
            BackgroundPalette,
            SpritePalette);
    }
}
