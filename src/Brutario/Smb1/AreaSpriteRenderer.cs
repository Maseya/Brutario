// <copyright file="AreaSpriteRenderer.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed under
//     GNU Affero General Public License. See LICENSE in project root for full
//     license information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Smb1
{
    using System;
    using System.Collections.Generic;
    using static System.Math;

    public class AreaSpriteRenderer
    {
        public AreaSpriteRenderer()
        {
            Commands = new Dictionary<AreaSpriteCode, SpriteCallback>()
            {
                { AreaSpriteCode.GreenKoopaTroopa, GreenKoopaTroopa },
                { AreaSpriteCode.RedKoopaTroopa, RedKoopaTroopa },
                { AreaSpriteCode.BuzzyBeetle, BuzzyBeetle },
                { AreaSpriteCode.GreenKoopaTroopa2, GreenKoopaTroopa },
                { AreaSpriteCode.RedKoopaTroopa2, RedKoopaTroopa },
                { AreaSpriteCode.Goomba, Goomba },
                { AreaSpriteCode.FireBarClockwise, FireBarClockwise },
                { AreaSpriteCode.FastFireBarClockwise, FireBarClockwiseFast },
                { AreaSpriteCode.FireBarCounterClockwise, FireBarCounterClockwise },
                { AreaSpriteCode.FastFireBarCounterClockwise, FireBarCounterClockwiseFast },
                { AreaSpriteCode.LongFireBarClockwise, LongFireBarClockwiseFast},
                { AreaSpriteCode.LiftDown, LiftDown },
                { AreaSpriteCode.LiftUp, LiftUp },
                { AreaSpriteCode.ShortLiftDown, ShortLiftDown },
                { AreaSpriteCode.ShortLiftUp, ShortLiftUp },
                { AreaSpriteCode.BalanceRopeLift, BalanceRopeLift },
                { AreaSpriteCode.TwoGoombasY10, TwoGoombasY10 },
                { AreaSpriteCode.ThreeGoombasY10, ThreeGoombasY10 },
                { AreaSpriteCode.TwoGoombasY6, TwoGoombasY6 },
                { AreaSpriteCode.ThreeGoombasY6, ThreeGoombasY6 },
                { AreaSpriteCode.TwoGreenKoopasY10, TwoGreenKoopasY10 },
                { AreaSpriteCode.ThreeGreenKoopasY10, ThreeGreenKoopasY10 },
                { AreaSpriteCode.TwoGreenKoopasY6, TwoGreenKoopasY6 },
                { AreaSpriteCode.ThreeGreenKoopasY6, ThreeGreenKoopasY6 },
            };

            ObjectCommands = new Dictionary<AreaObjectCode, SpriteCallback>()
            {
                { AreaObjectCode.QuestionBlockPowerup, Powerup },
                { AreaObjectCode.BrickPowerup, Powerup },
                { AreaObjectCode.Brick1UP, Brick1Up },
                { AreaObjectCode.Brick10Coins, Brick10Coins },
                { AreaObjectCode.HiddenBlock1UP, Brick1Up },
                { AreaObjectCode.EnterablePipe, PiranhaPlant },
                { AreaObjectCode.UnenterablePipe, PiranhaPlant },
                { AreaObjectCode.SpringBoard, SpringBoard },
                { AreaObjectCode.FlagPole, FlagPole },
                { AreaObjectCode.AltFlagPole, FlagPole },
                { AreaObjectCode.BrickBeanstalk, BeanStalk },
                { AreaObjectCode.HiddenBlockCoin, Coin },
                { AreaObjectCode.BrickStar, Star },
            };
        }

        private delegate IEnumerable<Sprite> SpriteCallback(int x, int y, int frame);

        private Dictionary<AreaSpriteCode, SpriteCallback> Commands
        {
            get;
        }

        private Dictionary<AreaObjectCode, SpriteCallback> ObjectCommands
        {
            get;
        }

        public List<Sprite> GetSprites(
            IEnumerable<AreaSpriteCommand> areaSpriteCommands,
            IEnumerable<AreaObjectCommand> areaObjectData,
            int frame,
            bool showPipePiranhaPlants)
        {
            var result = new List<Sprite>();
            result.AddRange(GetSprites(areaSpriteCommands, frame));
            result.AddRange(GetSprites(areaObjectData, frame, showPipePiranhaPlants));

            return result;
        }

        public List<Sprite> GetSprites(
            IEnumerable<AreaObjectCommand> areaObjectData,
            int frame,
            bool showPipePiranhaPlants)
        {
            var result = new List<Sprite>();
            var screen = 0;
            var screenSkip = false;
            foreach (var command in areaObjectData)
            {
                if (command.ScreenFlag && !screenSkip)
                {
                    screen += 0x10;
                }

                screenSkip = command.Code == AreaObjectCode.ScreenSkip;
                if (screenSkip)
                {
                    screen = command.BaseCommand << 4;
                    screenSkip = true;
                    continue;
                }

                if (!showPipePiranhaPlants && (
                    command.Code == AreaObjectCode.EnterablePipe
                    || command.Code == AreaObjectCode.UnenterablePipe))
                {
                    continue;
                }

                var x = (screen | command.X) << 4;
                var y = (command.Y + 2) << 4;
                if (ObjectCommands.TryGetValue(command.Code, out var getSprites))
                {
                    result.AddRange(getSprites(x, y, frame));
                }
            }

            return result;
        }

        public List<Sprite> GetSprites(
            IEnumerable<AreaSpriteCommand> areaSpriteCommands,
            int frame)
        {
            var result = new List<Sprite>();
            var screen = 0;
            var screenSkip = false;
            foreach (var command in areaSpriteCommands)
            {
                if (command.ScreenFlag && !screenSkip)
                {
                    screen += 0x10;
                }

                screenSkip = command.Code == AreaSpriteCode.ScreenSkip;
                if (screenSkip)
                {
                    screen = command.BaseCommand << 4;
                    screenSkip = true;
                    continue;
                }

                var x = (screen | command.X) << 4;
                var y = (command.Y + 1) << 4;
                if (Commands.TryGetValue(command.Code, out var getSprites))
                {
                    result.AddRange(getSprites(x, y, frame));
                }
            }

            return result;
        }

        private IEnumerable<Sprite> GreenKoopaTroopa(int x, int y, int frame)
        {
            return KoopaTroopa(x, y, 5, frame);
        }

        private IEnumerable<Sprite> RedKoopaTroopa(int x, int y, int frame)
        {
            return KoopaTroopa(x, y, 6, frame);
        }

        private IEnumerable<Sprite> GoldKoopaTroopa(int x, int y, int frame)
        {
            return KoopaTroopa(x, y, 7, frame);
        }

        private IEnumerable<Sprite> KoopaTroopa(int x, int y, int palette, int frame)
        {
            var index = (((frame + (x >> 1)) & 8) >> 3) * 5;
            y -= 7;
            var tile = new ChrTile(0xA0 + index, palette, LayerPriority.Priority2, TileFlip.Horizontal);
            yield return new Sprite(x, y, tile);

            y += 8;
            tile.TileIndex++;
            yield return new Sprite(x + 8, y, tile);

            tile.TileIndex++;
            yield return new Sprite(x, y, tile);

            y += 8;
            tile.TileIndex++;
            yield return new Sprite(x + 8, y, tile);

            tile.TileIndex++;
            yield return new Sprite(x, y, tile);
        }

        private IEnumerable<Sprite> Goomba(int x, int y, int frame)
        {
            y++;
            var tile = new ChrTile(0xC6, 1, LayerPriority.Priority2, 0);
            yield return new Sprite(x, y, tile);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, tile);

            y += 8;
            if (((frame + (x >> 1)) & 8) >> 3 == 0)
            {
                tile.TileIndex++;
                yield return new Sprite(x, y, tile);

                tile.TileIndex++;
                yield return new Sprite(x + 8, y, tile);
            }
            else
            {
                tile.TileIndex += 2;
                tile.XFlipped = true;
                yield return new Sprite(x, y, tile);

                tile.TileIndex--;
                yield return new Sprite(x + 8, y, tile);
            }
        }

        private IEnumerable<Sprite> BuzzyBeetle(int x, int y, int frame)
        {
            y += 2;
            var index = ((frame + (x << 1)) & 8) >> 1;
            var tile = new ChrTile(0xAA + index, 5, LayerPriority.Priority2, TileFlip.Horizontal);

            yield return new Sprite(x + 8, y, tile);

            tile.TileIndex++;
            yield return new Sprite(x, y, tile);

            tile.TileIndex++;
            y += 8;
            yield return new Sprite(x + 8, y, tile);

            tile.TileIndex++;
            yield return new Sprite(x, y, tile);
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
                    tile);
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

        private IEnumerable<Sprite> LiftDown(int x, int y, int frame)
        {
            var tile = new ChrTile(0x87, 7, LayerPriority.Priority2, 0);
            y += ((frame << 6) / 75) + (x >> 4);
            y &= 0xFF;
            x += 8;
            for (var i = 0; i <= 0x30; i += 8)
            {
                yield return new Sprite(x + i, y, tile);
            }
        }

        private IEnumerable<Sprite> LiftUp(int x, int y, int frame)
        {
            var tile = new ChrTile(0x87, 6, LayerPriority.Priority2, 0);
            y -= ((frame << 6) / 75) + (x >> 4);
            y &= 0xFF;
            x += 8;
            for (var i = 0; i <= 0x30; i += 8)
            {
                yield return new Sprite(x + i, y, tile);
            }
        }

        private IEnumerable<Sprite> ShortLiftDown(int x, int y, int frame)
        {
            var tile = new ChrTile(0x87, 7, LayerPriority.Priority2, 0);
            y += ((frame << 6) / 75) + (x >> 4);
            y &= 0xFF;
            x += 8;
            for (var i = 0; i <= 0x18; i += 8)
            {
                yield return new Sprite(x + i, y, tile);
            }
        }

        private IEnumerable<Sprite> ShortLiftUp(int x, int y, int frame)
        {
            var tile = new ChrTile(0x87, 6, LayerPriority.Priority2, 0);
            y -= ((frame << 6) / 75) + (x >> 4);
            y &= 0xFF;
            x += 8;
            for (var i = 0; i <= 0x18; i += 8)
            {
                yield return new Sprite(x + i, y, tile);
            }
        }

        private IEnumerable<Sprite> BalanceRopeLift(int x, int y, int frame)
        {
            var tile = new ChrTile(0x87, 6, LayerPriority.Priority2, 0);
            x -= 4;
            y -= 0x10;
            for (var i = 0; i <= 0x30; i += 8)
            {
                yield return new Sprite(x + i, y, tile);
            }
        }

        private IEnumerable<Sprite> Powerup(int x, int y, int frame)
        {
            return Mushroom(x, y, 6);
        }

        private IEnumerable<Sprite> Brick1Up(int x, int y, int frame)
        {
            return Mushroom(x, y, 5);
        }

        private IEnumerable<Sprite> Mushroom(int x, int y, int palette)
        {
            var tile = new ChrTile(0xE9, palette, LayerPriority.Priority2, 0);
            yield return new Sprite(x, y - 8, tile);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y - 8, tile);

            tile.TileIndex = 0x78;
            yield return new Sprite(x, y, tile, TileProperties.Transparent);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, tile, TileProperties.Transparent);
        }

        private IEnumerable<Sprite> Coin(int x, int y, int frame)
        {
            var tile = new ChrTile(0x28, 2, LayerPriority.Priority2, 0);
            yield return new Sprite(x, y - 8, tile, TileProperties.Transparent);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y - 8, tile, TileProperties.Transparent);

            tile.TileIndex = 0x38;
            yield return new Sprite(x, y, tile, TileProperties.Transparent);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, tile, TileProperties.Transparent);
        }

        private IEnumerable<Sprite> Star(int x, int y, int frame)
        {
            var tile = new ChrTile(0x8D, 2, LayerPriority.Priority2, 0);
            yield return new Sprite(x, y - 8, tile);

            tile.XFlipped = true;
            yield return new Sprite(x + 8, y - 8, tile);

            tile.TileIndex = 0xE4;
            tile.XFlipped = false;
            yield return new Sprite(x, y, tile, TileProperties.Transparent);

            tile.XFlipped = true;
            yield return new Sprite(x + 8, y, tile, TileProperties.Transparent);
        }

        private IEnumerable<Sprite> PiranhaPlant(int x, int y, int frame)
        {
            var index = ((frame + (x >> 1)) & 0x10) == 0 ? 0xE5 : 0xEC;
            x += 8;
            y -= PirhanaPlantOffset(frame - (x >> 2));

            var tile = new ChrTile(index, 5, 0, 0);
            yield return new Sprite(x, y, tile);

            tile.TileIndex++;
            yield return new Sprite(x, y + 8, tile);

            tile.TileIndex--;
            tile.TileFlip = TileFlip.Horizontal;
            x += 8;
            yield return new Sprite(x, y, tile);

            tile.TileIndex++;
            yield return new Sprite(x, y + 8, tile);

            y += 0x10;
            tile.TileIndex = 0xEB;
            tile.PaletteIndex = 2;
            yield return new Sprite(x, y, tile);

            tile.TileFlip = 0;
            yield return new Sprite(x - 8, y, tile);
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
            yield return new Sprite(x, y, tile);

            tile.TileFlip = TileFlip.Horizontal;
            yield return new Sprite(x + 8, y, tile);

            tile.TileFlip = TileFlip.Veritcal;
            yield return new Sprite(x, y + 0x10, tile);

            tile.TileFlip = TileFlip.Both;
            yield return new Sprite(x + 8, y + 0x10, tile);

            tile.TileIndex++;
            tile.TileFlip = 0;
            yield return new Sprite(x, y + 8, tile);

            tile.TileFlip = TileFlip.Horizontal;
            yield return new Sprite(x + 8, y + 8, tile);
        }

        private IEnumerable<Sprite> FlagPole(int x, int y, int frame)
        {
            y = 0x30;
            x += 8;

            var index = 4 - (((frame + (x >> 1)) / 12 % 3) << 1);

            var tile = new ChrTile(0x120 + index, 5, LayerPriority.Priority2, 0);
            yield return new Sprite(x, y, tile);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, tile);

            tile.TileIndex += 0x0F;
            yield return new Sprite(x, y + 8, tile);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y + 8, tile);

            x += 0x60;
            y += 0x40;
            index = ((frame + (x >> 1)) >> 2) & 6;
            tile = new ChrTile(0x104 + index, 5, LayerPriority.Priority0, 0);
            yield return new Sprite(x, y, tile);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, tile);

            tile.TileIndex += 0x0F;
            yield return new Sprite(x, y + 8, tile);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y + 8, tile);
        }

        private IEnumerable<Sprite> BeanStalk(int x, int y, int frame)
        {
            var tile = new ChrTile(0x12C, 5, LayerPriority.Priority3, 0);
            for (y -= 0x10; y > 0x10; y -= 0x10)
            {
                tile.TileIndex = 0x12C;
                yield return new Sprite(x, y, tile);

                tile.TileIndex++;
                yield return new Sprite(x + 8, y, tile);

                tile.TileIndex += 0x0F;
                yield return new Sprite(x, y + 8, tile);

                tile.TileIndex++;
                yield return new Sprite(x + 8, y + 8, tile);
            }

            tile = new ChrTile(0x12A, 5, LayerPriority.Priority3, 0);
            yield return new Sprite(x, y, tile);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, tile);

            tile.TileIndex += 0x0F;
            yield return new Sprite(x, y + 8, tile);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y + 8, tile);
        }

        private IEnumerable<Sprite> Brick10Coins(int x, int y, int frame)
        {
            x -= 4;
            y -= 8;
            var tile = new ChrTile(0x2C, 2, LayerPriority.Priority3, 0);
            yield return new Sprite(x, y, tile);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, tile);

            tile.TileIndex += 0x0F;
            yield return new Sprite(x, y + 8, tile);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y + 8, tile);

            tile.TileIndex = 0x2E;
            x += 8;
            yield return new Sprite(x, y, tile);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, tile);

            tile.TileIndex += 0x0F;
            yield return new Sprite(x, y + 8, tile);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y + 8, tile);
        }
    }
}
