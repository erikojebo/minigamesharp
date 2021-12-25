using MiniGameSharp.Math;
using MiniGameSharp.Shapes;

namespace MiniGameSharp.Collisions
{
    public class CollisionDetector
    {
        public static bool IsCollision(GameObject obj1, GameObject obj2)
        {
            var box1 = obj1.BoundingBox();
            var box2 = obj2.BoundingBox();

            return IsCollision(box1, box2);
        }

        public static bool IsCollision(BoundingBox box1, BoundingBox box2)
        {
            return false;
        }
    }
}