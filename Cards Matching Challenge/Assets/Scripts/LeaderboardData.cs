using Assets.Scripts.Socket;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardData : MonoBehaviour
{
    public ClientConfiguration websocket;

    public GameObject LeaderboardParent;
    public GameObject LeaderboardPrefab;
    public List<Player> leaderboard;

    public void UpdateLeaderboardData(List<Player> data)
    {
        leaderboard.Clear();
        leaderboard = data;

        for (int x = 0; x < leaderboard.Count; x++)
        {
            GameObject prefab = Instantiate(LeaderboardPrefab);
            prefab.transform.SetParent(LeaderboardParent.transform);

            prefab.GetComponent<RectTransform>().localPosition = new Vector3(
                -115,
                100 - (40 * x),
                0
            );

            prefab.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            Text[] prefabChildren = prefab.GetComponentsInChildren<Text>();

            prefabChildren[0].text = leaderboard[x].name;
            prefabChildren[1].text = leaderboard[x].moves.ToString();
            prefabChildren[2].text = leaderboard[x].seconds.ToString();
            prefabChildren[3].text = leaderboard[x].score.ToString();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            websocket.SendMessage("LEADERBOARD_GET");
        }
    }

}
