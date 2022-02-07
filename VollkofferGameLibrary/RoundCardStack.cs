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
        public int StackDimension;
    }
}
