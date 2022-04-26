using System;
using System.Collections.Generic;
using System.Linq;
using Mkb.Cards.Enums;
using Shouldly;
using Xunit;

namespace Mkb.Cards.Test
{
    public class FunctionTests
    {
        [Fact]
        public void Ensure_Get_Cards_returns_all_cards()
        {
            var cards = Functions.GetCards();
            foreach (var suitName in Enum.GetNames(typeof(CardSuit)))
            {
                foreach (var cardValue in Enum.GetNames(typeof(CardValue)))
                {
                    var card = new Card((CardValue) Enum.Parse(typeof(CardValue), cardValue), (CardSuit) Enum.Parse(typeof(CardSuit), suitName));

                    cards.Any(t => t.Equal(card) == Equality.Equal).ShouldBeTrue();
                }
            }

            cards.Count().ShouldBe(52);
        }

        [Fact]
        public void Ensure_ShuffledDeck_is_shuffled()
        {
            var cards = Functions.GetCards().ToArray();
            var deck = Functions.BuildShuffledDeck();
            var cardsReturned = new List<Card>();
            for (int i = 0; i < cards.Length; i++)
            {
                cardsReturned.Add(deck.Draw());
            }

            cards.Length.ShouldBe(cardsReturned.Count);

            Dictionary<int, int> values = new Dictionary<int, int>();
            for (var index = 0; index < cardsReturned.Count; index++)
            {
                var e = cards[index];
                values.Add(index, cardsReturned.IndexOf(cardsReturned.FirstOrDefault(t => e.Equal(t) == Equality.Equal)));
            }

            values.Select(t => t.Key == t.Value).Any(r => r == false).ShouldBeTrue();
        }

        [Fact]
        public void Ensure_a_deck_contains_all_cards()
        {
            var deck = Functions.BuildDeck();
            var cards = new List<Card>();
            Card card = null;
            do
            {
                card = deck.Draw();
                if (card != null)
                {
                    cards.Add(card);
                }
            } while (card != null);

            foreach (var suitName in Enum.GetNames(typeof(CardSuit)))
            {
                foreach (var cardValue in Enum.GetNames(typeof(CardValue)))
                {
                    var testCard = new Card((CardValue) Enum.Parse(typeof(CardValue), cardValue), (CardSuit) Enum.Parse(typeof(CardSuit), suitName));

                    cards.Any(t => t.Equal(testCard) == Equality.Equal).ShouldBeTrue();
                }
            }

            cards.Count().ShouldBe(52);
        }
    }
}