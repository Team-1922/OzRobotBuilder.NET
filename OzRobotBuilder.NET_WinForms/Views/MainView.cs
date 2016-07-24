using CommonLib;
using CommonLib.Model;
using CommonLib.Model.CompositeTypes;
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
            HierarchialView.Nodes.Clear();

            //var tree = doc.GetTree();

            //RecreateTreeViewRecursive(tree, treeView1.Nodes.Add(tree.Key));

            HierarchialView.Nodes.Add(RecreateTreeViewRecurse(rootNode));

            //set the active node
            HierarchialView.SelectedNode = HierarchialView.Nodes[0];

            //finally refresh the grid view (this usually just clears it)
            RefreshGridView(/*tree*/);

            //expand the tree view
            HierarchialView.ExpandAll();
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
            RefreshGridView(HierarchialView.SelectedNode);
        }
        /// <summary>
        /// Updates the grid view with the given tree view item (this does not set this node as active however)
        /// </summary>
        /// <param name="activeNode">the tree node to update the grid view with</param>
        public void RefreshGridView(TreeNode activeNode)
        {
            //this might not be right
            SelectedNodeEditor.Rows.Clear();

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
                SelectedNodeEditor.Rows.Add(nodeData.Text, nodeData.Data);
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

        private void SelectedNodeEditor_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string gridName = SelectedNodeEditor.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value as string;
            string newValue = SelectedNodeEditor.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as string;
            string fullPath = null;
            foreach(TreeNode node in HierarchialView.SelectedNode.Nodes)
            {
                if(node.Text == gridName)
                {
                    fullPath = node.FullPath;
                }
            }
            if (null == fullPath)
                return;
            if (null == App.Controller)
                return;
            App.Controller.UpdateDocument(fullPath, newValue);
        }
        private void SelectedNodeEditor_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //get the "name" part
            var name = SelectedNodeEditor.Rows[e.RowIndex].Cells[0].Value.ToString();

            //is the "name" a subnode?
            if (!HierarchialView.SelectedNode.Nodes.ContainsKey(name))
                return;


            RefreshGridView();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == App.Controller)
                return;
            App.Controller.OpenFile();
        }
        private void HierarchialView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (!WillUpdateSelection(e.Node))
                e.Cancel = true;
        }
        private void HierarchialView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            RefreshGridView();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var controller = App.Controller as KeyValueController;
            if (null == controller)
                return;
            controller.SaveDocument();
        }

        private void subsystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var controller = App.Controller as KeyValueController;
            if (null == controller)
                return;
            controller.AddSubsystem();
        }

        private void commandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var controller = App.Controller as KeyValueController;
            if (null == controller)
                return;
            controller.AddCommand();
        }

        private void joystickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var controller = App.Controller as KeyValueController;
            if (null == controller)
                return;
            controller.AddJoystick();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void HierarchialView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                //open the appropriate context menu

                //the root node
                if (HierarchialView.Nodes.Count == 0)
                    return;
                if(e.Node == HierarchialView.Nodes[0])
                {
                    HierarchialView.SelectedNode = e.Node;
                    RobotContextMenu.Show(MousePosition);
                }
                var nodeAsDataNode = e.Node as DictTreeNode;
                if(null != nodeAsDataNode)
                {
                    if (nodeAsDataNode.Data == typeof(OzSubsystemData).ToString())
                    {
                        HierarchialView.SelectedNode = e.Node;
                        SubsystemContextMenu.Show(MousePosition);
                    }
                }
            }
        }

        private OzSubsystemData GetSelectedNodeSubsystem()
        {
            var robotDoc = App.DocumentManager.OpenDoc as RobotDocument;
            if (null == robotDoc)
                return null;

            //find the correct subsystem
            var name = HierarchialView.SelectedNode.Text;
            foreach (var subsystem in robotDoc.Subsystems)
            {
                if (subsystem.Name == name)
                    return subsystem;
            }
            return null;
        }

        private void addPWMMotorControllerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var controller = App.Controller as KeyValueController;
            if (null == controller)
                return;
            if (HierarchialView.SelectedNode == null)
                return;
            controller.AddPWMMotorController(HierarchialView.SelectedNode.FullPath);
        }

        private void addTalonSRXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var controller = App.Controller as KeyValueController;
            if (null == controller)
                return;
            controller.AddTalonSRX(HierarchialView.SelectedNode.FullPath);
        }

        private void addAnalogInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var controller = App.Controller as KeyValueController;
            if (null == controller)
                return;
            controller.AddAnalogInput(HierarchialView.SelectedNode.FullPath);
        }

        private void addQuadratureEncoderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var controller = App.Controller as KeyValueController;
            if (null == controller)
                return;
            controller.AddQuadratureEncoder(HierarchialView.SelectedNode.FullPath);
        }

        private void addDigitalInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var controller = App.Controller as KeyValueController;
            if (null == controller)
                return;
            controller.AddDigitalInput(HierarchialView.SelectedNode.FullPath);
        }
        #endregion
    }
}
