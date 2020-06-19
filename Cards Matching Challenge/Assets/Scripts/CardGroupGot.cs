using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGroupGot : MonoBehaviour
{
    public List<GameObject> Deck;

    private void Start()
    {
        //CardData

        foreach (CardData Card in this.GetComponentsInChildren<CardData>())
        {
            Deck.Add(Card.gameObject);
        }
    }

    public void CheckCardGot(int CardGroupGot)
    {
        for (int x = 0; x < Deck.Count; x++)
        {
            if (Deck[x].GetComponent<CardData>().cardGroup == CardGroupGot)
            {
                Deck[x].GetComponent<MouseInput>().StartCoroutine("RiseCard");
                return;
            }
        }
    }

}
