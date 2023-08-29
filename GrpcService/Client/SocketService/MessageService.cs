using Client.Core;
using Grpc.Core;
using PrimeServerService.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.SocketService
{
    internal class MessageService
    {
        internal static async Task<bool> SendMessageInternal(RequestMessage message, string hostname, int port)
        {
            Channel channel = new Channel(hostname, port, ChannelCredentials.Insecure);

            ServerResponse response = null;

            ServerService.ServerServiceClient client = new ServerService.ServerServiceClient(channel);

            var data = new ServerRequest() { Id = message.Id, Number = message.Number, Timestamp = DateTime.Now.Millisecond };
            
            try
            {
                response = await client.ReceiveRequestAsync(data);
            }
            catch (RpcException ex)
            {

                if (ex.StatusCode == StatusCode.Unavailable)
                {
                    int retries = 2;
                    while (retries > 0)
                    {
                        try
                        {
                            response = await client.ReceiveRequestAsync(data);
                            break;
                        }
                        catch (RpcException) { }
                        catch (Exception) { throw; }

                        retries--;
                    }

                }
                if (response == null)
                {
                    throw;
                }
            }

            return response.IsPrime;
        }
    }
}
