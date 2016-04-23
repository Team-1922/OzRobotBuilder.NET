using NLua;
using System;

namespace ConsumerProgram.ExtLua
{
    class OzLuaState : Lua
    {
        //override this so we can inject the custom code to 
        //  extend c# class with extra member functions
        public new object this[string name] 
        {
            get { return base[name]; }
            set
            {
                base[name] = value;
                if(value is ILuaSendable)
                {
                    /*
                     * This section is NOT very safe and needs some revision
                     */
                    ILuaSendable castedValue = (ILuaSendable)value;
                    DoString(String.Format(castedValue.GetFormattedExtScriptText(), name));
                }
            }
        }

        //I am not sure where to put these features yet, but here seems good for now

        //The extended features added to the class through lua is in a script file, but
        //  must be formattable in order to extend each instance of the class
        //  These rules may change over time, but also might become a script file themself
        public static string FormatExtScript(string script, string className)
        {
            return 
                "{0}_ext = {{}}" +
                script
                    .Replace("{", "{{")
                    .Replace("}", "}}")
                    .Replace(className + ".", "{0}_ext:")
                    .Replace(className, "{0}_ext")
                    .Replace("self", "{0}");
        }
    }
}
