using System.Drawing;
using Rectangle = MiniGameSharp.Shapes.Rectangle;

namespace MiniGameSharp.ConsoleApp.Asteroids.GameObjects
{
    public class Bullet : Rectangle
    {
        public Bullet(float x, float y, int width, int height, Color color)
            : base(x, y, width, height, color)
        {
            
        }
    }
}