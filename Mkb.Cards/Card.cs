using System;
using System.IO;
using System.Runtime.CompilerServices;
using Mkb.Cards.Enums;

[assembly: InternalsVisibleTo("Mkb.Cards.Test")]

namespace Mkb.Cards
{
    public class Card
    {
        private System.Drawing.Image _image = null;
        public System.Drawing.Image Image => _image ?? (_image = System.Drawing.Image.FromFile(GetFilePath()));
        public CardValue Value { get; }
        public CardSuit Suit { get; }
        
        public Card(CardValue cardValue, CardSuit suit)
        {
            Value = cardValue;
            Suit = suit;
        }

        public Equality Equal(Card card)
        {
            if (card == this || (card.Value == this.Value && card.Suit == this.Suit))
            {
                return Equality.Equal;
            }

            if (card.Value == this.Value)
            {
                return Equality.EqualValue;
            }

            return card.Suit == this.Suit ? Equality.EqualSuit : Equality.None;
        }
        
        private static string ImageNameFromValue(CardValue value)
        {
            switch (value)
            {
                case CardValue.Ace:
                    return "a";
                case CardValue.King:
                    return "k";
                case CardValue.Queen:
                    return "q";
                case CardValue.Jack:
                    return "j";
                default:
                    return ((int) value).ToString();
            }
        }

        internal string GetFilePath() => Path.Combine(Path.Combine(Environment.CurrentDirectory, "Cards"), $"{Suit.ToString().ToLower()}s-{ImageNameFromValue(Value)}-150.png");
    }
}