namespace MiniGameSharp.Math
{
    public struct Line
    {
        public float X1 { get; }
        public float Y1 { get; }
        public float X2 { get; }
        public float Y2 { get; }

        public float XMin { get; }
        public float YMin { get; }
        
        public float XMax { get; }
        public float YMax { get; }
        
        public Line(float x1, float y1, float x2, float y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            
            XMin = System.Math.Min(X1, X2);
            YMin = System.Math.Min(Y1, Y2);
            
            XMax = System.Math.Max(X1, X2);
            YMax = System.Math.Max(Y1, Y2);
        }
    }
}