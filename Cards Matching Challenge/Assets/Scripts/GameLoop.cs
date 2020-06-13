using Assets.Scripts.Socket;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public bool IsGameLoopActive = true;
    public PlayerSessionInformation session;

    public GameObject CardsParentGameObject;
    public List<bool> EveryCardActive;

    public bool GameHasActiveCard = true;

    private void Start()
    {
        foreach (CardData card in CardsParentGameObject.GetComponentsInChildren<CardData>())
        {
            Debug.Log(card.name  + " - " + card.isActiveToPlay);
        }
    }

    private void Update()
    {
        if (!IsGameLoopActive) { return; }

        GameHasActiveCard = false;
        foreach (CardData card in CardsParentGameObject.GetComponentsInChildren<CardData>())
        {
            if (card.isActiveToPlay)
            {
                GameHasActiveCard = true;
            }
        }

        if (GameHasActiveCard == false)
        {
            Debug.Log("Acabou!");
            IsGameLoopActive = false;

            Player p = session.GetSessionInformation();

            Debug.Log(p.name + p.moves + p.seconds);
        }
    }
}
