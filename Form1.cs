using BlackJackOOP.Enums;
using BlackJackOOP.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BlackJackOOP
{
    public partial class Form1 : Form
    {
        private List<Player> players = new List<Player>();        // nooit null
        private Deck deck = new Deck();                            // direct een nieuw deck
        private List<FlowLayoutPanel> panels = new List<FlowLayoutPanel>();

        public Form1()
        {
            InitializeComponent();
        }

        // Event handler voor "Aantal spelers" buttons
        private void playerbutton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int aantalSpelers = Convert.ToInt32(btn.Tag);

            // Maak nieuw deck
            deck = new Deck();

            // Maak spelers en bijbehorende panelen
            players = new List<Player>();
            panels = new List<FlowLayoutPanel>();

            // verwijder oude dynamische panelen (als die er waren)
            foreach (var pnl in panels)
            {
                this.Controls.Remove(pnl);
                pnl.Dispose();
            }
            panels.Clear();

            for (int i = 0; i < aantalSpelers; i++)
            {
                // speler aanmaken
                players.Add(new Player());

                // dynamisch panel voor deze speler
                FlowLayoutPanel panel = new FlowLayoutPanel();
                panel.Width = 400;
                panel.Height = 130;
                panel.Top = 20 + i * 150; // verticale spacing
                panel.Left = 20;
                panel.BackColor = Color.LightGray;
                panel.BorderStyle = BorderStyle.FixedSingle;

                this.Controls.Add(panel);
                panels.Add(panel);

                // geef 2 kaarten aan speler
                Hand hand = players[i].Hands[0];
                hand.AddCard(deck.DrawCard());
                hand.AddCard(deck.DrawCard());

                // toon kaarten in panel
                foreach (var card in hand.Cards)
                {
                    ShowCard(card, panel);
                }
            }
        }

        // Event handler voor testknop "Gamestart"
        private void Gamestart_Click(object sender, EventArgs e)
        {
            if (players == null || players.Count == 0)
            {
                MessageBox.Show("Selecteer eerst het aantal spelers!");
                return;
            }

            // Voor test: toon alle spelers en hun kaarten in MessageBox
            string info = "";
            for (int i = 0; i < players.Count; i++)
            {
                info += $"Speler {i + 1}: ";
                foreach (var card in players[i].Hands[0].Cards)
                {
                    info += card.ToString() + ", ";
                }
                info += Environment.NewLine;
            }
            MessageBox.Show(info);
        }

        // Toon een kaart in een FlowLayoutPanel
        private void ShowCard(Card card, FlowLayoutPanel panel)
        {
            PictureBox pb = new PictureBox();
            pb.Width = 80;
            pb.Height = 120;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            // Path naar kaart image
            string path = Path.Combine(Application.StartupPath, "PNG-cards-1.3", card.GetImageFileName());

            if (File.Exists(path))
            {
                pb.Image = Image.FromFile(path);
            }
            else
            {
                // fallback: lege kleur als bestand niet gevonden
                pb.BackColor = Color.Green;
            }

            panel.Controls.Add(pb);
        }
    }
}