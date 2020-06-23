using Assets.Scripts.Socket;
using System;
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
        leaderboard = new List<Player>();
        leaderboard = data;

        foreach (Transform child in LeaderboardParent.transform)
        {
            Destroy(child.gameObject);
        }

        for (int x = 0; (x < leaderboard.Count) && (x < 9); x++)
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

            //Convert from seconds
            TimeSpan time = TimeSpan.FromSeconds(leaderboard[x].seconds);

            prefabChildren[0].text = (x + 1).ToString();
            prefabChildren[1].text = leaderboard[x].name;
            prefabChildren[2].text = leaderboard[x].moves.ToString();
            prefabChildren[3].text = time.ToString(@"mm\:ss");
            prefabChildren[4].text = leaderboard[x].score.ToString();
        }
    }

}
