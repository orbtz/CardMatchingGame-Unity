using System.Collections;
using UnityEngine;

public class CardMatchCheck : MonoBehaviour
{
    public GameObject firstCard;
    public GameObject secondCard;

    public PlayerSessionInformation PlayerSessionInformation;

    public bool isPlayingFlipAnimation = false;
    public bool isPlayingClickAnimation = false;

    bool OnDoubleCardFlipFunctionActive = false;

    IEnumerator DelayedFlip()
    {
        yield return new WaitForSeconds(0.8f);

        firstCard.GetComponent<MouseInput>().StartCoroutine("RiseCard");
        secondCard.GetComponent<MouseInput>().StartCoroutine("RiseCard");

        firstCard.GetComponent<CardData>().isActiveToFlip = true;
        secondCard.GetComponent<CardData>().isActiveToFlip = true;

        firstCard = null;
        secondCard = null;

        isPlayingFlipAnimation = false;
        OnDoubleCardFlipFunctionActive = false;
    }

    IEnumerator DelayedClick()
    {
        yield return new WaitForSeconds(0.8f);

        firstCard = null;
        secondCard = null;

        isPlayingClickAnimation = false;
        OnDoubleCardFlipFunctionActive = false;
    }

    public void OnDoubleCardFlip()
    {
        PlayerSessionInformation.SetExtraMove(); //Add Move

        int firstCardGroup = firstCard.GetComponent<CardData>().cardGroup;
        int secondCardGroup = secondCard.GetComponent<CardData>().cardGroup;

        if (firstCardGroup == secondCardGroup)
        {
            // Set Inactive
            firstCard.GetComponent<CardData>().SetCardInactiveToPlay();
            secondCard.GetComponent<CardData>().SetCardInactiveToPlay();

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
        if (firstCard != null && secondCard != null)
        {
            if (!OnDoubleCardFlipFunctionActive)
            {
                OnDoubleCardFlipFunctionActive = true;
                OnDoubleCardFlip();
            }
        }
    }
}
