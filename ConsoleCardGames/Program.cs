using System;
using LightBlueFox.Games.Vollkoffer;

namespace ConsoleCardGames
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            GameManager m = new GameManager(1);
            m.AddPlayer(new ConsolePlayer("Jakob", 0, m));
            m.AddPlayer(new ConsolePlayer("Andreas", 1, m));
            m.AddPlayer(new ConsolePlayer("Simon", 2, m));
            m.AddPlayer(new ConsolePlayer("Marianne", 3, m));

            m.StartGame();
        }
    }
}
