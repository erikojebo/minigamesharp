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

        protected void SetCorners(Vector[] corners)
        {
            Corners = corners;
            
            GenerateLines();
        }

        private void GenerateLines()
        {
            Lines = new Line[Corners.Length];
            
            for (int i = 0; i < Corners.Length; i++)
            {
                var nextIndex = (i + 1) % Corners.Length;
                
                Lines[i] = new Line(Corners[i].X, Corners[i].Y, Corners[nextIndex].X, Corners[nextIndex].Y);
            }
        }
    }
}