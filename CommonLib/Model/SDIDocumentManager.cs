using CommonLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model
{
    public abstract class SDIDocumentManager
    {
        protected List<IDocumentSerializer> DocSerializers = new List<IDocumentSerializer>();
        public Document OpenDoc { get; set; }

        protected abstract bool LoadDocument(string path);
        /// <summary>
        /// Save the Open Document
        /// </summary>
        /// <param name="path">The path to save the document to; left blank if saving to {@Link OpenDoc.Path}</param>
        /// <returns>success of the save</returns>
        public abstract bool Save(string path);
        //public abstract StreamReader GetFileStream(string filePath);

        public void CloseDocument()
        {
            OpenDoc = null;
        }
        public bool OpenDocument(string path)
        {
            CloseDocument();
            if (!File.Exists(path))
                return false;
            return LoadDocument(path);
        }
        public void RegisterDocSerializer(IDocumentSerializer loader)
        {
            DocSerializers.Add(loader);
        }
        public IDocumentSerializer GetAppropriateDocSerializer(string filePath)
        {
            //use this so directory names CAN have periods
            string[] splitFilename = filePath.Split(Path.DirectorySeparatorChar);
            if (splitFilename.Length == 0)
                return null;

            string nameOnly = splitFilename[splitFilename.Length - 1];

            //split by periods NOW
            splitFilename = nameOnly.Split(new char[] { '.' }, 2, StringSplitOptions.None);

            //the SECOND option is the ENTIRE EXTENDED FILE EXTENTION
            if (splitFilename.Length != 2)
                return null;

            string fileExt = splitFilename[1];

            foreach (var loader in DocSerializers)
            {
                if (loader.GetFormat() == fileExt)
                    return loader;
            }

            return null;
        }
        //this WILL overwrite the zip file
        public bool SaveZip(string zipLoc)
        {
            ZipDocumentManager thisZip = this as ZipDocumentManager;
            if (null == thisZip)
                return false;

            return thisZip.Save(zipLoc);
        }
        public bool ImportZip(string zipLoc)
        {
            //if THIS is already a zip doc manager, then do nothing, becuase we are only in zip mode on the deployed bot
            if (this is ZipDocumentManager)
                return false;

            //this is safe, becuase the child managers should NOT add any data members
            ZipDocumentManager thisZip = this as ZipDocumentManager;
            if (null == thisZip)
                return false;

            //this may be unsafe: TODO:
            if (!thisZip.LoadDocument(zipLoc))
                return false;

            //Save the project with whichever loader THIS currently is
            return Save(zipLoc);
        }
        public string GetOpenDocJson()
        {
            //serialize the data
            return GetAppropriateDocSerializer(OpenDoc.Path)?.Serialize(OpenDoc);
        }
    }
}
