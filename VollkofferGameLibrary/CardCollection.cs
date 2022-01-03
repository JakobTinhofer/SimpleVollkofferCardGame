using System;
using System.Collections.Generic;
using System.Text;

namespace LightBlueFox.Games.Vollkoffer
{
    /// <summary>
    /// This class stores an amount of <see cref="Card"/>. Behaves pretty much like an alias for <see cref="List{Card}"/> with card as parameter.
    /// </summary>
    public class CardCollection : List<Card>
    {
        #region Fields
        private static Random r = new Random();
        #endregion

        #region Access Methods
        /// <summary>
        /// Returns all cards in this list where the given <see cref="CardFilter"/> returns true. Does not modify this <see cref="CardCollection"/>.
        /// </summary>
        /// <param name="f">A function taking a <see cref="Card"/> as parameter and returning a <see cref="bool"/></param>
        public CardCollection GetByFilter(CardFilter f)
        {
            CardCollection c = new CardCollection();
            foreach (var item in this)
            {
                if (f(item))
                    c.Add(item);
            }
            return c;
        }

        /// <summary>
        /// Returns all cards in this list where the given <see cref="CardFilter"/> returns true and removes them from the collection. Modifies this <see cref="CardCollection"/>.
        /// </summary>
        /// <param name="f">A function taking a <see cref="Card"/> as parameter and returning a <see cref="bool"/></param>
        public CardCollection PopByFilter(CardFilter f)
        {
            CardCollection toRemove = GetByFilter(f);
            foreach (var item in toRemove)
            {
                Remove(item);
            }
            return toRemove;
        }

        /// <summary>
        /// Returns a random card and removes it from the collection. 
        /// </summary>
        public Card PopRandom()
        {
            int i = r.Next(0, Count);
            Card c = this[i];
            RemoveAt(i);
            return c;
        }
        /// <summary>
        /// Returns multiple random card and removes them from the collection. 
        /// </summary>
        public CardCollection PopRandom(int count)
        {
            CardCollection col = new CardCollection();
            for (int i = 0; i < count; i++)
            {
                col.Add(PopRandom());
            }
            return col;
        }

        #endregion

        #region Add overwrites
        /// <summary>
        /// Adds one or more cards to this collection.
        /// </summary>
        public void Add(params Card[] cards)
        {
            AddRange(cards);
        }

        /// <summary>
        /// Adds all cards from one ore more <see cref="CardCollection"/>s to this collection.
        /// </summary>
        /// <param name="ccs"></param>
        public void Add(params CardCollection[] ccs)
        {
            foreach(var col in ccs)
            {
                Add(col);
            }
        }

        #endregion
        #region Constructors
        /// <summary>
        /// Create an empty collection.
        /// </summary>
        public CardCollection() { }

        /// <summary>
        /// Create a collection from one or more cards.
        /// </summary>
        public CardCollection(params Card[] cs)
        {
            Add(cs);
        }

        /// <summary>
        /// Create a collction by joining all of the given collections.
        /// </summary>
        /// <param name="ccs"></param>
        public CardCollection(params CardCollection[] ccs)
        {
            Add(ccs);
        }
        #endregion
    }
}
