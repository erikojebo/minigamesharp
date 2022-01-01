using System;
using System.Collections.Generic;
using System.Drawing;
using MiniGameSharp.Math;
using MiniGameSharp.Shapes;
using Rectangle = MiniGameSharp.Shapes.Rectangle;

namespace MiniGameSharp.ConsoleApp.Asteroids
{
    public class ObjectFactory
    {
        public const float ShipHeight = 40;
        public const float ShipWidth = 20;

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

        public static Polygon CreateShip(float x, float y)
        {
            var ship = new Polygon(x, y,
                new Vector(-ShipWidth / 2, ShipHeight * 1 / 3f),
                new Vector(ShipWidth / 2, ShipHeight * 1 / 3f),
                new Vector(0, -ShipHeight * 2 / 3f));

            ship.Velocity = new Vector(0, 0);

            return ship;
        }

        public static Rectangle CreateBullet(Polygon ship)
        {
            var gunPositionRelativeToCenterRotated = new Vector(0, ShipHeight * -2 / 3f)
                .Rotate(ship.Angle);

            var gunPositionAbsolute = gunPositionRelativeToCenterRotated
                .Add(new Vector(ship.X, ship.Y));

            var bullet = new Rectangle(gunPositionAbsolute.X, gunPositionAbsolute.Y,
                1, 5, Color.WhiteSmoke);

            bullet.Angle = ship.Angle;
            bullet.Velocity = gunPositionRelativeToCenterRotated.Resize(3);

            return bullet;
        }
    }
}