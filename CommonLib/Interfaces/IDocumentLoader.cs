using CommonLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Interfaces
{
    public interface IDocumentLoader
    {
        //this should a SINGLE file extension which this loader can load (i.e "txt" for .txt files or "rmap.xml" for .rmap.xml files)
        string GetFormat();
        Document Deserialize(StreamReader inputData);

        //returning a string here is OK for small files (which are the only ones we're dealing with)
        string Serialize(Document doc);
    }
}
