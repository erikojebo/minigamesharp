using System.Drawing;
using MiniGameSharp.Collisions;
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

        public abstract BoundingBoxPolygon BoundingBox();
        
        public bool HasCollidedWith(GameObject otherObject)
        {
            return CollisionDetector.IsCollision(BoundingBox(), otherObject.BoundingBox());
        }
    }
}