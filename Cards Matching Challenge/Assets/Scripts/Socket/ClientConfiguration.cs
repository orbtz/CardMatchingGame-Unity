using UnityEngine;

using NativeWebSocket;
using Assets.Scripts.Socket;
using System.Collections.Generic;

public class ClientConfiguration : MonoBehaviour
{
    WebSocket websocket;

    string JsonPlayer;

    WebsocketMessage message;
    string JsonMessage;

    public List<Player> leaderboardList;

    public PlayerSessionInformation PlayerSessionInformation;
    public LeaderboardData LeaderboardData;

    // Start is called before the first frame update
    async void Start()
    {
        //PlayerSessionInformation.SetGameStart();
        //PlayerSessionInformation.SetClock();

        websocket = new WebSocket("wss://card-matching-server.herokuapp.com/socket.io/?EIO=4&transport=websocket");
        //websocket = new WebSocket("ws://localhost:3000");

        websocket.OnOpen += () =>
        {
            Debug.Log("CONNECTED!");

        };

        websocket.OnError += (e) =>
        {
            Debug.Log("ERROR! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("DISCONNECTED");
        };

        websocket.OnMessage += (bytes) =>
        {
            // getting the message as a string
            string message = System.Text.Encoding.UTF8.GetString(bytes);

            //Check if it is about the leaderboard
            LeaderboardMessage messageFromJSON = JsonUtility.FromJson<LeaderboardMessage>(message);
            //string type = messageFromJSON.type;

            Debug.Log(messageFromJSON.value.Length);

            if (messageFromJSON.type == "LEADERBOARD_GET")
            {
                leaderboardList.Clear();

                for (int x = 0; x < messageFromJSON.value.Length; x++)
                {
                    Player player = new Player
                    {
                        name = messageFromJSON.value[x].name,
                        moves = messageFromJSON.value[x].moves,
                        seconds = messageFromJSON.value[x].seconds,
                        score = messageFromJSON.value[x].score
                    };

                    leaderboardList.Add(player);
                }

                LeaderboardData.UpdateLeaderboardData(leaderboardList);
            }

        };

        await websocket.Connect(); //Posso criar uma condição para tentar realizar a abertura de conexão
    }

    public void SendMessage(string dataType)
    {
        message = new WebsocketMessage()
        {
            type = dataType,
            value = ""
        };
        JsonMessage = JsonUtility.ToJson(message);

        websocket.SendText(JsonMessage); //Send Data
    }

    public void SendPlayerInformation(Player playerData)
    {
        JsonPlayer = JsonUtility.ToJson(playerData);

        message = new WebsocketMessage()
        {
            type = "LEADERBOARD_SUBMIT",
            value = JsonPlayer
        };
        JsonMessage = JsonUtility.ToJson(message);
        
        websocket.SendText(JsonMessage); //Send Data
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

}