using System;
using System.Collections.Generic;
using System.Text;

namespace LightBlueFox.Games.Vollkoffer
{
    /// <summary>
    /// A class representing a deck of cards.
    /// </summary>
    public class CardDeck : CardCollection
    {
        /// <summary>
        /// Creates a deck with one of each possible cards
        /// </summary>
        public CardDeck() : this(EZFilters.AllCards)
        {
        
        }

        /// <summary>
        /// Creates a deck with the cards that pass the given filter.
        /// </summary>
        /// <param name="f">The filter for which cards to include.</param>
        public CardDeck(CardFilter f)
        {
            var vals = Enum.GetValues(typeof(CardValues));

            foreach (CardValues value in vals)
            {
                foreach (var suit in CardSuits.AllSuits)
                {
                 
                    Card c = new Card(suit, value);
                    if (f(c)) 
                    { 
                        Add(c); 
                    }
                }
            }
        }
    }
}
