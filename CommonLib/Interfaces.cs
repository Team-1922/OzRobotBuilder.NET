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
}
