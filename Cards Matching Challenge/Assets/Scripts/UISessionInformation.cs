using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISessionInformation : MonoBehaviour
{
    public PlayerSessionInformation SessionInformation;

    public Text UIName;
    public Text UITime;
    public Text UIMoves;

    private void Start()
    {
        UIName.text = SessionInformation.playername;
    }

    private void Update()
    {
        UIMoves.text = "Tries: " + SessionInformation.moves;
        UITime.text = "Time: " + SessionInformation.seconds;
    }
}
