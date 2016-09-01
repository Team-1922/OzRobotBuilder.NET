using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public class ConnectionManager : IConnectionManager
    {
        public ConnectionManager(ushort port, IRequestDelegator delegator)
        {
            _port = port;
            _requestDelegator = delegator;
        }
        

        private List<Socket> outgoingConnections = new List<Socket>();

        public async Task Connect(string hostName, ushort port)
        {
            try
            {
                IPHostEntry ipHost = await Dns.GetHostEntryAsync(hostName);
                IPAddress ipAddress = ipHost.AddressList.Where(address => address.AddressFamily == AddressFamily.InterNetwork).First();
                if (ipAddress == null)
                {
                    Console.WriteLine("no IPv4 address");
                    return;
                }

                using (var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    client.Connect(ipAddress, port);
                    Console.WriteLine("client successfully connected");
                    var stream = new NetworkStream(client);
                    var cts = new CancellationTokenSource();

                    Task tSender = Sender(stream, cts);
                    Task tReceiver = Receiver(stream, cts.Token);
                    await Task.WhenAll(tSender, tReceiver);

                }
            }
            catch (SocketException ex)
            {
                WriteLine(ex.Message);
            }
        }
    }
}
