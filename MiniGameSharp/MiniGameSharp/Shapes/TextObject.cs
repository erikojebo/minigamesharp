using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using MiniGameSharp.Math;

namespace MiniGameSharp.Shapes
{
    public class TextObject : GameObject
    {
        public TextObject()
        {
            Font = new Font(FontFamily.GenericSansSerif, 14);
            Brush = new SolidBrush(Color.WhiteSmoke);
        }

        public TextObject(float x, float y, string text, float fontSize, Color color)
        {
            X = x;
            Y = y;
            Text = text;
            Font = new Font(FontFamily.GenericSansSerif, fontSize);
            Brush = new SolidBrush(color);
        }

        public string Text { get; set; }
        public Font Font { get; set; }
        public Brush Brush { get; set; }
        
        public float Width { get; set; }
        public float Height { get; set; }
        
        public override void Render(Graphics graphics)
        {
            graphics.TranslateTransform(X, Y);
            graphics.RotateTransform(Angle);
            graphics.DrawString(Text, Font, Brush, new PointF(0, 0));
            graphics.ResetTransform();
            
            var size = graphics.MeasureString(Text, Font);
            
            Width = size.Width;
            Height = size.Height;
        }

        public override BoundingBoxPolygon BoundingBox()
        {
            return new BoundingBoxRectangle(X, Y, Width, Height, Angle);
        }
    }
}