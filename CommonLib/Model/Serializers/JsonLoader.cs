using CommonLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using JsonPrettyPrinterPlus;

namespace CommonLib.Model.Serializers
{
    public class JsonLoader<DocumentName> : IDocumentSerializer
    {
        protected string Format;

        protected static JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };
        

        public JsonLoader(string format)
        {
            Format = format;
        }

        public Document Deserialize(StreamReader inputData)
        {
            DocumentName retData = JsonConvert.DeserializeObject<DocumentName>(inputData.ReadToEnd(), SerializerSettings);

            Document retDoc = retData as Document;

            if (null == retDoc)
            {
                //maybe do something here?  throw "DocLoadFailedException"?
            }
            return retDoc;
        }
        
        public string GetFormat()
        {
            return Format;
        }

        public string Serialize(Document doc)
        {
            //create the printer for this object
            JsonPrettyPrinter printer = new JsonPrettyPrinter(new JsonPrettyPrinterPlus.JsonPrettyPrinterInternals.JsonPPStrategyContext());
            
            return printer.PrettyPrint(JsonConvert.SerializeObject(doc, SerializerSettings));
        }
    }
}
