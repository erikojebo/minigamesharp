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
            
            AssertCollision(true, new Vector(1.5f, 1.5f), box);
            AssertCollision(false, new Vector(2.5f, 1.5f), box);
            AssertCollision(true, new Vector(1f, 1.5f), box);
            AssertCollision(false, new Vector(2.5f, 2.5f), box);
            AssertCollision(false, new Vector(1.5f, 2.5f), box);
        }

        private static void AssertCollision(bool expected, Vector point, BoundingBoxPolygon box)
        {
            Assert.AreEqual(expected, CollisionDetector.IsCollision(point, box));
        }
    }
}