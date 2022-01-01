using MiniGameSharp.Math;
using MiniGameSharp.Shapes;

namespace MiniGameSharp.ConsoleApp.Asteroids.GameObjects
{
    public class Ship : Polygon
    {
        public Ship(float centerX, float centerY, params Vector[] cornersRelativeToCenter) : 
            base(centerX, centerY, cornersRelativeToCenter)
        {
        }
    }
}