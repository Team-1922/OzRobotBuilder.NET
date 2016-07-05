using CommonLib.Interfaces;
using CommonLib.Model;
using CommonLib.Model.Documents;
using CommonLib.Model.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonLib.Controller
{
    /// <summary>
    /// The corresponding controller for <see cref="RobotDocument"/>
    /// </summary>
    public abstract class RobotDocController
    {
        /// <summary>
        /// The reference to the Application
        /// </summary>
        protected Application App;
        /// <summary>
        /// Construct a new <see cref="RobotDocController"/> with the reference to the application
        /// </summary>
        /// <param name="app"></param>
        public RobotDocController(Application app)
        {
            App = app;
        }
        /// <summary>
        /// Responsible for opening the document file
        /// </summary>
        public abstract void OpenFile();
        /// <summary>
        /// Updates the open document with the given path and value
        /// </summary>
        /// <param name="path">the path to the value to update; (bypasses container types with a "name" attribute of each object within it)</param>
        /// <param name="value">the value to update the variable at "path" with</param>
        public void UpdateDocument(string path, string value)
        {
            //at some point this json should be cached somewhere
            string jsonText = App.DocumentManager.GetOpenDocJson();
            var obj = JObject.Parse(jsonText);

            //Deal with the top layer (i.e. "RobotDocument")
            //strip off the top layer of the path if it matches
            var split = path.Split(new char[] { System.IO.Path.DirectorySeparatorChar }, 2, StringSplitOptions.None);
            if (split.Length < 2)
                return;

            //make sure this is the object
            bool success = false;
            foreach (var token in obj)
            {
                if (token.Key == "Name")
                {
                    if (token.Value.ToString() == split[0])
                    {
                        success = true;
                        break;
                    }
                }
            }
            if (!success)
                return;


            if (!UpdateDocumentRecursive(obj, split[1], value))
            {
                //TODO: log something
            }
            //now update the document
            var serializer = App.DocumentManager.GetAppropriateDocSerializer("blah.robot.json") as JsonLoader<RobotDocument>;
            App.DocumentManager.OpenDoc = serializer.Deserialize(obj.ToString());
        }
        /// <summary>
        /// recursive portion of <see cref="UpdateDocument(string, string)"/>
        /// </summary>
        /// <param name="obj">the next object to look down the tree of</param>
        /// <param name="path">the path with <paramref name="obj"/>'s name already stripped off</param>
        /// <param name="value">the value to update the found item with</param>
        /// <returns>if a proper value to update was found</returns>
        private bool UpdateDocumentRecursive(JObject obj, string path, string value)
        {
            string objName;
            string nextPath = StripPath(path, out objName);
            if (objName == "")
                return false;

            foreach (var token in obj)
            {
                //check if the value is of type obj recall the method
                if (token.Value.Type.ToString() == "Object")
                {
                    //is this the next subobject?
                    if (objName == token.Key)
                        return UpdateDocumentRecursive((JObject)token.Value, nextPath, value);
                }
                //if type is of array
                else if (token.Value.Type.ToString() == "Array")
                {
                    //loop through items in the array
                    foreach (var itm in token.Value)
                    {
                        string name = "";

                        //look for a "Name" attribute
                        var subObject = (JObject)itm;
                        foreach(var subToken in subObject)
                        {
                            if (subToken.Key == "Name")
                            {
                                name = subToken.Value.ToString();
                                break;
                            }
                        }
                        //if this name is the object name we are looking for, then use it
                        if (name == objName)
                            return UpdateDocumentRecursive(subObject, nextPath, value);
                    }
                }
                else if (token.Key == "$type")
                {
                    //DO NOTHING
                }
                else
                {
                    //if token.Value is not nested
                    if(objName == token.Key)
                    {
                        if (!token.Value.HasValues)
                        {
                            obj[token.Key] = value;
                            return true;
                        }
                    }
                }

            }
            return false;
        }
        /// <summary>
        /// takes a string path and separates the root of the path from the remaining parts
        /// </summary>
        /// <param name="path">the path to strip</param>
        /// <param name="root">the name of the root</param>
        /// <returns>the path after the root</returns>
        private string StripPath(string path, out string root)
        {
            var split = path.Split(new char[] { System.IO.Path.DirectorySeparatorChar }, 2, StringSplitOptions.None);
            if (split.Length >= 2)
            {
                root = split[0];
                return split[1];
            }
            if (split.Length == 1)
            {
                root = split[0];
                return "";
            }
            root = "";
            return null;
        }
        /// <summary>
        /// Generate a <see cref="KeyValueTreeNode"/> from a set of json text
        /// Adapted from: http://stackoverflow.com/questions/18769634/creating-tree-view-dynamically-according-to-json-text-in-winforms
        /// </summary>
        /// <param name="jsonText">the json serialized text; <see cref="TypeNameHandling"/> must be set to <see cref="TypeNameHandling.All"/></param>
        /// <returns>the entire tree generated from the json text</returns>
        public static KeyValueTreeNode JsonToDictionaryTree(string jsonText)
        {
            return JsonToDictionaryTree(JObject.Parse(jsonText));
        }
        /// <summary>
        /// Generate a <see cref="KeyValueTreeNode"/> from a <see cref="JObject"/>
        /// </summary>
        /// <param name="obj">the json object to turn into a tree node</param>
        /// <returns>the entire tree generated from <paramref name="obj"/></returns>
        private static KeyValueTreeNode JsonToDictionaryTree(JObject obj)
        {
            //creat the parent
            KeyValueTreeNode parent = new KeyValueTreeNode(GetObjectName(obj), GetObjectType(obj));

            //iterate through each token of the object
            foreach(var token in obj)
            {
                KeyValueTreeNode child = new KeyValueTreeNode(token.Key, "");

                //check if the value is of type obj recall the method
                if (token.Value.Type.ToString() == "Object")
                {
                    //create a new JObject using the the Token.value
                    JObject o = (JObject)token.Value;
                    //recall the method (this also handles the "type" and "name" retrieving)
                    child = JsonToDictionaryTree(o);
                    if (null == child)
                        continue;
                    //the programatic name of an object overwrites any kind of given name it has
                    //      (the only use for the "Name" attribute is if it is a part of a collection)
                    child.Key = token.Key;
                    //add the child to the parentNode
                    parent.AddChild(child);
                }
                //if type is of array
                else if (token.Value.Type.ToString() == "Array")
                {
                    //loop though the array (this does not add a child type AT ALL, becuase array types
                    //  will have the "$values" layer in between)
                    foreach (var itm in token.Value)
                    {
                        var itmChild = JsonToDictionaryTree((JObject)itm);
                        if (null == itmChild)
                            continue;
                        parent.AddChild(itmChild);
                    }
                }
                else if (token.Key == "$type")
                {
                    //DO NOTHING
                }
                else
                {
                    //if token.Value is not nested
                    // child.Text = token.Key.ToString();
                    //change the value into N/A if value == null or an empty string 
                    if (token.Value.ToString() == "")
                        child.Value = "N/A";
                    else
                        child.Value = token.Value.ToString();
                    parent.AddChild(child);
                }
            }

            return parent;
        }
        /// <summary>
        /// parses a <see cref="JObject"/> <code>$type</code> parameter into a string
        /// </summary>
        /// <param name="obj">the <see cref="JObject"/> to get the type from</param>
        /// <returns>the string representation of <paramref name="obj"/> type</returns>
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
        /// <summary>
        /// Gets the name of an object from its <code>Name</code> Attribute
        /// </summary>
        /// <param name="obj">the JObject to get the name from</param>
        /// <returns>the name of the object as loaded from the json <code>Name</code> attribute</returns>
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
    }
}
