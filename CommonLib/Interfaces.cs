using CommonLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace CommonLib
{
    /// <summary>
    /// Documents are not responsible for their own loading and saving.
    /// Separate classes are responsible for serializing each kind of document
    /// </summary>
    public interface IDocumentSerializer
    {
        /// <summary>
        /// This should be a single file extention which this loader can load (i.e. "txt" for .txt files or "rmap.xml" for .rmap.xml files)
        /// </summary>
        /// <returns>the file extention which this load can load</returns>
        string GetFormat();
        /// <summary>
        /// Deserializes a document from a stream of input data
        /// </summary>
        /// <param name="inputData">the input stream</param>
        /// <returns>A initialized document if deserialization was successful; null if not</returns>
        Document Deserialize(StreamReader inputData);
        /// <summary>
        /// Serializes a document into a string
        /// </summary>
        /// <param name="doc">the document to serialize</param>
        /// <returns>a string representation of the document which is compatable with <see cref="Deserialize(StreamReader)"/></returns>
        string Serialize(Document doc);
    }
    /// <summary>
    /// Children of this interface can have additional member functions added to each object individually in the lua
    /// </summary>
    public interface ILuaExt
    {
        /// <summary>
        /// Get the formatted script with the extended object's features
        /// </summary>
        /// <returns>formatted script with the extended object's methods</returns>
        string GetFormattedExtScriptText();
    }
    /// <summary>
    /// All views of an application must override this and be registered into the view manager
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Call any initialization code here (also access to an <see cref="Application"/> reference is available)
        /// </summary>
        /// <param name="app">the reference to the application</param>
        void Init(Application app);
        /// <summary>
        /// Called every update cycle;  do rendering and periodic updates here
        /// </summary>
        /// <param name="deltaTime">the time since the program started</param>
        void Update(TimeSpan deltaTime);
        /// <summary>
        /// Cleanup any nonmanaged resources here or other things
        /// </summary>
        void Destroy();
    }
}
