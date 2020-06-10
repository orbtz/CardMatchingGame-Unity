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

    // Start is called before the first frame update
    async void Start()
    {

        //websocket = new WebSocket("wss://card-matching-server.herokuapp.com/socket.io/?EIO=4&transport=websocket");
        websocket = new WebSocket("ws://localhost:3000");

        websocket.OnOpen += () =>
        {
            Debug.Log("CONNECTED!");

            Player player = new Player
            {
                name = "BAR",
                moves = 2,
                seconds = 4
            };

            string Jsonplayer = JsonUtility.ToJson(player);

            WebsocketMessage message = new WebsocketMessage
            {
                type = "LEADERBOARD_SUBMIT",
                data = Jsonplayer
            };

            Debug.Log(message.data);

            string JsonMessage = JsonUtility.ToJson(message);

            Debug.Log(JsonMessage);

            websocket.SendText(JsonMessage);

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

            var message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log(message);

        };

        await websocket.Connect();
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

}