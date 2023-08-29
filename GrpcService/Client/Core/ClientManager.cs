using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Core
{
    public class ClientManager
    {
        public static Client GetClient(string[] serverIps,  int port, bool showRTT)
        {
            return new Client(serverIps, port, showRTT);
        }
    }
}
