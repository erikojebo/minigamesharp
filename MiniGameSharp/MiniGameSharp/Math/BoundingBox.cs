namespace MiniGameSharp.Math
{
    public class BoundingBox
    {
        public BoundingBox(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public float Width { get; set; }
        public float Height { get; set; }
    }
}