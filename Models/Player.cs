using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJackOOP.Models
{
    public class Player
    {
        public List<Hand> Hands { get; set; }

        public Player()
        {
            Hands = new List<Hand>();
            Hands.Add(new Hand()); // start met 1 hand
        }
    }
}
