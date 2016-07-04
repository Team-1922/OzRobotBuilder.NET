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
    /// <summary>
    /// The built-in document loaders of the common lib
    /// </summary>
    public static class BuiltInLoaders
    {
        /// <summary>
        /// The document loader for robot documents
        /// </summary>
        public static JsonLoader<RobotDocument> RobotLoader = new JsonLoader<RobotDocument>("robot.json");
        /// <summary>
        /// registers all of the loaders of the <see cref="BuiltInLoaders"/> to the given document manager
        /// </summary>
        /// <param name="manager"></param>
        public static void RegisterLoaders(SDIDocumentManager manager)
        {
            manager.DocSerializers.Add(RobotLoader);
        }
    }
}
