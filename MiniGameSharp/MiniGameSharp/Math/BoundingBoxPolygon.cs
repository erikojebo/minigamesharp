namespace MiniGameSharp.Math
{
    public class BoundingBoxPolygon
    {
        protected BoundingBoxPolygon()
        {
        }
        
        public BoundingBoxPolygon(Vector[] corners)
        {
            SetCorners(corners);
        }
        
        public Vector[] Corners { get; private set; }
        public Line[] Lines { get; private set; }
        
        public float MinX { get; private set; }
        public float MinY { get; private set; }

        public float MaxX { get; private set; }
        public float MaxY { get; private set; }
        
        public float OuterWidth { get; private set; }
        public float OuterHeight { get; private set; }
        
        protected void SetCorners(Vector[] corners)
        {
            Corners = corners;
            
            ProcessCornersAndGenerateLines();
        }

        private void ProcessCornersAndGenerateLines()
        {
            Lines = new Line[Corners.Length];
            
            for (int i = 0; i < Corners.Length; i++)
            {
                var corner = Corners[i];
                
                if (i == 0 || corner.X < MinX)
                    MinX = corner.X;
                
                if (i == 0 || corner.Y < MinY)
                    MinY = corner.Y;
                
                if (i == 0 || corner.X > MaxX)
                    MaxX = corner.X;
                
                if (i == 0 || corner.Y > MaxY)
                    MaxY = corner.Y;
                
                var nextIndex = (i + 1) % Corners.Length;
                
                Lines[i] = new Line(corner.X, corner.Y, Corners[nextIndex].X, Corners[nextIndex].Y);
            }

            OuterWidth = MaxX - MinX;
            OuterHeight = MaxY - MinY;
        }
    }
}