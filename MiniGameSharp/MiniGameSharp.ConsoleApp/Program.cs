using System;
using MiniGameSharp.ConsoleApp.Asteroids;

namespace MiniGameSharp.ConsoleApp
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var game = new AsteroidsGame();
            
            game.Run();
        }
    }
}