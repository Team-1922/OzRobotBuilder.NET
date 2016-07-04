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
    /// <summary>
    /// The general class for json-serialized objects
    /// </summary>
    /// <typeparam name="DocumentName">The document type to load</typeparam>
    public class JsonLoader<DocumentName> : IDocumentSerializer where DocumentName : Document
    {
        /// <summary>
        /// The file extention without the first period to load
        /// </summary>
        protected string Format;
        /// <summary>
        /// the global serialization settings for json loading
        /// </summary>
        protected static JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };        
        /// <summary>
        /// Constructor to create instance
        /// </summary>
        /// <param name="format">the file extention withou tthe first period to load</param>
        public JsonLoader(string format)
        {
            Format = format;
        }
        /// <summary>
        /// load the document from the given stream
        /// </summary>
        /// <param name="inputData">the stream to load the document from</param>
        /// <returns>the loaded document; null if failed</returns>
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
        /// <summary>
        /// </summary>
        /// <returns>The file extention format</returns>
        public string GetFormat()
        {
            return Format;
        }
        /// <summary>
        /// json-serializes the given document into a string
        /// </summary>
        /// <param name="doc">the doc to serialize</param>
        /// <returns>the json string</returns>
        public string Serialize(Document doc)
        {
            //create the printer for this object
            JsonPrettyPrinter printer = new JsonPrettyPrinter(new JsonPrettyPrinterPlus.JsonPrettyPrinterInternals.JsonPPStrategyContext());
            
            return printer.PrettyPrint(JsonConvert.SerializeObject(doc, SerializerSettings));
        }
    }
}
