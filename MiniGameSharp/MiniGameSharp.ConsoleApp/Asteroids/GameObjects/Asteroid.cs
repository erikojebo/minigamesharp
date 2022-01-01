using MiniGameSharp.Math;
using MiniGameSharp.Shapes;

namespace MiniGameSharp.ConsoleApp.Asteroids.GameObjects
{
    public class Asteroid : Polygon
    {
        public Asteroid(float centerX, float centerY, int size, params Vector[] cornersRelativeToCenter) 
            : base(centerX, centerY, cornersRelativeToCenter)
        {
            Size = size;
        }
        
        public int Size { get; }
    }
}