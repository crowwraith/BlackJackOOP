using BlackJackOOP.Enums;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJackOOP.Models
{
    public class Hand
    {
        public List<Card> Cards { get; set; } = new List<Card>();

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        public int GetValue()
        {
            int total = 0;
            int aces = 0;

            foreach (var card in Cards)
            {
                total += card.Value;

                if (card.Rank == Rank.ACE)
                    aces++;
            }

            // kan 1 of 11 zijn
            while (total > 21 && aces > 0)
            {
                total -= 10;
                aces--;
            }

            return total;
        }
        public void PrintHand()
        {
            foreach (var card in Cards)
            {
                Debug.WriteLine($"{card.Rank} of {card.Suit}");
            }

            Debug.WriteLine("Totaal: " + GetValue());
        }
    }

}

