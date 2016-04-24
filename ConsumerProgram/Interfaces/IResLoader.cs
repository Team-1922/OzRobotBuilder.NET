using System.IO;
using System.Text.RegularExpressions;

namespace ConsumerProgram
{
    //this represents a resouce loading class
    public interface IResLoader
    {
        //this should a SINGLE file extention which this loader can load (i.e "txt" for .txt files or "rmap.xml" for .rmap.xml files)
        string GetFormat();

        IResData LoadResource(StreamReader streamReader);
    }
}
