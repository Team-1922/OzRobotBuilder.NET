using CommonLib.ExtLua;
using CommonLib.Model.PrimaryTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.ExtLua
{
    class ScriptOverride
    {
        private string OverriddenMethodsHeader = "";
        private Dictionary<string, string> OverriddenMethods = new Dictionary<string, string>();
        private OzLuaState LuaStateRef;

        public ScriptOverride(ref OzLuaState luaState)
        {
            LuaStateRef = luaState;
        }

        public void LoadOverriddenMethods(string script)
        {
            /*
             * 
             * Scripts should be in this format:
             * 
              
             
             @__ALLMETHODS__
            luanet.load_assembly 'System'
            Console = luanet.import_type 'System.Console'

            @FunctionName1
            --Parameters:
            --	method
            Console.WriteLine(method)

            @FunctionName2
            Console.WriteLine('OtherTest overriden')
             
             *
             * NOTE: the comments below "@Test" are just a reminder of what the parameters are; it doesn't change which parameters are sent
             * 
             * the @__ALLMETHODS__ does not have to be first, but it is convenient for it to be
             * 
             */
            string[] chunks = script.Split('@');

            //the 0th chunk is always empty

            for (int i = 1; i < chunks.Length; ++i)
            {
                string thisChunk = chunks[i];
                string test = thisChunk.Substring(0, 14);
                if (thisChunk.Substring(0, 14) == "__ALLMETHODS__")
                {
                    OverriddenMethodsHeader = thisChunk.Substring(14);
                    continue;
                }

                //get each method's section
                string[] lines = thisChunk.Split(new string[] { "\r\n", "\n" }, 2, StringSplitOptions.None);

                //we have a problem
                if (2 != lines.Length)
                    continue;

                //the first line is NOT lua script, it is a header for the function
                OverriddenMethods[lines[0]] = lines[1];
            }
        }

        public bool CallOverriddenMethod(string methodName, Dictionary<string, object> param, out object[] returnData)
        {
            returnData = null;

            //make sure this is overriden
            if (!OverriddenMethods.ContainsKey(methodName))
                return false;

            //load up the params
            if (null != param)
            {
                foreach (var pair in param)
                {
                    LuaStateRef[pair.Key] = pair.Value;
                }
            }

            //get the script to run
            string scriptText = OverriddenMethods[methodName];

            //run the script
            returnData = LuaStateRef.DoString(scriptText);

            return true;
        }
        public bool CallOverriddenMethod(string methodName, Dictionary<string, object> param)
        {
            object[] dummy;
            return CallOverriddenMethod(methodName, param, out dummy);
        }
        public bool CallOverriddenMethod(string methodName)
        {
            return CallOverriddenMethod(methodName, null);
        }
    }
}
