using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsumerProgram.ResourceManager.Loaders
{
    class XmlResLoader : IResLoader
    {
        public string GetFormat()
        {
            return"xml";
        }

        public IResData LoadResource(StreamReader streamReader)
        {
            Data.XmlResData ret = new Data.XmlResData();
            ret.LoadXml(streamReader.ReadToEnd());
            return ret;
        }
    }
}
