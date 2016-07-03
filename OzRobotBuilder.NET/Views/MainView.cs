using CommonLib;
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
    public class DictTreeNode : TreeNode
    {
        public string Data { get; set; }
    }

    public partial class MainView : Form
    {
        //TODO: relocate somewhere nicer?
        //credit: http://stackoverflow.com/questions/18769634/creating-tree-view-dynamically-according-to-json-text-in-winforms
        public static DictTreeNode Json2Tree(string text)
        {
            return Json2Tree(JObject.Parse(text));
        }
        public static DictTreeNode Json2Tree(JObject obj)
        {
            //create the parent node
            DictTreeNode parent = new DictTreeNode();
            parent.Text = GetObjectName(obj);
            parent.Data = GetObjectType(obj);

            //if this has no children, this it is NOT considered a valid item in the tree view
            if (obj.Count == 0)
                return null;

            //loop through the obj. all token should be pair<key, value>
            foreach (var token in obj)
            {
                //change the display Content of the parent
                //parent.Text = token.Key.ToString();
                //create the child node
                DictTreeNode child = new DictTreeNode();
                child.Text = token.Key;
                //check if the value is of type obj recall the method
                if (token.Value.Type.ToString() == "Object")
                {
                    // child.Text = token.Key.ToString();
                    //create a new JObject using the the Token.value
                    JObject o = (JObject)token.Value;
                    //recall the method (this also handles the "type" and "name" retrieving
                    child = Json2Tree(o);
                    if (null == child)
                        continue;
                    //the programatic name of an object overwrites any kind of given name it has
                    child.Text = token.Key;
                    //add the child to the parentNode
                    parent.Nodes.Add(child);
                }
                //if type is of array
                else if (token.Value.Type.ToString() == "Array")
                {
                    //loop though the array (this does not add a child type AT ALL, becuase array types
                    //  will have the "$values" layer in between
                    foreach (var itm in token.Value)
                    {
                        var itmChild = Json2Tree((JObject)itm);
                        if (null == itmChild)
                            continue;
                        parent.Nodes.Add(itmChild);
                    }
                }
                else if(token.Key == "$type")
                {
                    //DO NOTHING
                }
                else
                {
                    //if token.Value is not nested
                    // child.Text = token.Key.ToString();
                    //change the value into N/A if value == null or an empty string 
                    if (token.Value.ToString() == "")
                        child.Data = "N/A";
                    else
                        child.Data = token.Value.ToString();
                    parent.Nodes.Add(child);
                }
            }
            return parent;

        }
        private static string GetObjectType(JObject obj)
        {
            //now try to get the "type" property.  
            string type;
            try
            {
                //wow this looks unsafe, but we don't care what fails, we just want to know if any of it does
                type = obj.Property("$type").Value.ToString().Split(',')[0];
            }
            catch
            {
                type = "TypeError";
            }
            return type;
        }
        private static string GetObjectName(JObject obj)
        {
            //hopefully there is a "name" property
            string name;
            try
            {
                name = obj.Property("Name").Value.ToString();
                if (!Regex.IsMatch(name, @"^[a-zA-Z0-9]+$"))
                    throw new Exception();//doesn't matter what this is since it will be caught right below
            }
            catch
            {
                //no or bad name, well darn
                name = "NameError";
            }
            return name;
        }
        
        public MainView()
        {
            InitializeComponent();
        }

        private void MainView_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Updates the Tree View with the newly opened Document
        /// </summary>
        private void RecreateTreeView()
        {

            treeView1.Nodes.Clear();

            //get the tree from the document
            var doc = Program.DocManager.OpenDoc as RobotDocument;
            if (null == doc)
                return;//we have an issue

            //var tree = doc.GetTree();

            //RecreateTreeViewRecursive(tree, treeView1.Nodes.Add(tree.Key));

            treeView1.Nodes.Add(Json2Tree(Program.DocManager.GetOpenDocJson()));

            //set the active node
            treeView1.SelectedNode = treeView1.Nodes[0];

            //finally refresh the grid view (this usually just clears it)
            RefreshGridView(/*tree*/);

            //expand the tree view
            treeView1.ExpandAll();
        }

        internal void RecreateTreeViewRecursive(DataTreeNode node, TreeNode viewNode)
        {
            if (null == node)
                return;
            
            foreach(var child in node)
            {
                if(child.Count != 0)
                    RecreateTreeViewRecursive(child, viewNode.Nodes.Add(child.Key));
            }
        }

        [Obsolete]
        public object GetActiveTreeVieObject()
        {
            //validate the document type
            if (!(Program.DocManager.OpenDoc is CommonLib.Model.Documents.RobotDocument))
                return null;

            //get the active tree view node
            var node = treeView1.SelectedNode;

            //TODO: eventually will these nodes have some data associated with them?
            if (node.FullPath == "Subsystems" || node.FullPath == "Commands" || node.FullPath == "OperatorInterface")
                return null;

            //get the top most node to figure out where to get the data from
            var topParentNode = node;
            var tmpParent = topParentNode.Parent;
            while(tmpParent != null)
            {
                topParentNode = tmpParent;
                tmpParent = tmpParent.Parent;
            }

            //i wish this was a bit more generic
            if(topParentNode.Name == "Subsystem")
            {
                foreach(var subsystem in (Program.DocManager.OpenDoc as RobotDocument).Subsystems)
                {
                    if (subsystem.Name == topParentNode.Name)
                        return subsystem;
                }
            }
            /*if (topParentNode.Name == "Commands")
            {
                foreach (var command in (Program.DocManager.OpenDoc as RobotDocument).Commands)
                {
                    if (command.Name == topParentNode.Name)
                        return command;
                }
            }*/
            /*if (topParentNode.Name == "OperatorInterface")
            {
                foreach (var subsystem in (Program.DocManager.OpenDoc as RobotDocument).Subsystems)
                {
                    if (subsystem.Name == topParentNode.Name)
                        return subsystem;
                }
            }*/



            return null;
        }
        public TreeNode GetActiveTreeViewNode()
        {
            /*var path = treeView1.SelectedNode.FullPath;
            var openDoc = Program.DocManager.OpenDoc as RobotDocument;
            var tree = openDoc.GetTree();
            return tree.GetNodeFromPath(path);*/
            return null;
        }


        /// <summary>
        /// This updates the grid view with the selected tree view item
        /// </summary>
        public void RefreshGridView()
        {
            RefreshGridView(treeView1.SelectedNode);
        }
        public void RefreshGridView(TreeNode activeNode)
        {
            //this might not be right
            dataGridView1.Rows.Clear();

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

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Program.Controller.UpdateKey(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value as string, dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as string);
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //get the "name" part
            /*var name = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

            //is the "name" a subnode?
            var activeNode = GetActiveTreeViewNode();
            var child = activeNode.GetChild(name);
            if (null == child)
                return;//do nothing, we can't go deeper

            //find the child node
            var firstChild = treeView1.SelectedNode.FirstNode;
            while (null != firstChild)
            {
                if(firstChild.Text == name)
                {
                    treeView1.SelectedNode = firstChild;
                }
                firstChild = firstChild.NextNode;
            }

            RefreshGridView();*/
        }

        private void FileOpen(object sender, EventArgs e)
        {
            //pop open dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;
            openFileDialog.Filter = "Robot Files (*.robot.json)|*.robot.json";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Program.DocManager.OpenDocument(openFileDialog.FileName);
            }


            //finally refresh the grid view and recreate the tree view
            RecreateTreeView();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //refresh the selection
            RefreshGridView();
        }
    }
}
