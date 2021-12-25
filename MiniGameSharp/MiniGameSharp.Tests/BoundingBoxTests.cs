using MiniGameSharp.Math;
using NUnit.Framework;

namespace MiniGameSharp.Tests
{
    public class BoundingBoxTests
    {
        [Test]
        public void Can_get_corners_for_unrotated_box_without_offset()
        {
            var box = new BoundingBox(0, 0, 1, 2);

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
            
        }
    }
}