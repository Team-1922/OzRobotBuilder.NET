using CommonLib;
using CommonLib.Model;
using CommonLib.Model.Documents;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OzRobotBuilder.NET.Views
{
    /// <summary>
    /// The main view class 
    /// </summary>
    public partial class MainView : Form, IView
    {
        /// <summary>
        /// Reference to Application
        /// </summary>
        protected CommonLib.Application App;
        /// <summary>
        /// Constructor; initializes dialog components
        /// </summary>
        public MainView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Updates the Tree View with the newly opened Document
        /// </summary>
        public void RecreateTreeView(KeyValueTreeNode rootNode)
        {
            treeView1.Nodes.Clear();

            //var tree = doc.GetTree();

            //RecreateTreeViewRecursive(tree, treeView1.Nodes.Add(tree.Key));

            treeView1.Nodes.Add(RecreateTreeViewRecurse(rootNode));

            //set the active node
            treeView1.SelectedNode = treeView1.Nodes[0];

            //finally refresh the grid view (this usually just clears it)
            RefreshGridView(/*tree*/);

            //expand the tree view
            treeView1.ExpandAll();
        }
        private DictTreeNode RecreateTreeViewRecurse(KeyValueTreeNode node)
        {
            DictTreeNode parent = new DictTreeNode();
            parent.Text = node.Key;
            parent.Data = node.Value;
            foreach(var child in node)
            {
                parent.Nodes.Add(RecreateTreeViewRecurse(child));
            }
            return parent;
        }
        /// <summary>
        /// Determines whether or not the tree view will update if this new node is selected
        /// </summary>
        /// <param name="newNode">the node to update with</param>
        /// <returns>if the grid view should update with the new tree view selection</returns>
        public bool WillUpdateSelection(TreeNode newNode)
        {
            if (null == newNode)
                return false;

            if (newNode.GetNodeCount(false) == 0)
                return false;

            return true;
        }
        /// <summary>
        /// Updates the grid view with the selected tree view item
        /// </summary>
        public void RefreshGridView()
        {
            RefreshGridView(treeView1.SelectedNode);
        }
        /// <summary>
        /// Updates the grid view with the given tree view item (this does not set this node as active however)
        /// </summary>
        /// <param name="activeNode">the tree node to update the grid view with</param>
        public void RefreshGridView(TreeNode activeNode)
        {
            //this might not be right
            dataGridView1.Rows.Clear();

            if (null == activeNode)
            {
                Invalidate();
                return;
            }

            //add all of the children to the grid view but DO NOT recurse
            foreach(TreeNode node in activeNode.Nodes)
            {
                var nodeData = node as DictTreeNode;
                if (null == nodeData)
                    continue;
                dataGridView1.Rows.Add(nodeData.Text, nodeData.Data);
            }

            Invalidate();
        }

        #region Interited From IView
        /// <summary>
        /// Just needs to configure the application reference
        /// </summary>
        /// <param name="app">application reference</param>
        public void Init(CommonLib.Application app)
        {
            App = app;
        }
        /// <summary>
        /// does not need to do anything yet
        /// </summary>
        /// <param name="deltaTime">time since the program began</param>
        public void Update(TimeSpan deltaTime)
        {
            //nothing
        }
        /// <summary>
        /// Does not need to do anything
        /// </summary>
        public void Destroy()
        {
            //nothing
        }
        #endregion

        #region Event Handlers

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string gridName = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value as string;
            string newValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as string;
            string fullPath = null;
            foreach(TreeNode node in treeView1.SelectedNode.Nodes)
            {
                if(node.Text == gridName)
                {
                    fullPath = node.FullPath;
                }
            }
            if (null == fullPath)
                return;
            App.Controller.UpdateDocument(fullPath, newValue);
        }
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //get the "name" part
            var name = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

            //is the "name" a subnode?
            if (!treeView1.SelectedNode.Nodes.ContainsKey(name))
                return;


            RefreshGridView();
        }
        private void FileOpen(object sender, EventArgs e)
        {
            App.Controller.OpenFile();
        }
        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (!WillUpdateSelection(e.Node))
                e.Cancel = true;
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            RefreshGridView();
        }
        #endregion

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (App.Controller as KeyValueController).SaveDocument();
        }
    }
}
