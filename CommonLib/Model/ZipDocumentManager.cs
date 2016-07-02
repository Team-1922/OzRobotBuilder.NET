using CommonLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace CommonLib.Model
{
    /*
     * 
     * This Document Manager is used in the "RobotModel" and any other application which deals with the compiled .zip of the resouces
     * 
     */
    public class ZipDocumentManager : SDIDocumentManager
    {

        protected override bool LoadDocument(string path)
        {
            bool retVal = true;
            //open the file
            using (var archive = ZipFile.OpenRead(path))
            {
                ZipArchiveEntry entry = null;
                //look for a SINGLE document in the root of the zip file
                foreach(var s in archive.Entries)
                {
                    if(s.FullName.Contains("RobotDocument.robot.json"))
                    {
                        entry = s;
                    }
                }
                if(null == entry)
                {
                    Console.WriteLine(string.Format("File: \"{0}\" Does Not Contain A Valid Robot Document File", path));
                    return false;
                }
                IDocumentSerializer thisLoader = GetAppropriateDocSerializer(path);
                if (null == thisLoader)
                {
                    Console.WriteLine(string.Format("File: \"{0}\" Does Not Have Matching Loader", entry.FullName));
                    return false;
                }

                //open and read the whole string from the stream
                using (var stream = entry.Open())
                {
                    //stream.Seek(0, SeekOrigin.Begin);
                    using (var streamreader = new StreamReader(stream))
                    {
                        Document thisDoc = thisLoader.Deserialize(streamreader);
                        if (null == thisDoc)
                        {
                            Console.WriteLine(string.Format("File: \"{0}\" Failed to Load", entry.FullName));
                            retVal = false;
                        }
                        OpenDoc = thisDoc;
                    }
                }
            }

            return retVal;
        }

        public override bool Save(string path)
        {
            IDocumentSerializer thisLoader = GetAppropriateDocSerializer(OpenDoc.Path);
            if (null == thisLoader)
            {
                Console.WriteLine(string.Format("File: \"{0}\" Does Not Have Matching Loader", OpenDoc.Path));
                return false;
            }

            var fileStream = File.Create(path);
            using (ZipArchive archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
            {
                var entry = archive.CreateEntry("RobotDocument.robot.json", CompressionLevel.Optimal);
                var entryStream = entry.Open();

                var serializeString = thisLoader.Serialize(OpenDoc);
                var byteArray = Encoding.UTF8.GetBytes(serializeString);

                //save the document in the stream
                entryStream.Write(byteArray, 0, byteArray.Length);
            }

            return true;
        }
    }
}
