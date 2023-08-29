using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.SocketService;

namespace Client.Core
{
    public interface IClient
    {
        Task<bool> SendMessage(RequestMessage message);
    }
}
