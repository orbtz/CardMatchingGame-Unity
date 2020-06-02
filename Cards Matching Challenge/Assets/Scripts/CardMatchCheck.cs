using System.Collections;
using UnityEngine;

public class CardMatchCheck : MonoBehaviour
{
    public GameObject firstCard;
    public GameObject secondCard;

    public bool isPlayingFlipAnimation = false;

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

    }

    private void Update()
    {
        if (firstCard != null && secondCard != null)
        {
            int firstCardGroup = firstCard.GetComponent<CardData>().cardGroup;
            int secondCardGroup = secondCard.GetComponent<CardData>().cardGroup;

            if (firstCardGroup == secondCardGroup)
            {
                Debug.Log("IGUAIS");

                firstCard = null;
                secondCard = null;
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
    }
}
