using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace ConsumerProgram.ResManager
{
    class ResManager
    {
        public string ArchivePath { get; private set; }
        public Dictionary<string, IResData> ResCache { get; private set; } = new Dictionary<string, IResData>();

        protected List<IResLoader> ResLoaders = new List<IResLoader>();

        public ResManager()
        {
        }

        public void SetCacheLoc(string cacheLoc)
        {
            ArchivePath = cacheLoc;
            if(!File.Exists(cacheLoc))
            {
                throw new FileNotFoundException("Could Not Load Cache Location",cacheLoc);
            }

            ReloadResCache();
        }

        public void ClearResourceCache()
        {
            foreach(var resource in ResCache)
            {
                resource.Value.CleanUp();
            }
            ResCache.Clear();
        }

        public void ReloadResCache()
        {
            //clear out the existing resource cache
            ClearResourceCache();

            //open the file
            using (var archive = ZipFile.OpenRead(ArchivePath))
            {
                //iterate through each entry
                foreach(var s in archive.Entries)
                {
                    //ommit directories
                    if (!s.FullName.EndsWith("/"))
                    {
                        IResLoader thisLoader = GetLoader(s.FullName);
                        if (thisLoader == null)
                        {
                            System.Console.WriteLine(string.Format("File: \"{0}\" Does Not Have Matching Loader", s.FullName));
                            continue;
                        }

                        //open and read the whole string from the stream
                        using (var stream = s.Open())
                        {
                            //stream.Seek(0, SeekOrigin.Begin);
                            using (var streamreader = new StreamReader(stream))
                            {                                
                                ResCache.Add(s.FullName, thisLoader.LoadResource(streamreader));
                            }
                        }
                    }
                }
            }
        }
        
        //unknown behavior will occur if multiple loaders Regex's match the file name
        public void RegisterResLoader(IResLoader loader)
        {
            ResLoaders.Add(loader);
        }

        public IResLoader GetLoader(string fileName)
        {
            foreach(var loader in ResLoaders)
            {
                if (loader.GetFormat().IsMatch(fileName))
                    return loader;
            }

            return null;
        }
    }
}
