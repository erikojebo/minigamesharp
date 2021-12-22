using System.Drawing;

namespace MiniGameSharp.Shapes
{
    public abstract class Shape
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Angle { get; set; }

        public Brush Brush { get; set; }
        
        public abstract void Render(Graphics graphics);

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