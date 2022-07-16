using LightBlueFox.Games.Vollkoffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LightBlueFox.Games.Vollkoffer
{
    /// <summary>
    /// Handles a single round of a vollkoffer game
    /// </summary>
    public class VollkofferRound
    {
        /// <summary>
        /// The time each player has to complete his turn. If this time runs out, the player automatically passes
        /// </summary>
        public static int TurnLimitMS = 40000;

        /// <summary>
        /// The time between turns.
        /// </summary>
        public static int TurnPauseMS = 10000;

        /// <summary>
        /// The maximum time given to players to prepare.
        /// </summary>
        public static int TurnPreparationTimeMS = 5000;

        /// <summary>
        /// The Players which are currently playing in this round.
        /// </summary>
        private List<Player> players;
        private RoundContext roundContext;

        #region Turn Status

        private int currentPlayer = 0;
        private int turnNumber = 0;        

        #endregion

        public VollkofferRound(List<Player> players)
        {
            // Currently, only round with 4 players are supported
            //if(players.Count != 4)
            //{
            //    throw new NotImplementedException();
            //}

            // Create a new deck with all cards
            CardDeck deck = new CardDeck();
            roundContext = new RoundContext(PlayerInfo.FromPlayer(players), 13);
            // Hand out the cards to all players
            foreach (var player in players)
            {
                // Everyone gets 13 cards, since 52 / 4 = 13
                PlayerHand hand = new PlayerHand();
                hand.AddRange(deck.PopRandom(13));
                player.StartRound(hand, roundContext);

                // If some players already have a position set, but others do not, throw an exception.
                if ((player.Position == null) != (players[0].Position == null))
                {
                    throw new ArgumentException("Either all players need to have a position, or none.");
                }
            }

            // If player already have positions set, sort them by position.
            if(players[0].Position != null)
            {
                players = players.OrderByDescending((p) => p.Position).ToList();
            }

            this.players = players.ToList();
        }

        /// <summary>
        /// Starts the round.
        /// </summary>
        public void PlayRound()
        {
            currentPlayer = 0;
            playTurns();
        }

        private void playTurns()
        {
            // Create a new stack for this round
            TurnCardStack stack = new TurnCardStack();
            
            // The index of the player who put in the highest card. If no one played, default to 0.
            int trickWinner = 0;
            Player p = null;

            // Every player gets a go
            for(int i = 0; i < players.Count; i++, currentPlayer++)
            {
                int playerCount = players.Count((p) => { return p != null; });

                // Wrap around. 
                if (currentPlayer == players.Count)
                    currentPlayer = 0;

                // Skip players who are already finished
                if (players[currentPlayer] == null) continue;

                var pBefore = p;
                // This is the player whose turn it is.
                p = players[currentPlayer];

                // Gives Player time to prepare turn (ie display some screen etc.)
                LetPlayerPrepareTurn(p, pBefore);

                // Create a new turn context with information about this turn
                TurnContext context = new TurnContext(stack.HighestCard, currentPlayer, turnNumber, stack.StackDimension);

                
                Task<Card[]> turn = Task.Run(() => { return p.DoTurn(context); });
                // Wait for one of two events: Either the player finishes his turn, or the turn limit is reached.
                Task first = Task.WhenAny(turn, Task.Delay(TurnLimitMS)).GetAwaiter().GetResult();

                // Check whether or the player finished their turn on their own, or their time ran out.
                if (first == turn)
                {
                    roundContext.CallCardPlayed((PlayerInfo)p, turn.Result);
                    // See if they actually played some cards
                    if(turn.Result != null)
                    {
                        // Add the cards to the stack
                        try
                        {
                            stack.PlayCards(p, turn.Result);
                            trickWinner = currentPlayer;

                            //Check if player's hand is empty after playing cards, which means he won
                            if (p.Hand.Count == 0)
                            {
                                playerCount -= 1;

                                //1st to finish -> President, 2nd to finish -> Sekretär, ...
                                p.EndRound((PlayerPositions)playerCount);

                                roundContext.CallPlayerFinished((PlayerInfo)p, (PlayerPositions)playerCount);

                                //Remove from active players
                                players[currentPlayer] = null;

                                // If there is only one player left, the round is finished
                                if (playerCount == 1)
                                {
                                    players.Find((p) => { return p != null; }).EndRound(PlayerPositions.Vollkoffer);
                                    return;
                                }
                            }

                            // If an ace has been played, skip all other players (since there is no higher card) 
                            if (stack.HighestCard.Value == CardValues.Ace)
                            {
                                break;
                            }
                        }
                        catch (ArgumentException ex)
                        {

                            Console.WriteLine("User played invalid cards: {0}. Ignoring..", ex.Message);
                        }
                    }
                }

            }

            // Last player to play is the next one to go
            currentPlayer = trickWinner;

            // Inform players that the trick has finished.
            roundContext.CallTrickFinished(PlayerInfo.FromPlayer(players[currentPlayer]), turnNumber);

            // The turn is finished
            turnNumber++;

            playTurns();
        }


        private void LetPlayerPrepareTurn(Player p, Player pBefore)
        {
            Task Prep = Task.Run(() => { p.PrepareTurn(PlayerInfo.FromPlayer(pBefore)); });
            Task.WaitAny(Prep, Task.Delay(TurnPreparationTimeMS));
        }
    }
}
