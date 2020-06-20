using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNewPosition : MonoBehaviour
{
    public GameObject GameUI;
    public GameObject LeaderboardUI;

    public void CameraPositionGame()
    {
        GameUI.SetActive(true);
        LeaderboardUI.SetActive(false);

        transform.position = new Vector3(18.6f, 49.1f, 8f);
    }

    public void CameraPositionLeaderboard()
    {
        GameUI.SetActive(false);
        LeaderboardUI.SetActive(true);

        transform.position = new Vector3(18.6f, 49.1f, 30f);
    }
}
