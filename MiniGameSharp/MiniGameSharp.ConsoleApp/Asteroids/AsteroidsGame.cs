using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using MiniGameSharp.ConsoleApp.Asteroids.GameObjects;
using MiniGameSharp.Math;
using MiniGameSharp.Shapes;
using Color = System.Drawing.Color;

namespace MiniGameSharp.ConsoleApp.Asteroids
{
    public class AsteroidsGame : Game
    {
        private Ship _ship;
        private TextObject _messageText;
        private TextObject _scoreText;
        private int _score;
        private readonly Vector _upVector = new(0, -1);
        private readonly List<Asteroid> _asteroids = new();
        private readonly List<Bullet> _bullets = new();
        private readonly List<GameObject> _boundsWrapObjects = new();

        private Random _random = new();

        private bool _canFire = true;

        public AsteroidsGame()
        {
            BackgroundColor = Color.Black;
            Left = 0;
            Top = 0;
            Width = 1000;
            Height = 800;
        }

        protected override void OnStart()
        {
            var soundDirectory = Path.Combine(Environment.CurrentDirectory, "../../../../../Resources/Sounds");
            // AddSound($"{soundDirectory}/explosion.wav", "explosion");
            AddSound($"{soundDirectory}/pewpew_1.wav", "blaster");

            ResetObjects();
        }

        private void ResetObjects()
        {
            _asteroids.Clear();
            _bullets.Clear();
            _boundsWrapObjects.Clear();

            InitializeShip();
            InitializeAsteroids();
            InitializeTextObjects();
        }

        private void InitializeTextObjects()
        {
            _messageText = new TextObject(10, Height - 30, "", 12, Color.WhiteSmoke);
            AddGameObject(_messageText);

            _scoreText = new TextObject(10, 10, "", 12, Color.WhiteSmoke);
            AddGameObject(_scoreText);
            
            SetScore(0);
        }

        protected override void OnRestart()
        {
            ResetObjects();
        }

        private void InitializeAsteroids()
        {
            for (int i = 0; i < 10; i++)
            {
                var startPosition = new Vector(_random.Next(Width), _random.Next(Height));

                const int maxAsteroidRadius = 20;
                const int minAsteroidRadius = 10;

                var asteroid = ObjectFactory.CreateAsteroid(startPosition.X, startPosition.Y,
                    minAsteroidRadius, maxAsteroidRadius, 2, _random);

                AddAsteroid(asteroid);
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
            if (IsPaused && IsKeyDown(Key.R))
            {
                Reset();
            }

            if (IsPaused)
                return;

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

                    PlaySound("blaster");
                }
            }
            else
            {
                _canFire = true;
            }

            foreach (var asteroid in _asteroids.ToList())
            {
                if (asteroid.HasCollidedWith(_ship))
                {
                    Pause();

                    _messageText.Text = "Game Over. Press R to restart the game";
                }

                foreach (var bullet in _bullets.ToList())
                {
                    if (asteroid.HasCollidedWith(bullet))
                    {
                        IncrementScore(10);

                        _bullets.Remove(bullet);
                        _asteroids.Remove(asteroid);
                        RemoveGameObject(bullet);
                        RemoveGameObject(asteroid);

                        if (asteroid.Size > 1)
                        {
                            var asteroid1 = ObjectFactory.CreateAsteroid(asteroid.X, asteroid.Y, 5, 10, asteroid.Size - 1, _random);
                            var asteroid2 = ObjectFactory.CreateAsteroid(asteroid.X, asteroid.Y, 5, 10, asteroid.Size - 1, _random);

                            asteroid1.Velocity = asteroid.Velocity.Rotate(_random.Next(0, 180));
                            asteroid2.Velocity = asteroid.Velocity.Rotate(_random.Next(180, 360));

                            AddAsteroid(asteroid1);
                            AddAsteroid(asteroid2);
                        }
                    }
                }
            }

            foreach (var obj in _boundsWrapObjects)
            {
                WrapBounds(obj);
            }
        }

        private void IncrementScore(int scoreToAdd)
        {
            SetScore(_score + scoreToAdd);
        }
        
        private void SetScore(int newScore)
        {
            _score = newScore;
            _scoreText.Text = $"Score: {_score}";
        }

        private void AddAsteroid(Asteroid asteroid)
        {
            _asteroids.Add(asteroid);
            _boundsWrapObjects.Add(asteroid);

            AddGameObject(asteroid);
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