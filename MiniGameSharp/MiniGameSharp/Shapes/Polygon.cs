using System.Drawing;
using System.Linq;
using MiniGameSharp.Math;

namespace MiniGameSharp.Shapes
{
    public class Polygon : GameObject
    {
        private readonly Vector[] _cornersRelativeToCenter;

        public Polygon(float centerX, float centerY, params Vector[] cornersRelativeToCenter)
        {
            X = centerX;
            Y = centerY;

            _cornersRelativeToCenter = cornersRelativeToCenter;
        }

        public override void Render(Graphics graphics)
        {
            foreach (var line in BoundingBox().Lines)
            {
                graphics.DrawLine(new Pen(Color.WhiteSmoke), new PointF(line.X1, line.Y1), new PointF(line.X2, line.Y2));
            }
        }

        public override BoundingBoxPolygon BoundingBox()
        {
            var cornersAbsolute = _cornersRelativeToCenter.Select(x =>
                    x.Rotate(Angle).Add(new Vector(X, Y)))
                .ToArray();

            return new BoundingBoxPolygon(cornersAbsolute);
        }
    }
}