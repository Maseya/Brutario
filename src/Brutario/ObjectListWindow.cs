// <copyright file="ObjectListWindow.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed under GNU
//     Affero General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using Brutario.Smb1;

    public sealed partial class ObjectListWindow : Form
    {
        private AreaPlatformType _areaPlatformType;

        public ObjectListWindow()
        {
            InitializeComponent();
            ObjectItems = new ObjectList(this);
            SpriteItems = new SpriteList(this);
        }

        public event EventHandler AreaPlatformTypeChanged;

        public event EventHandler SelectedIndexChanged;

        public event EventHandler EditItem;

        public ObjectList ObjectItems
        {
            get;
        }

        public SpriteList SpriteItems
        {
            get;
        }

        public bool SpriteMode
        {
            get;
            set;
        }

        public AreaPlatformType AreaPlatformType
        {
            get
            {
                return _areaPlatformType;
            }

            set
            {
                if (AreaPlatformType == value || SpriteMode)
                {
                    return;
                }

                _areaPlatformType = value;
                SuspendLayout();
                for (var i = 0; i < ObjectItems.Count; i++)
                {
                    if (ObjectItems[i].Code == AreaObjectCode.AreaSpecificPlatform)
                    {
                        lvwObjects.Items[i].SubItems[3].Text = NameCommand(
                            ObjectItems[i],
                            value);
                    }
                }

                ResumeLayout();
                AreaPlatformTypeChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public int SelectedIndex
        {
            get
            {
                return lvwObjects.SelectedIndices.Count > 0
                    ? lvwObjects.SelectedIndices[0]
                    : -1;
            }

            set
            {
                if (value != -1)
                {
                    lvwObjects.Items[value].Selected = true;
                }
                else
                {
                    lvwObjects.SelectedItems.Clear();
                }
            }
        }

        private static string NameCommand(
            AreaObjectCommand command,
            AreaPlatformType areaPlatformType)
        {
            var length = command.Parameter + 1;

            switch (command.Code)
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

            case AreaObjectCode.ScreenSkip:
                return $"Skip to screen 0x{command.BaseCommand:X2}";

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

            case AreaObjectCode.BrickAndSceneryChange:
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

            return $"Unknown command: {command}";
        }

        private static string NameCommand(
            AreaSpriteCommand command)
        {
            switch (command.Code)
            {
            case AreaSpriteCode.AreaPointer:
                return $"Transition: Area Number={command.AreaNumber:X2}, Page={command.AreaPointerScreen + 1} (For world {1 + command.WorldLimit})";

            case AreaSpriteCode.GreenKoopaTroopa:
                return "Koopa Troopa (Green)";

            case AreaSpriteCode.RedKoopaTroopa:
                return "Koopa Troopa (Red; Walks off floors)";
            case AreaSpriteCode.BuzzyBeetle:
                return "Buzzy Beetle";

            case AreaSpriteCode.RedKoopaTroopa2:
                return "Koopa Troopa (Red; Stays on floors)";
            case AreaSpriteCode.GreenKoopaTroopa2:
                return "Koopa Troopa (Green; Walks in place)";
            case AreaSpriteCode.HammerBros:
                return "Hammer Bros.";

            case AreaSpriteCode.Goomba:
                return "Goomba";

            case AreaSpriteCode.Blooper:
                return "Squid";

            case AreaSpriteCode.BulletBill:
                return "Bullet Bill";

            case AreaSpriteCode.YellowKoopaParatroopa:
                return "Yellow Koopa Paratroopa (Flies in place)";

            case AreaSpriteCode.GreenCheepCheep:
                return "Green Cheep-Cheep";

            case AreaSpriteCode.RedCheepCheep:
                return "Red Cheep-Cheep";

            case AreaSpriteCode.Podoboo:
                return "Podoboo";

            case AreaSpriteCode.PiranhaPlant:
                return "Piranha Plant";

            case AreaSpriteCode.GreenKoopaParatroopa:
                return "Green Koopa Paratroopa (Leaping)";

            case AreaSpriteCode.RedKoopaParatroopa:
                return "Red Koopa Paratroopa (Flies vertically)";

            case AreaSpriteCode.GreenKoopaParatroopa2:
                return "Green Koopa Paratroopa (Flies horizontally)";

            case AreaSpriteCode.Lakitu:
                return "Lakitu";

            case AreaSpriteCode.Spiny:
                return "Spiny (undefined walk speed)";

            case AreaSpriteCode.RedFlyingCheepCheep:
                return "Red Flying Cheep-Cheep";

            case AreaSpriteCode.BowsersFire:
                return "Bowser's Fire (generator)";

            case AreaSpriteCode.Fireworks:
                return "Single Firework";

            case AreaSpriteCode.BulletBillOrCheepCheeps:
                return "Generator (Bullet Bill or Cheep-Cheeps)";

            case AreaSpriteCode.FireBarClockwise:
                return "Fire Bar (Clockwise)";

            case AreaSpriteCode.FastFireBarClockwise:
                return "Fire Bar (Fast; Clockwise)";
            case AreaSpriteCode.FireBarCounterClockwise:
                return "Fire Bar (Counter-Clockwise)";

            case AreaSpriteCode.FastFireBarCounterClockwise:
                return "Fire Bar (Fast; Counter-Clockwise)";
            case AreaSpriteCode.LongFireBarClockwise:
                return "Long Fire Bar (Fast; Clockwise)";
            case AreaSpriteCode.BalanceRopeLift:
                return "Rope for Lift Balance";

            case AreaSpriteCode.LiftDownThenUp:
                return "Lift (Down, then up)";

            case AreaSpriteCode.LiftUp:
                return "Lift (Up)";

            case AreaSpriteCode.LiftDown:
                return "Lift (Down)";

            case AreaSpriteCode.LiftLeftThenRight:
                return "Lift (Left, then right)";

            case AreaSpriteCode.LiftFalling:
                return "Lift (Falling)";

            case AreaSpriteCode.LiftRight:
                return "Lift (Right)";

            case AreaSpriteCode.ShortLiftUp:
                return "Short Lift (Up)";

            case AreaSpriteCode.ShortLiftDown:
                return "Short Lift (Down)";

            case AreaSpriteCode.Bowser:
                return "Bowser: King of the Koopa";

            case AreaSpriteCode.WarpZoneCommand:
                return "Command: Load Warp Zone";

            case AreaSpriteCode.ToadOrPrincess:
                return "Toad or Princess";

            case AreaSpriteCode.TwoGoombasY10:
                return "Two Goombas (Y=10)";

            case AreaSpriteCode.ThreeGoombasY10:
                return "Three Goombas (Y=10)";

            case AreaSpriteCode.TwoGoombasY6:
                return "Two Goombas (Y=6)";

            case AreaSpriteCode.ThreeGoombasY6:
                return "Three Goombas (Y=6)";

            case AreaSpriteCode.TwoGreenKoopasY10:
                return "Two Green Koopa Troopas (Y=10)";

            case AreaSpriteCode.ThreeGreenKoopasY10:
                return "Three Green Koopa Troopas (Y=10)";

            case AreaSpriteCode.TwoGreenKoopasY6:
                return "Two Green Koopa Troopas (Y=6)";

            case AreaSpriteCode.ThreeGreenKoopasY6:
                return "Three Green Koopa Troopas (Y=6)";

            case AreaSpriteCode.ScreenSkip:
                return $"Page Skip: {command.BaseCommand & 0x1F}";

            default:
                break;
            }

            return $"Unknown command: {command}";
        }

        private void OnSelectedIndexChanged(EventArgs e)
        {
            SelectedIndexChanged?.Invoke(this, e);
        }

        private void OnEditItem(EventArgs e)
        {
            EditItem?.Invoke(this, e);
        }

        private void Objects_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnEditItem(EventArgs.Empty);
            if (lvwObjects.SelectedItems.Count != 1)
            {
                return;
            }
        }

        private void Objects_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedIndexChanged(EventArgs.Empty);
        }

        private void Objects_ItemSelectionChanged(
            object sender,
            ListViewItemSelectionChangedEventArgs e)
        {
            OnSelectedIndexChanged(EventArgs.Empty);
        }

        public class ObjectList : IList<AreaObjectCommand>
        {
            public ObjectList(ObjectListWindow owner)
            {
                Owner = owner
                    ?? throw new ArgumentNullException(nameof(owner));
            }

            public int Count
            {
                get
                {
                    return BaseItems.Count;
                }
            }

            bool ICollection<AreaObjectCommand>.IsReadOnly
            {
                get
                {
                    return false;
                }
            }

            private ObjectListWindow Owner
            {
                get;
            }

            private ListView.ListViewItemCollection BaseItems
            {
                get
                {
                    return Owner.lvwObjects.Items;
                }
            }

            public AreaObjectCommand this[int index]
            {
                get
                {
                    return (AreaObjectCommand)BaseItems[index].Tag;
                }

                set
                {
                    var selectedIndex = Owner.SelectedIndex;
                    BaseItems[index] = Create(value, Owner.AreaPlatformType);
                    UpdatePages();
                    Owner.SelectedIndex = selectedIndex;
                }
            }

            public void Add(AreaObjectCommand item)
            {
                Insert(Count, item);
            }

            public void AddRange(IEnumerable<AreaObjectCommand> items)
            {
                var result = new List<ListViewItem>(items.Select(item =>
                    Create(item, Owner.AreaPlatformType)));
                BaseItems.AddRange(result.ToArray());
                UpdatePages();
            }

            public void Insert(int index, AreaObjectCommand item)
            {
                _ = BaseItems.Insert(index, Create(item, Owner.AreaPlatformType));
                UpdatePages();
            }

            public void Clear()
            {
                BaseItems.Clear();
            }

            public bool Contains(AreaObjectCommand item)
            {
                return IndexOf(item) != -1;
            }

            public int IndexOf(AreaObjectCommand item)
            {
                for (var i = 0; i < Count; i++)
                {
                    if (item.Equals(this[i]))
                    {
                        return i;
                    }
                }

                return -1;
            }

            public bool Remove(AreaObjectCommand item)
            {
                var index = IndexOf(item);
                if (index == -1)
                {
                    return false;
                }

                RemoveAt(index);
                return true;
            }

            public void RemoveAt(int index)
            {
                BaseItems.RemoveAt(index);
                UpdatePages();
            }

            public IEnumerator<AreaObjectCommand> GetEnumerator()
            {
                foreach (var item in BaseItems)
                {
                    yield return (AreaObjectCommand)(item as ListViewItem).Tag;
                }
            }

            void ICollection<AreaObjectCommand>.CopyTo(AreaObjectCommand[] dest, int index)
            {
                throw new NotSupportedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private static ListViewItem Create(
                AreaObjectCommand command,
                AreaPlatformType areaPlatformType)
            {
                var hex = $"{command.Value1:X2} {command.Value2:X2}";
                if (command.Size == 3)
                {
                    hex += $" {command.Value3:X2}";
                }

                return new ListViewItem(new string[] {
                    hex,
                    "ERROR",
                    $"{command.X:X1},{command.Y:X1}",
                    NameCommand(command, areaPlatformType)})
                {
                    Tag = command
                };
            }

            private void UpdatePages()
            {
                var page = 0;
                for (var i = 0; i < Count; i++)
                {
                    var command = this[i];
                    if (command.ScreenFlag)
                    {
                        page++;
                    }

                    if (command.Code == AreaObjectCode.ScreenSkip)
                    {
                        page = command.BaseCommand;
                    }

                    BaseItems[i].SubItems[1].Text = page.ToString();
                }
            }
        }

        public class SpriteList : IList<AreaSpriteCommand>
        {
            public SpriteList(ObjectListWindow owner)
            {
                Owner = owner
                    ?? throw new ArgumentNullException(nameof(owner));
            }

            public int Count
            {
                get
                {
                    return BaseItems.Count;
                }
            }

            bool ICollection<AreaSpriteCommand>.IsReadOnly
            {
                get
                {
                    return false;
                }
            }

            private ObjectListWindow Owner
            {
                get;
            }

            private ListView.ListViewItemCollection BaseItems
            {
                get
                {
                    return Owner.lvwObjects.Items;
                }
            }

            public AreaSpriteCommand this[int index]
            {
                get
                {
                    return (AreaSpriteCommand)BaseItems[index].Tag;
                }

                set
                {
                    BaseItems[index] = Create(value);
                    UpdatePages();
                }
            }

            public void Add(AreaSpriteCommand item)
            {
                Insert(Count, item);
            }

            public void AddRange(IEnumerable<AreaSpriteCommand> items)
            {
                var result = new List<ListViewItem>(items.Select(item =>
                    Create(item)));
                BaseItems.AddRange(result.ToArray());
                UpdatePages();
            }

            public void Insert(int index, AreaSpriteCommand item)
            {
                _ = BaseItems.Insert(index, Create(item));
                UpdatePages();
            }

            public void Clear()
            {
                BaseItems.Clear();
            }

            public bool Contains(AreaSpriteCommand item)
            {
                return IndexOf(item) != -1;
            }

            public int IndexOf(AreaSpriteCommand item)
            {
                for (var i = 0; i < Count; i++)
                {
                    if (item.Equals(this[i]))
                    {
                        return i;
                    }
                }

                return -1;
            }

            public bool Remove(AreaSpriteCommand item)
            {
                var index = IndexOf(item);
                if (index == -1)
                {
                    return false;
                }

                RemoveAt(index);
                return true;
            }

            public void RemoveAt(int index)
            {
                BaseItems.RemoveAt(index);
                UpdatePages();
            }

            public IEnumerator<AreaSpriteCommand> GetEnumerator()
            {
                foreach (var item in BaseItems)
                {
                    yield return (AreaSpriteCommand)(item as ListViewItem).Tag;
                }
            }

            void ICollection<AreaSpriteCommand>.CopyTo(AreaSpriteCommand[] dest, int index)
            {
                throw new NotSupportedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private static ListViewItem Create(AreaSpriteCommand command)
            {
                var hex = $"{command.Value1:X2} {command.Value2:X2}";
                if (command.Size == 3)
                {
                    hex += $" {command.Value3:X2}";
                }

                return new ListViewItem(new string[] {
                    hex,
                    "ERROR",
                    $"{command.X:X1},{command.Y:X1}",
                    NameCommand(command)})
                {
                    Tag = command
                };
            }

            private void UpdatePages()
            {
                var page = 0;
                for (var i = 0; i < Count; i++)
                {
                    var command = this[i];
                    if (command.ScreenFlag)
                    {
                        page++;
                    }

                    if (command.Code == AreaSpriteCode.ScreenSkip)
                    {
                        page = command.BaseCommand;
                    }

                    BaseItems[i].SubItems[1].Text = page.ToString();
                }
            }
        }
    }
}
