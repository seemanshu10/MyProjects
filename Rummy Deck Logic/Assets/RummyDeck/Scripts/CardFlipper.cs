using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlipper : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    CardFace card;

    public AnimationCurve scaleCurve;
    public float duration = 0.5f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        card = GetComponent<CardFace>();
    }

    public void FlipCard(Sprite startImage, Sprite endImage,int cardIndex)
    {
        StopCoroutine(Flip(startImage, endImage, cardIndex)); // to stop the player smashing the hit me button 
        StartCoroutine(Flip(startImage, endImage, cardIndex));
    }

    IEnumerator Flip(Sprite startImage, Sprite endImage, int cardIndex)
    {
        spriteRenderer.sprite = startImage;

        float time = 0f;
        while(time <=1f)
        {
            float scale = scaleCurve.Evaluate(time);
            time = time + Time.deltaTime /duration;

            Vector3 localScale = transform.localScale;
            localScale.x = scale; 
            transform.localScale = localScale;

            if (time >= 0.5f)
            {
                spriteRenderer.sprite = endImage;
            }

            yield return new WaitForFixedUpdate();
        }

        if(cardIndex == 1)
        {
            card.ToggleFace(false);
        }
        else
        {
            card.cardIndex = cardIndex;
            card.ToggleFace(true);
        }
    }
}
