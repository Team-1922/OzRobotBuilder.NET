using CommonLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CommonLib.Model.Documents;
using Newtonsoft.Json;

namespace CommonLib.Model.Serializers
{
    public static class BuiltInLoaders
    {
        public static JsonLoader<RobotDocument> RobotLoader = new JsonLoader<RobotDocument>("robot.json");

        public static void RegisterLoaders(SDIDocumentManager manager)
        {
            manager.RegisterDocSerializer(RobotLoader);
        }
    }
}
