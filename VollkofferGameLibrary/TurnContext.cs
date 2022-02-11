using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBlueFox.Games.Vollkoffer
{
    public class TurnContext
    {
        #region Serializable Content
        public Card HighestCard { get; private set; }
        public int? StackDimension { get; private set; }
        public int PlayerIndex { get; private set; }
        public int RoundNumber { get; private set; }
        #endregion

        public TurnContext(Card hc, int pi, int rn) { 
            HighestCard = hc;
            PlayerIndex = pi;
            RoundNumber = rn;
        }

        #region Serialization

        public TurnContext(byte[] bytes)
        {
            if (bytes[0] == 0) StackDimension = null;
            else StackDimension = bytes[0];

            PlayerIndex = bytes[1];
            RoundNumber = bytes[2];

            if (bytes[3] == 0) HighestCard = null;
            else HighestCard = new Card(new byte[] { bytes[4], bytes[5] });
        }

        public byte[] Serialize()
        {
            byte[] bytes = new byte[6];
            
            if (StackDimension == null) bytes[0] = 0;
            else bytes[0] = (byte)StackDimension;

            bytes[1] = (byte)PlayerIndex;
            bytes[2] = (byte)RoundNumber;
            if(HighestCard != null)
            {
                bytes[3] = 1;
                Array.Copy(HighestCard.Serialize(), 0, bytes, 4, 2);
            }
            else
            {
                bytes[3] = 0;
            }
            return bytes;
        }

        #endregion
    }
}
