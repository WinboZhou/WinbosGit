using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFish
{
    class Deck
    {
        private List<Card> cards;
        private Random random = new Random();

        public Deck()
        {
            cards = new List<Card>();
            for (int suit = 0; suit < 4; suit++)
                for (int value = 1; value < 14; value++)
                    cards.Add(new Card((Suits)suit, (Values)value));
        }
        public Deck(IEnumerable<Card> initialCards)
        {
            cards = new List<Card>(initialCards);
        }
        public int Count { get { return cards.Count; } }

        public void Add(Card cardToAdd)
        {
            cards.Add(cardToAdd);
        }
        public Card Deal(int index)
        {
            Card CardToDeal = cards[index];
            cards.RemoveAt(index);
            return CardToDeal;
        }
        public void Shuffle()
        {
            List<Card> CardsToShuffle = new List<Card>();
            int cardsCount = cards.Count;
            for (int i = 0; i < cardsCount; i++)
                CardsToShuffle.Add(cards[i]);
            cards.Clear();
            for(int i=0;i<cardsCount;i++)
            {
                int randomNumber = random.Next(cardsCount -1-i);
                cards.Add(CardsToShuffle[randomNumber]);
                CardsToShuffle.Remove(CardsToShuffle[randomNumber]);
            }
        }
        public IEnumerable<string> GetCardNames ()
        {
            int cardsCount = cards.Count;
            string[] CardNames = new string[cardsCount];
            for (int i = 0; i < cardsCount; i++)
                CardNames[i] = cards[i].Name;
            return CardNames;
        }
        public void SortBySuit()
        {
            cards.Sort(new CardComparer_bySuit());
        }
        public void SortByValue()
        {
            cards.Sort(new CardComparer_byValue());
        }
        public Card Peek(int cardNumber)
        {
            return cards[cardNumber];
        }
        public Card Deal()
        {
            return Deal(0);
        }
        public bool ContainsValue(Values value)
        {
            foreach(Card card in cards)
            {
                if (card.Value == value)
                    return true;
            }
            return false;
        }
        public Deck PullOutValues(Values value)
        {
            Deck deckToReturn = new Deck(new Card[] { });
            for (int i = cards.Count - 1; i >= 0; i--)
                deckToReturn.Add(Deal(i));
            return deckToReturn;
        }
        public bool HasBook(Values value)
        {
            int NumberOfCards = 0;
            foreach (Card card in cards)
                if (card.Value == value)
                    NumberOfCards++;
            if (NumberOfCards == 4)
                return true;
            else
                return false;
        }
    }
}
