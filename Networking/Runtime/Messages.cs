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

    [System.Serializable]
    public class BaseMessage {
        public string method;
    }

    [System.Serializable]
    public class InitClientMessage {
        public string method = "init_client";
        public int client_id;
    }

    [System.Serializable]
    public class ClientReadyMessage {
        public string method = "ready";
        public string playerName;
    }

    [System.Serializable]
    public class ClientQuitMessage {
        public string method = "quit";
    }

}