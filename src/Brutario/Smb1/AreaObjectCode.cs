﻿// <copyright file="AreaObjectCode.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed under GNU
//     Affero General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Smb1
{
    public enum AreaObjectCode
    {
        QuestionBlockPowerup,
        QuestionBlockCoin,
        HiddenBlockCoin,
        HiddenBlock1UP,
        BrickPowerup,
        BrickBeanstalk,
        BrickStar,
        Brick10Coins,
        Brick1UP,
        SidewaysPipe,
        UsedBlock,
        SpringBoard,
        JPipe,
        FlagPole,
        Empty,
        Empty2,
        AreaSpecificPlatform = 0x10,

        GreenIsland = AreaSpecificPlatform | 8 | AreaPlatformType.Trees,
        MushroomIsland = AreaSpecificPlatform | 8 | AreaPlatformType.Mushrooms,
        Cannon = AreaSpecificPlatform | 8 | AreaPlatformType.BulletBillTurrets,
        CloudGround = AreaSpecificPlatform | 8 | AreaPlatformType.CloudGround,

        HorizontalBricks = 0x20,
        HorizontalStones = 0x30,
        HorizontalCoins = 0x40,
        VerticalBricks = 0x50,
        VerticalStones = 0x60,
        UnenterablePipe = 0x70,
        EnterablePipe = 0x78,

        Hole = 0x0C00,
        BalanceHorizontalRope = 0x0C10,
        BridgeV7 = 0x0C20,
        BridgeV8 = 0x0C30,
        BridgeV10 = 0x0C40,
        HoleWithWaterOrLava = 0x0C50,
        HorizontalQuestionBlocksV3 = 0x0C60,
        HorizontalQuestionBlocksV7 = 0x0C70,

        ScreenJump = 0x0D00,
        AltJPipe = 0x0D40,
        AltFlagPole,
        BowserAxe,
        RopeForAxe,
        BowserBridge,
        ScrollStopWarpZone,
        ScrollStop,
        AltScrollStop,
        RedCheepCheepFlying,
        BulletBillGenerator,
        StopGenerator,
        LoopCommand,

        BrickAndSceneryChange = 0x0E00,
        ForegroundChange = 0x0E40,

        RopeForLift = 0x0F00,
        PulleyRope = 0x0F10,
        EmptyTile = 0x0F12,
        Castle = 0x0F20,
        CastleCeilingCap = 0x0F28,
        Staircase = 0x0F30,
        CastleStairs = 0x0F32,
        CastleRectangularCeilingTiles = 0x0F34,
        CastleFloorRightEdge = 0x0F36,
        CastleFloorLeftEdge = 0x0F38,
        CastleFloorLeftWall = 0x0F3A,
        CastleFloorRightWall = 0x0F3C,
        VerticalSeaBlocks = 0x0F3E,
        ExtendableJPipe = 0x0F40,
        VerticalBalls = 0x0F50,
    }
}
