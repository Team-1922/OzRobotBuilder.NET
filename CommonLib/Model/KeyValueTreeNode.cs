using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model
{
    /// <summary>
    /// A tree datatype where each node has a key and a value
    /// </summary>
    public class KeyValueTreeNode : IEnumerable<KeyValueTreeNode>
    {
        private Dictionary<string, KeyValueTreeNode> _children = new Dictionary<string, KeyValueTreeNode>();
        /// <summary>
        /// The key used to access this node from its parent
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// the data this node stores
        /// </summary>
        public string Value { get; set; } 
        /// <summary>
        /// Access the full path to this object (looks at each parent node)
        /// </summary>
        public string Path { get
            {
                if (null == Parent)
                    return Key;
                return string.Format("{0}{1}{2}", Parent.Path, System.IO.Path.DirectorySeparatorChar, Key);
            } }
        /// <summary>
        /// a reference to the parent of this node (null if no parent)
        /// </summary>
        public KeyValueTreeNode Parent { get; private set; }
        /// <summary>
        /// Construct a new node from the given key and value
        /// </summary>
        /// <param name="key">node key</param>
        /// <param name="value">node value</param>
        public KeyValueTreeNode(string key, string value)
        {
            Key = key;
            Value = value;
        }
        /// <summary>
        /// Addes a given node as a child to this node
        /// </summary>
        /// <param name="child">child node to add</param>
        public void AddChild(KeyValueTreeNode child)
        {
            //do NOT allow circular references (this REALLY screws with the path)
            if (child.GetChild(Key) == this)
                throw new ArgumentException("Attempt to Create Circular Relation Between Two KeyValueTreeNode's");
            child.Parent = this;
            _children.Add(child.Key, child);
        }
        /// <summary>
        /// Access to the child nodes of this node
        /// </summary>
        /// <param name="key"></param>
        /// <returns>the child with <see cref="Key"/> <paramref name="key"/></returns>
        public KeyValueTreeNode GetChild(string key)
        {
            if (!_children.ContainsKey(key))
                return null;
            return _children[key];
        }
        /// <summary>
        /// Looks through the whole subtree to overwrite a node's value with <paramref name="value"/>
        /// </summary>
        /// <param name="path">the path (including this instances <see cref="Key"/>) to the desired value to overwrite</param>
        /// <param name="value">value to update with</param>
        /// <returns>whether a value at <paramref name="path"/> was found</returns>
        public bool SetChildValue(string path, string value)
        {
            //strip off our layer
            var splitPath = path.Split(new char[] { System.IO.Path.DirectorySeparatorChar }, 2, StringSplitOptions.None);
            if(splitPath.Length == 1)
            {
                Value = value;
                return true;
            }
            if (splitPath.Length != 2)
                return false;

            //call childrens' function
            foreach(var child in _children)
            {
                if (child.Value.SetChildValue(splitPath[1], value))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Override for IEnumerable
        /// </summary>
        /// <returns>the enumerator of the <see cref="_children"/> dictionary</returns>
        public IEnumerator<KeyValueTreeNode> GetEnumerator()
        {
            return _children.Values.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
