using UnityEngine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace AsyncSocket
{ 
    public class SocketSelectClient
    {
        private Socket client = null;
        public bool Connected
        {
            get
            {
                return client.Connected;
            }
        }

        public bool Connect(string hostname, int port)
        {
            Debug.Log("connecting...");
            try
            {
                IPAddress ipAddress = null;
                if (!IPAddress.TryParse(hostname, out ipAddress))
                {
                    IPHostEntry ipHostInfo = Dns.GetHostEntry(hostname);
                    ipAddress = ipHostInfo.AddressList[0];
                }
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //client.ReceiveTimeout = 1;
                //client.SendTimeout = 1;
                client.Connect(remoteEP);
                client.Blocking = false;
                client.ReceiveBufferSize = 1024 * 16;
                client.SendBufferSize = 1024 * 16;
                client.NoDelay = true;
                return client.Connected;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return false;
            }
            

        }

        public int Receive(byte[] buffer, int offset, int size) //return number of actually readed bytes
        {
            try
            {
                //if (!client.Poll(0, SelectMode.SelectRead) || client.Available == 0)
                //    return 0;
                if (!client.Poll(0, SelectMode.SelectRead))
                    return 0;
                // Debug.Log("select read " + client.Poll(0, SelectMode.SelectRead) + " and available " + client.Available);
                return client.Receive(buffer, offset, size, SocketFlags.None);
            }
            catch (SocketException e)
            {
                // 10035 == WSAEWOULDBLOCK
                if (e.NativeErrorCode.Equals(10035))
                {
                    Debug.Log("Still Connected, but the Send would block");
                    return 0;
                }
                else
                {
                    if (client.Connected) {
                        Debug.Log(String.Format("Disconnected: error code {0}!", e.NativeErrorCode));
                    }
                    return 0;
                }
            }
        }
        public int Send(byte[] buffer, int offset, int size) //return true, if data buffered to send
        {
            if (!client.Poll(0, SelectMode.SelectWrite))
                return 0;

            try
            {
                return client.Send(buffer, offset, size, SocketFlags.None);
            } 
            catch (SocketException e)
            {
                // 10035 == WSAEWOULDBLOCK
                if (e.NativeErrorCode.Equals(10035))
                {
                    Debug.Log("Still Connected, but the Send would block");
                    return 0;
                }
                else
                {
                    if (client.Connected) {
                        Debug.Log(String.Format("Disconnected: error code {0}!", e.NativeErrorCode));
                    }
                    return 0;
                }
            }
        }

        public bool CheckForError()
        {
            return client.Poll(0, SelectMode.SelectError);
        }

        public void Shutdown()
        {
            client.Blocking = true;
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
    };



}
