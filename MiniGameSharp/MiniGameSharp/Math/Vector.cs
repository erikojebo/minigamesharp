namespace MiniGameSharp.Math
{
    public struct Vector
    {
        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }

        public float Length => (float)System.Math.Sqrt(X * X + Y * Y);

        public Vector Normalize()
        {
            return Resize(1);
        }

        public Vector Resize(float length)
        {
            var scaleFactor = length / Length;

            return new Vector(X * scaleFactor, Y * scaleFactor);
        }

        public Vector CapLength(float length)
        {
            if (Length > length)
            {
                return Resize(length);
            }

            return this;
        }

        public readonly Vector Rotate(double angleInDegrees)
        {
            var angleInRadians = angleInDegrees / 360 * System.Math.PI * 2;

            var newX = X * (float)System.Math.Cos(angleInRadians) - Y * (float)System.Math.Sin(angleInRadians);
            var newY = X * (float)System.Math.Sin(angleInRadians) + Y * (float)System.Math.Cos(angleInRadians);

            // Avoid float rounding errors to make debugging and testing easier
            newX = (float)System.Math.Round(newX, 8);
            newY = (float)System.Math.Round(newY, 8);

            return new Vector(newX, newY);
        }

        public Vector Add(float x, float y)
        {
            return new Vector(X + x, Y + y);
        }

        public Vector Add(Vector vector)
        {
            return new Vector(X + vector.X, Y + vector.Y);
        }

        public readonly Vector Scale(float scaleFactor)
        {
            return new Vector(X * scaleFactor, Y * scaleFactor);
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }

        public float DotProduct(Vector v)
        {
            return X * v.X + Y * v.Y;
        }
    }
}