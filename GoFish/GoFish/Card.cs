﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFish
{
    class Card
    {
        public Suits Suit { get; set; }
        public Values Value { get; set; }
        public string Name
        { get
            {
                return Value + " of " + Suit;
            }
        }
        public Card(Suits suit, Values value)
        {
            Suit = suit;
            Value = value;
        }
        public static string Plural(Values value)
        {
            if (value == Values.Six)
                return "Sixes";
            else
                return value.ToString() + "s";
        }
 
    }
}
