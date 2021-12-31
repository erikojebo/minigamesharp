using MiniGameSharp.Collisions;
using MiniGameSharp.Math;
using NUnit.Framework;

namespace MiniGameSharp.Tests
{
    public class CollisionDetectorTests
    {
        [Test]
        public void Point_inside_rectangle_collides()
        {
            var box = new BoundingBoxPolygon(
                new[]
                {
                    new Vector(1, 1),
                    new Vector(2, 1),
                    new Vector(2, 2),
                    new Vector(1, 2)
                }
            );
            
            Assert.IsTrue(CollisionDetector.IsCollision(new Vector(1.5f, 1.5f), box));
        }
    }
}