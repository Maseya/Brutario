namespace Brutario.Smb1
{
    using System;
    using System.Collections.Generic;

    public class SpriteListEditor : ListEditor<AreaSpriteCommand>
    {
        public SpriteListEditor()
            : base()
        { }

        public SpriteListEditor(IEnumerable<AreaSpriteCommand> items)
            : base(items)
        { }

        public SpriteListEditor(IEnumerable<byte> byteData)
            : this(GetAreaData(byteData))
        { }

        public static IEnumerable<AreaSpriteCommand> GetAreaData(
            IEnumerable<byte> bytes)
        {
            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            using var en = bytes.GetEnumerator();
            if (!en.MoveNext())
            {
                throw new ArgumentException();
            }

            while (en.Current != AreaSpriteCommand.TerminationCode)
            {
                if (AreaSpriteCommand.IsThreeByteSpecifier(en.Current))
                {
                    var list = new List<byte>(GetBytes(3));
                    yield return new AreaSpriteCommand(
                        list[0],
                        list[1],
                        list[2]);
                }
                else
                {
                    var list = new List<byte>(GetBytes(2));
                    yield return new AreaSpriteCommand(
                        list[0],
                        list[1]);
                }
            }

            IEnumerable<byte> GetBytes(int size)
            {
                for (var i = 0; i < size; i++)
                {
                    yield return en.Current;

                    if (!en.MoveNext())
                    {
                        throw new ArgumentException();
                    }
                }
            }
        }

        public byte[] ToByteArray()
        {
            var result = new List<byte>();
            foreach (var command in Items)
            {
                result.Add(command.Value1);
                result.Add(command.Value2);
                if (command.IsThreeByteCommand)
                {
                    result.Add(command.Value3);
                }
            }

            result.Add(AreaSpriteCommand.TerminationCode);

            return result.ToArray();
        }

        public IEnumerable<(int x, int y)> EnumeratePositions()
        {
            var screen = 0;
            var screenSkip = false;
            foreach (var command in Items)
            {
                if (command.ScreenFlag && !screenSkip)
                {
                    screen++;
                }
                else if (command.Code == AreaSpriteCode.ScreenJump)
                {
                    screen = command.BaseCommand;
                }

                yield return (
                    (screen << 4) | command.X,
                    command.Y + (IsFirebar(command.Code) ? 0 : 1));
            }

            static bool IsFirebar(AreaSpriteCode code)
            {
                return ((uint)code - (uint)AreaSpriteCode.FireBarClockwise) < 5;
            }
        }

        public int GetIndex(int x, int y)
        {
            var i = 0;
            foreach (var p in EnumeratePositions())
            {
                if (p == (x, y))
                {
                    return i;
                }

                i++;
            }

            return -1;
        }
    }
}
