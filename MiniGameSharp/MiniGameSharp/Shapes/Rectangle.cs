﻿using System.Drawing;

namespace MiniGameSharp.Shapes
{
    public class Rectangle : Shape
    {
        public Rectangle()
        {
            
        }

        public Rectangle(int x, int y, int width, int height, Color color)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            
            SetColor(color);
        }
        
        public float Width { get; set; }
        public float Height { get; set; }
        
        public override void Render(Graphics graphics)
        {
            var xCenter = X + Width / 2;
            var yCenter = Y + Height / 2;
            graphics.TranslateTransform(xCenter, yCenter);
            graphics.RotateTransform(Angle);
            graphics.FillRectangle(Brush, -Width / 2, -Height / 2, Width, Height);
        }

    }
}