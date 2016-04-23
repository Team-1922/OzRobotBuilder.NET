using System;
using System.Collections.Generic;
using System.Xml;
using WPILib;

namespace ConsumerProgram.ResManager.Data
{
    /**
     * The RobotMap is a mapping from the ports sensors and actuators are wired into
     * to a variable name. This provides flexibility changing wiring, makes checking
     * the wiring easier and significantly reduces the number of magic numbers
     * floating around.
     */
    public class RobotMap
    {
        XmlResData XmlData;

        public void ReloadXml(ref IResData xmlData)
        {
            XmlResData resData = xmlData as XmlResData;
            if (null == resData)
            {
                throw new ArgumentException("Invalid Data Type", "xmlData");
            }

            //no sense in trying anything if there is no data
            if (null == resData.FirstChild)
                return;

            XmlData = resData;
        }


        //in "key", the name of the root node SHOULD be omitted
        //      Format: "node-name/child-name/another-child-name.attribute-name" for getting an attribute
        //              "node-name/child-name/another-child-name" for getting all of the XML contained within the "another-child-name" block

        public string GetValue(string key)
        {
            XmlNode root = XmlData.FirstChild;
            if (null == root)
                return "";
            if (key == "RobotMap")
                return root.Value;
            if (null == root.FirstChild)
                return "";
            return RecurseGetVal(root, key);
        }
        public int GetValueI(string key)
        {
            string value = GetValue(key);
            int ret = 0;
            int.TryParse(value, out ret);

            //this is OK, becuase if "TryParse" Failes, ret = 0
            return ret;
        }
        public float GetValueF(string key)
        {
            string value = GetValue(key);
            float ret = 0.0f;
            float.TryParse(value, out ret);

            //this is OK, becuase if "TryParse" Failes, ret = 0
            return ret;
        }
        public double GetValueD(string key)
        {
            string value = GetValue(key);
            double ret = 0.0;
            double.TryParse(value, out ret);

            //this is OK, becuase if "TryParse" Failes, ret = 0
            return ret;
        }

        #region Private Member Functions
        private string RecurseGetVal(XmlNode prevNode, string subseqNodes)
        {
            string[] splitNodes = subseqNodes.Split(new char[] { '/' }, 2);

            //this means it's the attribute; no more child nodes
            if (splitNodes.Length < 2)
            {
                splitNodes = subseqNodes.Split(new char[] { '.' }, 2);

                //this means the user wants to query the ENTIRE node
                if (splitNodes.Length < 2)
                {
                    foreach (XmlNode node in prevNode.ChildNodes)
                    {
                        if (node.Name == subseqNodes)
                            return node.Value;
                    }
                    //none found
                    return "";
                }


                XmlNode attb = prevNode.Attributes.GetNamedItem(subseqNodes);
                if (null == attb)
                    return "";
                return attb.Value;
            }

            //if it is NOT an attribute this time, continue recursing
            foreach (XmlNode node in prevNode.ChildNodes)
            {
                if (node.Name == splitNodes[0])
                    return RecurseGetVal(node, splitNodes[1]);
            }

            //no node found
            return "";
        }
        #endregion
    }
}
