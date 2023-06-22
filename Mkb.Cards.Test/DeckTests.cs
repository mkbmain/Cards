using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Mkb.Cards.Enums;
using Shouldly;
using Xunit;

namespace Mkb.Cards.Test
{
    public class DeckTests
    {
        [Fact]
        public void Ensure_by_default_we_get_cards_back_in_order_submitted()
        {
            var cards = new[]
            {
                new Card(CardValue.Ace, CardSuit.Club), new Card(CardValue.Two, CardSuit.Club), new Card(CardValue.Ten, CardSuit.Diamond),
                new Card(CardValue.Four, CardSuit.Spade)
            };

            var deck = new Deck(cards);

            for (int i = 0; i < cards.Length; i++)
            {
                var card = cards[i];
                deck.Draw().ShouldBe(card);
            }
        }

        [Fact]
        public void Ensure_if_we_draw_last_card_event_is_fired()
        {
            var cards = new[] {new Card(CardValue.Ace, CardSuit.Club), new Card(CardValue.Eight, CardSuit.Club)};

            var deck = new Deck(cards);
            bool eventFired = false;
            deck.LastCardDrawnEvent += deck1 => { eventFired = true; };
            deck.Draw();
            eventFired.ShouldBeFalse();
            deck.Draw();
            eventFired.ShouldBeTrue();
        }

        [Fact]
        public void Ensure_if_we_shuffle_cards_are_in_different_order()
        {
            var cards = Functions.GetCards().ToArray();
            var deck = new Deck(cards);
            deck.Shuffle();
            var cardsReturned = new List<Card>();
            for (int i = 0; i < cards.Length; i++)
            {
                cardsReturned.Add(deck.Draw());
            }

            cards.Length.ShouldBe(cardsReturned.Count);
            cards.All(t => cardsReturned.Contains(t)).ShouldBeTrue();
            Dictionary<int, int> values = new Dictionary<int, int>();
            for (var index = 0; index < cardsReturned.Count; index++)
            {
                var e = cards[index];
                values.Add(index, cardsReturned.IndexOf(e));
            }

            values.Select(t => t.Key == t.Value).Any(r => r == false).ShouldBeTrue();
        }
    }
}