using System.Collections;
using UnityEngine;

public class CardMatchCheck : MonoBehaviour
{
    public GameObject firstCard;
    public GameObject secondCard;
    public GameObject thirdCard;

    public PlayerSessionInformation PlayerSessionInformation;

    public bool isPlayingFlipAnimation = false;
    public bool isPlayingClickAnimation = false;

    bool OnAllCardFlipFunctionActive = false;

    bool wasFirstCard = true;

    IEnumerator DelayedFlip()
    {
        yield return new WaitForSeconds(0.8f);

        firstCard.GetComponent<MouseInput>().StartCoroutine("RiseCard");
        secondCard.GetComponent<MouseInput>().StartCoroutine("RiseCard");
        thirdCard.GetComponent<MouseInput>().StartCoroutine("RiseCard");

        firstCard.GetComponent<CardData>().isActiveToFlip = true;
        secondCard.GetComponent<CardData>().isActiveToFlip = true;
        thirdCard.GetComponent<CardData>().isActiveToFlip = true;

        firstCard = null;
        secondCard = null;
        thirdCard = null;

        isPlayingFlipAnimation = false;
        OnAllCardFlipFunctionActive = false;
    }

    IEnumerator DelayedClick()
    {
        yield return new WaitForSeconds(0.8f);

        firstCard = null;
        secondCard = null;
        thirdCard = null;

        isPlayingClickAnimation = false;
        OnAllCardFlipFunctionActive = false;
    }

    public void OnLastCardFlip()
    {
        if (wasFirstCard)
        {
            PlayerSessionInformation.SetGameStart();
            PlayerSessionInformation.SetClock();

            wasFirstCard = false;
        }

        PlayerSessionInformation.SetExtraMove(); //Add Move

        int firstCardGroup = firstCard.GetComponent<CardData>().cardGroup;
        int secondCardGroup = secondCard.GetComponent<CardData>().cardGroup;
        int thirdCardGroup = thirdCard.GetComponent<CardData>().cardGroup;

        //Ficar de Olho
        if (firstCardGroup == secondCardGroup && firstCardGroup == thirdCardGroup)
        {
            // Set Inactive
            firstCard.GetComponent<CardData>().SetCardInactiveToPlay();
            secondCard.GetComponent<CardData>().SetCardInactiveToPlay();
            thirdCard.GetComponent<CardData>().SetCardInactiveToPlay();

            if (!isPlayingClickAnimation)
            {
                StartCoroutine(DelayedClick());
                isPlayingClickAnimation = true;
            }
        }
        else
        {
            if (!isPlayingFlipAnimation)
            {
                StartCoroutine(DelayedFlip());
                isPlayingFlipAnimation = true;
            }
        }
    }

    private void Update()
    {
        if (firstCard != null && secondCard != null && thirdCard != null)
        {
            if (!OnAllCardFlipFunctionActive)
            {
                OnAllCardFlipFunctionActive = true;
                OnLastCardFlip();
            }
        }
    }
}
