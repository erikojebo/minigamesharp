using System.Drawing;
using MiniGameSharp.Math;

namespace MiniGameSharp.Shapes
{
    public class Triangle : Shape
    {
        public Triangle()
        {
            
        }

        public Triangle(int x, int y, int width, int height, Color color)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            
            SetColor(color);
        }
        
        public float Width { get; set; }
        public float Height { get; set; }
        
        public override void Render(Graphics graphics)
        {
            var yOffset = 2 / 3f;
            var xCenter = X + Width / 2;
            var yCenter = Y + Height * yOffset;

            graphics.TranslateTransform(xCenter, yCenter);
            graphics.RotateTransform(Angle);
            
            graphics.FillPolygon(Brush, new []
            {
                new PointF(-Width / 2, Height * (1 - yOffset)),
                new PointF(Width / 2, Height * (1 - yOffset)),
                new PointF(0, -Height * yOffset)
            });
            
            graphics.ResetTransform();
        }

        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(X, Y, Width, Height);
        }
    }
}