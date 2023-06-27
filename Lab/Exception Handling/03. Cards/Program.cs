using System;
using System.Collections.Generic;

namespace _03._Cards
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Card> validCards = new List<Card>();
            string[] cards = Console.ReadLine().Split(',', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < cards.Length; i++)
            {
                string[] currentCard = cards[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string currentCardFace = currentCard[0];
                string currentCardSuit = currentCard[1];
                try
                {
                    Card card = new Card(currentCardFace, currentCardSuit);
                    validCards.Add(card);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }

            Console.WriteLine(String.Join(" ", validCards));
        }
    }
    public class Card
    {
        private string cardFace;
        private string cardSuit;

        public Card(string cardFace, string cardSuit)
        {
            CardFace = cardFace;
            CardSuit = cardSuit;
        }

        public string CardFace
        {
            get { return cardFace; }
            private set
            {
                if (value == "2")
                {
                    cardFace = value;
                }
                else if (value == "3")
                {
                    cardFace = value;
                }
                else if (value == "4")
                {
                    cardFace = value;
                }
                else if (value == "5")
                {
                    cardFace = value;
                }
                else if (value == "6")
                {
                    cardFace = value;
                }
                else if (value == "7")
                {
                    cardFace = value;
                }
                else if (value == "8")
                {
                    cardFace = value;
                }
                else if (value == "9")
                {
                    cardFace = value;
                }
                else if (value == "10")
                {
                    cardFace = value;
                }
                else if (value == "J")
                {
                    cardFace = value;
                }
                else if (value == "Q")
                {
                    cardFace = value;
                }
                else if (value == "K")
                {
                    cardFace = value;
                }
                else if (value == "A")
                {
                    cardFace = value;
                }
                else
                {
                    throw new ArgumentException("Invalid card!");
                }
            }
        }
        public string CardSuit 
        {
            get => cardSuit;
            private set 
            {
                if (value == "S")
                {
                    cardSuit = value;
                }
                else if (value == "H")
                {
                    cardSuit = value;
                }
                else if (value == "D")
                {
                    cardSuit = value;
                }
                else if (value == "C")
                {
                    cardSuit = value;
                }
                else
                {
                    throw new ArgumentException("Invalid card!");
                }
            }
        }

        public override string ToString()
        {
            if (CardSuit is "S")
            {
                cardSuit = "\u2660";
            }
            else if (CardSuit is "H")
            {
                cardSuit = "\u2665";
            }
            else if (CardSuit is "D")
            {
                cardSuit = "\u2666";
            }
            else if (CardSuit is "C")
            {
                cardSuit = "\u2663";
            }

            return $"[{CardFace}{CardSuit}]";
        }
    }
}
