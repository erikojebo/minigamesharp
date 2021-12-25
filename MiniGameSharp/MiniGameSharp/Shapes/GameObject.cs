using System.Drawing;
using MiniGameSharp.Math;

namespace MiniGameSharp.Shapes
{
    public abstract class GameObject 
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Angle { get; set; }

        public Vector Velocity { get; set; }
        
        public abstract void Render(Graphics graphics);

        public abstract BoundingBox BoundingBox();
    }
}