using BlackJackOOP.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using BlackJackOOP.Models;
namespace BlackJackOOP.Models;

public class Deck
{
    public List<Card> Cards { get; private set; }

    public Deck()
    {
        Cards = new List<Card>();

        foreach (Rank rank in Enum.GetValues<Rank>())
        {
            foreach (Suit suit in Enum.GetValues<Suit>())
            {
                Cards.Add(new Card(rank, suit, false));
            }
        }

        Shuffle();
    }

    public void Shuffle()
    {
        Random rng = new Random();

        int n = Cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);

            Card temp = Cards[k];
            Cards[k] = Cards[n];
            Cards[n] = temp;
        }
    }

    public Card DrawCard()
    {
        if (Cards.Count == 0)
            throw new InvalidOperationException("Deck is leeg!");

        Card card = Cards[0];
        Cards.RemoveAt(0);

        return card;
    }
}