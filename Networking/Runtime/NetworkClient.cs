using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using System.Net;
using System.Net.Sockets;
using AsyncSocket;

namespace BoardGame.Networking {

    public class NetworkClient
    {

        public List<byte[]> received_messages = new List<byte[]>();
        public readonly object net_received_data_syncLock = new object();
        public SocketSelectClient client = null;

        private List<byte[]> send_chunks = new List<byte[]>();
        private List<byte[]> received_chunks = new List<byte[]>();
        private int received_chunks_size = 0;
        private int current_message_size = -1;

        public float last_info_shown = 0f, info_show_rate = 1f;
        public int sended_immediatly_count = 0;
        public int sended_partially_count = 0;
        public int send_queued_count = 0;
        public int received_count = 0;

        public int debug_id = 0;

        public static string ConvertStringToHex(string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }

        public void clientConnect()
        {
            // GameGlobal.Log("clientConnect to " + GameGlobal.server_address);
            client = new SocketSelectClient();
            client.Connect("127.0.0.1", 20140);  // TODO set connection params from UI 
        }

        private void JoinReceivedChunks()
        {
            if (received_chunks_size == 0)
                return;
            byte[] buff = new byte[received_chunks_size];
            int offset = 0;
            foreach (byte[] chunk in received_chunks)
            {
                System.Buffer.BlockCopy(chunk, 0, buff, offset, chunk.Length);
                offset += chunk.Length;
            }
            received_chunks.Clear();
            received_chunks.Add(buff);
        }


        private bool processReceivingMsg()
        {
            //GameGlobal.Log("processReceivingMsg: received_chunks_size " + received_chunks_size + "; current_message_size " + current_message_size);

            if (received_chunks_size > 4 && current_message_size == -1)
            { // can extract data size
                JoinReceivedChunks();
                byte[] msg = received_chunks[0];
                current_message_size = BitConverter.ToInt32(msg, 0);
                //GameGlobal.Log("processReceivingMsg: got size " + current_message_size);
                return true;
            }
            if ((received_chunks_size - 4) >= current_message_size && current_message_size != -1)
            { // can extract overall message
                JoinReceivedChunks();
                byte[] msg = received_chunks[0];
                byte[] data = new byte[current_message_size];
                System.Buffer.BlockCopy(msg, 4, data, 0, current_message_size);
                lock (net_received_data_syncLock)
                    received_messages.Add(data);
                received_chunks.Clear();

                //GameGlobal.Log("message extracted");

                if ((received_chunks_size - 4) > current_message_size)
                {
                    //GameGlobal.Log("extracting remained chunk");
                    byte[] last_chunk = new byte[received_chunks_size - current_message_size - 4];
                    System.Buffer.BlockCopy(msg, current_message_size+4, last_chunk, 0, last_chunk.Length);
                    received_chunks.Add(last_chunk);
                    received_chunks_size = last_chunk.Length;
                    //GameGlobal.Log("remained chunk size " + received_chunks_size);
                }
                else
                    received_chunks_size = 0;
                current_message_size = -1;
                return true;
            }
            return false;
        }

        public void SendReceive()
        {

            if ((Time.time - last_info_shown) > info_show_rate && client.Connected)
            {
                // string s = "";
                // s += "send: immediatly " + sended_immediatly_count;
                // s += " partially " + sended_partially_count;
                // s += " queued " + send_queued_count;
                // s += " ; received " + received_count;
                // GameGlobal.Log(s);
                last_info_shown = Time.time;
                received_count = sended_immediatly_count = sended_partially_count = send_queued_count = 0;
            }

            // Send loop
            while (send_chunks.Count > 0)
            {
                int sended = client.Send(send_chunks[0], 0, send_chunks[0].Length);
                if (sended == 0)
                    break;
                if (sended < send_chunks[0].Length)
                    send_chunks[0] = send_chunks[0].Skip(sended).ToArray();
                else
                    send_chunks.RemoveAt(0);
            } 
            
            // Receive loop
            byte[] buffer = new byte[1024];
            int received = 0;
            do
            {
                received = client.Receive(buffer, 0, buffer.Length);
                if (received == 0)
                    break;
                byte[] chunk = buffer.Take(received).ToArray();

                //GameGlobal.Log("receive chunk:\n" + HexDump.Utils.HexDump(chunk, 16));

                received_chunks.Add(chunk);
                received_chunks_size += chunk.Length;
            } while (received > 0);

            int chunks_was = received_messages.Count;
            while (processReceivingMsg());
            received_count += received_messages.Count - chunks_was;

        }

        public void Send<T>(T obj)
        {
            byte[] buff = GetBytes(JsonUtility.ToJson(obj));
            client.Send(buff, 0, buff.Length);
        }

        public void Send(byte[] msg)
        { // TODO maybe split to chunks for buffer align
            int sended = client.Send(msg, 0, msg.Length);
            if (sended == msg.Length)
            {
                sended_immediatly_count++;
                //GameGlobal.Log("sended chunk immediatly:\n" + HexDump.Utils.HexDump(msg, 16));
            }
            else
            {
                if (sended > 0)
                {
                    sended_partially_count++;
                    //GameGlobal.Log("sended chunk partially:\n" + HexDump.Utils.HexDump(msg, 16));
                }
                else
                {
                    //GameGlobal.Log("send chunk queued:\n" + HexDump.Utils.HexDump(msg, 16));
                }
                send_queued_count++;
                send_chunks.Add(msg.Skip(sended).ToArray());
            }
        }

        static public byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char) + 4];
            byte[] sz = BitConverter.GetBytes((int)(str.Length * sizeof(char)));
            System.Buffer.BlockCopy(sz, 0, bytes, 0, sz.Length);
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 4, bytes.Length - 4);
            return bytes;
        }

    }

}