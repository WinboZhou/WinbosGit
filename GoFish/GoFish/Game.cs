using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoFish
{
    class Game
    {
        private List<Player> players;
        private Dictionary<Values, Player> books;
        private Deck stock;
        private TextBox textBoxOnTheForm;
        public Game(string playerName,IEnumerable<string> opponentNames,TextBox textBoxOnTheForm)
        {
            Random random = new Random();
            this.textBoxOnTheForm = textBoxOnTheForm;
            players = new List<Player>();
            players.Add(new Player(playerName, random, textBoxOnTheForm));
            foreach(string name in opponentNames)
            {
                players.Add(new Player(name, random, textBoxOnTheForm));
            }
            books = new Dictionary<Values, Player>();
            stock = new Deck();
            Deal();
            players[0].SortHand();
        }
        private void Deal()
        {
            stock.Shuffle();
            players[0].AskForACard(players,0,stock);
            foreach(Player player in players)
            {
                List<Values> value = new List<Values>();
                value.AddRange(player.PullOutBooks());
                for(int i = 0;i<value.Count;i++)
                    books.Add(value[i], player);
            }
        }
        public bool PlayOneRound(int selectedPlayerCard)
        {

            return true;
        }
    }
}
