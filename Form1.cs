using BlackJackOOP.Enum;

namespace BlackJackOOP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Card card1 = new Card(Rank.KING, Suit.HEARTS, false);


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

    }
}
