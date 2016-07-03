using CommonLib.Interfaces;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model
{
    public class FolderDocumentManager : SDIDocumentManager
    {
        /*
         * 
         * TODO: Exception handling here needs to be improved
         * 
         */
        public override bool Save(string path)
        {
            if (path == "")
                path = OpenDoc.Path;

            string textData = GetOpenDocJson();
            if (null == textData)
                return false;

            //make sure the whole path exists
            string wPath = Path.GetDirectoryName(OpenDoc.Path);
            if("" != wPath)
                Directory.CreateDirectory(Path.GetDirectoryName(OpenDoc.Path));

            //create a file and write into this document (this is the most basic way to do it; there are probably better ways)
            File.WriteAllText(OpenDoc.Path, textData);
            return true;
        }
        [Obsolete]
        static List<string> RecurseLoadFilePaths(string path)
        {
            List<string> retVals = new List<string>();

            //add the files in this directory
            var files = Directory.GetFiles(path);
            if (null != files && files.Length > 0)
            {
                retVals.AddRange(files.AsEnumerable());
            }

            //recurse the child directories
            var dirs = Directory.GetDirectories(path);
            if (null != dirs && dirs.Length > 0)
            {
                foreach (var dir in dirs)
                {
                    retVals.AddRange(RecurseLoadFilePaths(dir));
                }
            }
            return retVals;
        }

        protected override bool LoadDocument(string path)
        {
            //standard stuff

            IDocumentSerializer thisLoader = GetAppropriateDocSerializer(path);
            if (null == thisLoader)
            {
                Console.WriteLine(string.Format("File: \"{0}\" Does Not Have Matching Loader", path));
                return false;
            }

            //open and read the whole string from the stream
            using (var stream = File.OpenRead(path))
            {
                //stream.Seek(0, SeekOrigin.Begin);
                using (var streamreader = new StreamReader(stream))
                {
                    Document thisDoc = thisLoader.Deserialize(streamreader);
                    if (null == thisDoc)
                    {
                        Console.WriteLine(String.Format("File: \"{0}\" Failed to Load", path));
                        return false;
                    }
                    OpenDoc = thisDoc;
                }
            }

            return true;
        }
    }
}
