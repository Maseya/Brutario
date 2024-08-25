// <copyright file="AreaObjectCommand.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1.AreaData.ObjectData;

using System;
using System.Collections.ObjectModel;

using HeaderData;

using Maseya.Smas.Smb1;

public struct AreaObjectCommand : IEquatable<AreaObjectCommand>
{
    /// <summary>
    /// The object command to read that defined the end of the area object data.
    /// </summary>
    public const byte TerminationCode = 0xFD;

    public static readonly ReadOnlyCollection<AreaObjectCode> ValidCodes =
        new(new AreaObjectCode[]
        {
            AreaObjectCode.QuestionBlockPowerup,
            AreaObjectCode.QuestionBlockCoin,
            AreaObjectCode.HiddenBlockCoin,
            AreaObjectCode.HiddenBlock1UP,
            AreaObjectCode.BrickPowerup,
            AreaObjectCode.BrickBeanstalk,
            AreaObjectCode.BrickStar,
            AreaObjectCode.Brick10Coins,
            AreaObjectCode.Brick1UP,
            AreaObjectCode.SidewaysPipe,
            AreaObjectCode.UsedBlock,
            AreaObjectCode.SpringBoard,
            AreaObjectCode.JPipe,
            AreaObjectCode.FlagPole,
            AreaObjectCode.Empty,
            AreaObjectCode.Empty2,
            AreaObjectCode.AreaSpecificPlatform,
            AreaObjectCode.HorizontalBricks,
            AreaObjectCode.HorizontalStones,
            AreaObjectCode.HorizontalCoins,
            AreaObjectCode.VerticalBricks,
            AreaObjectCode.VerticalStones,
            AreaObjectCode.UnenterablePipe,
            AreaObjectCode.EnterablePipe,
            AreaObjectCode.Hole,
            AreaObjectCode.BalanceHorizontalRope,
            AreaObjectCode.BridgeV7,
            AreaObjectCode.BridgeV8,
            AreaObjectCode.BridgeV10,
            AreaObjectCode.HoleWithWaterOrLava,
            AreaObjectCode.HorizontalQuestionBlocksV3,
            AreaObjectCode.HorizontalQuestionBlocksV7,
            AreaObjectCode.AltJPipe,
            AreaObjectCode.AltFlagPole,
            AreaObjectCode.BowserAxe,
            AreaObjectCode.RopeForAxe,
            AreaObjectCode.BowserBridge,
            AreaObjectCode.ScrollStopWarpZone,
            AreaObjectCode.ScrollStop,
            AreaObjectCode.AltScrollStop,
            AreaObjectCode.RedCheepCheepFlying,
            AreaObjectCode.BulletBillGenerator,
            AreaObjectCode.StopGenerator,
            AreaObjectCode.LoopCommand,
            AreaObjectCode.TerrainAndBackgroundSceneryChange,
            AreaObjectCode.ForegroundChange,
            AreaObjectCode.RopeForLift,
            AreaObjectCode.PulleyRope,
            AreaObjectCode.EmptyTile,
            AreaObjectCode.Castle,
            AreaObjectCode.CastleCeilingCap,
            AreaObjectCode.Staircase,
            AreaObjectCode.CastleStairs,
            AreaObjectCode.CastleRectangularCeilingTiles,
            AreaObjectCode.CastleFloorRightEdge,
            AreaObjectCode.CastleFloorLeftEdge,
            AreaObjectCode.CastleFloorLeftWall,
            AreaObjectCode.CastleFloorRightWall,
            AreaObjectCode.VerticalSeaBlocks,
            AreaObjectCode.ExtendableJPipe,
            AreaObjectCode.VerticalBalls,
        });

    public AreaObjectCommand(byte value1, byte value2, byte value3 = 0)
    {
        Value1 = value1;
        Value2 = value2;
        Value3 = (byte)(((value1 & 0x0F) == 0x0F) ? value3 : 0);
    }

    public byte Value1
    {
        get;
        set;
    }

    public byte Value2
    {
        get;
        set;
    }

    public byte Value3
    {
        get;
        set;
    }

    /// <summary>
    /// The size, in bytes, of this <see cref="AreaObjectCommand"/>.
    /// </summary>
    public int Size
    {
        get
        {
            return IsThreeByteCommand ? 3 : 2;
        }
    }

    public int X
    {
        get
        {
            return Value1 >> 4;
        }

        set
        {
            Value1 &= 0x0F;
            Value1 |= (byte)((value & 0x0F) << 4);
        }
    }

    public bool HasYCoord
    {
        get
        {
            var y = Value1 & 0x0F;
            return y is < 0x0C or 0x0F;
        }
    }

    public int Y
    {
        get
        {
            return IsThreeByteCommand ? Value2 >> 4 : Value1 & 0x0F;
        }

        set
        {
            if (IsThreeByteCommand)
            {
                Value2 &= 0x0F;
                Value2 |= (byte)((value & 0x0F) << 4);
            }
            else
            {
                Value1 &= 0xF0;
                Value1 |= (byte)(value & 0x0F);
            }
        }
    }

    public bool ScreenFlag
    {
        get
        {
            return ((IsThreeByteCommand ? Value3 : Value2) & 0x80) != 0;
        }

        set
        {
            var mask = (byte)(value ? 0x80 : 0);
            if (IsThreeByteCommand)
            {
                Value3 &= 0x7F;
                Value3 |= mask;
            }
            else
            {
                Value2 &= 0x7F;
                Value2 |= mask;
            }
        }
    }

    public int BaseCommand
    {
        get
        {
            return (IsThreeByteCommand ? Value3 : Value2) & 0x7F;
        }

        set
        {
            if (IsThreeByteCommand)
            {
                Value3 &= 0x80;
                Value3 |= (byte)(value & 0x7F);
            }
            else
            {
                Value2 &= 0x80;
                Value2 |= (byte)(value & 0x7F);
            }
        }
    }

    public int Command
    {
        get
        {
            return (BaseCommand >> 4) & 7;
        }

        set
        {
            BaseCommand &= 0x8F;
            BaseCommand |= (byte)((value & 7) << 4);
        }
    }

    public int Parameter
    {
        get
        {
            return Value2 & 0x0F;
        }

        set
        {
            Value2 &= 0xF0;
            Value2 |= (byte)(value & 0x0F);
        }
    }

    public AreaObjectCode Code
    {
        get
        {
            return (Value1 & 0x0F) switch
            {
                0x0F => (AreaObjectCode)(0xF00 | BaseCommand),
                0x0C => (AreaObjectCode)(0xC00 | (Command << 4)),
                0x0D => (Command & ~1) == 0
                    ? AreaObjectCode.ScreenJump
                    : (AreaObjectCode)(0xD00 | (Command << 4) | Parameter),
                0x0E => (AreaObjectCode)(0xE00 | ((Command & 4) << 4)),
                _ => Command switch
                {
                    0 => (AreaObjectCode)Parameter,
                    7 => Parameter < 8
                        ? AreaObjectCode.UnenterablePipe
                        : AreaObjectCode.EnterablePipe,
                    _ => (AreaObjectCode)(Command << 4),
                },
            };
        }
    }

    public bool IsExtendableObject
    {
        get
        {
            return Code.IsExtendableObject();
        }
    }

    public bool IsTerrainAndBackgroundChange
    {
        get
        {
            return Code == AreaObjectCode.TerrainAndBackgroundSceneryChange;
        }
    }

    public bool IsForegroundChange
    {
        get
        {
            return Code == AreaObjectCode.ForegroundChange;
        }
    }

    public int Length
    {
        get
        {
            return Code switch
            {
                AreaObjectCode.ScreenJump => Value2 & 0x1F,
                AreaObjectCode.EnterablePipe or
                AreaObjectCode.UnenterablePipe => Parameter & 7,
                _ => IsExtendableObject ? Parameter : 0,
            };
        }

        set
        {
            switch (Code)
            {
                case AreaObjectCode.ScreenJump:
                    Value2 &= 0xE0;
                    Value2 |= (byte)(value & 0x1F);
                    break;

                case AreaObjectCode.EnterablePipe:
                case AreaObjectCode.UnenterablePipe:
                    Parameter &= 0xF8;
                    Parameter |= (byte)(value & 7);
                    break;

                default:
                    if (IsExtendableObject)
                    {
                        Parameter &= 0xF0;
                        Parameter |= (byte)(value & 0x0F);
                    }

                    break;
            }
        }
    }

    public ForegroundScenery ForegroundScenery
    {
        get
        {
            return (ForegroundScenery)(Code == AreaObjectCode.ForegroundChange
                ? Parameter & 7
                : 0);
        }

        set
        {
            if (Code == AreaObjectCode.ForegroundChange)
            {
                Parameter &= 0xF8;
                Parameter |= (byte)((int)value & 7);
            }
        }
    }

    public TerrainMode TerrainMode
    {
        get
        {
            return (TerrainMode)(Code == AreaObjectCode.TerrainAndBackgroundSceneryChange
                ? BaseCommand & 0x0F
                : 0);
        }

        set
        {
            if (Code == AreaObjectCode.TerrainAndBackgroundSceneryChange)
            {
                BaseCommand &= 0xF0;
                BaseCommand |= (int)value & 0x0F;
            }
        }
    }

    public BackgroundScenery BackgroundScenery
    {
        get
        {
            return (BackgroundScenery)(Code == AreaObjectCode.TerrainAndBackgroundSceneryChange
                ? (BaseCommand >> 4) & 3
                : 0);
        }

        set
        {
            if (Code == AreaObjectCode.TerrainAndBackgroundSceneryChange)
            {
                BaseCommand &= 0xCF;
                BaseCommand |= ((int)value & 3) << 4;
            }
        }
    }

    public string BaseName
    {
        get
        {
            return Code.BaseName();
        }
    }

    public bool IsThreeByteCommand
    {
        get
        {
            return IsThreeByteSpecifier(Value1);
        }
    }

    public bool IsValid
    {
        get
        {
            return Value1 != 0xFD
                && (IsThreeByteCommand || Value3 == 0);
        }
    }

    public string HexString
    {
        get
        {
            return $"{Value1:X2} {Value2:X2}"
                + (IsThreeByteCommand ? $" {Value3:X2}" : String.Empty);
        }
    }

    public static bool operator ==(AreaObjectCommand left, AreaObjectCommand right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(AreaObjectCommand left, AreaObjectCommand right)
    {
        return !(left == right);
    }

    public static bool IsThreeByteSpecifier(int coordinates)
    {
        return (coordinates & 0x0F) == 0x0F;
    }

    public string FullName(AreaPlatformType areaPlatformType)
    {
        var length = Parameter + 1;

        switch (Code)
        {
            case AreaObjectCode.QuestionBlockPowerup:
                return "Question Block (Powerup)";

            case AreaObjectCode.QuestionBlockCoin:
                return "Question Block (Coin)";

            case AreaObjectCode.HiddenBlockCoin:
                return "Hidden Block (Coin)";

            case AreaObjectCode.HiddenBlock1UP:
                return "Hidden Block (1UP)";

            case AreaObjectCode.BrickPowerup:
                return "Brick (Powerup)";

            case AreaObjectCode.BrickBeanstalk:
                return "Brick (Beanstalk)";

            case AreaObjectCode.BrickStar:
                return "Brick (Star)";

            case AreaObjectCode.Brick10Coins:
                return "Brick (10 Coins)";

            case AreaObjectCode.Brick1UP:
                return "Brick (1UP)";

            case AreaObjectCode.SidewaysPipe:
                return "Sideways Pipe Cap";

            case AreaObjectCode.UsedBlock:
                return "Used Block";

            case AreaObjectCode.SpringBoard:
                return "Spring Board";

            case AreaObjectCode.JPipe:
            case AreaObjectCode.AltJPipe:
                return "J-Pipe";

            case AreaObjectCode.FlagPole:
            case AreaObjectCode.AltFlagPole:
                return "Flag Pole";

            case AreaObjectCode.Empty:
            case AreaObjectCode.Empty2:
                return "Nothing";

            case AreaObjectCode.AreaSpecificPlatform:
                switch (areaPlatformType)
                {
                    case AreaPlatformType.Trees:
                        return $"Tree Top Platform (Width={length})";

                    case AreaPlatformType.Mushrooms:
                        return $"Mushroom Platform (Width={length})";

                    case AreaPlatformType.BulletBillTurrets:
                        return $"Bullet Bill Shooter (Height={length})";

                    case AreaPlatformType.CloudGround:
                        return $"Cloud Ground (Width={length})";

                    default:
                        break;
                }

                break;

            case AreaObjectCode.HorizontalBricks:
                return $"Horizontal Bricks (Width={length})";

            case AreaObjectCode.HorizontalStones:
                return $"Horizontal Blocks (Width={length})";

            case AreaObjectCode.HorizontalCoins:
                return $"Horizontal Coins (Width={length})";

            case AreaObjectCode.VerticalBricks:
                return $"Vertical Bricks (Height={length})";

            case AreaObjectCode.VerticalStones:
                return $"Vertical Blocks (Height={length})";

            case AreaObjectCode.UnenterablePipe:
                return $"Unenterable Pipe (Height={length})";

            case AreaObjectCode.EnterablePipe:
                return $"Enterable Pipe (Height={length})";

            case AreaObjectCode.Hole:
                return $"Hole (Width={length})";

            case AreaObjectCode.BalanceHorizontalRope:
                return $"Pulley Platforms (Width={length})";

            case AreaObjectCode.BridgeV7:
                return $"Rope Bridge (Y=7, Width={length})";

            case AreaObjectCode.BridgeV8:
                return $"Rope Bridge (Y=8, Width={length})";

            case AreaObjectCode.BridgeV10:
                return $"Rope Bridge (Y=10, Width={length})";

            case AreaObjectCode.HoleWithWaterOrLava:
                return $"Hole with water or lava (Width={length})";

            case AreaObjectCode.HorizontalQuestionBlocksV3:
                return $"Row of Coin Blocks (Y=3, Width={length})";

            case AreaObjectCode.HorizontalQuestionBlocksV7:
                return $"Row of Coin Blocks (Y=7, Width={length})";

            case AreaObjectCode.ScreenJump:
                return $"Skip to screen 0x{BaseCommand:X2}";

            case AreaObjectCode.BowserAxe:
                return $"Bowser Axe";

            case AreaObjectCode.BowserBridge:
                return $"Bowser Bridge";

            case AreaObjectCode.ScrollStopWarpZone:
                return $"Scroll Stop (Warp Zone)";

            case AreaObjectCode.ScrollStop:
            case AreaObjectCode.AltScrollStop:
                return $"Scroll Stop";

            case AreaObjectCode.RedCheepCheepFlying:
                return $"Generator: Red flying cheep-cheeps";

            case AreaObjectCode.BulletBillGenerator:
                return $"Generator: Bullet Bills";

            case AreaObjectCode.StopGenerator:
                return $"Stop Generator (also stops Lakitus)";

            case AreaObjectCode.LoopCommand:
                return $"Screen Loop Command";

            case AreaObjectCode.TerrainAndBackgroundSceneryChange:
                return "Brick and scenery change";

            case AreaObjectCode.ForegroundChange:
                return "Foreground Change";

            case AreaObjectCode.RopeForLift:
                return "Rope for platform lifts";

            case AreaObjectCode.PulleyRope:
                return $"Rope for pulley platforms (Height={length})";

            case AreaObjectCode.EmptyTile:
                return "Empty tile";

            case AreaObjectCode.Castle:
                return "Castle";

            case AreaObjectCode.CastleCeilingCap:
                return "Castle Object: Ceiling Cap Tile";

            case AreaObjectCode.Staircase:
                return $"Staircase (Width={length})";

            case AreaObjectCode.CastleStairs:
                return "Castle Object: Descending Stairs";

            case AreaObjectCode.CastleRectangularCeilingTiles:
                return "Castle Object: Rectangular Ceiling Tiles";

            case AreaObjectCode.CastleFloorRightEdge:
                return "Castle Object: Right-Facing Wall To Floor";

            case AreaObjectCode.CastleFloorLeftEdge:
                return "Castle Object: Left-Facing Wall To Floor";

            case AreaObjectCode.CastleFloorLeftWall:
                return "Castle Object: Left-Facing Wall";

            case AreaObjectCode.CastleFloorRightWall:
                return "Castle Object: Right-Facing Wall";

            case AreaObjectCode.VerticalSeaBlocks:
                return $"Vertical Sea Blocks (Height={length})";

            case AreaObjectCode.ExtendableJPipe:
                return $"Extendable J-Pipe (Height={length})";

            case AreaObjectCode.VerticalBalls:
                return $"Vertical Climbing Balls (Height={length})";
        }

        return $"Unknown command: {this}";
    }

    public override bool Equals(object? obj)
    {
        return obj is AreaObjectCommand other && Equals(other);
    }

    public bool Equals(AreaObjectCommand other)
    {
        return Value1.Equals(other.Value1) && Value2.Equals(other.Value2)
            && (!IsThreeByteCommand || Value3.Equals(other.Value3));
    }

    public override int GetHashCode()
    {
        return IsThreeByteCommand
            ? HashCode.Combine(Value1, Value2, Value3)
            : HashCode.Combine(Value1, Value2);
    }

    public override string ToString()
    {
        return $"({X}, {Y}): {BaseName}";
    }
}
