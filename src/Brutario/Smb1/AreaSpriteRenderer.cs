// <copyright file="AreaSpriteRenderer.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed under GNU
//     Affero General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Smb1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static System.Math;

    public class AreaSpriteRenderer
    {
        public AreaSpriteRenderer(GameData gameData)
        {
            GameData = gameData
                ?? throw new ArgumentNullException(nameof(gameData));

            Commands = new Dictionary<AreaSpriteCode, SpriteCallback>()
            {
                { AreaSpriteCode.AreaPointer, AreaPointer },
                { AreaSpriteCode.GreenKoopaTroopa, GreenKoopaTroopa },
                { AreaSpriteCode.RedKoopaTroopa, RedKoopaTroopa },
                { AreaSpriteCode.BuzzyBeetle, BuzzyBeetle },
                { AreaSpriteCode.RedKoopaTroopa2, RedKoopaTroopa },
                { AreaSpriteCode.GreenKoopaTroopa2, GreenKoopaTroopa },
                { AreaSpriteCode.HammerBros, HammerBros },
                { AreaSpriteCode.Goomba, Goomba },
                { AreaSpriteCode.Blooper, Blooper },
                { AreaSpriteCode.BulletBill, BulletBill },
                { AreaSpriteCode.YellowKoopaParatroopa, GoldKoopaParaTroopa },
                { AreaSpriteCode.GreenCheepCheep, GreenCheepCheep },
                { AreaSpriteCode.RedCheepCheep, RedCheepCheep },
                { AreaSpriteCode.Podoboo, Podoboo },
                { AreaSpriteCode.PiranhaPlant, PiranhaPlant },
                { AreaSpriteCode.GreenKoopaParatroopa, GreenKoopaParaTroopa },
                { AreaSpriteCode.RedKoopaParatroopa, RedKoopaParaTroopa },
                { AreaSpriteCode.GreenKoopaParatroopa2, GreenKoopaParaTroopa2 },
                { AreaSpriteCode.Lakitu, Lakitu },
                { AreaSpriteCode.Spiny, Spiny },
                { AreaSpriteCode.RedFlyingCheepCheep, RedFlyingCheepCheep },
                { AreaSpriteCode.BowsersFire, BowserFire },
                { AreaSpriteCode.Fireworks, Firework },
                { AreaSpriteCode.BulletBillOrCheepCheeps, BulletBill },
                { AreaSpriteCode.FireBarClockwise, FireBarClockwise },
                { AreaSpriteCode.FastFireBarClockwise, FireBarClockwiseFast },
                { AreaSpriteCode.FireBarCounterClockwise, FireBarCounterClockwise },
                { AreaSpriteCode.FastFireBarCounterClockwise, FireBarCounterClockwiseFast },
                { AreaSpriteCode.LongFireBarClockwise, LongFireBarClockwiseFast},
                { AreaSpriteCode.BalanceRopeLift, BalanceRopeLift },
                { AreaSpriteCode.LiftDownThenUp,  StationaryLift },
                { AreaSpriteCode.LiftUp, LiftUp },
                { AreaSpriteCode.LiftDown, LiftDown },
                { AreaSpriteCode.LiftLeftThenRight, StationaryLift },
                { AreaSpriteCode.LiftFalling, StationaryLift },
                { AreaSpriteCode.LiftRight, StationaryLift },
                { AreaSpriteCode.ShortLiftUp, ShortLiftUp },
                { AreaSpriteCode.ShortLiftDown, ShortLiftDown },
                { AreaSpriteCode.Bowser, Bowser },
                { AreaSpriteCode.WarpZoneCommand, WarpZone },
                { AreaSpriteCode.ToadOrPrincess, Toad },
                { AreaSpriteCode.TwoGoombasY10, TwoGoombasY10 },
                { AreaSpriteCode.ThreeGoombasY10, ThreeGoombasY10 },
                { AreaSpriteCode.TwoGoombasY6, TwoGoombasY6 },
                { AreaSpriteCode.ThreeGoombasY6, ThreeGoombasY6 },
                { AreaSpriteCode.TwoGreenKoopasY10, TwoGreenKoopasY10 },
                { AreaSpriteCode.ThreeGreenKoopasY10, ThreeGreenKoopasY10 },
                { AreaSpriteCode.TwoGreenKoopasY6, TwoGreenKoopasY6 },
                { AreaSpriteCode.ThreeGreenKoopasY6, ThreeGreenKoopasY6 },
                { AreaSpriteCode.ScreenSkip, SpriteScreenSkip },
            };

            ObjectCommands = new Dictionary<AreaObjectCode, SpriteCallback>()
            {
                { AreaObjectCode.QuestionBlockPowerup, Powerup },
                { AreaObjectCode.BrickPowerup, Powerup },
                { AreaObjectCode.Brick1UP, Brick1Up },
                { AreaObjectCode.Brick10Coins, Brick10Coins },
                { AreaObjectCode.HiddenBlock1UP, HiddenBlock1UP },
                { AreaObjectCode.EnterablePipe, PipePiranhaPlant },
                { AreaObjectCode.UnenterablePipe, PipePiranhaPlant },
                { AreaObjectCode.SpringBoard, SpringBoard },
                { AreaObjectCode.FlagPole, FlagPole },
                { AreaObjectCode.AltFlagPole, FlagPole },
                { AreaObjectCode.BrickBeanstalk, BeanStalk },
                { AreaObjectCode.HiddenBlockCoin, Coin },
                { AreaObjectCode.BrickStar, Star },
                { AreaObjectCode.BulletBillGenerator, BulletBill },
                { AreaObjectCode.RedCheepCheepFlying, RedFlyingCheepCheep },
                { AreaObjectCode.StopGenerator, StopGenerator },
                { AreaObjectCode.LoopCommand, Loop },
                { AreaObjectCode.ScrollStop, ScrollStop },
                { AreaObjectCode.ScrollStopWarpZone, ScrollStopWarpZone },
                { AreaObjectCode.AltScrollStop, ScrollStop },
                { AreaObjectCode.ScreenSkip, ObjectScreenSkip },
            };
        }

        private delegate IEnumerable<Sprite> SpriteCallback(int x, int y, int frame);

        private GameData GameData
        {
            get;
        }

        private Dictionary<AreaSpriteCode, SpriteCallback> Commands
        {
            get;
        }

        private Dictionary<AreaObjectCode, SpriteCallback> ObjectCommands
        {
            get;
        }

        public IEnumerable<Sprite> GetSprites(
            IEnumerable<AreaSpriteCommand> areaSpriteCommands,
            IEnumerable<AreaObjectCommand> areaObjectData,
            int frame,
            AreaType areaType,
            bool showPipePiranhaPlants)
        {
            return Enumerable.Concat(
                GetSprites(areaSpriteCommands, frame),
                GetSprites(areaObjectData, frame, areaType, showPipePiranhaPlants));
        }

        public IEnumerable<Sprite> GetSprites(
            IEnumerable<AreaObjectCommand> areaObjectData,
            int frame,
            AreaType areaType,
            bool showPipePiranhaPlants)
        {
            if (areaType == AreaType.Water)
            {
                ObjectCommands[AreaObjectCode.BulletBillGenerator] = RedCheepCheep;
            }
            else
            {
                ObjectCommands[AreaObjectCode.BulletBillGenerator] = BulletBill;
            }

            var screen = 0;
            var screenSkip = false;
            foreach (var command in areaObjectData)
            {
                if (command.ScreenFlag && !screenSkip)
                {
                    screen += 0x10;
                }

                var x = (screen | command.X) << 4;
                var y = (command.Y + 2) << 4;
                screenSkip = command.Code == AreaObjectCode.ScreenSkip;
                if (screenSkip)
                {
                    screen = command.BaseCommand << 4;
                    screenSkip = true;
                }

                if (!showPipePiranhaPlants && (
                    command.Code == AreaObjectCode.EnterablePipe
                    || command.Code == AreaObjectCode.UnenterablePipe))
                {
                    continue;
                }

                if (ObjectCommands.TryGetValue(command.Code, out var getSprites))
                {
                    foreach (var sprite in getSprites(x, y, frame))
                    {
                        yield return sprite;
                    }
                }
            }
        }

        public IEnumerable<Sprite> GetPlayerSprite(
            int x,
            int y,
            Player player,
            PlayerState state,
            int frame)
        {
            var tile = new SpriteTile(
                GfxData.MarioPixelDataStartIndex / 0x40,
                0x0F,
                9,
                false,
                false);

            tile.TileIndex += frame * 8;

            if (player == Player.Luigi)
            {
                tile.TileIndex += 0x100;
            }

            if (state == PlayerState.Small)
            {
                tile.TileIndex += 0x60;
            }

            for (var i = 0; i < 8; i++)
            {
                yield return new Sprite(
                    x + ((i & 1) << 3),
                    y + ((i & 6) << 2),
                    tile);
                tile.TileIndex++;
            }
        }

        public IEnumerable<Sprite> GetSprites(
            IEnumerable<AreaSpriteCommand> areaSpriteCommands,
            int frame)
        {
            var screen = 0;
            var screenSkip = false;
            var lastAreaPointerX = -0x80;
            var areaPointerOffset = 0;

            foreach (var command in areaSpriteCommands)
            {
                if (command.ScreenFlag && !screenSkip)
                {
                    screen += 0x10;
                }

                var x = (screen | command.X) << 4;
                var y = (command.Y + 1) << 4;
                if (command.Code == AreaSpriteCode.AreaPointer)
                {
                    if (x - lastAreaPointerX < 0x80)
                    {
                        areaPointerOffset += 8;
                        y += areaPointerOffset;
                    }
                    else
                    {
                        areaPointerOffset = 0;
                    }
                    lastAreaPointerX = x;
                }
                screenSkip = command.Code == AreaSpriteCode.ScreenSkip;
                if (screenSkip)
                {
                    screen = command.BaseCommand << 4;
                }

                if (Commands.TryGetValue(command.Code, out var getSprites))
                {
                    foreach (var sprite in getSprites(x, y, frame))
                    {
                        var result = sprite;
                        if (command.HardWorldFlag)
                        {
                            result.TileProperties |= TileProperties.Invert;
                        }
                        yield return result;
                    }
                }
            }
        }

        private IEnumerable<Sprite> GreenKoopaTroopa(int x, int y, int frame)
        {
            return KoopaTroopa(x, y, 5, frame);
        }

        private IEnumerable<Sprite> RedKoopaTroopa(int x, int y, int frame)
        {
            return KoopaTroopa(x, y, 6, frame);
        }

        private IEnumerable<Sprite> KoopaTroopa(int x, int y, int palette, int frame)
        {
            var index = (((frame + (x >> 1)) & 8) >> 3) * 5;
            y -= 7;
            var tile = new ChrTile(0xA0 + index, palette, LayerPriority.Priority2, TileFlip.Horizontal);
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            y += 8;
            tile.TileIndex++;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            y += 8;
            tile.TileIndex++;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
        }

        private IEnumerable<Sprite> GreenKoopaParaTroopa(int x, int y, int frame)
        {
            return KoopaParaTroopa(x, y, 5, frame / 2);
        }

        private IEnumerable<Sprite> GreenKoopaParaTroopa2(int x, int y, int frame)
        {
            return KoopaParaTroopa(x, y, 5, frame);
        }

        private IEnumerable<Sprite> RedKoopaParaTroopa(int x, int y, int frame)
        {
            return KoopaParaTroopa(x, y, 6, frame);
        }

        private IEnumerable<Sprite> GoldKoopaParaTroopa(int x, int y, int frame)
        {
            return KoopaParaTroopa(x, y, 7, frame);
        }

        private IEnumerable<Sprite> KoopaParaTroopa(int x, int y, int palette, int frame)
        {
            y -= 7;
            var index = ((frame + (x >> 1)) & 8) >> 3;
            var tile = new ChrTile(0xA0 + (index * 5), palette, LayerPriority.Priority2, TileFlip.Horizontal);
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex = 0x69 + (index << 1);
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            y += 8;
            tile.TileIndex = 0xA2 + (index * 5);
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex = 0x6A + (index * 2);
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            y += 8;
            tile.TileIndex = 0xA3 + (index * 5);
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
        }

        private IEnumerable<Sprite> Text(int x, int y, int palette, string text)
        {
            var tile = new SpriteTile(0, palette, 11, false, false);
            foreach (var c in text.ToUpper())
            {
                if (c >= '0' && c <= '9')
                {
                    tile.TileIndex = c - '0';
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    tile.TileIndex = c - 'A' + 10;
                }
                else if (c == '-')
                {
                    tile.TileIndex = 0x28;
                }
                else if (c == '!')
                {
                    tile.TileIndex = 0x2B;
                }
                else if (c == '#')
                {
                    tile.TileIndex = 0x29;
                }
                else if (c == ' ')
                {
                    tile.TileIndex = 0x24;
                }

                yield return new Sprite(x, y, tile);
                x += 8;
            }
        }

        private IEnumerable<Sprite> Lakitu(int x, int y, int frame)
        {
            y -= 7;
            var tile = new ChrTile(0xB8, 5, LayerPriority.Priority2, 0);
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            y += 8;
            tile.TileIndex++;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            y += 8;
            tile.TileIndex++;
            tile.XFlipped = true;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.XFlipped = false;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
        }

        private IEnumerable<Sprite> HammerBros(int x, int y, int frame)
        {
            y -= 7;

            const int cycle = 96;
            const int distance = 8;
            var offset = frame % cycle;
            if (offset > cycle / 2)
            {
                x += (int)(distance * ((offset - (3 * (cycle / 4.0))) / (cycle / 2.0)));
            }
            else
            {
                x += (int)(distance * ((cycle / 4.0) - offset) / (cycle / 2.0));
            }

            var tile = new ChrTile(0x7C, 5, LayerPriority.Priority2, TileFlip.Horizontal);
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            if (((frame + (x >> 1)) & 8) >> 3 == 0)
            {
                y += 8;
                tile.TileIndex = 0x88;
                yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

                tile.TileIndex++;
                yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

                y += 8;
                tile.TileIndex++;
                yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
            }
            else
            {
                y += 8;
                tile.TileIndex = 0x8C;
                yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

                tile.TileIndex = 0xD1;
                yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

                y += 8;
                tile.TileIndex = 0xD2;
                yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
            }

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
        }

        private IEnumerable<Sprite> Goomba(int x, int y, int frame)
        {
            y++;
            var tile = new ChrTile(0xC6, 1, LayerPriority.Priority2, 0);
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            y += 8;
            if (((frame + (x >> 1)) & 8) >> 3 == 0)
            {
                tile.TileIndex++;
                yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

                tile.TileIndex++;
                yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
            }
            else
            {
                tile.TileIndex += 2;
                tile.XFlipped = true;
                yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

                tile.TileIndex--;
                yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
            }
        }

        private IEnumerable<Sprite> BuzzyBeetle(int x, int y, int frame)
        {
            y += 2;
            var index = ((frame + (x << 1)) & 8) >> 1;
            var tile = new ChrTile(0xAA + index, 5, LayerPriority.Priority2, TileFlip.Horizontal);

            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            y += 8;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
        }

        private IEnumerable<Sprite> Spiny(int x, int y, int frame)
        {
            y++;
            var index = ((frame + (x << 1)) & 16) >> 2;
            var tile = new ChrTile(0x96 + index, 6, LayerPriority.Priority2, TileFlip.Horizontal);

            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            y += 8;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
        }

        private IEnumerable<Sprite> Firework(int x, int y, int frame)
        {
            y++;
            frame = (frame + x + y) % 90;
            if (frame > 20)
            {
                //yield break;
            }

            // Maybe set up a click to explode?
            var index = 1;// frame < 2 ? 2 : frame < 4 ? 1 : 0;

            var tile = new ChrTile(0xCA + index, 4, LayerPriority.Priority2, 0);

            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.XFlipped = true;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.YFlipped = true;
            yield return new Sprite(x + 8, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.XFlipped = false;
            yield return new Sprite(x, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
        }

        private IEnumerable<Sprite> Toad(int x, int y, int frame)
        {
            y -= 7;
            var tile = new ChrTile(0xCD, 6, LayerPriority.Priority2, 0);
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.XFlipped = true;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            y += 8;
            tile.TileIndex++;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.XFlipped = false;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            y += 8;
            tile.TileIndex++;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.XFlipped = true;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
        }

        private IEnumerable<Sprite> Bowser(int x, int y, int frame)
        {
            y -= 0x17;
            var tile = new ChrTile(0x1C5, 0, LayerPriority.Priority2, TileFlip.Horizontal);
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex--;
            yield return new Sprite(x + 16, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            y += 8;
            tile.TileIndex = 0x1EF;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex--;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            y += 8;
            tile.TileIndex += 0x10;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            y -= 8;
            tile.TileIndex = 0x1C1;
            yield return new Sprite(x + 16, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex--;
            yield return new Sprite(x + 24, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            y += 8;
            tile.TileIndex += 0x10;
            yield return new Sprite(x + 24, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x + 16, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex += 0x12;
            for (var i = 0; i < 2; i++)
            {
                y += 8;
                for (var j = 0; j < 4; j++)
                {
                    yield return new Sprite(x + (j << 3), y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
                    tile.TileIndex--;
                }

                tile.TileIndex += 0x10 + 4;
            }
        }

        private IEnumerable<Sprite> BowserFire(int x, int y, int frame)
        {
            y++;
            var index = (frame + (x << 1)) / 12 % 3;
            var offsets = new int[] { 0, 3, 0x20 };

            var tile = new ChrTile(0x186 + offsets[index], 0, LayerPriority.Priority2, 0);
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x + 16, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex += 0x10;
            y += 8;
            yield return new Sprite(x + 16, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex--;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex--;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
        }

        private IEnumerable<Sprite> GreenCheepCheep(int x, int y, int frame)
        {
            return CheepCheep(x, y, 5, frame);
        }

        private IEnumerable<Sprite> RedFlyingCheepCheep(int x, int y, int frame)
        {
            return CheepCheep(x, y, 6, (int)(1.5 * frame));
        }

        private IEnumerable<Sprite> StopGenerator(int x, int y, int frame)
        {
            return Text(x, y + 8, 2, "Stop Generator");
        }

        private IEnumerable<Sprite> Loop(int x, int y, int frame)
        {
            return Text(x, y + 8, 2, "Loop");
        }

        private IEnumerable<Sprite> ScrollStop(int x, int y, int frame)
        {
            return Text(x, y, 1, "Scroll Stop");
        }

        private IEnumerable<Sprite> ObjectScreenSkip(int x, int y, int frame)
        {
            return Text(x, y, 2, "Page Skip");
        }

        private IEnumerable<Sprite> SpriteScreenSkip(int x, int y, int frame)
        {
            return Text(x, y + 8, 1, "Page Skip");
        }

        private IEnumerable<Sprite> AreaPointer(int x, int y, int frame)
        {
            return Text(x, y, 1, "Area Pointer");
        }

        private IEnumerable<Sprite> WarpZone(int x, int y, int frame)
        {
            return Text(x, y, 1, "Warpzone");
        }

        private IEnumerable<Sprite> ScrollStopWarpZone(int x, int y, int frame)
        {
            return Text(x, y, 1, "Scroll Stop Warpzone");
        }

        private IEnumerable<Sprite> RedCheepCheep(int x, int y, int frame)
        {
            return CheepCheep(x, y, 6, frame);
        }

        private IEnumerable<Sprite> CheepCheep(int x, int y, int palette, int frame)
        {
            var index = ((frame + (x << 1)) & 0x10) >> 4;
            var tile = new ChrTile(0xB2 + (index * 4), palette, LayerPriority.Priority2, TileFlip.Horizontal);

            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex = 0xB3;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex += 1 + (index * 3);
            y += 8;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex = 0xB5;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
        }

        private IEnumerable<Sprite> Podoboo(int x, int y, int frame)
        {
            int index = 0;// (((frame + x + y) >> 2) % 6) & ~1;
            var tile = new ChrTile(0x162 + index, 2, LayerPriority.Priority2, 0);

            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex += 0x10;
            yield return new Sprite(x + 8, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex--;
            yield return new Sprite(x, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
        }

        private IEnumerable<Sprite> FireBarClockwise(int x, int y, int frame)
        {
            return FireBar(x, y, frame, 0x70, 6);
        }

        private IEnumerable<Sprite> FireBarCounterClockwise(int x, int y, int frame)
        {
            return FireBar(x, y, frame, -0x70, 6);
        }

        private IEnumerable<Sprite> FireBarClockwiseFast(int x, int y, int frame)
        {
            return FireBar(x, y, frame, 0x68, 6);
        }

        private IEnumerable<Sprite> FireBarCounterClockwiseFast(int x, int y, int frame)
        {
            return FireBar(x, y, frame, -0x68, 6);
        }

        private IEnumerable<Sprite> LongFireBarClockwiseFast(int x, int y, int frame)
        {
            return FireBar(x, y, frame, 0x68, 12);
        }

        private IEnumerable<Sprite> FireBar(int x, int y, int frame, int rate, int size)
        {
            var tile = new ChrTile(
                0xBE + ((frame & 4) >> 2),
                4,
                LayerPriority.Priority2,
                (TileFlip)((frame & 0x0C) >> 2));

            x += 4;
            y -= 12;

            var angle = (((frame & ~3) + x + y) >> 1) % rate * 2 * PI / rate;
            for (var i = 0; i < size; i++)
            {
                yield return new Sprite(
                    x + (int)(8 * i * Cos(angle)),
                    y + (int)(8 * i * Sin(angle)),
                    new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
            }
        }

        private IEnumerable<Sprite> TwoGoombasY10(int x, int y, int frame)
        {
            return Goombas(x, 2, 0xC0, frame);
        }

        private IEnumerable<Sprite> ThreeGoombasY10(int x, int y, int frame)
        {
            return Goombas(x, 3, 0xC0, frame);
        }

        private IEnumerable<Sprite> TwoGoombasY6(int x, int y, int frame)
        {
            return Goombas(x, 2, 0x80, frame);
        }

        private IEnumerable<Sprite> ThreeGoombasY6(int x, int y, int frame)
        {
            return Goombas(x, 3, 0x80, frame);
        }

        private IEnumerable<Sprite> TwoGreenKoopasY10(int x, int y, int frame)
        {
            return GreenKoopas(x, 2, 0xC0, frame);
        }

        private IEnumerable<Sprite> ThreeGreenKoopasY10(int x, int y, int frame)
        {
            return GreenKoopas(x, 3, 0xC0, frame);
        }

        private IEnumerable<Sprite> TwoGreenKoopasY6(int x, int y, int frame)
        {
            return GreenKoopas(x, 2, 0x80, frame);
        }

        private IEnumerable<Sprite> ThreeGreenKoopasY6(int x, int y, int frame)
        {
            return GreenKoopas(x, 3, 0x80, frame);
        }

        private IEnumerable<Sprite> Goombas(int x, int count, int y, int frame)
        {
            x -= 0x30;
            for (var i = 0; i < count; i++, x += 0x18)
            {
                foreach (var sprite in Goomba(x, y, frame))
                {
                    yield return sprite;
                }
            }
        }

        private IEnumerable<Sprite> GreenKoopas(int x, int count, int y, int frame)
        {
            x -= 0x30;
            for (var i = 0; i < count; i++, x += 0x18)
            {
                foreach (var sprite in GreenKoopaTroopa(x, y, frame))
                {
                    yield return sprite;
                }
            }
        }

        private IEnumerable<Sprite> StationaryLift(int x, int y, int frame)
        {
            return Lift(x, y, 6);
        }

        private IEnumerable<Sprite> LiftDown(int x, int y, int frame)
        {
            var tile = new ChrTile(0x87, 6, LayerPriority.Priority2, 0);
            y += ((frame << 6) / 75) + (x >> 4);
            y &= 0xFF;
            x += 8;
            return Lift(x, y, 6);
        }

        private IEnumerable<Sprite> LiftUp(int x, int y, int frame)
        {
            y -= ((frame << 6) / 75) + (x >> 4);
            y &= 0xFF;
            x += 8;
            return Lift(x, y, 6);
        }

        private IEnumerable<Sprite> Lift(int x, int y, int size)
        {
            var tile = new ChrTile(0x87, 6, LayerPriority.Priority2, 0);
            for (var i = 0; i <= 8 * size; i += 8)
            {
                yield return new Sprite(x + i, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
            }
        }

        private IEnumerable<Sprite> ShortLiftDown(int x, int y, int frame)
        {
            y += ((frame << 6) / 75) + (x >> 4);
            y &= 0xFF;
            x += 8;
            return Lift(x, y, 3);
        }

        private IEnumerable<Sprite> ShortLiftUp(int x, int y, int frame)
        {
            y -= ((frame << 6) / 75) + (x >> 4);
            y &= 0xFF;
            x += 8;
            return Lift(x, y, 3);
        }

        private IEnumerable<Sprite> BalanceRopeLift(int x, int y, int frame)
        {
            x -= 4;
            y -= 0x10;
            return Lift(x, y, 6);
        }

        private IEnumerable<Sprite> Powerup(int x, int y, int frame)
        {
            return Mushroom(x, y, 6);
        }

        private IEnumerable<Sprite> Brick1Up(int x, int y, int frame)
        {
            return Mushroom(x, y, 5);
        }

        private IEnumerable<Sprite> HiddenBlock1UP(int x, int y, int frame)
        {
            foreach (var sprite in Mushroom(x, y, 5))
            {
                yield return sprite;
            }

            foreach (var sprite in HiddenQuestionBlock(x, y, frame))
            {
                yield return sprite;
            }
        }

        private IEnumerable<Sprite> HiddenQuestionBlock(int x, int y, int frame)
        {
            var tile = GameData.Map16Tiles[0xE7];
            yield return new Sprite(x, y, new SpriteTile(tile[0], 0, 0), TileProperties.Transparent);
            yield return new Sprite(x, y + 8, new SpriteTile(tile[2], 0, 0), TileProperties.Transparent);
            yield return new Sprite(x + 8, y, new SpriteTile(tile[1], 0, 0), TileProperties.Transparent);
            yield return new Sprite(x + 8, y + 8, new SpriteTile(tile[3], 0, 0), TileProperties.Transparent);
        }

        private IEnumerable<Sprite> Mushroom(int x, int y, int palette)
        {
            var tile = new ChrTile(0xE9, palette, LayerPriority.Priority2, 0);
            yield return new Sprite(x, y - 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x + 8, y - 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex = 0x78;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40), TileProperties.Transparent);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40), TileProperties.Transparent);
        }

        private IEnumerable<Sprite> Coin(int x, int y, int frame)
        {
            var tile = new ChrTile(0x28, 2, LayerPriority.Priority2, 0);
            yield return new Sprite(x, y - 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40), TileProperties.Transparent);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y - 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40), TileProperties.Transparent);

            tile.TileIndex = 0x38;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40), TileProperties.Transparent);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40), TileProperties.Transparent);

            foreach (var sprite in HiddenQuestionBlock(x, y, frame))
            {
                yield return sprite;
            }
        }

        private IEnumerable<Sprite> BulletBill(int x, int y, int frame)
        {
            var tile = new ChrTile(0x65, 5, LayerPriority.Priority2, TileFlip.Horizontal);
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex--;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex += 0x10;
            yield return new Sprite(x + 8, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
        }

        private IEnumerable<Sprite> Blooper(int x, int y, int frame)
        {
            const int descentTime = 60;
            const int ascentTime = 80;
            const int totalTime = ascentTime + descentTime;
            const int distance = 12;
            int relativeFrame = (frame + x + y) % totalTime;
            bool isAscending = relativeFrame >= descentTime;
            if (isAscending)
            {
                relativeFrame -= descentTime;
                y += (int)(distance * ((ascentTime - relativeFrame) / ((float)ascentTime)));
            }
            else
            {
                y += (int)(distance * (relativeFrame / (float)descentTime));
            }

            y++;
            var tile = new ChrTile(0xDC, 2, LayerPriority.Priority2, 0);
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.XFlipped = true;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            y += 8;
            if (isAscending)
            {
                tile.TileIndex++;
                tile.XFlipped = false;
                yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

                tile.XFlipped = true;
                yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

                y += 8;
                tile.TileIndex++;
                tile.XFlipped = false;
                yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

                tile.XFlipped = true;
                yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
            }
            else
            {
                tile.TileIndex = 0xDF;
                tile.XFlipped = false;
                yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

                tile.XFlipped = true;
                yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
            }
        }

        private IEnumerable<Sprite> Star(int x, int y, int frame)
        {
            var tile = new ChrTile(0x8D, 2, LayerPriority.Priority2, 0);
            yield return new Sprite(x, y - 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.XFlipped = true;
            yield return new Sprite(x + 8, y - 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex = 0xE4;
            tile.XFlipped = false;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40), TileProperties.Transparent);

            tile.XFlipped = true;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40), TileProperties.Transparent);
        }

        private IEnumerable<Sprite> PipePiranhaPlant(int x, int y, int frame)
        {
            return PiranhaPlant(x + 8, y, frame);
        }

        private IEnumerable<Sprite> PiranhaPlant(int x, int y, int frame)
        {
            var index = ((frame + (x >> 1)) & 0x10) == 0 ? 0xE5 : 0xEC;
            y -= PirhanaPlantOffset(frame - (x >> 2));

            var tile = new ChrTile(index, 5, 0, 0);
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex--;
            tile.TileFlip = TileFlip.Horizontal;
            x += 8;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            y += 0x10;
            tile.TileIndex = 0xEB;
            tile.PaletteIndex = 2;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileFlip = 0;
            yield return new Sprite(x - 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
        }

        private int PirhanaPlantOffset(int cycle)
        {
            cycle &= 0xFF;

            if (cycle < 0x68)
            {
                return 0;
            }

            if (cycle < 0x80)
            {
                return cycle - 0x68;
            }

            if (cycle < 0xE8)
            {
                return 0x18;
            }

            return 0x18 - (cycle - 0xE8);
        }

        private IEnumerable<Sprite> SpringBoard(int x, int y, int frame)
        {
            var tile = new ChrTile(0xF2, 6, LayerPriority.Priority2, 0);
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileFlip = TileFlip.Horizontal;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileFlip = TileFlip.Veritcal;
            yield return new Sprite(x, y + 0x10, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileFlip = TileFlip.Both;
            yield return new Sprite(x + 8, y + 0x10, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            tile.TileFlip = 0;
            yield return new Sprite(x, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileFlip = TileFlip.Horizontal;
            yield return new Sprite(x + 8, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
        }

        private IEnumerable<Sprite> FlagPole(int x, int y, int frame)
        {
            y = 0x30;
            x += 8;

            var index = 4 - (((frame + (x >> 1)) / 12 % 3) << 1);

            var tile = new ChrTile(0x120 + index, 5, LayerPriority.Priority2, 0);
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex += 0x0F;
            yield return new Sprite(x, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x + 8, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            x += 0x60;
            y += 0x40;
            index = ((frame + (x >> 1)) >> 2) & 6;
            tile = new ChrTile(0x104 + index, 5, LayerPriority.Priority0, 0);
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex += 0x0F;
            yield return new Sprite(x, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x + 8, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
        }

        private IEnumerable<Sprite> BeanStalk(int x, int y, int frame)
        {
            var tile = new ChrTile(0x12C, 5, LayerPriority.Priority3, 0);
            for (y -= 0x10; y > 0x10; y -= 0x10)
            {
                tile.TileIndex = 0x12C;
                yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

                tile.TileIndex++;
                yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

                tile.TileIndex += 0x0F;
                yield return new Sprite(x, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

                tile.TileIndex++;
                yield return new Sprite(x + 8, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
            }

            tile = new ChrTile(0x12A, 5, LayerPriority.Priority3, 0);
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex += 0x0F;
            yield return new Sprite(x, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x + 8, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
        }

        private IEnumerable<Sprite> Brick10Coins(int x, int y, int frame)
        {
            x -= 4;
            y -= 8;
            var tile = new ChrTile(0x2C, 2, LayerPriority.Priority3, 0);
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex += 0x0F;
            yield return new Sprite(x, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x + 8, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex = 0x2E;
            x += 8;
            yield return new Sprite(x, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex += 0x0F;
            yield return new Sprite(x, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));

            tile.TileIndex++;
            yield return new Sprite(x + 8, y + 8, new SpriteTile(tile, GfxData.SpritePixelDataStartIndex / 0x40));
        }
    }
}
