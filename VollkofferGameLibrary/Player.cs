using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBlueFox.Games.Vollkoffer
{
    /// <summary>
    /// One of the participants in the game.
    /// </summary>
    public abstract class Player
    {
        /// <summary>
        /// Cards in the hand of the player. 
        /// </summary>
        public PlayerHand Hand { get; private set; }

        /// <summary>
        /// The score of the player, indicating his performance in this lobby.
        /// </summary>
        public int Score { get; private set; }

        /// <summary>
        /// The ID of the player.
        /// </summary>
        public int PlayerID { get; private set; }

        /// <summary>
        /// Name of the player.
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// The rank of the player. Can be null if no round has been played yet.
        /// </summary>
        public PlayerPositions? Position { get; private set; }

        /// <summary>
        /// Gives time to prepare a your turn. The max timeout is <see cref="VollkofferRound.TurnPreparationTimeMS"/>.
        /// </summary>
        public abstract void PrepareTurn(PlayerInfo prevPlayer);

        /// <summary>
        /// The context of the current round. May be null if the round is not currently beeing played.
        /// </summary>
        protected RoundContext RoundContext { get; private set; }

        /// <summary>
        /// Called on round start.
        /// </summary>
        /// <param name="hand"></param>
        internal void StartRound(PlayerHand hand, RoundContext c)
        {
            Hand = hand;
            this.RoundContext = c;
        }

        /// <summary>
        /// Called on round end.
        /// </summary>
        internal void EndRound(PlayerPositions newPos)
        {
            Position = newPos;
            Score += (int)newPos;
        }



        /// <summary>
        /// Access more information about the current game, lobby and players within it.
        /// </summary>
        protected GamePlayerInterface Interface { get; private set; }

        /// <summary>
        /// Create a new Player
        /// </summary>
        /// <param name="name">The name of the player</param>
        /// <param name="ID">The id of the player</param>
        /// <param name="inter">The interface it is connected to.</param>
        public Player(string name, int ID, GamePlayerInterface inter)
        {
            Name = name;
            PlayerID = ID;
            Interface = inter;
        }

        /// <summary>
        /// Next player is asked to play. 
        /// </summary>
        /// <param name="stack"> Cards that have already been played by all the players in this round.</param>
        public abstract Card[] DoTurn(TurnContext stack);
    }
}
