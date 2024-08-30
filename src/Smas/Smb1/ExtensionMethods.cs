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
    private static readonly ImmutableHashSet<ObjectType> HorizontallyExtendableObjects =
        ImmutableHashSet.Create(
            ObjectType.AreaSpecificPlatform,
            ObjectType.GreenIsland,
            ObjectType.MushroomIsland,
            ObjectType.CloudGround,
            ObjectType.HorizontalBricks,
            ObjectType.HorizontalBlocks,
            ObjectType.HorizontalCoins,
            ObjectType.Hole,
            ObjectType.BalanceHorizontalRope,
            ObjectType.BridgeV7,
            ObjectType.BridgeV8,
            ObjectType.BridgeV10,
            ObjectType.HoleWithWaterOrLava,
            ObjectType.HorizontalQuestionBlocksV3,
            ObjectType.HorizontalQuestionBlocksV7,
            ObjectType.Staircase);

    private static readonly ImmutableHashSet<ObjectType> VerticallyExtendableObjects =
        ImmutableHashSet.Create(
            ObjectType.VerticalBricks,
            ObjectType.VerticalBlocks,
            ObjectType.UnenterablePipe,
            ObjectType.EnterablePipe,
            ObjectType.RopeForLift,
            ObjectType.PulleyRope,
            ObjectType.Castle,
            ObjectType.CastleCeilingCap,
            ObjectType.Staircase,
            ObjectType.VerticalSeaBlocks,
            ObjectType.ExtendableJPipe,
            ObjectType.VerticalBalls);

    private static readonly ImmutableHashSet<ObjectType> ExtendableObjects =
        HorizontallyExtendableObjects
        .Union(VerticallyExtendableObjects)
        .Add(ObjectType.PageSkip);

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

    public static ObjectType ToObjectCode(this AreaPlatformType type)
    {
        return ObjectType.AreaSpecificPlatform | (ObjectType)(8 | (int)type);
    }

    public static bool IsHorizontallyExtendableObject(this ObjectType code)
    {
        return HorizontallyExtendableObjects.Contains(code);
    }

    public static bool IsVerticallyExtendableObject(this ObjectType code)
    {
        return VerticallyExtendableObjects.Contains(code);
    }

    public static bool IsExtendableObject(this ObjectType code)
    {
        return ExtendableObjects.Contains(code);
    }

    public static string BaseName(this ObjectType code)
    {
        switch (code)
        {
            case ObjectType.QuestionBlockPowerup:
                return AddDescriptor(
                    Resources.QuestionBlock,
                    Resources.Powerup);

            case ObjectType.QuestionBlockCoin:
                return AddDescriptor(
                    Resources.QuestionBlock,
                    Resources.Coin);

            case ObjectType.HiddenBlockCoin:
                return AddDescriptor(
                    Resources.HiddenBlock,
                    Resources.Coin);

            case ObjectType.HiddenBlock1UP:
                return AddDescriptor(
                    Resources.HiddenBlock,
                    Resources.LifeMushroom);

            case ObjectType.BrickPowerup:
                return AddDescriptor(
                    Resources.Brick,
                    Resources.Powerup);

            case ObjectType.BrickBeanstalk:
                return AddDescriptor(
                    Resources.Brick,
                    Resources.Beanstalk);

            case ObjectType.BrickStar:
                return AddDescriptor(
                    Resources.Brick,
                    Resources.Star);

            case ObjectType.Brick10Coins:
                return AddDescriptor(
                    Resources.Brick,
                    Resources.TenCoins);

            case ObjectType.Brick1UP:
                return AddDescriptor(
                    Resources.Brick,
                    Resources.LifeMushroom);

            case ObjectType.SidewaysPipe:
                return Resources.SidewaysPipe;

            case ObjectType.UsedBlock:
                return Resources.UsedBlock;

            case ObjectType.Spring:
                return Resources.SpringBoard;

            case ObjectType.JPipe:
            case ObjectType.AltJPipe:
                return Resources.JPipe;

            case ObjectType.FlagPole:
            case ObjectType.AltFlagPole:
                return Resources.FlagPole;

            case ObjectType.Nothing1:
            case ObjectType.Nothing2:
                return Resources.Empty;

            case ObjectType.AreaSpecificPlatform:
                return Resources.AreaSpecificPlatform;

            case ObjectType.GreenIsland:
                return Resources.AreaSpecificPlatform_Trees;

            case ObjectType.MushroomIsland:
                return Resources.AreaSpecificPlatform_Mushrooms;

            case ObjectType.Cannon:
                return Resources.AreaSpecificPlatform_BulletBillTurrets;

            case ObjectType.CloudGround:
                // This is not a mistake. The regular ground changes to clouds, and
                // area specific platform is still trees.
                return Resources.AreaSpecificPlatform_Trees;

            case ObjectType.HorizontalBricks:
                return Resources.HorizontalBricks;

            case ObjectType.HorizontalBlocks:
                return Resources.HorizontalStones;

            case ObjectType.HorizontalCoins:
                return Resources.HorizontalCoins;

            case ObjectType.VerticalBricks:
                return Resources.VerticalBricks;

            case ObjectType.VerticalBlocks:
                return Resources.VerticalStones;

            case ObjectType.UnenterablePipe:
                return Resources.UnenterablePipe;

            case ObjectType.EnterablePipe:
                return Resources.EnterablePipe;

            case ObjectType.Hole:
                return Resources.Hole;

            case ObjectType.BalanceHorizontalRope:
                return Resources.BalanceHorizontalRope;

            case ObjectType.BridgeV7:
                return AddYDescriptor(Resources.Bridge, 7);

            case ObjectType.BridgeV8:
                return AddYDescriptor(Resources.Bridge, 8);

            case ObjectType.BridgeV10:
                return AddYDescriptor(Resources.Bridge, 10);

            case ObjectType.HoleWithWaterOrLava:
                return Resources.HoleWithWaterOrLava;

            case ObjectType.HorizontalQuestionBlocksV3:
                return AddYDescriptor(Resources.HorizontalQuestionBlocks, 3);

            case ObjectType.HorizontalQuestionBlocksV7:
                return AddYDescriptor(Resources.HorizontalQuestionBlocks, 7);

            case ObjectType.PageSkip:
                return Resources.ScreenJump;

            case ObjectType.BowserAxe:
                return Resources.BowserAxe;

            case ObjectType.RopeForAxe:
                return Resources.RopeForAxe;

            case ObjectType.BowserBridge:
                return Resources.BowserBridge;

            case ObjectType.ScrollStopWarpZone:
                return Resources.ScrollStopWarpZone;

            case ObjectType.ScrollStop:
            case ObjectType.AltScrollStop:
                return Resources.ScrollStop;

            case ObjectType.JumpingCheepCheepGenerator:
                return Resources.RedCheepCheepFlying;

            case ObjectType.BulletBillGenerator:
                return Resources.BulletBillGenerator;

            case ObjectType.StopGenerator:
                return Resources.StopGenerator;

            case ObjectType.LoopCommand:
                return Resources.LoopCommand;

            case ObjectType.TerrainAndBackgroundSceneryChange:
                return Resources.BrickAndSceneryChange;

            case ObjectType.ForegroundSceneryChange:
                return Resources.ForegroundChange;

            case ObjectType.RopeForLift:
                return Resources.RopeForLift;

            case ObjectType.PulleyRope:
                return Resources.PulleyRope;

            case ObjectType.EmptyTile:
                return Resources.EmptyTile;

            case ObjectType.Castle:
                return Resources.Castle;

            case ObjectType.CastleCeilingCap:
                return Resources.CastleCeilingCap;

            case ObjectType.Staircase:
                return Resources.Staircase;

            case ObjectType.CastleStairs:
                return Resources.CastleStairs;

            case ObjectType.CastleRectangularCeilingTiles:
                return Resources.CastleRectangularCeilingTiles;

            case ObjectType.CastleFloorRightEdge:
                return Resources.CastleFloorRightEdge;

            case ObjectType.CastleFloorLeftEdge:
                return Resources.CastleFloorLeftEdge;

            case ObjectType.CastleFloorLeftWall:
                return Resources.CastleFloorLeftWall;

            case ObjectType.CastleFloorRightWall:
                return Resources.CastleFloorRightWall;

            case ObjectType.VerticalSeaBlocks:
                return Resources.VerticalSeaBlocks;

            case ObjectType.ExtendableJPipe:
                return Resources.ExtendableJPipe;

            case ObjectType.VerticalBalls:
                return Resources.VerticalBalls;

            default:
                return String.Format(
                    Resources.UnknownCommand,
                    ((int)code).ToString("X4"));
        }
    }

    public static int GetMaxLength(this ObjectType code)
    {
        switch (code)
        {
            case ObjectType.PageSkip:
                return 0x20;

            case ObjectType.EnterablePipe:
            case ObjectType.UnenterablePipe:
                return 8;

            case ObjectType.Staircase:
                return 9;

            case ObjectType.Castle:
                return 8;

            default:
                return code.IsExtendableObject() ? 0x10 : 1;
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

            case AreaSpriteCode.RedKoopaTroopaPatrol:
                return AddTwoDescriptors(
                    Resources.KoopaParatroopa,
                    Resources.Red,
                    Resources.WalksOffFloors);

            case AreaSpriteCode.GreenKoopaTroopaStopped:
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

            case AreaSpriteCode.YellowKoopaParatroopaStopped:
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

            case AreaSpriteCode.GreenKoopaParatroopaLeaping:
                return AddTwoDescriptors(
                    Resources.KoopaParatroopa,
                    Resources.Green,
                    Resources.Leaping);

            case AreaSpriteCode.RedKoopaParatroopa:
                return AddTwoDescriptors(
                    Resources.KoopaParatroopa,
                    Resources.Red,
                    Resources.FliesVertically);

            case AreaSpriteCode.GreenKoopaParatroopaFlying:
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
        var code = command.ObjectType;
        switch (code)
        {
            case ObjectType.AreaSpecificPlatform:
                code = areaPlatformType.ToObjectCode();
                break;

            case ObjectType.BridgeV7:
                return AddTwoDescriptors(
                    Resources.Bridge,
                    YDescriptor(7),
                    WidthDescriptor(length));

            case ObjectType.BridgeV8:
                return AddTwoDescriptors(
                    Resources.Bridge,
                    YDescriptor(8),
                    WidthDescriptor(length));

            case ObjectType.BridgeV10:
                return AddTwoDescriptors(
                    Resources.Bridge,
                    YDescriptor(10),
                    WidthDescriptor(length));

            case ObjectType.HorizontalQuestionBlocksV3:
                return AddTwoDescriptors(
                    Resources.HorizontalQuestionBlocks,
                    YDescriptor(3),
                    WidthDescriptor(length));

            case ObjectType.HorizontalQuestionBlocksV7:
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
            : code == ObjectType.PageSkip
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
