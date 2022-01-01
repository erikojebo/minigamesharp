using System;
using System.Collections.Generic;
using System.Windows.Input;
using MiniGameSharp.Math;
using MiniGameSharp.Shapes;
using Color = System.Drawing.Color;
using Rectangle = MiniGameSharp.Shapes.Rectangle;

namespace MiniGameSharp.ConsoleApp.Asteroids
{
    public class AsteroidsGame : Game
    {
        private Polygon _ship;
        private readonly Vector _upVector = new(0, -1);
        private readonly List<Polygon> _asteroids = new();
        private readonly List<Rectangle> _bullets = new();
        private readonly List<GameObject> _boundsWrapObjects = new();

        private Random _random = new();

        private bool _canFire = true;

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
            InitializeShip();
            InitializeAsteroids();
        }

        private void InitializeAsteroids()
        {
            for (int i = 0; i < 10; i++)
            {
                var startPosition = new Vector(_random.Next(Width), _random.Next(Height));

                const int maxAsteroidRadius = 20;
                const int minAsteroidRadius = 10;

                var asteroid = ObjectFactory.CreateAsteroid(startPosition.X, startPosition.Y,
                    minAsteroidRadius, maxAsteroidRadius, _random);

                AddGameObject(asteroid);

                _asteroids.Add(asteroid);
                _boundsWrapObjects.Add(asteroid);
            }
        }

        private void InitializeShip()
        {
            _ship = ObjectFactory.CreateShip(Width / 2f, Height / 2f);

            _boundsWrapObjects.Add(_ship);

            AddGameObject(_ship);
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

            if (IsKeyDown(Key.Space))
            {
                if (_canFire)
                {
                    var bullet = ObjectFactory.CreateBullet(_ship);

                    _bullets.Add(bullet);
                    AddGameObject(bullet);

                    _canFire = false;
                }
            }
            else
            {
                _canFire = true;
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