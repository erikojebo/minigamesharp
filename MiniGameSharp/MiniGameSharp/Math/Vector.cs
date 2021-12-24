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

            return new Vector(
                X * (float)System.Math.Cos(angleInRadians) - Y * (float)System.Math.Sin(angleInRadians),
                X * (float)System.Math.Sin(angleInRadians) + Y * (float)System.Math.Cos(angleInRadians));
        }

        public Vector Add(Vector vector)
        {
            return new Vector(X + vector.X, Y + vector.Y);
        }

        public readonly Vector Scale(float scaleFactor)
        {
            return new Vector(X * scaleFactor, Y * scaleFactor);
        }
    }
}