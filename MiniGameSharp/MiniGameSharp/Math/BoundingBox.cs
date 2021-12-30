using System.Collections.Generic;

namespace MiniGameSharp.Math
{
    public class BoundingBox
    {
        public BoundingBox(float x, float y, float width, float height, float angle = 0)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Angle = angle;
        }

        public float X { get; }
        public float Y { get; }
        public float Width { get; }
        public float Height { get; }
        public float Angle { get; }

        public List<Vector> Corners
        {
            get
            {
                var verticalEdge = new Vector(0, Height);
                var horizontalEdge = new Vector(Width, 0);
                
                var rotatedVerticalEdge = verticalEdge.Rotate(Angle);
                var rotatedHorizontalEdge = horizontalEdge.Rotate(Angle);
                
                return new()
                {
                    new Vector(X, Y),
                    rotatedHorizontalEdge.Add(X, Y),
                    rotatedVerticalEdge.Add(X, Y),
                    rotatedVerticalEdge.Add(rotatedHorizontalEdge).Add(X, Y)
                };
            }
        }
    }
}