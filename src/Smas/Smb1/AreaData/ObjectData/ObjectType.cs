// <copyright file="AreaObjectCode.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1.AreaData.ObjectData;

public enum ObjectType
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
    Spring,
    JPipe,
    FlagPole,
    Nothing1,
    Nothing2,
    AreaSpecificPlatform = 0x10,

    GreenIsland = AreaSpecificPlatform | 8 | AreaPlatformType.Trees,
    MushroomIsland = AreaSpecificPlatform | 8 | AreaPlatformType.Mushrooms,
    Cannon = AreaSpecificPlatform | 8 | AreaPlatformType.BulletBillTurrets,
    CloudGround = AreaSpecificPlatform | 8 | AreaPlatformType.CloudGround,

    HorizontalBricks = 0x20,
    HorizontalBlocks = 0x30,
    HorizontalCoins = 0x40,
    VerticalBricks = 0x50,
    VerticalBlocks = 0x60,
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

    PageSkip = 0x0D00,
    AltJPipe = 0x0D40,
    AltFlagPole,
    BowserAxe,
    RopeForAxe,
    BowserBridge,
    ScrollStopWarpZone,
    ScrollStop,
    AltScrollStop,
    JumpingCheepCheepGenerator,
    BulletBillGenerator,
    StopGenerator,
    LoopCommand,

    TerrainAndBackgroundSceneryChange = 0x0E00,
    ForegroundSceneryChange = 0x0E40,

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
