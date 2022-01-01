using System;
using System.Collections.Generic;
using MiniGameSharp.Math;
using MiniGameSharp.Shapes;

namespace MiniGameSharp.ConsoleApp.Asteroids
{
    public class ObjectFactory
    {
        public static Polygon CreateAsteroid(float x, float y, int minRadius, int maxRadius, Random random)
        {
            var cornerCount = random.Next(5, 10);

            var corners = new List<Vector>();

            var angle = 360f / cornerCount;
            for (int j = 0; j < cornerCount; j++)
            {
                var cornerRelativeToCenter = new Vector(random.Next(minRadius, maxRadius), 0)
                    .Rotate(angle * j);

                corners.Add(cornerRelativeToCenter);
            }

            var asteroid = new Polygon(x, y, corners.ToArray());

            // var asteroidRectangle = new Rectangle( 40, 40, Color.WhiteSmoke);

            var directionVector = new Vector(
                random.Next(-100, 100) / 100f,
                random.Next(-100, 100) / 100f);

            asteroid.Velocity = directionVector.Scale(2);

            return asteroid;
        }
    }
}