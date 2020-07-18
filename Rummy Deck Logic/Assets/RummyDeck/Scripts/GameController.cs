using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int dealersFirstCard =-1;

    public DeckShuffle deck;
    public DeckShuffle player;
    public DeckShuffle player1;
    public DeckShuffle dealer;

    public Button hitButton;
    public Button stickButton;
    public Button playAgain;

    public Text winner;
    /*
     * cards dealt to players
     * First player hits/sticks/bust
     * Dealer's turn;must have minimum of 17 scor hand
     * Dealer's cards; first card ids hidden,subsequent cards are facing
     */

    #region public methods()
    public void Hit()
    {
        player.Push(deck.Pop());
        player1.Push(deck.Pop());
        if(player.HandValue()>21)
        { 
            hitButton.interactable = false;
            stickButton.interactable = false;
            StartCoroutine(DealersTurn());
        }

        if (player1.HandValue() > 21)
        {
            hitButton.interactable = false;
            stickButton.interactable = false;
            StartCoroutine(DealersTurn());
        }
    }  

    public void Stick()
    {
        hitButton.interactable = false;
        stickButton.interactable = false;
        StartCoroutine(DealersTurn());
    }

    public void PlayAgain()
    {
        playAgain.interactable = false;

        player.GetComponent<ViewCard>().Clear();
        player1.GetComponent<ViewCard>().Clear();
        dealer.GetComponent<ViewCard>().Clear();
        deck.GetComponent<ViewCard>().Clear();

        deck.Shuffle();

        winner.text = "";
        hitButton.interactable = true;
        stickButton.interactable = true;

        dealersFirstCard = -1;

        StartGame();
    }
    #endregion

    #region UnityMessages
    // Start is called before the first frame update
    private void Start()
    {
        StartGame();
    }

    #endregion

    
    void StartGame()
    {
        for(int i =0;i< 2;i++)
        {
            player.Push(deck.Pop());
            player1.Push(deck.Pop());
            HitDealer();
        }
    }


    void HitDealer()
    {
        int card = deck.Pop();

        if(dealersFirstCard < 0)
        {
            dealersFirstCard = card;
        }
        dealer.Push(card);
        if(dealer.CardCount >=2)
        {
            ViewCard view = dealer.GetComponent<ViewCard>();
            view.Toggle(card, true);
        }
    }
    

    IEnumerator DealersTurn()
    {
        hitButton.interactable = false;
        stickButton.interactable = false;

        ViewCard view = dealer.GetComponent<ViewCard>();
        view.Toggle(dealersFirstCard, true);
        view.ShowCards();
        yield return new WaitForSeconds(1f);
        
        while (dealer.HandValue() < 17)
        {
            HitDealer();
            yield return new WaitForSeconds(1f);
        }

        if(player.HandValue() > 21 || (dealer.HandValue() >= player.HandValue() && dealer.HandValue() <=21))
        {
            winner.text = " Sorry You loose";
        }
        else if (dealer.HandValue() > 21 || (player.HandValue() <=21 && player.HandValue() > dealer.HandValue()))
        {
            winner.text = " You Win";
        }
        else
        {
            winner.text = "House wins";
        }


        if (player1.HandValue() > 21 || (dealer.HandValue() >= player1.HandValue() && dealer.HandValue() <= 21))
        {
            winner.text = " player 2 loose";
        }
        else if (dealer.HandValue() > 21 || (player1.HandValue() <= 21 && player1.HandValue() > dealer.HandValue()))
        {
            winner.text = " Player 2 Win";
        }
        else
        {
            winner.text = "House wins";
        }
        yield return new WaitForSeconds(1f);
        playAgain.interactable = true;
    }
}
