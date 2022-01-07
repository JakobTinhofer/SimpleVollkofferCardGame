using System;
using System.Collections.Generic;
using System.Text;

namespace LightBlueFox.Games.Vollkoffer
{
    /// <summary>
    /// Defines a way of filtering cards. The check should return true if the card passes the filter, or false if it doesn't.
    /// This check passes for every card that is not a joker.
    /// You can also use premade filters. Have a look at <see cref="EZFilters"/> if there is already a filter that does what you need it to do.
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public delegate bool CardFilter(Card c);

    /// <summary>
    /// A collection of handy <see cref="CardFilter"/>-tests
    /// </summary>
    public static class EZFilters
    {
        /// <summary>
        /// True for all cards
        /// </summary>
        public readonly static CardFilter AllCards = (Card c) => { return true; };
        /// <summary>
        /// True for all cards bigger than <see cref="CardValues.Ten"/>
        /// </summary>
        public static bool OnlyFiguren(Card c)
        {
            return c.Value > CardValues.Ten;
        }
    }
}
