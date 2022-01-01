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
            foreach (var corner in box1.Corners)
            {
                if (IsCollision(corner, box2))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsCollision(Vector v, BoundingBoxPolygon box)
        {
            // Is point within polygon?
            var leftIntersections = 0;

            foreach (var line in box.Lines)
            {
                if (line.YMax < v.Y || line.YMin > v.Y)
                {
                    continue;
                }

                if (line.XMax < v.X)
                {
                    leftIntersections++;
                } 
                else if (line.XMin > v.X)
                {
                    // Do nothing, it is enough to track the intersections to one side of the point
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