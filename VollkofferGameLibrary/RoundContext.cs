using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBlueFox.Games.Vollkoffer
{
    public class RoundContext
    {
        private Dictionary<int, int> nrOfCardsByPID = new Dictionary<int, int>();

        public int GetNrOfCardsOfPlayer(int playerID)
        {
            return nrOfCardsByPID[playerID];
        }

        public int GetNrOfCardsOfPlayer(PlayerInfo p)
        {
            return nrOfCardsByPID[p.PlayerID];
        }

        public RoundContext(PlayerInfo[] players, int cardsPerPlayer)
        {
            foreach (var p in players)
            {
                nrOfCardsByPID.Add(p.PlayerID, cardsPerPlayer);
            }
        }

        public event CardPlayedHandler CardPlayed;
        public event PlayerFinishedHandler PlayerFinished;
        public event TrickFinishedHandler TrickFinished;

        internal void CallTrickFinished(PlayerInfo p, int nr)
        {
            TrickFinished?.Invoke(p, nr);
        }

        internal void CallPlayerFinished(PlayerInfo p, PlayerPositions pp)
        {
            PlayerFinished?.Invoke(p, pp);
        }
        internal void CallCardPlayed(PlayerInfo p, Card[] cards)
        {
            if(cards != null) nrOfCardsByPID[p.PlayerID] -= cards.Length;
            CardPlayed?.Invoke(p, cards);
        }
    }

    public delegate void CardPlayedHandler(PlayerInfo player, Card[] cards);
    public delegate void PlayerFinishedHandler(PlayerInfo player, PlayerPositions position);
    public delegate void TrickFinishedHandler(PlayerInfo trickWinner, int turnNumber);
}
