using CommonLib.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace CommonLib
{
    public interface IDocumentSerializer
    {
        //this should a SINGLE file extension which this loader can load (i.e "txt" for .txt files or "rmap.xml" for .rmap.xml files)
        string GetFormat();
        Document Deserialize(StreamReader inputData);

        //returning a string here is OK for small files (which are the only ones we're dealing with)
        string Serialize(Document doc);
    }
    public interface ILuaExt
    {
        //this should return a formatted script with the extended class
        //  features (extended features CAN be per-object; that's how they
        //  are treated anyways)
        string GetFormattedExtScriptText();
    }
    public interface IDictionarySerialize
    {
        void Deserialize(Dictionary<string, string> data);
        Dictionary<string, string> Serialize();
    }
    public interface ITreeNodeSerialize
    {
        DataTreeNode GetTree();
        bool DeserializeTree(DataTreeNode node);
    }
}
