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
        private Polygon _ship;
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
            const float shipHeight = 40;
            const float shipWidth = 20;
            
            _ship = new Polygon(Width / 2f, Height / 2f,
                new Vector(-shipWidth / 2, shipHeight * 1/3f),
                new Vector(shipWidth / 2, shipHeight * 1/3f),
                new Vector(0, -shipHeight * 2/3f)); 
            
            // (Width / 2, Height / 2, 20, 40, Color.White);
            
            _boundsWrapObjects.Add(_ship);

            AddGameObject(_ship);

            _ship.Velocity = _upVector.Scale(0);

            for (int i = 0; i < 10; i++)
            {
                var asteroidRectangle = new Rectangle(_random.Next(Width), _random.Next(Height), 40, 40, Color.WhiteSmoke);

                var directionVector = new Vector(
                    _random.Next(-100, 100) / 100f,
                    _random.Next(-100, 100) / 100f);

                asteroidRectangle.Velocity = directionVector.Scale(2);

                AddGameObject(asteroidRectangle);

                _asteroids.Add(asteroidRectangle);
                _boundsWrapObjects.Add(asteroidRectangle);
            }
        }

        protected override void OnUpdate()
        {
            if (IsKeyDown(Key.Left))
            {
                _ship.Angle -= 5;
            }

            if (IsKeyDown(Key.Right))
            {
                _ship.Angle += 5;
            }

            if (IsKeyDown(Key.Up))
            {
                var thrustVectorScale = 0.20f;
                var thrustVector = _upVector.Rotate(_ship.Angle).Scale(thrustVectorScale);

                _ship.Velocity = _ship.Velocity.Add(thrustVector).CapLength(7);
            }

            foreach (var asteroid in _asteroids)
            {
                if (asteroid.HasCollidedWith(_ship))
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
                asteroid.X = -bounds.OuterWidth;
            }

            if (asteroid.X + bounds.OuterWidth < 0)
            {
                asteroid.X = Width;
            }

            if (asteroid.Y > Height)
            {
                asteroid.Y = -bounds.OuterHeight;
            }

            if (asteroid.Y + bounds.OuterHeight < 0)
            {
                asteroid.Y = Height;
            }
        }
    }
}