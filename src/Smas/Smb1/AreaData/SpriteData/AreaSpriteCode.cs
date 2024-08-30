// <copyright file="AreaSpriteCode.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1.AreaData.SpriteData;

public enum AreaSpriteCode
{
    AreaPointer = -1,

    /// <summary>
    /// Green Koopa Troopa (walks off floors).
    /// </summary>
    GreenKoopaTroopa,

    /// <summary>
    /// Red Koopa Troopa (walks off floors).
    /// </summary>
    RedKoopaTroopa,

    /// <summary>
    /// Buzzy Beetle.
    /// </summary>
    BuzzyBeetle,

    /// <summary>
    /// Red Koopa Troopa (stays on floors).
    /// </summary>
    RedKoopaTroopaPatrol,

    /// <summary>
    /// Green Koopa Troopa (walks in place).
    /// </summary>
    GreenKoopaTroopaStopped,

    /// <summary>
    /// Hammer Bros.
    /// </summary>
    HammerBros,

    /// <summary>
    /// Goomba
    /// </summary>
    Goomba,

    /// <summary>
    /// Blooper/Bloober/Squid
    /// </summary>
    Blooper,

    /// <summary>
    /// Bullet Bill
    /// </summary>
    BulletBill,

    /// <summary>
    /// Yellow Koopa Paratroopa (flies in place).
    /// </summary>
    YellowKoopaParatroopaStopped,

    /// <summary>
    /// Green Cheep-cheep (slow)
    /// </summary>
    GreenCheepCheep,

    /// <summary>
    /// Red Cheep-cheep (fast)
    /// </summary>
    RedCheepCheep,

    /// <summary>
    /// Podoboo (jumps up to height specified).
    /// </summary>
    Podoboo,

    /// <summary>
    /// Piranha Plant
    /// </summary>
    PiranhaPlant,

    /// <summary>
    /// Green Koopa Paratroopa (Leaping)
    /// </summary>
    GreenKoopaParatroopaLeaping,

    /// <summary>
    /// Red Koopa Paratroopa (flies vertically)
    /// </summary>
    RedKoopaParatroopa,

    /// <summary>
    /// Green Koopa Paratroopa (flies horizontally)
    /// </summary>
    GreenKoopaParatroopaFlying,

    /// <summary>
    /// Lakitu
    /// </summary>
    Lakitu,

    /// <summary>
    /// Spiny (Not intended for direct use).
    /// </summary>
    Spiny,

    /// <summary>
    /// Red Flying Cheep-cheeps (generator)
    /// </summary>
    RedFlyingCheepCheep = 0x14,

    /// <summary>
    /// Bowser's fire (generator)
    /// </summary>
    BowsersFire,

    /// <summary>
    /// Fireworks (generator)
    /// </summary>
    Fireworks,

    /// <summary>
    /// Bullet Bill/Cheep-cheep (generator)
    /// </summary>
    BulletBillOrCheepCheeps,

    /// <summary>
    /// Fire bar (clockwise)
    /// </summary>
    FireBarClockwise = 0x1B,

    /// <summary>
    /// Fast fire bar (clockwise)
    /// </summary>
    FastFireBarClockwise,

    /// <summary>
    /// Fire bar (counter-clockwise)
    /// </summary>
    FireBarCounterClockwise,

    /// <summary>
    /// Fast fire bar (counter-clockwise)
    /// </summary>
    FastFireBarCounterClockwise,

    /// <summary>
    /// Long fire bar (clockwise)
    /// </summary>
    LongFireBarClockwise,

    /// <summary>
    /// Lift for balance ropes
    /// </summary>
    BalanceRopeLift = 0x24,

    /// <summary>
    /// Lift (moves down then back up)
    /// </summary>
    LiftDownThenUp,

    /// <summary>
    /// Lift (moves up)
    /// </summary>
    LiftUp,

    /// <summary>
    /// Lift (moves down)
    /// </summary>
    LiftDown,

    /// <summary>
    /// Lift (moves left then right)
    /// </summary>
    LiftLeftThenRight,

    /// <summary>
    /// Lift (falls)
    /// </summary>
    LiftFalling,

    /// <summary>
    /// Lift (moves right)
    /// </summary>
    LiftRight,

    /// <summary>
    /// Short lift (moves up)
    /// </summary>
    ShortLiftUp,

    /// <summary>
    /// Short lift (moved down)
    /// </summary>
    ShortLiftDown,

    /// <summary>
    /// Bowser
    /// </summary>
    Bowser,

    /// <summary>
    /// Warp zone command
    /// </summary>
    WarpZoneCommand = 0x34,

    /// <summary>
    /// Toad or princess (depends on world)
    /// </summary>
    ToadOrPrincess,

    /// <summary>
    /// 2 Goombas separated horizontally by 8 pixels (Y = 10)
    /// </summary>
    TwoGoombasY10 = 0x37,

    /// <summary>
    /// 2 Goombas separated horizontally by 8 pixels (Y = 10)
    /// </summary>
    ThreeGoombasY10,

    /// <summary>
    /// 3 Goombas separated horizontally by 8 pixels (Y = 10)
    /// </summary>
    TwoGoombasY6,

    /// <summary>
    /// 2 Goombas separated horizontally by 8 pixels (Y = 6)
    /// </summary>
    ThreeGoombasY6,

    /// <summary>
    /// 2 Green Koopa Troopas separated horizontally by 8 pixels (Y = 6)
    /// </summary>
    TwoGreenKoopasY10,

    /// <summary>
    /// 3 Green Koopa Troopas separated horizontally by 8 pixels (Y = 10)
    /// </summary>
    ThreeGreenKoopasY10,

    /// <summary>
    /// 2 Green Koopa Troopas separated horizontally by 8 pixels (Y = 6)
    /// </summary>
    TwoGreenKoopasY6,

    /// <summary>
    /// 3 Green Koopa Troopas separated horizontally by 8 pixels (Y = 6)
    /// </summary>
    ThreeGreenKoopasY6,

    ScreenJump = 0x40,
}
