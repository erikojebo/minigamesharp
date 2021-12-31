using MiniGameSharp.Math;
using NUnit.Framework;

namespace MiniGameSharp.Tests.Math
{
    public class BoundingBoxTests
    {
        [Test]
        public void Can_get_corners_for_unrotated_box_without_offset()
        {
            var box = new BoundingBoxRectangle(0, 0, 1, 2);

            CollectionAssert.AreEquivalent(
                new []
                {
                    new Vector(0, 0),
                    new Vector(1, 0),
                    new Vector(1, 2),
                    new Vector(0, 2)
                }, box.Corners);
        }
        
        [Test]
        public void Can_get_corners_for_unrotated_box_with_offset()
        {
            var box = new BoundingBoxRectangle(10, 20, 1, 2);

            CollectionAssert.AreEquivalent(
                new []
                {
                    new Vector(10, 20),
                    new Vector(11, 20),
                    new Vector(11, 22),
                    new Vector(10, 22)
                }, box.Corners);
        }
        
        [Test]
        public void Can_get_corners_from_rotated_box_without_offset()
        {
            // (0,0) -> (-2,1)
            var box = new BoundingBoxRectangle(0, 0, 1, 2, 90);
 
            CollectionAssert.AreEquivalent(
                new []
                {
                    new Vector(0, 0),
                    new Vector(0, 1),
                    new Vector(-2, 1),
                    new Vector(-2, 0)
                }, box.Corners);
        }
        
        [Test]
        public void Can_get_corners_for_rotated_box_with_offset()
        {
            // (0,0) -> (-2,1)
            var box = new BoundingBoxRectangle(10, 20, 1, 2, 90);
 
            CollectionAssert.AreEquivalent(
                new []
                {
                    new Vector(10, 20),
                    new Vector(10, 21),
                    new Vector(8, 21),
                    new Vector(8, 20)
                }, box.Corners);
        }
    }
}