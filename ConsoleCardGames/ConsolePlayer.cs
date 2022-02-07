using LightBlueFox.Games.Vollkoffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCardGames
{
    internal class ConsolePlayer : Player
    {
        public override void DoTurn(RoundCardStack stack)
        {
            Console.WriteLine("It is your turn, " + Name + "!");
        }
    }
}
