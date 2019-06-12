using System.Drawing;

namespace Brutario
{
    public interface IAreaGraphic
    {
        int X
        {
            get; set;
        }

        int Y
        {
            get; set;
        }

        int Width
        {
            get;
            set;
        }

        int Height
        {
            get;
            set;
        }

        void Render(Graphics graphics);
    }
}
