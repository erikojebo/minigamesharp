using MiniGameSharp.Math;
using NUnit.Framework;

namespace MiniGameSharp.Tests
{
    public class VectorTests
    {
        [Test]
        public void Dot_product_works()
        {
            var v1 = new Vector(5, 12);
            var v2 = new Vector(-6, 8);
            
            Assert.AreEqual(66, v1.DotProduct(v2));
        }  
    }
}