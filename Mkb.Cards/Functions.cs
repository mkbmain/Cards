using System;
using System.Collections.Generic;
using System.Linq;
using Mkb.Cards.Enums;

namespace Mkb.Cards
{
    public static class Functions
    {
        internal static readonly Random Random = new Random(Guid.NewGuid().GetHashCode());

        public static IEnumerable<Card> GetCards() => Enum.GetValues(typeof(CardSuit)).Cast<CardSuit>()
            .SelectMany(suitName =>
                Enum.GetValues(typeof(CardValue)).Cast<CardValue>()
                    .Select(cardValue => new Card(cardValue, suitName)));
    }
}