using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Core
{
    public class ClientManager
    {
        public static Client GetClient(ConnectionProperties properties)
        {
            return new Client(properties);
        }
    }
}
