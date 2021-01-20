using System;
using System.Collections.Generic;
using static System.Math;

namespace Brutario.Smb1
{
    public class AreaSpriteRenderer
    {
        public AreaSpriteRenderer(AreaSpriteLoader areaSpriteLoader)
        {
            AreaSpriteLoader = areaSpriteLoader
                ?? throw new ArgumentNullException(nameof(areaSpriteLoader));

            Commands = new Dictionary<AreaSpriteCode, SpriteCallback>()
            {
                { AreaSpriteCode.GreenKoopaTroopa, GreenKoopaTroopa },
                { AreaSpriteCode.RedKoopaTroopa, RedKoopaTroopa },
                { AreaSpriteCode.BuzzyBeetle, BuzzyBeetle },
                { AreaSpriteCode.GreenKoopaTroopa2, GreenKoopaTroopa },
                { AreaSpriteCode.RedKoopaTroopa2, RedKoopaTroopa },
                { AreaSpriteCode.Goomba, Goomba },
                { AreaSpriteCode.FireBarClockwise, FireBarClockwise },
                { AreaSpriteCode.FireBarCounterClockwise, FireBarCounterClockwise },
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
            };
        }

        private delegate IEnumerable<Sprite> SpriteCallback(int x, int y);

        public AreaSpriteLoader AreaSpriteLoader
        {
            get;
        }

        public AreaLoader AreaLoader
        {
            get
            {
                return AreaSpriteLoader.AreaLoader;
            }
        }

        public GameData RomData
        {
            get
            {
                return AreaLoader.RomData;
            }
        }

        public RomIO Rom
        {
            get
            {
                return RomData.Rom;
            }
        }

        public List<Sprite> Sprites
        {
            get;
        }

        private int AnimationFrame
        {
            get
            {
                return RomData.AnimationFrame;
            }
        }

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
            IEnumerable<AreaObjectCommand> areaObjectData)
        {
            var result = new List<Sprite>();
            result.AddRange(GetSprites(areaSpriteCommands));
            result.AddRange(GetSprites(areaObjectData));

            return result;
        }

        public List<Sprite> GetSprites(
            IEnumerable<AreaObjectCommand>            areaObjectData)
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

                var x = (screen | command.X) << 4;
                var y = (command.Y + 2) << 4;
                if (ObjectCommands.TryGetValue(command.Code, out var getSprites))
                {
                    result.AddRange(getSprites(x, y));
                }

            }

            return result;
        }

        public List<Sprite> GetSprites(
            IEnumerable<AreaSpriteCommand> areaSpriteCommands)
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
                    result.AddRange(getSprites(x, y));
                }
            }

            return result;
        }

        private IEnumerable<Sprite> GreenKoopaTroopa(int x, int y)
        {
            return KoopaTroopa(x, y, 5);
        }

        private IEnumerable<Sprite> RedKoopaTroopa(int x, int y)
        {
            return KoopaTroopa(x, y, 6);
        }

        private IEnumerable<Sprite> GoldKoopaTroopa(int x, int y)
        {
            return KoopaTroopa(x, y, 7);
        }

        private IEnumerable<Sprite> KoopaTroopa(int x, int y, int palette)
        {
            var index = (((AnimationFrame + (x >> 1)) & 8) >> 3) * 5;
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

        private IEnumerable<Sprite> Goomba(int x, int y)
        {
            y++;
            var tile = new ChrTile(0xC6, 1, LayerPriority.Priority2, 0);
            yield return new Sprite(x, y, tile);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, tile);

            y += 8;
            if (((AnimationFrame + (x >> 1)) & 8) >> 3 == 0)
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

        private IEnumerable<Sprite> BuzzyBeetle(int x, int y)
        {
            y += 2;
            var index = (((AnimationFrame + (x << 1)) & 8) >> 1);
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

        private IEnumerable<Sprite> FireBarClockwise(int x, int y)
        {
            var tile = new ChrTile(
                0xBE + ((AnimationFrame & 4) >> 2),
                4,
                LayerPriority.Priority2,
                (TileFlip)((AnimationFrame & 0x0C) >> 2));

            x += 4;
            y -= 12;

            var rate = 0x70;
            var angle = (((AnimationFrame & ~3) >> 1) + (x >> 1)) % rate * 2 * PI / rate;
            for (var i = 0; i < 6; i++)
            {
                yield return new Sprite(
                    x + (int)(8 * i * Cos(angle)),
                    y + (int)(8 * i * Sin(angle)),
                    tile);
            }
        }

        private IEnumerable<Sprite> FireBarCounterClockwise(int x, int y)
        {
            var tile = new ChrTile(
                0xBE + ((AnimationFrame & 4) >> 2),
                4,
                LayerPriority.Priority2,
                (TileFlip)((AnimationFrame & 0x0C) >> 2));

            x += 4;
            y -= 12;

            var rate = 0x70;
            var angle = -((((AnimationFrame & ~3) >> 1) + (x >> 1)) % rate) * 2 * PI / rate;
            for (var i = 0; i < 6; i++)
            {
                yield return new Sprite(
                    x + (int)(8 * i * Cos(angle)),
                    y + (int)(8 * i * Sin(angle)),
                    tile);
            }
        }

        private IEnumerable<Sprite> TwoGoombasY10(int x, int y)
        {
            return Goombas(x, 2, 0xC0);
        }

        private IEnumerable<Sprite> ThreeGoombasY10(int x, int y)
        {
            return Goombas(x, 3, 0xC0);
        }

        private IEnumerable<Sprite> TwoGoombasY6(int x, int y)
        {
            return Goombas(x, 2, 0x80);
        }

        private IEnumerable<Sprite> ThreeGoombasY6(int x, int y)
        {
            return Goombas(x, 3, 0x80);
        }

        private IEnumerable<Sprite> TwoGreenKoopasY10(int x, int y)
        {
            return GreenKoopas(x, 2, 0xC0);
        }

        private IEnumerable<Sprite> ThreeGreenKoopasY10(int x, int y)
        {
            return GreenKoopas(x, 3, 0xC0);
        }

        private IEnumerable<Sprite> TwoGreenKoopasY6(int x, int y)
        {
            return GreenKoopas(x, 2, 0x80);
        }

        private IEnumerable<Sprite> ThreeGreenKoopasY6(int x, int y)
        {
            return GreenKoopas(x, 3, 0x80);
        }

        private IEnumerable<Sprite> Goombas(int x, int count, int y)
        {
            x -= 0x30;
            for (var i = 0; i < count; i++, x += 0x18)
            {
                foreach (var sprite in Goomba(x, y))
                {
                    yield return sprite;
                }
            }
        }

        private IEnumerable<Sprite> GreenKoopas(int x, int count, int y)
        {
            x -= 0x30;
            for (var i = 0; i < count; i++, x += 0x18)
            {
                foreach (var sprite in GreenKoopaTroopa(x, y))
                {
                    yield return sprite;
                }
            }
        }

        private IEnumerable<Sprite> LiftDown(int x, int y)
        {
            var tile = new ChrTile(0x87, 7, LayerPriority.Priority2, 0);
            y += ((AnimationFrame << 6) / 75) + (x >> 4);
            y &= 0xFF;
            x += 8;
            for (var i = 0; i <= 0x30; i += 8)
            {
                yield return new Sprite(x + i, y, tile);
            }
        }

        private IEnumerable<Sprite> LiftUp(int x, int y)
        {
            var tile = new ChrTile(0x87, 6, LayerPriority.Priority2, 0);
            y -= ((AnimationFrame << 6) / 75) + (x >> 4);
            y &= 0xFF;
            x += 8;
            for (var i = 0; i <= 0x30; i += 8)
            {
                yield return new Sprite(x + i, y, tile);
            }
        }

        private IEnumerable<Sprite> ShortLiftDown(int x, int y)
        {
            var tile = new ChrTile(0x87, 7, LayerPriority.Priority2, 0);
            y += ((AnimationFrame << 6) / 75) + (x >> 4);
            y &= 0xFF;
            x += 8;
            for (var i = 0; i <= 0x18; i += 8)
            {
                yield return new Sprite(x + i, y, tile);
            }
        }

        private IEnumerable<Sprite> ShortLiftUp(int x, int y)
        {
            var tile = new ChrTile(0x87, 6, LayerPriority.Priority2, 0);
            y -= ((AnimationFrame << 6) / 75) + (x >> 4);
            y &= 0xFF;
            x += 8;
            for (var i = 0; i <= 0x18; i += 8)
            {
                yield return new Sprite(x + i, y, tile);
            }
        }

        private IEnumerable<Sprite> BalanceRopeLift(int x, int y)
        {
            var tile = new ChrTile(0x87, 6, LayerPriority.Priority2, 0);
            x -= 4;
            y -= 0x10;
            for (var i = 0; i <= 0x30; i += 8)
            {
                yield return new Sprite(x + i, y, tile);
            }
        }

        private IEnumerable<Sprite> Powerup(int x, int y)
        {
            return Mushroom(x, y, 6);
        }

        private IEnumerable<Sprite> Brick1Up(int x, int y)
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

        private IEnumerable<Sprite> PiranhaPlant(int x, int y)
        {
            if (RomData.AreaNumber == 0x25)
            {
                //yield break;
            }

            var index = ((AnimationFrame + (x >> 1)) & 0x10) == 0 ? 0xE5 : 0xEC;
            x += 8;
            y -= PirhanaPlantOffset(AnimationFrame - (x >> 2));

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

        private IEnumerable<Sprite> SpringBoard(int x, int y)
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
        private IEnumerable<Sprite> FlagPole(int x, int y)
        {
            y = 0x30;
            x += 8;

            var index = 4 - (((AnimationFrame + (x >> 1)) / 12 % 3) << 1);

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
            index = ((AnimationFrame + (x >> 1)) >> 2) & 6;
            tile = new ChrTile(0x104 + index, 5, LayerPriority.Priority0, 0);
            yield return new Sprite(x, y, tile);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y, tile);

            tile.TileIndex += 0x0F;
            yield return new Sprite(x, y + 8, tile);

            tile.TileIndex++;
            yield return new Sprite(x + 8, y + 8, tile);
        }

        private IEnumerable<Sprite> BeanStalk(int x, int y)
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

        private IEnumerable<Sprite> Brick10Coins(int x, int y)
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
