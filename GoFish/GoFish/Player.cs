using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoFish
{
    class Player
    {
        private string name;
        public string Name { get { return name; } }
        private Random random;
        private Deck cards;
        private TextBox textBoxOnForm;
        public Player(String name, Random random,TextBox textBoxOnForm)
        {
            this.name = name;
            this.random = random;
            textBoxOnForm.Text += this.name + " just joined the game.\r\n";
            this.textBoxOnForm = textBoxOnForm;
        }
        public IEnumerable<Values> PullOutBooks()
        {
            List<Values> books = new List<Values>();
            for(int i = 1; i < 14; i++)
            {
                Values value = (Values)i;
                int howMany = 0;
                for (int card = 0; card < cards.Count; card++)
                    if (cards.Peek(card).Value == value)
                        howMany++;
                if (howMany==4)
                {
                    books.Add(value);
                    cards.PullOutValues(value);
                }
            }
            return books;
        }
        public Values GetRandomValue()
        {
            List<Values> valueList = new List<Values>();
            for(int i=0;i<cards.Count;i++)
                valueList.Add(cards.Peek(i).Value);
            for(int j=1;j<valueList.Count;j++)
            {
                for(int k = 0; k < j; k++)
                {
                    if (valueList[j] == valueList[k])
                    {
                        valueList.RemoveAt(j);
                        j--;
                    }
                }
            }
            Random random = new Random();
            int randomvalue = random.Next(valueList.Count);
            return valueList[randomvalue];
        }
        public Deck DoYouHaveAny(Values value)
        {
            textBoxOnForm.Text += name + " has " + cards.PullOutValues(value).Count.ToString() + Card.Plural(value);
            return cards.PullOutValues(value);
        }
        public void AskForACard(List<Player> players,int myIndex,Deck stock,Values value)
        {
            textBoxOnForm.Text += players[myIndex] + " asks if anyone has a " + value.ToString();
            List<int> valueCount = new List<int>();
            for(int i = 0; i < players.Count; i++)
            {
                Deck deckvalue = players[i].DoYouHaveAny(value);
                
                if (i != myIndex)
                {
                    if (deckvalue.Count == 0)
                    {
                        valueCount.Add(deckvalue.Count);
                    }
                    else
                    {
                        for (int j = 0; j < deckvalue.Count; j++)
                            players[myIndex].cards.Add(deckvalue.Peek(j));
                    }
                }
            }
            if(valueCount.Count == (players.Count - 1))
            {
                players[myIndex].cards.Add(stock.Deal());
            }
        }
        public void AskForACard(List<Player> players, int myIndex, Deck stock)
        {
            if (stock.Count == 52)
            {
                for (int i = 0; i < players.Count; i++)
                {

                    for (int j = 0; j < 5; j++)
                        players[i].cards.Add(stock.Deal());

                }
            }   
            else
            {
                Values RanValue = players[myIndex].GetRandomValue();
                players[myIndex].AskForACard(players, myIndex, stock, RanValue);
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].cards.Count == 0)
                    {
                        if (stock.Count >= 5)
                        {
                            for (int j = 0; j < 5; j++)
                                players[i].cards.Add(stock.Deal());
                        }
                        else
                        {
                            int l = stock.Count;
                            for (int k = 0; k < l; k++)
                                players[i].cards.Add(stock.Deal());
                        }
                    }
                }
            }
        }
        public int CardCount { get { return cards.Count; } }
        public void TakeCard(Card card) { cards.Add(card); }
        public IEnumerable<string> GetCardNames() { return cards.GetCardNames(); }
        public Card Peek(int cardNumber) { return cards.Peek(cardNumber); }
        public void SortHand() { cards.SortByValue(); }
    }
}
