using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectToGame : MonoBehaviour
{
    public InputField playerNameInput;

    public void ButtonConnection()
    {
        PlayerPrefs.SetString("playerName", playerNameInput.text);
        SceneManager.LoadScene("Game");
    }

}
