using BlackJackOOP.Enums;
using BlackJackOOP.Models;

namespace BlackJackOOP
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            Card card1 = new Card(Rank.KING, Suit.HEARTS, false);

            Deck deck = new Deck();

        }

        private void Gamestart_Click(object sender, EventArgs e)
        {

        }
        private void playerbutton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int nummer = Convert.ToInt32(btn.Tag);

            MessageBox.Show("Je klikte op knop " + nummer);
        }


        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            flowLayoutPanel1.Left = (this.ClientSize.Width - flowLayoutPanel1.Width) / 2;
            flowLayoutPanel1.Top = (this.ClientSize.Height - flowLayoutPanel1.Height) / 2;
        }
        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {
            flowLayoutPanel2.Left = (this.ClientSize.Width - flowLayoutPanel2.Width) / 2;
            flowLayoutPanel2.Top = 30;
        }
        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {
            flowLayoutPanel3.Left = (this.ClientSize.Width - flowLayoutPanel3.Width) / 2;
            int marge = (int)(this.ClientSize.Height * 0.20); // 20% marge vanaf de button zelf.
            flowLayoutPanel1.Top = this.ClientSize.Height - flowLayoutPanel1.Height - marge;
        }



        private void label1_ClientSizeChanged(object sender, EventArgs e)
        {
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = 10;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = 10;
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = 10;
        }

        private void None(object sender, EventArgs e)
        {

        }


    }
}
