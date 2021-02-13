using UnityEngine;

using System.Collections;
using System.Collections.Generic;


using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BoardGame.Networking {

    public class NetLogic 
    {

        public NetworkClient client = new NetworkClient();

        private bool sended_ready = false;

        public delegate void handleServerMessage(string jsonMessage);
        public Dictionary<string, handleServerMessage> server_message_handlers = new Dictionary<string, handleServerMessage>();

        public INetworkMessageHandler messageHandler;

        public NetLogic()
        {
            server_message_handlers.Add("init_client", handle_init_client);
        }


        public void OnApplicationQuit()
        {
            Debug.Log("OnApplicationQuit");
            if (client != null)
            {
                if (client.client.Connected)
                {
                    client.Send(new ClientQuitMessage());

                    client.client.Shutdown();
                }
            }

        }

        public void handle_init_client(string jsonMessage)
        {
            messageHandler.handleInitClient(JsonUtility.FromJson<InitClientMessage>(jsonMessage));
        }

        private bool processReceivingMessage()
        {
            byte[] msg = null;
            lock (client.net_received_data_syncLock)
            {
                if (client.received_messages.Count == 0)
                    return false;

                msg = client.received_messages[0];
                client.received_messages.RemoveAt(0);
            }

            string jsonMessage = System.Text.Encoding.Unicode.GetString(msg);

            try
            {
                BaseMessage baseMessage =  JsonUtility.FromJson<BaseMessage>(jsonMessage);

                if (server_message_handlers.ContainsKey(baseMessage.method)) 
                    server_message_handlers[baseMessage.method](jsonMessage);
                
            }
            catch (System.Exception e)
            {
                Debug.Log("got exception " + e.ToString());
                Debug.Log("original message >>" + jsonMessage + "<<");
            }

            return true;

        }

        // Update is called once per frame
        public void Update()
        {
            if (client.client == null)
                client.clientConnect();

            if (!client.client.Connected)
                return;

            if (!sended_ready)
            {
                ClientReadyMessage clientReadyMessage = new ClientReadyMessage();                   
                clientReadyMessage.playerName = "awesome player name";
                client.Send(clientReadyMessage);
                sended_ready = true;
            }

            while (processReceivingMessage()) ;

        }
    }
}