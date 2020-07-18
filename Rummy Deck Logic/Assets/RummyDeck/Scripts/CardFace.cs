using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFace : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    
    public Sprite[] faces;
    public Sprite cardBack;

    public int cardIndex; //eg. face[cardIndex]

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ToggleFace(bool showFace)
    {
        if(showFace)
        {
            //show the card
           spriteRenderer.sprite = faces[cardIndex];
        }
        else
        {
            //showFace the card back
            spriteRenderer.sprite = cardBack;
        }
    }

}
