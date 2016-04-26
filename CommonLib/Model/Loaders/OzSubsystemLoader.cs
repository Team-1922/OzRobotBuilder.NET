using CommonLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CommonLib.Model.LoadableTypes;
using Newtonsoft.Json;

namespace CommonLib.Model.Loaders
{
    class OzSubsystemLoader : JsonLoader<OzSubsystemDoc>
    {
        public override string GetFormat()
        {
            return "subsystem.json";
        }
    }
}
