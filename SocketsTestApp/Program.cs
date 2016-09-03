using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.WebFramework.Sockets;

namespace SocketsTestApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SocketClient client = new SocketClient();

            client.OpenConnectionAsync("localhost", 8082).Wait();

            string message = "";
            do
            {
                message = Console.ReadLine();
                var response = Utils.SerializeResponse(client.SendAsync(Utils.ParseRequest(message)).Result);

                Console.WriteLine(response);

            } while (message != "shutdown");
        }
    }
}
