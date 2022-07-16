using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBlueFox.Games.Vollkoffer
{
    public sealed class RemotePlayer : Player
    {
        public RemotePlayer(string name, int ID, GamePlayerInterface inter) : base(name, ID, inter)
        {
        }

        public override Card[] DoTurn(TurnContext stack)
        {
            throw new NotImplementedException();
        }

        public override void PrepareTurn(PlayerInfo prevP)
        {
            throw new NotImplementedException();
        }
    }
}
