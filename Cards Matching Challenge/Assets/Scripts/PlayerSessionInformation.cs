using Assets.Scripts.Socket;
using System.Collections;
using UnityEngine;

public class PlayerSessionInformation : MonoBehaviour
{
    public string playername;
    public int moves;
    public int seconds;
    public float finalPoints;

    public void SetGameStart()
    {
        moves = 0;
        seconds = 0;
        finalPoints = 0;
    }

    public void SetPlayerName(string name)
    {
        playername = name;
    }

    public void SetExtraMove()
    {
        moves += 1;
    }

    public void SetClock()
    {
        StartCoroutine(Timer());
    }

    public void StopClock()
    {
        StopAllCoroutines();
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);

        StartCoroutine(Timer());
        seconds += 1;
    }

    public float SetAndReturnFinalResult()
    {
        finalPoints = seconds * moves;
        return (finalPoints);
    }

    public Player GetSessionInformation()
    {
        return new Player()
        {
            name = playername,
            moves = moves,
            seconds = seconds
        };
    }

}
