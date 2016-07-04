using CommonLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model
{
    /// <summary>
    /// A Single Document manager
    /// </summary>
    public abstract class SDIDocumentManager
    {
        /// <summary>
        /// The document serializers
        /// </summary>
        public List<IDocumentSerializer> DocSerializers { get; private set; } = new List<IDocumentSerializer>();
        /// <summary>
        /// The single open document
        /// </summary>
        public Document OpenDoc { get; set; }
        /// <summary>
        /// Loads a document from a given file path
        /// </summary>
        /// <param name="path">the location of the document to load</param>
        /// <returns>whether or not the document loaded successfully</returns>
        protected abstract bool LoadDocument(string path);
        /// <summary>
        /// Save the Open Document
        /// </summary>
        /// <param name="path">The path to save the document to; left blank if saving to <see cref="Document.Path"/></param>
        /// <returns>success of the save</returns>
        public abstract bool Save(string path);
        /// <summary>
        /// Close the Open document
        /// </summary>
        public void CloseDocument()
        {
            OpenDoc = null;
        }
        /// <summary>
        /// Open a document at a given path
        /// </summary>
        /// <param name="path">The path of the document to open</param>
        /// <returns>whether or not the document opened successfully</returns>
        public bool OpenDocument(string path)
        {
            CloseDocument();
            if (!File.Exists(path))
                return false;
            return LoadDocument(path);
        }
        /// <summary>
        /// Get the document serializer for the file extention given
        /// </summary>
        /// <param name="filePath">the path to the file to find the loader for</param>
        /// <returns>the document serializer needed to load a document</returns>
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
        /// <summary>
        /// Save whatever document manager is being used into a zip file
        /// </summary>
        /// <param name="zipLoc">the location of the zip file to save to</param>
        /// <returns>the success of the save</returns>
        public bool SaveZip(string zipLoc)
        {
            ZipDocumentManager thisZip = this as ZipDocumentManager;
            if (null == thisZip)
                return false;

            return thisZip.Save(zipLoc);
        }
        /// <summary>
        /// Imports a zip file into whatever document manager this is
        /// </summary>
        /// <param name="zipLoc">the location of the zip file to load from</param>
        /// <returns>the success of the import</returns>
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
        /// <summary>
        /// Serializes the open document into a json format
        /// </summary>
        /// <returns>the json representation of the open document</returns>
        public string GetOpenDocJson()
        {
            //serialize the data
            return GetAppropriateDocSerializer(OpenDoc.Path)?.Serialize(OpenDoc);
        }
    }
}
