using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBlueFox.Games.Vollkoffer
{
    public class PlayerInfo
    {
        public string Name { get; private set; }

        public int Score { get; private set; }

        public int PlayerID { get; private set; } 

        public PlayerPositions? Position { get; private set; }

        public PlayerInfo(string name, int score, int pid, PlayerPositions? pos)
        {
            Name = name;
            Score = score;
            PlayerID = pid;
        }

        public static PlayerInfo FromPlayer(Player p)
        {
            if (p == null) return null;
            return new PlayerInfo(p.Name, p.Score, p.PlayerID, p.Position);
        }

        public static PlayerInfo[] FromPlayer(IEnumerable<Player> ps)
        {
            PlayerInfo[] res = new PlayerInfo[ps.Count()];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = FromPlayer(ps.ElementAt(i));
            }
            return res;
        }

        public static explicit operator PlayerInfo(Player p) { return FromPlayer(p); }
    }
}
