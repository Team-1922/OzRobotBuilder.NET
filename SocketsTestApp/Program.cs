﻿using System;
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
            RequestDelegator rd = new RequestDelegator("", null);
            SocketClient client = new SocketClient(rd);

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