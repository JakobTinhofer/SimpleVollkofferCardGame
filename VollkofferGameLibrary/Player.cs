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
        public PlayerHand Hand;
        /// <summary>
        /// Name of the player.
        /// </summary>
        public string Name;

       /// <summary>
       /// Next player is asked to play. 
       /// </summary>
       /// <param name="stack"> Cards that have already been played by all the players in this round.</param>

        public abstract void DoTurn(RoundCardStack stack);
    }
}
