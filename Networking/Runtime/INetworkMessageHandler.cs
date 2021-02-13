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
   
    public interface INetworkMessageHandler {
        void handleInitClient(InitClientMessage message);
    };

}