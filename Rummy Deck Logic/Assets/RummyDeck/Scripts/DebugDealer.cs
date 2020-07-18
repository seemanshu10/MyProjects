using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDealer : MonoBehaviour
{
    public DeckShuffle dealer;
    public DeckShuffle player;

    int count;
    
    //int[] cards = new int[] {  }; // To test and force card onto the player
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 256, 28), "Hit Me!"))
        {
            player.Push(dealer.Pop());
        }

        //if (GUI.Button(new Rect(10, 10, 256, 28), "Hit Me!"))
        //{
        //    player.Push(cards[count++]);
        //}
    }

}
