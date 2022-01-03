using System;
using System.Collections.Generic;
using System.Text;

namespace LightBlueFox.Games.Vollkoffer
{
    /// <summary>
    /// Represents a certain playing card, like the four of diamonds. Does not have to be unique
    /// </summary>
    public abstract class Card
    {
        /// <summary>
        /// The deck this card is from.
        /// </summary>
        public CardDeck OwnerDeck { get; private set; }

        /// <summary>
        /// The suit of the card, like <see cref="CardSuits.Clubs"/> or <see cref="CardSuits.Hearts"/>
        /// </summary>
        public CardSuits Suit { get; private set; }

        /// <summary>
        /// The value / number of the card, like <see cref="CardValues.Eight"/> or <see cref="CardValues.Queen"/>
        /// </summary>
        public CardValues Value { get; private set; }

        /// <summary>
        /// Create a new card of a certain suit and value.
        /// </summary>
        /// <param name="s">The suit of the card, like <see cref="CardSuits.Clubs"/> or <see cref="CardSuits.Hearts"/></param>
        /// <param name="v">The value / number of the card, like <see cref="CardValues.Eight"/> or <see cref="CardValues.Queen"/></param>
        public Card(CardSuits s, CardValues v)
        {
            Suit = s; Value = v;
        }
    }
}
