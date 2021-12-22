using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using MiniGameSharp.Shapes;

namespace MiniGameSharp
{
    public class Game
    {
        private readonly GameForm _form;
        private readonly List<Shape> _shapes = new();
        private bool _isShuttingDown;

        protected Game()
        {
            _form = new GameForm(OnPaint);
        }
        
        public Color BackgroundColor { get; set; }

        public int Width
        {
            get => _form.Width;
            set => _form.Width = value;
        }
        
        public int Height
        {
            get => _form.Height;
            set => _form.Height = value;
        }
        
        public int Left
        {
            get => _form.Left;
            set => _form.Left = value;
        }
        
        public int Top
        {
            get => _form.Top;
            set => _form.Top = value;
        }
        
        public void Run()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            var thread = new Thread(StartGameLoop);
            thread.Start(cancellationToken);

            _form.Closed += (o,e) =>
            {
                Console.WriteLine("Shutting down...");
                _isShuttingDown = true;
                cancellationTokenSource.Cancel();
            };
            
            Application.Run(_form);
        }

        private void StartGameLoop(object cancellationTokenObj)
        {
            var cancellationToken = (CancellationToken)cancellationTokenObj;

            var stopwatch = new Stopwatch();
            
            OnStart();
            
            stopwatch.Start();

            long oldTime = 0;
            long newTime = 0;
            long deltaTime = 0;
            double accumulator = 0;
            double timePerFrame = 1000 / 60.0;

            int frameCounter = 0;
            int updatesCounter = 0;
            long counterAccumulator = 0;
            
            while (!cancellationToken.IsCancellationRequested)
            {
                newTime = stopwatch.ElapsedMilliseconds;
                deltaTime = newTime - oldTime;
                oldTime = newTime;
                accumulator += deltaTime;

                counterAccumulator += deltaTime;
                frameCounter += 1;
                
                if (counterAccumulator > 1000)
                {
                    Console.WriteLine("FPS:" + frameCounter + "\tUpdates per second: " + updatesCounter);
                    frameCounter = 0;
                    updatesCounter = 0;
                    counterAccumulator = 0;
                }

                var shouldRender = false;
                
                while(accumulator > timePerFrame)
                {
                    shouldRender = true;
                    
                    PerformUpdate();
                    accumulator -= timePerFrame;
                    updatesCounter += 1;
                }

                if (shouldRender)
                {
                    if (_form.IsHandleCreated)
                    {
                        _form.BeginInvoke((Action)_form.Refresh);
                    }
                }

                Application.DoEvents();
            }
        }

        protected virtual void OnStart()
        {
        }

        private void PerformUpdate()
        {
            foreach (var shape in _shapes)
            {
                shape.X += shape.Velocity.X;
                shape.Y += shape.Velocity.Y;
            }
            
            OnUpdate();
        }
        protected virtual void OnUpdate()
        {
        }

        private void OnPaint(Graphics g)
        {
            g.Clear(BackgroundColor);
            
            foreach (var shape in _shapes)
            {
                shape.Render(g);
            }
        }

        public void AddShape(Shape shape)
        {
            _shapes.Add(shape);
        }

        public bool IsKeyDown(Key key)
        {
            if (_form.IsDisposed || _isShuttingDown)
                return false;

            try
            {
                bool isKeyDown = false;
                _form.Invoke((Action)(() => isKeyDown = Keyboard.IsKeyDown(key)));
            
                return isKeyDown;
            }
            catch (Exception)
            {
                if (!_form.IsDisposed && !_isShuttingDown)
                    Console.Write("Could not read IsKeyDown");
                
                return false;
            }
        }
    }
}