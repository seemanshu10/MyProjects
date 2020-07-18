using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(DeckShuffle))]
public class ViewCard : MonoBehaviour
{
    DeckShuffle deckShuffle;
    Dictionary<int,CardView> fetchCards;

    int lastCount;
    
    public Vector3 start;
    public float cardOffset;
    public bool faceUp = false;
    public bool reverseLayerOrder = false;
    public GameObject cardPrefab;

    public void Toggle(int card,bool isFaceUp)
    {
        fetchCards[card].IsFaceUp = isFaceUp;
    }
    
    public void Clear()
    {
        deckShuffle.Reset();

        foreach (CardView view in fetchCards.Values) 
        {
            Destroy(view.Card);
        }
        fetchCards.Clear();
    }

    private void Awake()
    {
        fetchCards = new Dictionary<int, CardView>();
        deckShuffle = GetComponent<DeckShuffle>();
        ShowCards();
        lastCount = deckShuffle.CardCount;

        deckShuffle.CardRemoved += DeckShuffle_CardRemoved;
        deckShuffle.CardAdded += DeckShuffle_CardAdded;
    }

    private void DeckShuffle_CardAdded(object sender, CardEventArgs e)
    {
        float co = cardOffset * deckShuffle.CardCount;
        Vector3 temp = start + new Vector3(co, 0f);
        AddCard(temp, e.CardIndex, deckShuffle.CardCount);
    }

    private void DeckShuffle_CardRemoved(object sender, CardEventArgs e)
    {
        if(fetchCards.ContainsKey(e.CardIndex))
        {
            Destroy(fetchCards[e.CardIndex].Card);
            fetchCards.Remove(e.CardIndex);
        }
    }


    private void Update()
    {
        ShowCards();
        //if (lastCount != deckShuffle.CardCount)
        //{
        //    lastCount = deckShuffle.CardCount;
        //    ShowCards();
        //}
    }

    public void ShowCards()
    {
        int cardCount = 0;
        if (deckShuffle.HasCards)
        {
            foreach (int i in deckShuffle.GetCards())
            {
                float co = cardOffset * cardCount;

                Vector3 temp = start + new Vector3(co, 0f);

                AddCard(temp, i, cardCount);
                cardCount++;

            }
        }
        
    }

    void AddCard(Vector3 position, int cardIndex,int positionalIndex)
    {
        
        if(fetchCards.ContainsKey(cardIndex))
        {
            if(!faceUp)
            {
                CardFace face = fetchCards[cardIndex].Card.GetComponent<CardFace>();
                face.ToggleFace(fetchCards[cardIndex].IsFaceUp);
            }
            return;
        }

        GameObject cardCopy = (GameObject)Instantiate(cardPrefab);
        cardCopy.transform.position = position;

        CardFace cardFace = cardCopy.GetComponent<CardFace>();
        cardFace.cardIndex = cardIndex;
        cardFace.ToggleFace(faceUp);

        SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();
        if(reverseLayerOrder)
        {
            spriteRenderer.sortingOrder = 51 -positionalIndex;
        }
        else
        {
            spriteRenderer.sortingOrder = positionalIndex;
        }

        fetchCards.Add(cardIndex,new CardView(cardCopy));

        //Debug.Log("Hand Value :" + deckShuffle.HandValue());
    }

}
