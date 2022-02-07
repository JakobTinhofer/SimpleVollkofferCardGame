using System;
using System.Collections.Generic;
using System.Text;

namespace LightBlueFox.Games.Vollkoffer
{
    /// <summary>
    /// Refers to the cards that have been played in this round
    /// </summary>
    public class RoundCardStack : CardCollection
    {
        /// <summary>
        /// Indicates the number of cards to be played (e.g. pairs or triples)
        /// </summary>
        public int? StackDimension;

        /// <summary>
        /// A collection containing one of each card played, sorted in descending order.
        /// </summary>
        public CardCollection RepresentativeCards;

        /// <summary>
        /// Let a player play some cards. This function also checks if those cards can be played, if not it throws an exception.
        /// </summary>
        /// <exception cref="ArgumentException">Given cards cannot be played in this round.</exception>
        public void TryPlayCards(params Card[] cards)
        {
            if(cards.Length > 4 || cards.Length == 0)
            {
                throw new ArgumentException("You can only play between 1 and 4 cards!");
            }
            
            if(StackDimension != null && cards.Length != StackDimension)
            {
                throw new ArgumentException("You can only play exactly " + StackDimension + " cards at a time!");
            }

            foreach (var card in cards)
            {
                if(card.Value != cards[0].Value)
                {
                    throw new ArgumentException("You can only play cards of same value!");
                }
            }

            if (StackDimension == null)
                StackDimension = cards.Length;

            this.Add(cards);

            RepresentativeCards.Add(cards[0]);
            RepresentativeCards.Sort((c1, c2) => { return c1.Value.CompareTo(c2.Value); });
        }
    }
}
