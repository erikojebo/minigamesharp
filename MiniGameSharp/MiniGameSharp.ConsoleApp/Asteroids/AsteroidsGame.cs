using System.Drawing;
using System.Windows.Input;
using MiniGameSharp.Math;
using MiniGameSharp.Shapes;

namespace MiniGameSharp.ConsoleApp.Asteroids
{
    public class AsteroidsGame : Game
    {
        private Triangle _shipTriangle;
        private readonly Vector _upVector = new(0, -1);
        private Vector _velocity;

        protected override void OnStart()
        {
            _shipTriangle = new Triangle(100, 100, 20, 40, Color.White);
            AddShape(_shipTriangle);

            _velocity = _upVector;
            
            BackgroundColor = Color.Black;
            Left = 0;
            Top = 0;
            Width = 1000;
            Height = 900;
        }

        protected override void OnUpdate()
        {
            _shipTriangle.X += _velocity.X;
            _shipTriangle.Y += _velocity.Y;

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
                var thrustVectorScale = 0.25f;
                var thrustVector = _upVector.Rotate(_shipTriangle.Angle).Scale(thrustVectorScale);

                _velocity = _velocity.Add(thrustVector).CapLength(7);
            }

            if (_shipTriangle.X > Width)
            {
                _shipTriangle.X = -_shipTriangle.Width;
            }

            if (_shipTriangle.X + _shipTriangle.Width < 0)
            {
                _shipTriangle.X = Width;
            }

            if (_shipTriangle.Y > Height)
            {
                _shipTriangle.Y = -_shipTriangle.Height;
            }

            if (_shipTriangle.Y + _shipTriangle.Height < 0)
            {
                _shipTriangle.Y = Height;
            }
        }
    }
}