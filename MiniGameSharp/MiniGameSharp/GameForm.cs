using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniGameSharp
{
    public class GameForm : Form
    {
        private readonly Action<Graphics> _onPaintCallback;

        public GameForm(Action<Graphics> onPaintCallback)
        {
            _onPaintCallback = onPaintCallback;
            
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _onPaintCallback(e.Graphics);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }
    }
}