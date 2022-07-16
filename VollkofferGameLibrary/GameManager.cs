using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBlueFox.Games.Vollkoffer
{
    public class GameManager : GamePlayerInterface
    {
        private List<Player> players = new List<Player>();
        public GameManager(int id)
        {
            GameID = id;
        }

        public override PlayerInfo[] Players { get => PlayerInfo.FromPlayer(players); }

        public void AddPlayer(Player p)
        {
            players.Add(p);
        }

        public void StartGame()
        {
            InvokeGameStarted();

            while (true)
            {
                VollkofferRound r = new VollkofferRound(players);
                

                // Here you need to exchange the cards 
                

                r.PlayRound();

                InvokeRoundConcluded();
            }
        }
    }
}
