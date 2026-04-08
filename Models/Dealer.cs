using BlackJackOOP.Models;

public class Dealer
{
    public Hand Hand { get; set; } = new Hand();

    public void AddCard(Card card)
    {
        Hand.AddCard(card);
    }

    public void RevealHiddenCard()
    {
        if (Hand.Cards.Count > 1)
        {
            Hand.Cards[1].Flip(); // meestal 2e kaart face down
        }
    }

    public bool ShouldHit()
    {
        return Hand.GetValue() < 17;
    }

    public void PlayTurn(Deck deck)
    {
        // Eerst verborgen kaart omdraaien
        RevealHiddenCard();

        // Daarna kaarten trekken volgens regels
        while (ShouldHit())
        {
            AddCard(deck.DrawCard());
        }
    }
}