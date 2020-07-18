using System.Collections.Generic;
using UnityEngine;

public class DeckShuffle : MonoBehaviour
{
    //list of cards
    List<int> cards;

    public bool createDeck;

    public bool HasCards
    {
        get { return cards != null && cards.Count > 0; }

    }

    public event CardEventHandler CardRemoved;
    public event CardEventHandler CardAdded;

    public int CardCount
    {
        get
        {
            if (cards == null)
                return 0;
            else
                return cards.Count;        
        }
    }


    public IEnumerable<int> GetCards()
    {
        foreach(int i in cards)
        {
            yield return i;
        }
    }

    
    private void Awake()
    {
        cards = new List<int>();
        if(createDeck)
        {
            Shuffle();
        }
            
    }

    public void Shuffle()
    {
        cards.Clear();

        for (int i =0;i<52;i++)
        {
            cards.Add(i);
        }

        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);//Random Value
            int temp = cards[k];  //Shuffle code
            cards[k] = cards[n];
            cards[n] = temp;
        }
    }

    public void Reset()
    {
        cards.Clear();
    }

    public int Pop()
    {
        int temp = cards[0];
        cards.RemoveAt(0);

        CardRemoved?.Invoke(this, new CardEventArgs(temp));

        return temp;
    }

    public void Push(int card)
    {
        cards.Add(card);

        CardAdded?.Invoke(this, new CardEventArgs(card));
    }

    public int HandValue()
    {
        int total = 0;
        int aces = 0;

        foreach(int card in GetCards())
        {
            int cardRank = card % 13;

            if(cardRank <=8)
            {
                cardRank += 2;
                total = total + cardRank;
            }
            else if(cardRank > 8 && cardRank < 12)
            {
                    cardRank = 10;
                    total = total + cardRank;
            }
            else
            {
                aces++;
            }
           
        }

        for(int i =0; i < aces; i++)
        {
            if(total + 11 <= 21)
            {
                total = total + 11;
            }
            else
            {
                total = total + 1;
            }
        }
  
        return total;
    }
}
