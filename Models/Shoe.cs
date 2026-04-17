using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJackOOP.Models
{
    public class Shoe
    {
        public List<Card> Cards { get; private set; }
        // met hoeveel decks wil je spelen? -> antwoorden met knop aan het begin van de ronde, daarna aantal spelers vragen. 
        public Shoe(int numberOfDecks)
        {
            Cards = new List<Card>();
            for (int i = 0; i < numberOfDecks; i++)
            {
                Deck deck = new Deck();
                Cards.AddRange(deck.Cards);
            }
        }


    }
}
