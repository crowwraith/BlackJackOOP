using BlackJackOOP.Enums;
using BlackJackOOP.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BlackJackOOP
{
    public partial class Form1 : Form
    {
        private List<Player> players = new List<Player>();
        private Deck deck = new Deck();
        private List<FlowLayoutPanel> panels = new List<FlowLayoutPanel>();
        private Dealer dealer = new Dealer();
        private FlowLayoutPanel dealerPanel;
        public Form1()
        {
            InitializeComponent();
        }

        // Aantal spelers selecteren
        private void playerbutton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int aantalSpelers = Convert.ToInt32(btn.Tag);

            // Oude panels opruimen
            foreach (var pnl in panels)
            {
                this.Controls.Remove(pnl);
                pnl.Dispose();
            }
            panels.Clear();
            players.Clear();

            for (int i = 0; i < aantalSpelers; i++)
            {
                // speler toevoegen
                players.Add(new Player());

                // hoofdpanel per speler
                FlowLayoutPanel spelerPanel = new FlowLayoutPanel();
                spelerPanel.Width = 400;
                spelerPanel.Height = 150;
                spelerPanel.Top = 20 + i * 170;
                spelerPanel.Left = 20;
                spelerPanel.BackColor = Color.LightGray;
                spelerPanel.BorderStyle = BorderStyle.FixedSingle;
                spelerPanel.FlowDirection = FlowDirection.TopDown;
                spelerPanel.Visible = false; // nog verbergen

                // label bovenaan
                Label spelerLabel = new Label();
                spelerLabel.Text = $"Speler {i + 1}";
                spelerLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                spelerLabel.AutoSize = true;
                spelerLabel.ForeColor = Color.Black;
                spelerPanel.Controls.Add(spelerLabel);

                // nested panel voor de eerste hand
                FlowLayoutPanel handPanel = new FlowLayoutPanel();
                handPanel.FlowDirection = FlowDirection.LeftToRight;
                handPanel.Width = 400;
                handPanel.Height = 130;
                handPanel.BackColor = Color.Transparent;
                spelerPanel.Controls.Add(handPanel);

                this.Controls.Add(spelerPanel);
                panels.Add(spelerPanel);
            }
        }

        // Spel starten
        private void Gamestart_Click(object sender, EventArgs e)
        {
            if (players == null || players.Count == 0)
            {
                MessageBox.Show("Selecteer eerst het aantal spelers!");
                return;
            }

            StartGame();
            DealInitialCards();
            RenderCards();
        }

        // Setup spel en UI
        private void StartGame()
        {
            deck = new Deck(); // nieuw deck
            button1.Visible = false;
            flowLayoutPanel1.Visible = false;
            label1.Visible = false;

            // Dealer panel maken
            dealerPanel = new FlowLayoutPanel();
            dealerPanel.Width = 400;
            dealerPanel.Height = 150;
            dealerPanel.Top = 20;
            dealerPanel.Left = 500;
            dealerPanel.BackColor = Color.DarkRed;
            dealerPanel.BorderStyle = BorderStyle.FixedSingle;
            dealerPanel.FlowDirection = FlowDirection.TopDown;

            // label
            Label dealerLabel = new Label();
            dealerLabel.Text = "Dealer";
            dealerLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dealerLabel.ForeColor = Color.White;
            dealerLabel.AutoSize = true;

            dealerPanel.Controls.Add(dealerLabel);

            // kaarten panel
            FlowLayoutPanel handPanel = new FlowLayoutPanel();
            handPanel.FlowDirection = FlowDirection.LeftToRight;
            handPanel.Width = 400;
            handPanel.Height = 130;
            handPanel.BackColor = Color.Transparent;

            dealerPanel.Controls.Add(handPanel);

            this.Controls.Add(dealerPanel);
        }

        // Kaarten uitdelen (data, nog geen UI)
        private void DealInitialCards()
        {
            // spelers
            foreach (var player in players)
            {
                Hand hand = player.Hands[0];
                hand.Cards.Clear();
                hand.AddCard(deck.DrawCard());
                hand.AddCard(deck.DrawCard());
            }

            // dealer
            dealer.Hand.Cards.Clear();

            dealer.AddCard(deck.DrawCard());

            Card hidden = deck.DrawCard();
            hidden.Flip(); // 👈 belangrijk
            dealer.AddCard(hidden);
        }

        // Kaarten tonen op scherm
        private void RenderCards()
        {
            PrintDealerHand();
            for (int i = 0; i < players.Count; i++)
            {
                FlowLayoutPanel spelerPanel = panels[i];

                // nested handpanels ophalen (alleen de FlowLayoutPanels, niet het label)
                var handPanels = spelerPanel.Controls.OfType<FlowLayoutPanel>().ToList();

                for (int h = 0; h < players[i].Hands.Count; h++)
                {
                    Hand hand = players[i].Hands[h];

                    FlowLayoutPanel handPanel;

                    // als de hand nog geen panel heeft, maak een nieuwe (voor splits)
                    if (h < handPanels.Count)
                    {
                        handPanel = handPanels[h];
                    }
                    else
                    {
                        handPanel = new FlowLayoutPanel();
                        handPanel.FlowDirection = FlowDirection.LeftToRight;
                        handPanel.Width = 400;
                        handPanel.Height = 130;
                        handPanel.BackColor = Color.Transparent;
                        spelerPanel.Controls.Add(handPanel);
                    }

                    // oude kaarten verwijderen
                    handPanel.Controls.Clear();

                    // kaarten toevoegen
                    foreach (var card in hand.Cards)
                    {
                        ShowCard(card, handPanel);
                    }
                }

                // spelerpanel zichtbaar maken
                spelerPanel.Visible = true;
            }
        }

        // Kaart visualiseren
        private void ShowCard(Card card, FlowLayoutPanel panel)
        {
            PictureBox pb = new PictureBox();
            pb.Width = 80;
            pb.Height = 120;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            string path = Path.Combine(Application.StartupPath, "PNG-cards-1.3", card.GetImageFileName());

            if (File.Exists(path))
            {
                pb.Image = Image.FromFile(path);
            }
            else
            {
                pb.BackColor = Color.Green; // fallback
            }

            panel.Controls.Add(pb);
        }

        private void RenderDealer()
        {
            var handPanel = dealerPanel.Controls.OfType<FlowLayoutPanel>().First();
            handPanel.Controls.Clear();

            foreach (var card in dealer.Hand.Cards)
            {
                PictureBox pb = new PictureBox();
                pb.Width = 80;
                pb.Height = 120;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;

                string fileName = card.IsFaceDown
                    ? "card_back.png"
                    : card.GetImageFileName();

                string path = Path.Combine(Application.StartupPath, "PNG-cards-1.3", card.GetImageFileName());

                if (File.Exists(path))
                    pb.Image = Image.FromFile(path);
                else
                    pb.BackColor = Color.Black;

                handPanel.Controls.Add(pb);
            }
        }
        private void PrintDealerHand()
        {
            Debug.WriteLine("=== Dealer Hand ===");

            foreach (var card in dealer.Hand.Cards)
            {
                if (card.IsFaceDown)
                    Debug.WriteLine("Face down card");
                else
                    Debug.WriteLine($"{card.Rank} of {card.Suit}");
            }

            Debug.WriteLine("Totaal (zichtbaar): " + dealer.Hand.GetValue());
        }
        private void label1_ClientSizeChanged(object sender, EventArgs e)
        {
            // voorlopig leeg
        }
    }
}