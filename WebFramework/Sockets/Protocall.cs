using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public static class Protocall
    {
        public enum Method
        {
            Get,
            Set,
            Add,
            Delete,
            Close
        }
        public static string MethodToString(Method method)
        {
            switch (method)
            {
                case Method.Get:
                    return "GET";
                case Method.Set:
                    return "SET";
                case Method.Add:
                    return "ADD";
                case Method.Delete:
                    return "DELETE";
                case Method.Close:
                    return "CLOSE";
                default:
                    throw new ArgumentException($"Given Method \"{method.ToString()}\" Does Not Exist!");
            }
        }
        public static Method StringToMethod(string method)
        {
            method = method.ToUpperInvariant().Trim();
            switch(method)
            {
                case "GET":
                    return Method.Get;
                case "SET":
                    return Method.Set;
                case "ADD":
                    return Method.Add;
                case "DELETE":
                    return Method.Delete;
                case "CLOSE":
                    return Method.Close;
                default:
                    throw new ArgumentException($"Given Method \"{method}\" Does Not Exist!");
            }
        }
    }
}
