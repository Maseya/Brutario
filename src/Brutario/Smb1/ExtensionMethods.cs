namespace Brutario.Smb1
{
    public static class ExtensionMethods
    {
        public static AreaObjectCode ToObjectCode(this AreaPlatformType type)
        {
            return AreaObjectCode.AreaSpecificPlatform
                | (AreaObjectCode)(8 | (int)type);
        }

        public static bool IsExtendableObject(this AreaObjectCode code)
        {
            switch (code)
            {
            case AreaObjectCode.QuestionBlockPowerup:
            case AreaObjectCode.QuestionBlockCoin:
            case AreaObjectCode.HiddenBlockCoin:
            case AreaObjectCode.HiddenBlock1UP:
            case AreaObjectCode.BrickPowerup:
            case AreaObjectCode.BrickBeanstalk:
            case AreaObjectCode.BrickStar:
            case AreaObjectCode.Brick10Coins:
            case AreaObjectCode.Brick1UP:
            case AreaObjectCode.SidewaysPipe:
            case AreaObjectCode.UsedBlock:
            case AreaObjectCode.SpringBoard:
            case AreaObjectCode.JPipe:
            case AreaObjectCode.FlagPole:
            case AreaObjectCode.Empty:
            case AreaObjectCode.Empty2:
                return false;

            case AreaObjectCode.AreaSpecificPlatform:
            case AreaObjectCode.GreenIsland:
            case AreaObjectCode.MushroomIsland:
            case AreaObjectCode.Cannon:
            case AreaObjectCode.CloudGround:
            case AreaObjectCode.HorizontalBricks:
            case AreaObjectCode.HorizontalStones:
            case AreaObjectCode.HorizontalCoins:
            case AreaObjectCode.VerticalBricks:
            case AreaObjectCode.VerticalStones:
            case AreaObjectCode.UnenterablePipe:
            case AreaObjectCode.EnterablePipe:
            case AreaObjectCode.Hole:
            case AreaObjectCode.BalanceHorizontalRope:
            case AreaObjectCode.BridgeV7:
            case AreaObjectCode.BridgeV8:
            case AreaObjectCode.BridgeV10:
            case AreaObjectCode.HoleWithWaterOrLava:
            case AreaObjectCode.HorizontalQuestionBlocksV3:
            case AreaObjectCode.HorizontalQuestionBlocksV7:
            case AreaObjectCode.ScreenJump:
                return true;

            case AreaObjectCode.AltJPipe:
            case AreaObjectCode.AltFlagPole:
            case AreaObjectCode.BowserAxe:
            case AreaObjectCode.RopeForAxe:
            case AreaObjectCode.BowserBridge:
            case AreaObjectCode.ScrollStopWarpZone:
            case AreaObjectCode.ScrollStop:
            case AreaObjectCode.AltScrollStop:
            case AreaObjectCode.RedCheepCheepFlying:
            case AreaObjectCode.BulletBillGenerator:
            case AreaObjectCode.StopGenerator:
            case AreaObjectCode.LoopCommand:
            case AreaObjectCode.BrickAndSceneryChange:
            case AreaObjectCode.ForegroundChange:
                return false;

            case AreaObjectCode.RopeForLift:
            case AreaObjectCode.PulleyRope:
                return true;

            case AreaObjectCode.EmptyTile:
                return false;

            case AreaObjectCode.Castle:
            case AreaObjectCode.CastleCeilingCap:
            case AreaObjectCode.Staircase:
            case AreaObjectCode.CastleStairs:
            case AreaObjectCode.CastleRectangularCeilingTiles:
            case AreaObjectCode.CastleFloorRightEdge:
            case AreaObjectCode.CastleFloorLeftEdge:
            case AreaObjectCode.CastleFloorLeftWall:
            case AreaObjectCode.CastleFloorRightWall:
            case AreaObjectCode.VerticalSeaBlocks:
            case AreaObjectCode.ExtendableJPipe:
            case AreaObjectCode.VerticalBalls:
                return true;

            default:
                return false;
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
            }

            return code.IsExtendableObject() ? 0x0F : 0;
        }

        public static string BaseName(this AreaObjectCode code)
        {
            return code switch
            {
                AreaObjectCode.QuestionBlockPowerup => "Question Block (Powerup)",
                AreaObjectCode.QuestionBlockCoin => "Question Block (Coin)",
                AreaObjectCode.HiddenBlockCoin => "Hidden Block (Coin)",
                AreaObjectCode.HiddenBlock1UP => "Hidden Block (1UP)",
                AreaObjectCode.BrickPowerup => "Brick (Powerup)",
                AreaObjectCode.BrickBeanstalk => "Brick (Beanstalk)",
                AreaObjectCode.BrickStar => "Brick (Star)",
                AreaObjectCode.Brick10Coins => "Brick (10 Coins)",
                AreaObjectCode.Brick1UP => "Brick (1UP)",
                AreaObjectCode.SidewaysPipe => "Sideways Pipe Cap",
                AreaObjectCode.UsedBlock => "Used Block",
                AreaObjectCode.SpringBoard => "Spring Board",
                AreaObjectCode.JPipe => "J-Pipe",
                AreaObjectCode.AltJPipe => "J-Pipe",
                AreaObjectCode.FlagPole => "Flag Pole",
                AreaObjectCode.AltFlagPole => "Flag Pole",
                AreaObjectCode.Empty => "Nothing",
                AreaObjectCode.Empty2 => "Nothing",
                AreaObjectCode.AreaSpecificPlatform => "Area-Specific Platform",
                AreaObjectCode.GreenIsland => "Tree-Top Platform",
                AreaObjectCode.MushroomIsland => "Mushroom Platform",
                AreaObjectCode.Cannon => "Bullet-Bill Shooter",
                AreaObjectCode.CloudGround => "Cloud Ground",
                AreaObjectCode.HorizontalBricks => "Horizontal Bricks",
                AreaObjectCode.HorizontalStones => "Horizontal Blocks",
                AreaObjectCode.HorizontalCoins => "Horizontal Coins",
                AreaObjectCode.VerticalBricks => "Vertical Bricks",
                AreaObjectCode.VerticalStones => "Vertical Blocks",
                AreaObjectCode.UnenterablePipe => "Unenterable Pipe",
                AreaObjectCode.EnterablePipe => "Enterable Pipe",
                AreaObjectCode.Hole => "Hole",
                AreaObjectCode.BalanceHorizontalRope => "Pulley Platforms",
                AreaObjectCode.BridgeV7 => "Rope Bridge (Y=7)",
                AreaObjectCode.BridgeV8 => "Rope Bridge (Y=8)",
                AreaObjectCode.BridgeV10 => "Rope Bridge (Y=10)",
                AreaObjectCode.HoleWithWaterOrLava => "Hole with water or lava",
                AreaObjectCode.HorizontalQuestionBlocksV3 => "Row of Coin Blocks (Y=3)",
                AreaObjectCode.HorizontalQuestionBlocksV7 => "Row of Coin Blocks (Y=7)",
                AreaObjectCode.ScreenJump => "Screen Skip",
                AreaObjectCode.BowserAxe => "Bowser Axe",
                AreaObjectCode.RopeForAxe => "Rope For Axe",
                AreaObjectCode.BowserBridge => "Bowser Bridge",
                AreaObjectCode.ScrollStopWarpZone => "Scroll Stop (Warp Zone)",
                AreaObjectCode.ScrollStop => "Scroll Stop",
                AreaObjectCode.AltScrollStop => "Scroll Stop",
                AreaObjectCode.RedCheepCheepFlying => "Generator: Red flying cheep-cheeps",
                AreaObjectCode.BulletBillGenerator => "Generator: Bullet Bills",
                AreaObjectCode.StopGenerator => "Stop Generator (also stops Lakitus)",
                AreaObjectCode.LoopCommand => "Screen Loop Command",
                AreaObjectCode.BrickAndSceneryChange => "Brick and scenery change",
                AreaObjectCode.ForegroundChange => "Foreground Change",
                AreaObjectCode.RopeForLift => "Rope for platform lifts",
                AreaObjectCode.PulleyRope => "Rope for pulley platforms",
                AreaObjectCode.EmptyTile => "Empty tile",
                AreaObjectCode.Castle => "Castle",
                AreaObjectCode.CastleCeilingCap => "Castle Object: Ceiling Cap Tile",
                AreaObjectCode.Staircase => "Staircase",
                AreaObjectCode.CastleStairs => "Castle Object: Descending Stairs",
                AreaObjectCode.CastleRectangularCeilingTiles => "Castle Object: Rectangular Ceiling Tiles",
                AreaObjectCode.CastleFloorRightEdge => "Castle Object: Right-Facing Wall To Floor",
                AreaObjectCode.CastleFloorLeftEdge => "Castle Object: Left-Facing Wall To Floor",
                AreaObjectCode.CastleFloorLeftWall => "Castle Object: Left-Facing Wall",
                AreaObjectCode.CastleFloorRightWall => "Castle Object: Right-Facing Wall",
                AreaObjectCode.VerticalSeaBlocks => "Vertical Sea Blocks",
                AreaObjectCode.ExtendableJPipe => "Extendable J-Pipe",
                AreaObjectCode.VerticalBalls => "Vertical Climbing Balls",
                _ => $"Unknown object code: ${(int)code:X}",
            };
        }

        public static string BaseName(this AreaSpriteCode code)
        {
            return code switch
            {
                AreaSpriteCode.AreaPointer => "Transition Command",
                AreaSpriteCode.GreenKoopaTroopa => "Koopa Troopa (Green)",
                AreaSpriteCode.RedKoopaTroopa => "Koopa Troopa (Red; Walks off floors)",
                AreaSpriteCode.BuzzyBeetle => "Buzzy Beetle",
                AreaSpriteCode.RedKoopaTroopa2 => "Koopa Troopa (Red; Stays on floors)",
                AreaSpriteCode.GreenKoopaTroopa2 => "Koopa Troopa (Green; Walks in place)",
                AreaSpriteCode.HammerBros => "Hammer Bros.",
                AreaSpriteCode.Goomba => "Goomba",
                AreaSpriteCode.Blooper => "Squid",
                AreaSpriteCode.BulletBill => "Bullet Bill",
                AreaSpriteCode.YellowKoopaParatroopa => "Yellow Koopa Paratroopa (Flies in place)",
                AreaSpriteCode.GreenCheepCheep => "Green Cheep-Cheep",
                AreaSpriteCode.RedCheepCheep => "Red Cheep-Cheep",
                AreaSpriteCode.Podoboo => "Podoboo",
                AreaSpriteCode.PiranhaPlant => "Piranha Plant",
                AreaSpriteCode.GreenKoopaParatroopa => "Green Koopa Paratroopa (Leaping)",
                AreaSpriteCode.RedKoopaParatroopa => "Red Koopa Paratroopa (Flies vertically)",
                AreaSpriteCode.GreenKoopaParatroopa2 => "Green Koopa Paratroopa (Flies horizontally)",
                AreaSpriteCode.Lakitu => "Lakitu",
                AreaSpriteCode.Spiny => "Spiny (undefined walk speed)",
                AreaSpriteCode.RedFlyingCheepCheep => "Red Flying Cheep-Cheep",
                AreaSpriteCode.BowsersFire => "Bowser's Fire (generator)",
                AreaSpriteCode.Fireworks => "Single Firework",
                AreaSpriteCode.BulletBillOrCheepCheeps => "Generator (Bullet Bill or Cheep-Cheeps)",
                AreaSpriteCode.FireBarClockwise => "Fire Bar (Clockwise)",
                AreaSpriteCode.FastFireBarClockwise => "Fire Bar (Fast; Clockwise)",
                AreaSpriteCode.FireBarCounterClockwise => "Fire Bar (Counter-Clockwise)",
                AreaSpriteCode.FastFireBarCounterClockwise => "Fire Bar (Fast; Counter-Clockwise)",
                AreaSpriteCode.LongFireBarClockwise => "Long Fire Bar (Fast; Clockwise)",
                AreaSpriteCode.BalanceRopeLift => "Rope for Lift Balance",
                AreaSpriteCode.LiftDownThenUp => "Lift (Down, then up)",
                AreaSpriteCode.LiftUp => "Lift (Up)",
                AreaSpriteCode.LiftDown => "Lift (Down)",
                AreaSpriteCode.LiftLeftThenRight => "Lift (Left, then right)",
                AreaSpriteCode.LiftFalling => "Lift (Falling)",
                AreaSpriteCode.LiftRight => "Lift (Right)",
                AreaSpriteCode.ShortLiftUp => "Short Lift (Up)",
                AreaSpriteCode.ShortLiftDown => "Short Lift (Down)",
                AreaSpriteCode.Bowser => "Bowser: King of the Koopa",
                AreaSpriteCode.WarpZoneCommand => "Command: Load Warp Zone",
                AreaSpriteCode.ToadOrPrincess => "Toad or Princess",
                AreaSpriteCode.TwoGoombasY10 => "Two Goombas (Y=10)",
                AreaSpriteCode.ThreeGoombasY10 => "Three Goombas (Y=10)",
                AreaSpriteCode.TwoGoombasY6 => "Two Goombas (Y=6)",
                AreaSpriteCode.ThreeGoombasY6 => "Three Goombas (Y=6)",
                AreaSpriteCode.TwoGreenKoopasY10 => "Two Green Koopa Troopas (Y=10)",
                AreaSpriteCode.ThreeGreenKoopasY10 => "Three Green Koopa Troopas (Y=10)",
                AreaSpriteCode.TwoGreenKoopasY6 => "Two Green Koopa Troopas (Y=6)",
                AreaSpriteCode.ThreeGreenKoopasY6 => "Three Green Koopa Troopas (Y=6)",
                AreaSpriteCode.ScreenJump => "Page Skip",
                _ => "Unknown code",
            };
        }
    }
}
