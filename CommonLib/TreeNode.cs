using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class DataTreeNode : IEnumerable<DataTreeNode>
    {
        private readonly Dictionary<string, DataTreeNode> _children =
                                            new Dictionary<string, DataTreeNode>();

        public string Key { get; private set; }
        public string Data { get; set; }
        public bool DataReadOnly = false;
        public DataTreeNode Parent { get; private set; }
        public DataTreeNode(string key, string data, bool dataReadOnly = false)
        {
            this.Key = key;
            this.Data = data;
            DataReadOnly = dataReadOnly;
        }
        public DataTreeNode GetChild(string key)
        {
            return this._children[key];
        }
        public void Add(DataTreeNode item)
        {
            if (item.Parent != null)
            {
                item.Parent._children.Remove(item.Key);
            }

            item.Parent = this;
            this._children.Add(item.Key, item);
        }
        public IEnumerator<DataTreeNode> GetEnumerator()
        {
            return this._children.Values.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Count
        {
            get { return this._children.Count; }
        }

        public DataTreeNode GetNodeFromPath(string path)
        {
            //is it this node?
            if (path == Key)
                return this;
            //strip off our layer
            var splitString = path.Split(new char[] { Path.DirectorySeparatorChar }, 2, StringSplitOptions.None);
            if (splitString.Length != 2)
                return null;
            if (splitString[0] != Key)
                return null;
            foreach(var child in this)
            {
                var ret = child.GetNodeFromPath(splitString[1]);
                if (null != ret)
                    return ret;
            }
            return null;
        }
    }

}
