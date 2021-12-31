namespace MiniGameSharp.Math
{
    public class BoundingBoxRectangle : BoundingBoxPolygon
    {
        public BoundingBoxRectangle(float x, float y, float width, float height, float angle = 0)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Angle = angle;

            GenerateCorners();
        }

        public float X { get; }
        public float Y { get; }
        public float Width { get; }
        public float Height { get; }
        public float Angle { get; }

        private void GenerateCorners()
        {
            var verticalEdge = new Vector(0, Height);
            var horizontalEdge = new Vector(Width, 0);

            var rotatedVerticalEdge = verticalEdge.Rotate(Angle);
            var rotatedHorizontalEdge = horizontalEdge.Rotate(Angle);

            var corners = new[]
            {
                new Vector(X, Y),
                rotatedHorizontalEdge.Add(X, Y),
                rotatedVerticalEdge.Add(X, Y),
                rotatedVerticalEdge.Add(rotatedHorizontalEdge).Add(X, Y)
            };

            SetCorners(corners);
        }
    }
}