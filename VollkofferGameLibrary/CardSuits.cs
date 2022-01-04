using System;
using System.Collections.Generic;
using System.Text;

namespace LightBlueFox.Games.Vollkoffer
{
    /// <summary>
    /// Representations of the different suits: Hearts (♥), Clubs (♣), Spades (♠), Diamonds (♦) but also just Red or just Black (for <see cref="CardValues.Joker"/>)
    /// </summary>
    public class CardSuits
    {
        /// <summary>
        /// Everything you need to know about a specific suit.
        /// </summary>
        #region Card Suite Stuff
        /// <summary>
        /// The symbol representing this suit, like ♥ or ♣. May be zero if the card only specifies its color (e.g. its <see cref="Card.Value"/> is <see cref="CardValues.Joker"/>)
        /// </summary>
        public string SuitSymbol { get; private set; }
        /// <summary>
        /// The name of the suit, like Hearts, Spades or Red (if the card is a joker). In contrast to <see cref="CardSuits.SuitSymbol"/>, this is always defined.
        /// </summary>
        public string SuitName { get; private set; }
        /// <summary>
        /// Whether the card is red or black. Hearts and Diamonds are always red, while Clubs and Spades are black.
        /// </summary>
        public CardColor Color { get; private set; }
        private CardSuits(string s, string n, CardColor c) { SuitSymbol = s; SuitName = n; Color = c; }
        private CardSuits(CardColor c) { Color = c; SuitName = c.ToString(); }

        /// <summary>
        /// This is just <see cref="CardSuits.SuitName"/>.
        /// </summary>
        /// <returns>Returns <see cref="CardSuits.SuitName"/> of this suit.</returns>
        public override string ToString()
        {
            return SuitName;
        }
        #endregion

        #region Suits
        public readonly static CardSuits Hearts = new CardSuits("♥", "Hearts", CardColor.Red);
        public readonly static CardSuits Diamonds = new CardSuits("♦", "Diamonds", CardColor.Red);
        public readonly static CardSuits Clubs = new CardSuits("♣", "Clubs", CardColor.Black);
        public readonly static CardSuits Spades = new CardSuits("♠", "Spades", CardColor.Black);

        /// <summary>
        /// This card has no suit, but is red. This might indicate a joker, or a falsely created Card.
        /// </summary>
        public readonly static CardSuits Red = new CardSuits(CardColor.Red);
        /// <summary>
        /// This card has no suit, but is black. This might indicate a joker, or a falsely created Card.
        /// </summary>
        public readonly static CardSuits Black = new CardSuits(CardColor.Black);
        public static CardSuits[] AllSuits = {Hearts, Diamonds, Clubs, Spades}; 
        #endregion
    }

    /// <summary>
    /// Color of the card. Hearts and Diamonds are always red, while Clubs and Spades are black.
    /// </summary>
    public enum CardColor
    {
        Red,
        Black
    }
}
