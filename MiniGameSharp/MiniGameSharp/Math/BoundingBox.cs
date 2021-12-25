﻿using System.Collections;
using System.Collections.Generic;

namespace MiniGameSharp.Math
{
    public class BoundingBox
    {
        public BoundingBox(float x, float y, float width, float height, float angle = 0)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Angle = angle;
        }

        public float X { get; }
        public float Y { get; }
        public float Width { get; }
        public float Height { get; }
        public float Angle { get; }

        public List<Vector> Corners
        {
            get
            {
                var rotatedVerticalEdge = new Vector(0, Height).Rotate(Angle).Add(X, Y);
                var rotatedHorizontalEdge = new Vector(Width, 0).Rotate(Angle).Add(X, Y);
                return new()
                {
                    new Vector(X, Y),
                    rotatedHorizontalEdge,
                    rotatedVerticalEdge,
                    rotatedVerticalEdge.Add(rotatedHorizontalEdge)
                };
            }
        }
    }
}