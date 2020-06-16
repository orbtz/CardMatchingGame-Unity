using System;
using System.Collections.Generic;
using UnityEngine;

public class CardSort : MonoBehaviour
{
    public GameObject CardsParentGameObject;

    public List<GameObject> Deck;

    //x = 5 * (0 - 5)
    //z = 6 * (0 - 2)

    // Ideia: Cada loop pega-se o número de cartas restantes do 'mainDeck', pega-se uma carta aleatória
    // e coloca ele no 'sortedDeck'
    // Ao final, ele define uma posição fixa para cada item da lista

    public void SortEveryCard()
    {
        Debug.Log(CardsParentGameObject.GetComponentsInChildren<Rigidbody>().Length);


        foreach ( Rigidbody CardObject in CardsParentGameObject.GetComponentsInChildren<Rigidbody>() )
        {
            Deck.Add(CardObject.gameObject);
        }

        System.Random random = new System.Random();
        for (int i = 0; i < Deck.Count -1; i++)
        {
            int j = random.Next(i, Deck.Count -1);
            GameObject temporary = Deck[i];
            Deck[i] = Deck[j];
            Deck[j] = temporary;
        }

    }

    public void PlaceCards()
    {
        int cardCount = 0;

        for (int i = 0; i < 6; i++)
        {
            for (int k = 0; k < 3; k++)
            {
                Deck[cardCount].transform.localPosition = new Vector3(5 * i, 0, 6 * k);
                cardCount++;
            }
        }
    }

}
