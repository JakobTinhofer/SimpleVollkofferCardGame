using LightBlueFox.Games.Vollkoffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleCardGames
{
    internal class ConsolePlayer : Player
    {
        public ConsolePlayer(string name, int id, GamePlayerInterface interf) : base(name, id, interf) { 
        
        }

        public override Card[] DoTurn(TurnContext context)
        {
            Console.Clear();
            Console.WriteLine("It is your turn, " + Name + "!");
            DisplayStackInfo(context);
            Console.WriteLine("These are your cards: ");
            Hand.DisplayHand();
            
            return GetUserInput(context);
        }

        public override void PrepareTurn(PlayerInfo prevPlayer)
        {
            Console.Clear();
            if(prevPlayer != null)
            {
                if(prevPlayer.PlayerID == PlayerID && prevPlayer.Name == Name)
                {
                    Console.WriteLine("{0}, Since you played the highest card, you start this turn.", Name);
                }
                else
                {
                    Console.WriteLine("{0} finshed his turn.", prevPlayer.Name);
                }
            }
            Console.Write("{0}'s turn starts now. Press any key to continue...", Name);
            Console.ReadKey(true);
        }

        private void DisplayStackInfo(TurnContext c)
        {
            Console.Write("Top of the stack: ");
            if(c.StackDimension == null || c.HighestCard == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("No cards played yet.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = (ConsoleColor)new Random().Next(10, 15);
                for (int i = 0; i < c.StackDimension; i++)
                {
                    Console.Write("{0} ", (c.HighestCard.Value < CardValues.Ten ? ((int)c.HighestCard.Value).ToString() : c.HighestCard.Value.ToString()[0]));
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            
        }

        private Card[] GetUserInput(TurnContext c)
        {
            List<Card> result = new List<Card>();
            Console.WriteLine("Pick the cards to play: type the character in brackets above [e.g. (3)]. To play multiple cards, type multiple characters!");
            Console.Write("Cards: ");
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    input.Replace(" ", "");
                    input.ToLower();
                    if (input.Length > 4)
                    {
                        throw new ArgumentException("You cannot play more than 4 cards!");
                    }
                    else if (input.Length == 0) return null;
                    foreach (var chr in input)
                    {
                        var card = Hand.Find((c) => {

                            if (result.Contains(c)) return false;
                            
                            if (c.Value < CardValues.Ten)
                            {
                                return ((int)c.Value).ToString()[0] == chr;
                            }
                            else
                            {
                                return c.Value.ToString().ToLower()[0] == chr;
                            }
                        });

                        if (card == null) throw new ArgumentException("Could not find card '" + chr + "'!");
                        else result.Add(card);
                    }
                    TurnCardStack.ValidatePlayCards(c.StackDimension, c.HighestCard, this, result.ToArray());
                    return result.ToArray();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid input! Error: " + ex.Message);
                    result.Clear();
                }
                
            }
        }
        
    }

    internal static class Extensions
    {
        public static void DisplayHand(this PlayerHand hand)
        {
            hand.Sort((x, y) => { return x.Value.CompareTo(y.Value); });
            foreach (var card in hand)
            {
                Console.Write("(");
                Console.ForegroundColor = (ConsoleColor)new Random().Next(10, 15);
                Console.Write("{0}", (card.Value < CardValues.Ten ? ((int)card.Value).ToString() : card.Value.ToString()[0]));
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(") {0}", card.ToString());
            }
        }

        public static async Task DisplayTimer(int ms)
        {
            Console.Write("Your turn time... " + ms);
            await Task.Delay(ms);
            Console.WriteLine("Turn ended.");
        }
    }
}
