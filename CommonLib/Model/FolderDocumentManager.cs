using CommonLib.Interfaces;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model
{
    public class FolderDocumentManager : DocumentManager
    {
        /*
         * 
         * TODO: Exception handling here needs to be improved
         * 
         */
        public override bool SaveAll()
        {
            foreach(var doc in OpenDocuments)
            {
                var serializer = GetAppropriateDocSerializer(doc.Path);
                if(null == serializer)
                {
                    continue;//logging is handled by the "GetAppropriateDocSerializer" method
                }

                //serialize the data
                string textData = serializer.Serialize(doc);

                //make sure the whole path exists
                string path = Path.GetDirectoryName(doc.Path);
                if("" != path)
                    Directory.CreateDirectory(Path.GetDirectoryName(doc.Path));

                //create a file and write into this document (this is the most basic way to do it; there are probably better ways)
                File.WriteAllText(doc.Path, textData);
            }
            return true;
        }

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

        protected override bool LoadProject()
        {
            bool retVal = true;
            var allFiles = RecurseLoadFilePaths(ProjectLocation);

            //iterate through each entry
            foreach (var s in allFiles)
            {
                IDocumentSerializer thisLoader = GetAppropriateDocSerializer(s);
                if (null == thisLoader)
                {
                    Console.WriteLine(string.Format("File: \"{0}\" Does Not Have Matching Loader", s));
                    continue;
                }

                //open and read the whole string from the stream
                using (var stream = File.OpenRead(s))
                {
                    //stream.Seek(0, SeekOrigin.Begin);
                    using (var streamreader = new StreamReader(stream))
                    {
                        Document thisDoc = thisLoader.Deserialize(streamreader);
                        if (null == thisDoc)
                        {
                            Console.WriteLine(String.Format("File: \"{0}\" Failed to Load", s));
                            retVal = false;
                        }
                        AddDocument(thisDoc);
                    }
                }                
            }            
            return retVal;
        }
    }
}
