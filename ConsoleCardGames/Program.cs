using System;
using LightBlueFox.Games.Vollkoffer;

namespace ConsoleCardGames
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(new Card(CardSuits.Diamonds, CardValues.Ace));
        }
    }
}
