using System;
using System.Collections.Generic;
using System.Drawing;
using MiniGameSharp.ConsoleApp.Asteroids.GameObjects;
using MiniGameSharp.Math;

namespace MiniGameSharp.ConsoleApp.Asteroids
{
    public class ObjectFactory
    {
        public const float ShipHeight = 40;
        public const float ShipWidth = 20;

        public static Asteroid CreateAsteroid(float x, float y, int minRadius, int maxRadius, int size, Random random)
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

            var asteroid = new Asteroid(x, y, size, corners.ToArray());

            // var asteroidRectangle = new Rectangle( 40, 40, Color.WhiteSmoke);

            var directionVector = new Vector(
                random.Next(-100, 100) / 100f,
                random.Next(-100, 100) / 100f);

            var asteroidSpeed = 2;
            asteroid.Velocity = directionVector.Scale(asteroidSpeed);

            return asteroid;
        }

        public static Ship CreateShip(float x, float y)
        {
            var ship = new Ship(x, y,
                new Vector(-ShipWidth / 2, ShipHeight * 1 / 3f),
                new Vector(ShipWidth / 2, ShipHeight * 1 / 3f),
                new Vector(0, -ShipHeight * 2 / 3f));

            ship.Velocity = new Vector(0, 0);

            return ship;
        }

        public static Bullet CreateBullet(Ship ship)
        {
            var gunPositionRelativeToCenterRotated = new Vector(0, ShipHeight * -2 / 3f)
                .Rotate(ship.Angle);

            var gunPositionAbsolute = gunPositionRelativeToCenterRotated
                .Add(new Vector(ship.X, ship.Y));

            var bullet = new Bullet(gunPositionAbsolute.X, gunPositionAbsolute.Y,
                1, 5, Color.WhiteSmoke);

            bullet.Angle = ship.Angle;
            
            var bulletSpeed = 7;
            bullet.Velocity = gunPositionRelativeToCenterRotated.Resize(bulletSpeed);

            return bullet;
        }
    }
}