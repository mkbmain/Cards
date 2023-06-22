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
    }
}