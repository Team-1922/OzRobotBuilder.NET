using NLua;
using CommonLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLib.ExtLua
{
    /// <summary>
    /// A small layer on top of the <see cref="Lua"/> class to allow custom lua injection
    /// </summary>
    public class OzLuaState : Lua
    {
        /// <summary>
        /// This command should be recalled if the lua environment crashes due to an exception
        /// TODO: handle exceptions better
        /// </summary>
        /// <param name="headerCode">lua script to be executed automatically</param>
        public void InitEnvHeader(string headerCode)
        {
            //ensure this has already been done
            LoadCLRPackage();

            //run the header for the entire rest of the environment to use
            DoString(headerCode);
        }
        
        /// <summary>
        /// Overriden in order to inject custom code to extend a c# class with extra member functions
        /// </summary>
        /// <param name="name">name of the instance to get/set</param>
        /// <returns>the object loaded with name <paramref name="name"/></returns>
        public new object this[string name] 
        {
            get { return base[name]; }
            set
            {
                base[name] = value;

                ILuaExt castedValue = value as ILuaExt;
                if (null == castedValue)
                    return;

                try
                {
                    DoString(String.Format(castedValue.GetFormattedExtScriptText(), name));
                }
                catch(FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch(NLua.Exceptions.LuaScriptException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch(NLua.Exceptions.LuaException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }


        /// <summary>
        /// The extended features added to the class through lua is in a script file, but 
        /// must be formattable in order to extend each instance of the class.
        /// These rules may change over time, but also might become a script file themself.
        /// TODO: I am not sure where to put these features yet, but here seems good for now
        /// </summary>
        /// <param name="script">the text of the script containing extentions to the lua class</param>
        /// <param name="objectName">the name of the object which is being extended</param>
        /// <returns>a formatted string to be run in the lua environment to add features to said class</returns>
        public static string FormatExtScript(string script, string objectName)
        {
            return 
                "{0}_ext = {{}}" +
                script
                    .Replace("{", "{{")
                    .Replace("}", "}}")
                    .Replace(objectName + ".", "{0}_ext:")
                    .Replace(objectName, "{0}_ext")
                    .Replace("self", "{0}");
        }
    }
}
