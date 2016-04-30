using CommonLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model
{
    public abstract class DocumentManager
    {
        public string ProjectLocation { get; private set; }
        protected List<IDocumentSerializer> DocSerializers = new List<IDocumentSerializer>();
        protected List<Document> OpenDocuments = new List<Document>();

        protected abstract bool LoadProject();
        public abstract bool SaveAll();
        //public abstract StreamReader GetFileStream(string filePath);

        public void CloseProject()
        {
            ProjectLocation = "";
            OpenDocuments.Clear();
        }
        public bool OpenProject(string path)
        {
            CloseProject();
            if (!Directory.Exists(path) || File.Exists(path))
                return false;
            ProjectLocation = path;
            return LoadProject();
        }
        public void RegisterDocSerializer(IDocumentSerializer loader)
        {
            DocSerializers.Add(loader);
        }
        public IDocumentSerializer GetAppropriateDocSerializer(string filePath)
        {
            //use this so directory names CAN have periods
            string[] splitFilename = filePath.Split('/');
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
        public void AddDocument(Document doc)
        {
            OpenDocuments.Add(doc);
        }
        //this WILL overwrite the zip file
        public bool SaveZip(string zipLoc)
        {
            ZipDocumentManager thisZip = this as ZipDocumentManager;
            if (null == thisZip)
                return false;

            return thisZip.SaveAll();
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

            CloseProject();
            ProjectLocation = zipLoc;

            //this may be unsafe: TODO:
            if (!thisZip.LoadProject())
                return false;

            //Save the project with whichever loader THIS currently is
            return SaveAll();
        }
    }
}
