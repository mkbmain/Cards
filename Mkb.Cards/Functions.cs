using System;
using System.Collections.Generic;
using Mkb.Cards.Enums;

namespace Mkb.Cards
{
    public static class Functions
    {
        internal static Random Random = new System.Random(Guid.NewGuid().GetHashCode());

        public static IEnumerable<Card> GetCards()
        {
            var cards = new List<Card>();
            foreach (var suitName in Enum.GetNames(typeof(CardSuit)))
            {
                foreach (var cardValue in Enum.GetNames(typeof(CardValue)))
                {
                    cards.Add(new Card((CardValue) Enum.Parse(typeof(CardValue), cardValue), (CardSuit) Enum.Parse(typeof(CardSuit), suitName)));
                }
            }

            return cards;
        }

        public static Deck BuildDeck()
        {
           return new Deck(GetCards());
        }
        
        public static Deck BuildShuffledDeck()
        {
            var deck = BuildDeck();
            deck.Shuffle();
            return deck;
        }
    }
}