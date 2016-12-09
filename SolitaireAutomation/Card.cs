namespace SolitaireAutomation
{
    internal class Card
    {
        Rank rank;
        Suit suit;

        public Card(string cardName)
        {
            string[] cardArray = cardName.Split(' ');
            setRank(cardArray[0]);
            setSuit(cardArray[2]);
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