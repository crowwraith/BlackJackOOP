using BlackJackOOP.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using BlackJackOOP.Models;
namespace BlackJackOOP.Models;
public class Deck
{
    public List<Card> cards { get; private set; }

    public Deck()

    {
        cards = new List<Card>();

        foreach (Rank rank in Enum.GetValues<Rank>())
        {
            foreach (Suit suit in Enum.GetValues<Suit>())
            {
                cards.Add(new Card(rank, suit, false));
            }
        }
    }
}