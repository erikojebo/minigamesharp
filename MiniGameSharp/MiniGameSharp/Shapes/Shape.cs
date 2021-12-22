using System.Drawing;

namespace MiniGameSharp.Shapes
{
    public abstract class Shape : GameObject
    {
        public Brush Brush { get; set; }
        
        public void SetColor(Color color)
        {
            Brush = new SolidBrush(color);
        }

        public void SetBrush(Brush brush)
        {
            Brush = brush;
        }
    }
}