using BlackJackOOP.Enums;

public class Card
{
    public Rank Rank { get; }
    public Suit Suit { get; }

    private bool isFaceDown;
    public bool IsFaceDown => isFaceDown;

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
    public string GetImageFileName()
    {
        string rankString = Rank switch
        {
            Rank.ACE => "ace",
            Rank.JACK => "jack",
            Rank.QUEEN => "queen",
            Rank.KING => "king",
            _ => ((int)Rank).ToString() 
        };

        string suitString = Suit.ToString().ToLower(); 

        return $"{rankString}_of_{suitString}.png";
    }
}