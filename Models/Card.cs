using BlackJackOOP.Enum;

public class Card
{
    public Rank Rank { get; }
    public Suit Suit { get; }

    private bool isFaceDown;

    public int Value
    {
        get
        {
            switch (Rank)
            {
                case Rank.JACK:
                case Rank.QUEEN:
                case Rank.KING:
                    return 10;

                case Rank.ACE:
                    return 11;

                default:
                    return (int)Rank;
            }
        }
    }

    public Card(Rank rank, Suit suit, bool isFaceDown)
    {
        Rank = rank;
        Suit = suit;
        this.isFaceDown = isFaceDown;
    }

    public void Flip()
    {
        isFaceDown = !isFaceDown;
    }

    public override string ToString()
    {
        if (isFaceDown)
            return "Card is face down";

        return $"{Rank} of {Suit}";
    }
}