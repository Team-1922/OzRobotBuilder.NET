using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsumerProgram.ResManager.Data
{
    class XmlResData : XmlDocument, IResData
    {
        public string GetHumanReadable()
        {
            return "XML Data";
        }

        public void CleanUp()
        {
            //nothing to do here
        }
    }
}
