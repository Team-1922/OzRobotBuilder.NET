using System.IO;
using System.Text.RegularExpressions;

namespace ConsumerProgram
{
    //this represents a resouce loading class
    interface IResLoader
    {
        //this should return a regular expression for matching which files types should be loaded
        Regex GetFormat();

        IResData LoadResource(StreamReader streamReader);
    }
}
