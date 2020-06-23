using Assets.Scripts;
using Assets.Scripts.Socket;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    public bool IsGameLoopActive = true;
    public PlayerSessionInformation session;

    public ClientConfiguration clientConf;

    public GameObject CardsParentGameObject;
    public CardSort CardSort;
    public List<bool> EveryCardActive;

    public GameObject WinScreen;

    public bool GameHasActiveCard = true;

    private void Start()
    {
        CardSort.SortEveryCard();
        CardSort.PlaceCards();

        session.SetPlayerName(PlayerPrefs.GetString("playerName"));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene( "Game" );
        }

        if (!IsGameLoopActive) { return; }

        //Check if it has active cards
        GameHasActiveCard = false;
        foreach (CardData card in CardsParentGameObject.GetComponentsInChildren<CardData>())
        {
            if (card.isActiveToPlay)
            {
                GameHasActiveCard = true;
            }
        }

        //Session will stop
        if (GameHasActiveCard == false)
        {
            IsGameLoopActive = false;

            //SHOW WIN WINDOW
            WinScreen.SetActive(true);

            session.SetFinalScore();
            session.StopClock();

            Player p = session.GetSessionInformation();

            Debug.Log("Name:" + p.name);
            Debug.Log("Moves:" + p.moves);
            Debug.Log("Time:" + p.seconds);
            Debug.Log("Score:" + p.score);

            clientConf.SendPlayerInformation(p);
            clientConf.SendMessage("LEADERBOARD_GET");
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("Game");
    }
}
