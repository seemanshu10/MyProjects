using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCard : MonoBehaviour
{
    CardFlipper flipper;
    CardFace cardFace;
    int cardIndex = 0;
    
    public GameObject card;

    private void Awake()
    {
        cardFace = card.GetComponent<CardFace>();
        flipper = card.GetComponent<CardFlipper>();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 28), "Hit Me!"))
        {
            if (cardIndex >= cardFace.faces.Length)
            {
                cardIndex = 0;
                flipper.FlipCard(cardFace.faces[cardFace.faces.Length - 1], cardFace.cardBack, -1);
            }
            else
            {
                if(cardIndex > 0)
                {
                    flipper.FlipCard(cardFace.faces[cardIndex - 1], cardFace.faces[cardIndex], cardIndex);
                }
                else
                {
                    flipper.FlipCard(cardFace.cardBack, cardFace.faces[cardIndex], cardIndex);
                }
                cardIndex++;
            }
        }
    }
}
