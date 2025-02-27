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
    }
}