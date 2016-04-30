using CommonLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace CommonLib.Model
{
    /*
     * 
     * This Document Manager is used in the "RobotModel" and any other application which deals with the compiled .zip of the resouces
     * 
     */
    public class ZipDocumentManager : DocumentManager
    {

        protected override bool LoadProject()
        {
            bool retVal = true;
            //open the file
            using (var archive = ZipFile.OpenRead(ProjectLocation))
            {
                //iterate through each entry
                foreach(var s in archive.Entries)
                {
                    //ommit directories
                    if (!s.FullName.EndsWith("/"))
                    {
                        IDocumentSerializer thisLoader = GetAppropriateDocSerializer(s.FullName);
                        if (null == thisLoader)
                        {
                            Console.WriteLine(string.Format("File: \"{0}\" Does Not Have Matching Loader", s.FullName));
                            continue;
                        }

                        //open and read the whole string from the stream
                        using (var stream = s.Open())
                        {
                            //stream.Seek(0, SeekOrigin.Begin);
                            using (var streamreader = new StreamReader(stream))
                            {
                                Document thisDoc = thisLoader.Deserialize(streamreader);
                                if (null == thisDoc)
                                {
                                    Console.WriteLine(String.Format("File: \"{0}\" Failed to Load", s.FullName));
                                    retVal = false;
                                }                       
                                AddDocument(thisDoc);
                            }
                        }
                    }
                }
            }

            return retVal;
        }

        public override bool SaveAll()
        {
            return true;
        }
    }
}
