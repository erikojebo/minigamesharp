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

        public static bool IsCollision(BoundingBoxPolygon box1, BoundingBoxPolygon box2)
        {
            return false;
        }

        public static bool IsCollision(Vector v, BoundingBoxPolygon box)
        {
            // Is point within polygon?

            var intersectionLineY = v.Y;

            var leftIntersections = 0;
            var rightIntersections = 0;

            foreach (var line in box.Lines)
            {
                if (line.YMax < v.Y || line.YMin > v.Y)
                {
                    continue;
                }

                // if (line.Y1 == v.Y || line.Y2 == v.Y)
                // {
                //     return true; // Is this a collision?
                // }

                if (line.XMax < v.X)
                {
                    leftIntersections++;
                } 
                else if (line.XMin > v.X)
                {
                    rightIntersections++;
                }
                else
                {
                    // Find intersection x between lines and check if it is left or right
                    var crossingPointOffset = (v.Y - line.Y1) * (line.X2 - line.X1) / (line.Y2 - line.Y1);
                    var crossingPointX = line.X1 + crossingPointOffset;

                    if (crossingPointX <= v.X)
                    {
                        leftIntersections++;
                    }
                }
            }

            return leftIntersections % 2 != 0;
        }
    }
}