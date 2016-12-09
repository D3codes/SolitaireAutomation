using System.Threading;
using System.Windows.Automation;

namespace SolitaireAutomation
{
    internal class Card
    {
        Rank rank;
        Suit suit;
        AutomationElement ae;
        int numberInStack;

        public Card(string cardName, AutomationElement ae, int num)
        {
            string[] cardArray = cardName.Split(' ');
            setRank(cardArray[0]);
            setSuit(cardArray[2]);

            this.ae = ae;
            numberInStack = num;
        }

        public static Card emptySpace(AutomationElement ae)
        {
            return new Card("Joker of Hearts", ae, 0);
        }

        public void click()
        {
            InvokePattern ipClickCard = (InvokePattern)ae.GetCurrentPattern(InvokePattern.Pattern);
            ipClickCard.Invoke();
            Thread.Sleep(500);
        }

        public int getNumberInStack()
        {
            return numberInStack;
        }

        public Rank getRank()
        {
            return rank;
        }

        public Suit getSuit()
        {
            return suit;
        }

        public bool isBlack()
        {
            if(suit == Suit.CLUBS || suit == Suit.SPADES)
            {
                return true;
            }

            return false;
        }

        public string toString()
        {
            return(rank + " of " + suit);
        }

        private void setRank(string r)
        {
            switch (r)
            {
                case "Ace":
                    rank = Rank.ACE;
                    break;

                case "Two":
                    rank = Rank.TWO;
                    break;

                case "Three":
                    rank = Rank.THREE;
                    break;

                case "Four":
                    rank = Rank.FOUR;
                    break;

                case "Five":
                    rank = Rank.FIVE;
                    break;

                case "Six":
                    rank = Rank.SIX;
                    break;

                case "Seven":
                    rank = Rank.SEVEN;
                    break;

                case "Eight":
                    rank = Rank.EIGHT;
                    break;

                case "Nine":
                    rank = Rank.NINE;
                    break;

                case "Ten":
                    rank = Rank.TEN;
                    break;

                case "Jack":
                    rank = Rank.JACK;
                    break;

                case "Queen":
                    rank = Rank.QUEEN;
                    break;

                case "King":
                    rank = Rank.KING;
                    break;

                default:
                    rank = Rank.JOKER;
                    break;
            }
        }

        private void setSuit(string s)
        {
            switch(s)
            {
                case "Clubs":
                    suit = Suit.CLUBS;
                    break;

                case "Spades":
                    suit = Suit.SPADES;
                    break;

                case "Diamonds":
                    suit = Suit.DIAMONDS;
                    break;

                case "Hearts":
                    suit = Suit.HEARTS;
                    break;

                default:
                    break;
            }
        }
    }
}