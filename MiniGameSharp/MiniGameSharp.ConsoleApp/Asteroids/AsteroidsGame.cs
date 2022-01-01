using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Input;
using MiniGameSharp.Math;
using MiniGameSharp.Shapes;
using Rectangle = MiniGameSharp.Shapes.Rectangle;

namespace MiniGameSharp.ConsoleApp.Asteroids
{
    public class AsteroidsGame : Game
    {
        private Triangle _shipTriangle;
        private readonly Vector _upVector = new(0, -1);
        private readonly List<Rectangle> _asteroids = new();
        private readonly List<GameObject> _boundsWrapObjects = new();

        private Random _random = new();

        public AsteroidsGame()
        {
            BackgroundColor = Color.Black;
            Left = 0;
            Top = 0;
            Width = 1000;
            Height = 900;
        }

        protected override void OnStart()
        {
            _shipTriangle = new Triangle(Width / 2, Height / 2, 20, 40, Color.White);
            _boundsWrapObjects.Add(_shipTriangle);

            AddShape(_shipTriangle);

            _shipTriangle.Velocity = _upVector.Scale(0);

            for (int i = 0; i < 10; i++)
            {
                var asteroidRectangle = new Rectangle(_random.Next(Width), _random.Next(Height), 40, 40, Color.WhiteSmoke);

                var directionVector = new Vector(
                    _random.Next(-100, 100) / 100f,
                    _random.Next(-100, 100) / 100f);

                asteroidRectangle.Velocity = directionVector.Scale(2);

                AddShape(asteroidRectangle);

                _asteroids.Add(asteroidRectangle);
                _boundsWrapObjects.Add(asteroidRectangle);
            }
        }

        protected override void OnUpdate()
        {
            if (IsKeyDown(Key.Left))
            {
                _shipTriangle.Angle -= 5;
            }

            if (IsKeyDown(Key.Right))
            {
                _shipTriangle.Angle += 5;
            }

            if (IsKeyDown(Key.Up))
            {
                var thrustVectorScale = 0.20f;
                var thrustVector = _upVector.Rotate(_shipTriangle.Angle).Scale(thrustVectorScale);

                _shipTriangle.Velocity = _shipTriangle.Velocity.Add(thrustVector).CapLength(7);
            }

            foreach (var asteroid in _asteroids)
            {
                if (asteroid.HasCollidedWith(_shipTriangle))
                {
                    Pause();
                }
            }

            foreach (var obj in _boundsWrapObjects)
            {
                WrapBounds(obj);
            }
        }

        private void WrapBounds(GameObject asteroid)
        {
            var bounds = asteroid.BoundingBox();
            
            if (asteroid.X > Width)
            {
                asteroid.X = -bounds.Width;
            }

            if (asteroid.X + bounds.Width < 0)
            {
                asteroid.X = Width;
            }

            if (asteroid.Y > Height)
            {
                asteroid.Y = -bounds.Height;
            }

            if (asteroid.Y + bounds.Height < 0)
            {
                asteroid.Y = Height;
            }
        }
    }
}