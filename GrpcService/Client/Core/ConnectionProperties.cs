using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Core
{
    public class ConnectionProperties
    {
        public ConnectionProperties(string[] hostname, int port, bool showRTT)
        {
            Hostname = hostname;
            Port = port;
            ShowRTT = showRTT;
        }

        public string[] Hostname { get; set; }
        public int Port { get; set; }
        public bool ShowRTT { get; set; }
    }
}
