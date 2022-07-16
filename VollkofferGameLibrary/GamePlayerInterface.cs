using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBlueFox.Games.Vollkoffer
{
    public abstract class GamePlayerInterface
    {
        public int GameID { get; protected set; }

        public abstract PlayerInfo[] Players { get; }

        public event GameStartedHandler GameStarted;
        public event RoundConcludedHandler RoundConcluded;
        protected void InvokeGameStarted()
        {
            GameStarted?.Invoke(this);
        }
        protected void InvokeRoundConcluded()
        {
            RoundConcluded?.Invoke(this);
        }
    }

    public delegate void GameStartedHandler(GamePlayerInterface sender);
    public delegate void RoundConcludedHandler(GamePlayerInterface sender);
}
