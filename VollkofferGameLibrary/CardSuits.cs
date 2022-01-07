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


        #region Overwrites and Operators
        public static bool operator ==(CardSuits s1, CardSuits s2)
        {
            return s1.SuitName == s2.SuitName;
        }

        public static bool operator !=(CardSuits s1, CardSuits s2)
        {
            return s1.SuitName == s2.SuitName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SuitName);
        }

        public override bool Equals(object obj)
        {
            if(obj.GetType() != typeof(CardSuits)) return false;

            return SuitName == ((CardSuits)obj).SuitName;
        }
        #endregion

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
