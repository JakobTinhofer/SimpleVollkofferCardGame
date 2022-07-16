using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightBlueFox.Games.Vollkoffer
{

    /// <summary>
    /// Refers to the cards that have been played in this round
    /// </summary>
    public class TurnCardStack : CardCollection
    {
        /// <summary>
        /// Indicates the number of cards to be played (e.g. pairs or triples)
        /// </summary>
        public int? StackDimension;

        /// <summary>
        /// Returns the highest card, or one of the highest if StackDimension is higher than 1.
        /// </summary>
        public Card HighestCard { 
            get {
                return Count == 0 ? null : this.Last();
            }
        }        

        internal void PlayCards(Player p, params Card[] cards)
        {
            try
            {
                ValidatePlayCards(StackDimension, HighestCard, p, cards);
                if (StackDimension == null)
                    StackDimension = cards.Length;
                foreach (var item in cards)
                {
                    p.Hand.Remove(item);
                }
                this.Add(cards);
                _sort();
            }
            catch (Exception)
            {
                throw;
            }   

            
        }

        public static void ValidatePlayCards(int? StackDimension, Card HighestCard, Player p, params Card[] cards)
        {
            if (cards.Length > 4 || cards.Length == 0)
            {
                throw new ArgumentException("You can only play between 1 and 4 cards!");
            }

            if (StackDimension != null && cards.Length != StackDimension)
            {
                throw new ArgumentException("You can only play exactly " + StackDimension + " cards at a time!");
            }

            foreach (var card in cards)
            {
                if (card.Value != cards[0].Value)
                {
                    throw new ArgumentException("You can only play cards of same value!");
                }
                if (!p.Hand.Contains(card))
                {
                    throw new ArgumentException("You cannot play a card you do not have!");
                }
            }

            if (HighestCard != null && cards[0].Value <= HighestCard.Value)
            {
                throw new ArgumentException("You cannot play this card, since it is not high enough!");
            }

            
        }

        private void _sort()
        {
            this.Sort((c1, c2) => { return c1.Value.CompareTo(c2.Value); });
        }

        public TurnCardStack() { }

        #region Serialization
        public TurnCardStack(byte[] bytes)
        {
            int count = bytes[0];
            for (int i = 1; i < count; i += 2)
            {
                Add(new Card(new byte[2] { bytes[i], bytes[i + 1] }));
            }
            _sort();
        }

        public byte[] Serialize()
        {
            byte[] bytes = new byte[1 + 2 * this.Count];
            bytes[0] = (byte)Count;
            for(int i = 0; i < this.Count; i += 2)
            {
                Array.Copy(this[i / 2].Serialize(), 0, bytes, 1 + i, 2);
            }
            return bytes;
        }
#endregion
    }
}
