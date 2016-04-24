using NLua;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsumerProgram.ExtLua
{
    public class OzLuaState : Lua
    {
        //this is for keeping track of existing chunks to allow for class overriding with scripts easier
        private List<string> ExistingChunks = new List<string>();

        //override this so we can inject the custom code to 
        //  extend c# class with extra member functions
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

        public new object[] DoString(byte[] chunk, string chunkName = "chunk")
        {
            if (!ExistingChunks.Contains(chunkName))
                ExistingChunks.Add(chunkName);
            return base.DoString(chunk, chunkName);
        }
        public new object[] DoString(string chunk, string chunkName = "chunk")
        {
            if (!ExistingChunks.Contains(chunkName))
                ExistingChunks.Add(chunkName);
            return base.DoString(chunk, chunkName);
        }

        public static string RandomChunkName(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string GetUniqueChunkName()
        {
            string chunkName = RandomChunkName(8);
            while (ExistingChunks.Contains(chunkName))
            {
                chunkName = RandomChunkName(8);
            }

            ExistingChunks.Add(chunkName);
            return chunkName;
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
