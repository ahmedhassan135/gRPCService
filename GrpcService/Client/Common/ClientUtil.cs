using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Common
{
    internal class ClientUtil
    {
        //Returns, based on the list of servers, which server to send the message to.
        public static string GetServerToRequest(long messageId, string[] serverIps)
        {
            var serverNumber = messageId % serverIps.Length;

            return serverIps[serverNumber];
        }
    }
}
