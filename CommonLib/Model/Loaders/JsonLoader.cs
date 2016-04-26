using CommonLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace CommonLib.Model.Loaders
{
    public abstract class JsonLoader<DocumentName> : IDocumentLoader
    {
        public Document Deserialize(StreamReader inputData)
        {
            DocumentName retData = JsonConvert.DeserializeObject<DocumentName>(inputData.ReadToEnd());

            Document retDoc = retData as Document;

            if (null == retDoc)
            {
                //maybe do something here?  throw "DocLoadFailedException"?
            }
            return retDoc;
        }

        //leave this abstract to let children loaders have their own format
        public abstract string GetFormat();

        public string Serialize(Document doc)
        {
            return JsonConvert.SerializeObject(doc);
        }
    }
}
