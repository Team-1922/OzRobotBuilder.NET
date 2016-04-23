using System.IO;
using System.Text.RegularExpressions;

namespace ConsumerProgram.ResManager.Loader
{
    class TextResourceLoader : IResLoader
    {
        public Regex GetFormat()
        {
            return new Regex("^.*\\.(txt|gitignore|gitattributes|md)$");
        }

        public IResData LoadResource(StreamReader streamReader)
        {
            return new Data.TextResData(streamReader.ReadToEnd());
        }
    }
}
