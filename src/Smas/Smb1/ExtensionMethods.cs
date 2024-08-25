// <copyright file="ExtensionMethods.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1;

using System.Collections.Generic;
using System.Collections.Immutable;

using AreaData.ObjectData;
using AreaData.SpriteData;

public static class ExtensionMethods
{
    private static readonly ImmutableHashSet<AreaObjectCode> HorizontallyExtendableObjects =
        ImmutableHashSet.Create(
            AreaObjectCode.AreaSpecificPlatform,
            AreaObjectCode.GreenIsland,
            AreaObjectCode.MushroomIsland,
            AreaObjectCode.CloudGround,
            AreaObjectCode.HorizontalBricks,
            AreaObjectCode.HorizontalStones,
            AreaObjectCode.HorizontalCoins,
            AreaObjectCode.Hole,
            AreaObjectCode.BalanceHorizontalRope,
            AreaObjectCode.BridgeV7,
            AreaObjectCode.BridgeV8,
            AreaObjectCode.BridgeV10,
            AreaObjectCode.HoleWithWaterOrLava,
            AreaObjectCode.HorizontalQuestionBlocksV3,
            AreaObjectCode.HorizontalQuestionBlocksV7,
            AreaObjectCode.Staircase);

    private static readonly ImmutableHashSet<AreaObjectCode> VerticallyExtendableObjects =
        ImmutableHashSet.Create(
            AreaObjectCode.VerticalBricks,
            AreaObjectCode.VerticalStones,
            AreaObjectCode.UnenterablePipe,
            AreaObjectCode.EnterablePipe,
            AreaObjectCode.RopeForLift,
            AreaObjectCode.PulleyRope,
            AreaObjectCode.Castle,
            AreaObjectCode.CastleCeilingCap,
            AreaObjectCode.Staircase,
            AreaObjectCode.VerticalSeaBlocks,
            AreaObjectCode.ExtendableJPipe,
            AreaObjectCode.VerticalBalls);

    private static readonly ImmutableHashSet<AreaObjectCode> ExtendableObjects =
        HorizontallyExtendableObjects
        .Union(VerticallyExtendableObjects)
        .Add(AreaObjectCode.ScreenJump);

    public static IEnumerable<byte> ToBytes(
        this IEnumerable<AreaObjectCommand> items)
    {
        foreach (var command in items)
        {
            yield return command.Value1;
            yield return command.Value2;
            if (command.IsThreeByteCommand)
            {
                yield return command.Value3;
            }
        }

        yield return AreaObjectCommand.TerminationCode;
    }

    public static IEnumerable<byte> ToBytes(
        this IEnumerable<AreaSpriteCommand> items)
    {
        foreach (var command in items)
        {
            yield return command.Value1;
            yield return command.Value2;
            if (command.IsThreeByteCommand)
            {
                yield return command.Value3;
            }
        }

        yield return AreaSpriteCommand.TerminationCode;
    }

    public static AreaObjectCode ToObjectCode(this AreaPlatformType type)
    {
        return AreaObjectCode.AreaSpecificPlatform | (AreaObjectCode)(8 | (int)type);
    }

    public static bool IsHorizontallyExtendableObject(this AreaObjectCode code)
    {
        return HorizontallyExtendableObjects.Contains(code);
    }

    public static bool IsVerticallyExtendableObject(this AreaObjectCode code)
    {
        return VerticallyExtendableObjects.Contains(code);
    }

    public static bool IsExtendableObject(this AreaObjectCode code)
    {
        return ExtendableObjects.Contains(code);
    }

    public static string BaseName(this AreaObjectCode code)
    {
        switch (code)
        {
            case AreaObjectCode.QuestionBlockPowerup:
                return AddDescriptor(
                    Resources.QuestionBlock,
                    Resources.Powerup);

            case AreaObjectCode.QuestionBlockCoin:
                return AddDescriptor(
                    Resources.QuestionBlock,
                    Resources.Coin);

            case AreaObjectCode.HiddenBlockCoin:
                return AddDescriptor(
                    Resources.HiddenBlock,
                    Resources.Coin);

            case AreaObjectCode.HiddenBlock1UP:
                return AddDescriptor(
                    Resources.HiddenBlock,
                    Resources.LifeMushroom);

            case AreaObjectCode.BrickPowerup:
                return AddDescriptor(
                    Resources.Brick,
                    Resources.Powerup);

            case AreaObjectCode.BrickBeanstalk:
                return AddDescriptor(
                    Resources.Brick,
                    Resources.Beanstalk);

            case AreaObjectCode.BrickStar:
                return AddDescriptor(
                    Resources.Brick,
                    Resources.Star);

            case AreaObjectCode.Brick10Coins:
                return AddDescriptor(
                    Resources.Brick,
                    Resources.TenCoins);

            case AreaObjectCode.Brick1UP:
                return AddDescriptor(
                    Resources.Brick,
                    Resources.LifeMushroom);

            case AreaObjectCode.SidewaysPipe:
                return Resources.SidewaysPipe;

            case AreaObjectCode.UsedBlock:
                return Resources.UsedBlock;

            case AreaObjectCode.SpringBoard:
                return Resources.SpringBoard;

            case AreaObjectCode.JPipe:
            case AreaObjectCode.AltJPipe:
                return Resources.JPipe;

            case AreaObjectCode.FlagPole:
            case AreaObjectCode.AltFlagPole:
                return Resources.FlagPole;

            case AreaObjectCode.Empty:
            case AreaObjectCode.Empty2:
                return Resources.Empty;

            case AreaObjectCode.AreaSpecificPlatform:
                return Resources.AreaSpecificPlatform;

            case AreaObjectCode.GreenIsland:
                return Resources.AreaSpecificPlatform_Trees;

            case AreaObjectCode.MushroomIsland:
                return Resources.AreaSpecificPlatform_Mushrooms;

            case AreaObjectCode.Cannon:
                return Resources.AreaSpecificPlatform_BulletBillTurrets;

            case AreaObjectCode.CloudGround:
                // This is not a mistake. The regular ground changes to clouds, and
                // area specific platform is still trees.
                return Resources.AreaSpecificPlatform_Trees;

            case AreaObjectCode.HorizontalBricks:
                return Resources.HorizontalBricks;

            case AreaObjectCode.HorizontalStones:
                return Resources.HorizontalStones;

            case AreaObjectCode.HorizontalCoins:
                return Resources.HorizontalCoins;

            case AreaObjectCode.VerticalBricks:
                return Resources.VerticalBricks;

            case AreaObjectCode.VerticalStones:
                return Resources.VerticalStones;

            case AreaObjectCode.UnenterablePipe:
                return Resources.UnenterablePipe;

            case AreaObjectCode.EnterablePipe:
                return Resources.EnterablePipe;

            case AreaObjectCode.Hole:
                return Resources.Hole;

            case AreaObjectCode.BalanceHorizontalRope:
                return Resources.BalanceHorizontalRope;

            case AreaObjectCode.BridgeV7:
                return AddYDescriptor(Resources.Bridge, 7);

            case AreaObjectCode.BridgeV8:
                return AddYDescriptor(Resources.Bridge, 8);

            case AreaObjectCode.BridgeV10:
                return AddYDescriptor(Resources.Bridge, 10);

            case AreaObjectCode.HoleWithWaterOrLava:
                return Resources.HoleWithWaterOrLava;

            case AreaObjectCode.HorizontalQuestionBlocksV3:
                return AddYDescriptor(Resources.HorizontalQuestionBlocks, 3);

            case AreaObjectCode.HorizontalQuestionBlocksV7:
                return AddYDescriptor(Resources.HorizontalQuestionBlocks, 7);

            case AreaObjectCode.ScreenJump:
                return Resources.ScreenJump;

            case AreaObjectCode.BowserAxe:
                return Resources.BowserAxe;

            case AreaObjectCode.RopeForAxe:
                return Resources.RopeForAxe;

            case AreaObjectCode.BowserBridge:
                return Resources.BowserBridge;

            case AreaObjectCode.ScrollStopWarpZone:
                return Resources.ScrollStopWarpZone;

            case AreaObjectCode.ScrollStop:
            case AreaObjectCode.AltScrollStop:
                return Resources.ScrollStop;

            case AreaObjectCode.RedCheepCheepFlying:
                return Resources.RedCheepCheepFlying;

            case AreaObjectCode.BulletBillGenerator:
                return Resources.BulletBillGenerator;

            case AreaObjectCode.StopGenerator:
                return Resources.StopGenerator;

            case AreaObjectCode.LoopCommand:
                return Resources.LoopCommand;

            case AreaObjectCode.TerrainAndBackgroundSceneryChange:
                return Resources.BrickAndSceneryChange;

            case AreaObjectCode.ForegroundChange:
                return Resources.ForegroundChange;

            case AreaObjectCode.RopeForLift:
                return Resources.RopeForLift;

            case AreaObjectCode.PulleyRope:
                return Resources.PulleyRope;

            case AreaObjectCode.EmptyTile:
                return Resources.EmptyTile;

            case AreaObjectCode.Castle:
                return Resources.Castle;

            case AreaObjectCode.CastleCeilingCap:
                return Resources.CastleCeilingCap;

            case AreaObjectCode.Staircase:
                return Resources.Staircase;

            case AreaObjectCode.CastleStairs:
                return Resources.CastleStairs;

            case AreaObjectCode.CastleRectangularCeilingTiles:
                return Resources.CastleRectangularCeilingTiles;

            case AreaObjectCode.CastleFloorRightEdge:
                return Resources.CastleFloorRightEdge;

            case AreaObjectCode.CastleFloorLeftEdge:
                return Resources.CastleFloorLeftEdge;

            case AreaObjectCode.CastleFloorLeftWall:
                return Resources.CastleFloorLeftWall;

            case AreaObjectCode.CastleFloorRightWall:
                return Resources.CastleFloorRightWall;

            case AreaObjectCode.VerticalSeaBlocks:
                return Resources.VerticalSeaBlocks;

            case AreaObjectCode.ExtendableJPipe:
                return Resources.ExtendableJPipe;

            case AreaObjectCode.VerticalBalls:
                return Resources.VerticalBalls;

            default:
                return String.Format(
                    Resources.UnknownCommand,
                    ((int)code).ToString("X4"));
        }
    }

    public static int GetMaxLength(this AreaObjectCode code)
    {
        switch (code)
        {
            case AreaObjectCode.ScreenJump:
                return 0x1F;

            case AreaObjectCode.EnterablePipe:
            case AreaObjectCode.UnenterablePipe:
                return 7;

            case AreaObjectCode.Staircase:
                return 8;

            case AreaObjectCode.Castle:
                return 7;

            default:
                return code.IsExtendableObject() ? 0x0F : 0;
        }
    }

    public static string BaseName(this AreaSpriteCode code)
    {
        switch (code)
        {
            case AreaSpriteCode.AreaPointer:
                return Resources.AreaPointer;

            case AreaSpriteCode.GreenKoopaTroopa:
                return AddDescriptor(
                    Resources.KoopaTroopa,
                    Resources.Green);

            case AreaSpriteCode.RedKoopaTroopa:
                return AddDescriptor(
                    Resources.KoopaTroopa,
                    Resources.Red);

            case AreaSpriteCode.BuzzyBeetle:
                return Resources.BuzzyBeetle;

            case AreaSpriteCode.RedKoopaTroopa2:
                return AddTwoDescriptors(
                    Resources.KoopaParatroopa,
                    Resources.Red,
                    Resources.WalksOffFloors);

            case AreaSpriteCode.GreenKoopaTroopa2:
                return AddTwoDescriptors(
                    Resources.KoopaParatroopa,
                    Resources.Green,
                    Resources.WalksInPlace);

            case AreaSpriteCode.HammerBros:
                return Resources.HammerBros;

            case AreaSpriteCode.Goomba:
                return Resources.Goomba;

            case AreaSpriteCode.Blooper:
                return Resources.Blooper;

            case AreaSpriteCode.BulletBill:
                return Resources.BulletBill;

            case AreaSpriteCode.YellowKoopaParatroopa:
                return AddTwoDescriptors(
                    Resources.KoopaParatroopa,
                    Resources.Yellow,
                    WarningDescriptor(Resources.FliesInPlace));

            case AreaSpriteCode.GreenCheepCheep:
                return AddDescriptor(
                    Resources.CheepCheep,
                    Resources.Green);

            case AreaSpriteCode.RedCheepCheep:
                return AddDescriptor(
                    Resources.CheepCheep,
                    Resources.Red);

            case AreaSpriteCode.Podoboo:
                return Resources.Podoboo;

            case AreaSpriteCode.PiranhaPlant:
                return Resources.PiranhaPlant;

            case AreaSpriteCode.GreenKoopaParatroopa:
                return AddTwoDescriptors(
                    Resources.KoopaParatroopa,
                    Resources.Green,
                    Resources.Leaping);

            case AreaSpriteCode.RedKoopaParatroopa:
                return AddTwoDescriptors(
                    Resources.KoopaParatroopa,
                    Resources.Red,
                    Resources.FliesVertically);

            case AreaSpriteCode.GreenKoopaParatroopa2:
                return AddTwoDescriptors(
                    Resources.KoopaParatroopa,
                    Resources.Green,
                    Resources.FliesHorizontally);

            case AreaSpriteCode.Lakitu:
                return Resources.Lakitu;

            case AreaSpriteCode.Spiny:
                return AddWarningDescriptor(
                    Resources.Spiny,
                    Resources.RandomWalkSpeed);

            case AreaSpriteCode.RedFlyingCheepCheep:
                return AddTwoDescriptors(
                    Resources.CheepCheep,
                    Resources.Red,
                    Resources.Flying);

            case AreaSpriteCode.BowsersFire:
                return AddDescriptor(
                    Resources.BowserFire,
                    Resources.Generator);

            case AreaSpriteCode.Fireworks:
                return Resources.Firework;

            case AreaSpriteCode.BulletBillOrCheepCheeps:
                return AddDescriptor(
                    Resources.Generator,
                    Resources.BulletBillOrCheepCheep);

            case AreaSpriteCode.FireBarClockwise:
                return AddDescriptor(
                    Resources.FireBar,
                    Resources.Clockwise);

            case AreaSpriteCode.FastFireBarClockwise:
                return AddTwoDescriptors(
                    Resources.FireBar,
                    Resources.Fast,
                    Resources.Clockwise);

            case AreaSpriteCode.FireBarCounterClockwise:
                return AddDescriptor(
                    Resources.FireBar,
                    Resources.CounterClockwise);

            case AreaSpriteCode.FastFireBarCounterClockwise:
                return AddTwoDescriptors(
                    Resources.FireBar,
                    Resources.Fast,
                    Resources.CounterClockwise);

            case AreaSpriteCode.LongFireBarClockwise:
                return AddTwoDescriptors(
                    Resources.FireBar,
                    Resources.Long,
                    Resources.CounterClockwise);

            case AreaSpriteCode.BalanceRopeLift:
                return Resources.BalanceRopeLift;

            case AreaSpriteCode.LiftDownThenUp:
                return AddTwoDescriptors(
                    Resources.Lift,
                    Resources.Down,
                    Resources.Up);

            case AreaSpriteCode.LiftUp:
                return AddDescriptor(
                    Resources.Lift,
                    Resources.Up);

            case AreaSpriteCode.LiftDown:
                return AddDescriptor(
                    Resources.Lift,
                    Resources.Down);

            case AreaSpriteCode.LiftLeftThenRight:
                return AddTwoDescriptors(
                    Resources.Lift,
                    Resources.Left,
                    Resources.Right);

            case AreaSpriteCode.LiftFalling:
                return AddDescriptor(
                    Resources.Lift,
                    Resources.Falling);

            case AreaSpriteCode.LiftRight:
                return AddDescriptor(
                    Resources.Lift,
                    Resources.Right);

            case AreaSpriteCode.ShortLiftUp:
                return AddTwoDescriptors(
                    Resources.Lift,
                    Resources.Short,
                    Resources.Up);

            case AreaSpriteCode.ShortLiftDown:
                return AddTwoDescriptors(
                    Resources.Lift,
                    Resources.Short,
                    Resources.Down);

            case AreaSpriteCode.Bowser:
                return Resources.Bowser;

            case AreaSpriteCode.WarpZoneCommand:
                return Resources.WarpZoneCommand;

            case AreaSpriteCode.ToadOrPrincess:
                return Resources.ToadOrPrincess;

            case AreaSpriteCode.TwoGoombasY10:
                return AddYDescriptor(Resources.Goomba2, 10);

            case AreaSpriteCode.ThreeGoombasY10:
                return AddYDescriptor(Resources.Goomba3, 10);

            case AreaSpriteCode.TwoGoombasY6:
                return AddYDescriptor(Resources.Goomba2, 6);

            case AreaSpriteCode.ThreeGoombasY6:
                return AddYDescriptor(Resources.Goomba3, 6);

            case AreaSpriteCode.TwoGreenKoopasY10:
                return AddYDescriptor(Resources.GreenKoopaTroopa2, 10);

            case AreaSpriteCode.ThreeGreenKoopasY10:
                return AddYDescriptor(Resources.GreenKoopaTroopa3, 10);

            case AreaSpriteCode.TwoGreenKoopasY6:
                return AddYDescriptor(Resources.GreenKoopaTroopa2, 6);

            case AreaSpriteCode.ThreeGreenKoopasY6:
                return AddYDescriptor(Resources.GreenKoopaTroopa3, 6);

            case AreaSpriteCode.ScreenJump:
                return Resources.ScreenJump;

            default:
                return String.Format(
                    Resources.UnknownCommand,
                    ((int)code).ToString("X2"));
        }
    }

    public static string GetDescription(
        this AreaObjectCommand command,
        AreaPlatformType areaPlatformType)
    {
        var length = command.Parameter + 1;
        var code = command.Code;
        switch (code)
        {
            case AreaObjectCode.AreaSpecificPlatform:
                code = areaPlatformType.ToObjectCode();
                break;

            case AreaObjectCode.BridgeV7:
                return AddTwoDescriptors(
                    Resources.Bridge,
                    YDescriptor(7),
                    WidthDescriptor(length));

            case AreaObjectCode.BridgeV8:
                return AddTwoDescriptors(
                    Resources.Bridge,
                    YDescriptor(8),
                    WidthDescriptor(length));

            case AreaObjectCode.BridgeV10:
                return AddTwoDescriptors(
                    Resources.Bridge,
                    YDescriptor(10),
                    WidthDescriptor(length));

            case AreaObjectCode.HorizontalQuestionBlocksV3:
                return AddTwoDescriptors(
                    Resources.HorizontalQuestionBlocks,
                    YDescriptor(3),
                    WidthDescriptor(length));

            case AreaObjectCode.HorizontalQuestionBlocksV7:
                return AddTwoDescriptors(
                    Resources.HorizontalQuestionBlocks,
                    YDescriptor(7),
                    WidthDescriptor(length));
        }

        var baseName = code.BaseName();
        return code.IsHorizontallyExtendableObject()
            ? AddWidthDescriptor(baseName, length)
            : code.IsVerticallyExtendableObject()
            ? AddHeightDescriptor(baseName, length)
            : code == AreaObjectCode.ScreenJump
            ? AddDescriptor(baseName, PageSetDescriptor(length))
            : baseName;
    }

    private static string AddDescriptor(string item, string description)
    {
        return String.Format(
            Resources.ObjectWithDescriptor,
            item,
            description);
    }

    private static string AddTwoDescriptors(
        string item,
        string description1,
        string description2)
    {
        return String.Format(
            Resources.ObjectWithTwoDescriptors,
            item,
            description1,
            description2);
    }

    private static string YDescriptor(object y)
    {
        return String.Format(Resources.WithSpecificYCoord, y);
    }

    private static string HeightDescriptor(object height)
    {
        return String.Format(Resources.VerticallyExtendableObject, height);
    }

    private static string WidthDescriptor(object width)
    {
        return String.Format(Resources.HorizontallyExtendableObject, width);
    }

    private static string AddYDescriptor(string item, object y)
    {
        return AddDescriptor(item, YDescriptor(y));
    }

    private static string AddHeightDescriptor(string item, object height)
    {
        return AddDescriptor(item, HeightDescriptor(height));
    }

    private static string AddWidthDescriptor(string item, object width)
    {
        return AddDescriptor(item, WidthDescriptor(width));
    }

    private static string PageSetDescriptor(int page)
    {
        return String.Format(Resources.SetPage, page);
    }

    private static string WarningDescriptor(string message)
    {
        return String.Format(Resources.WarningDescriptor, message);
    }

    private static string AddWarningDescriptor(string item, string message)
    {
        return AddDescriptor(item, WarningDescriptor(message));
    }
}
