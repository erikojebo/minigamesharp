using MiniGameSharp.Collisions;
using MiniGameSharp.Math;
using NUnit.Framework;

namespace MiniGameSharp.Tests
{
    public class CollisionDetectorTests
    {
        private BoundingBoxPolygon _unrotatedRectangle;
        private BoundingBoxPolygon _triangle;

        [SetUp]
        public void SetUp()
        {
            _unrotatedRectangle = new BoundingBoxPolygon(
                new[]
                {
                    new Vector(1, 1),
                    new Vector(2, 1),
                    new Vector(2, 2),
                    new Vector(1, 2)
                }
            );
            
            _triangle = new BoundingBoxPolygon(
                new[]
                {
                    new Vector(1, 1),
                    new Vector(3, 1),
                    new Vector(2, 3),
                }
            );
        }

        [Test]
        public void Point_inside_rectangle_collides()
        {
            AssertCollision(true, new Vector(1.5f, 1.5f), _unrotatedRectangle);
        }

        [Test]
        public void Point_with_y_within_rectangle_bounds_but_x_outside_of_bounds_is_not_collision()
        {
            AssertCollision(false, new Vector(2.5f, 1.5f), _unrotatedRectangle);
        }
        
        [Test]
        public void Point_exactly_on_rectangle_edge_collides()
        {
            AssertCollision(true, new Vector(1f, 1.5f), _unrotatedRectangle);
        }
        
        [Test]
        public void Point_with_x_within_rectangle_bounds_but_y_outside_of_bounds_is_not_collission()
        {
            AssertCollision(false, new Vector(1.5f, 2.5f), _unrotatedRectangle);
        }
        
        [Test]
        public void Point_inside_triangle_collides()
        {
            AssertCollision(true, new Vector(2, 2), _triangle);
        }

        [Test]
        public void Point_outside_triangle_but_inside_rectangular_area_is_not_collision()
        {
            AssertCollision(false, new Vector(1.9f, 2.9f), _triangle);
        }
        
        private static void AssertCollision(bool expected, Vector point, BoundingBoxPolygon box)
        {
            Assert.AreEqual(expected, CollisionDetector.IsCollision(point, box));
        }
    }
}