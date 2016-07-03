using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OzRobotBuilder.NET.Views
{
    /// <summary>
    /// A tree node which also has string data associated with it
    /// </summary>
    public class DictTreeNode : TreeNode
    {
        /// <summary>
        /// The string data associated with the tree node
        /// </summary>
        public string Data { get; set; }
    }
}
