using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Team1922.MVVM.Models;
using Team1922.MVVM.ViewModels;
using Team1922.WebFramework.Sockets;

namespace SocketsTestApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string path = "../OzRobotBuilder.NET/TestRobotFiles/TestingFile.robot";

            XmlSerializer serializer = new XmlSerializer(typeof(Robot));
            Robot robot;
            using (FileStream reader = new FileStream(path, FileMode.Open))
            {
                robot = (Robot)serializer.Deserialize(reader);
            }
            if (null == robot)
                return;

            using (var provider = new RobotViewModelBase() { ModelReference = robot })
            {
                provider.PrecomputeTree();
                using (SocketUpdatesServer updatesReceivable = new SocketUpdatesServer(new RequestDelegator(provider, true), 8082, 8083))
                {
                    updatesReceivable.StartListener();

                    SocketClient client = new SocketClient();

                    client.OpenConnectionAsync("localhost", 8082).Wait();
                    string message = "";
                    do
                    {
                        message = Console.ReadLine();
                        //var response = Utils.SerializeResponse(client.SendAsync(Utils.ParseRequest(message)).Result);
                        var response = client.SendAsync(new Request(message)).Result;

                        Console.WriteLine(response.ToString());

                    } while (message != "shutdown");

                    updatesReceivable.StopListener();
                }
            }
        }
    }
}
