using System.Collections.Generic;
using System.Linq;

namespace Mkb.Cards
{
    public class Deck
    {
        private List<Card> _cards;
        private List<Card> _discarded = new List<Card>();

        public delegate void LastCardDrawn(Deck deck);

        public event LastCardDrawn LastCardDrawnEvent;

        public Deck(IEnumerable<Card> cards)
        {
            _cards = cards.ToList();
        }

        public Card Draw()
        {
            var card = _cards.FirstOrDefault();
            if (card != null)
                _discarded.Add(card);
            
            _cards = _cards.Skip(1).ToList();

            if (_cards.Count == 0 && card != null)
                LastCardDrawnEvent?.Invoke(this);
            
            return card;
        }
        
        public void Shuffle()
        {
            _cards.AddRange(_discarded);
            _discarded.Clear();
            ShuffleRemainingDeck();
        }

        public void ShuffleRemainingDeck()
        {
            _cards = _cards.OrderBy(t => Functions.Random.Next(0, 400)).ToList();
        }
        
        public static Deck BuildDeck() => new Deck(Functions.GetCards());

        public static Deck BuildShuffledDeck()
        {
            var deck = BuildDeck();
            deck.Shuffle();
            return deck;
        }
    }
}