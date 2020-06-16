using UnityEngine;

using NativeWebSocket;
using System.Collections;

using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Assets.Scripts.Socket;

public class ClientConfiguration : MonoBehaviour
{
    WebSocket websocket;

    Player player;
    string JsonPlayer;

    WebsocketMessage message;
    string JsonMessage;

    string responseMessage;

    public PlayerSessionInformation PlayerSessionInformation;

    // Start is called before the first frame update
    async void Start()
    {
        //PlayerSessionInformation.SetGameStart();
        //PlayerSessionInformation.SetClock();

        //websocket = new WebSocket("wss://card-matching-server.herokuapp.com/socket.io/?EIO=4&transport=websocket");
        websocket = new WebSocket("ws://localhost:3000");

        websocket.OnOpen += () =>
        {
            Debug.Log("CONNECTED!");

            //Debug.Log(message.value);
            //Debug.Log(JsonMessage);

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
            //var message = System.Text.Encoding.UTF8.GetString(bytes);
            responseMessage = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log(responseMessage);
        };

        await websocket.Connect(); //Posso criar uma condição para tentar realizar a abertura de conexão
    }

    public void SendPlayerInformation(Player playerData)
    {
        //player = new Player()
        //{
        //    name = "FRB",
        //    moves = playerData.moves,
        //    seconds = playerData.seconds
        //};
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