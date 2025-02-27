using System;
using System.Collections.Generic;
using System.Linq;
using Mkb.Cards.Enums;
using Shouldly;
using Xunit;

namespace Mkb.Cards.Test
{
    public class CardTests
    {
        [Theory]
        [InlineData(CardValue.Ace, CardSuit.Spade)]
        [InlineData(CardValue.Two, CardSuit.Diamond)]
        [InlineData(CardValue.Four, CardSuit.Club)]
        public void Ensure_we_can_create_a_card(CardValue value,CardSuit suit)
        {
            var card = new Card(value, suit);
 
            card.Value.ShouldBe(value);
            card.Suit.ShouldBe(suit);
        }

        [Theory]
        [InlineData(CardValue.Ace, CardSuit.Spade)]
        [InlineData(CardValue.Two, CardSuit.Diamond)]
        public void Ensure_cards_can_be_equal(CardValue value, CardSuit suit)
        {
            var card = new Card(value, suit);
            var card2 = new Card(value, suit);
            
            card.Equal(card2).ShouldBe(Equality.Equal);
        }
        
        [Fact]
        public void Ensure_cards_can_be_equal_value()
        {
            var card = new Card(CardValue.Ace, CardSuit.Diamond);
            var card2 = new Card(CardValue.Ace, CardSuit.Club);
            
            card.Equal(card2).ShouldBe(Equality.EqualValue);
        }
        [Fact]
        public void Ensure_cards_can_be_equal_Suit()
        {
            var card = new Card(CardValue.Two, CardSuit.Diamond);
            var card2 = new Card(CardValue.Ace, CardSuit.Diamond);
            
            card.Equal(card2).ShouldBe(Equality.EqualSuit);
        }
        
        [Fact]
        public void Ensure_ShuffledDeck_is_shuffled()
        {
            var cards = Functions.GetCards().ToArray();
            var deck = Deck.BuildShuffledDeck();
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
            var deck = Deck.BuildDeck();
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