using CommonLib.ExtLua;
using CommonLib.Interfaces;
using CommonLib.Model.PrimaryTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.CompositeTypes
{
    /*
     * 
     * NOTE: any extra member variables the script needs as part of the command should be stored in the <CommandName>_ext table
     * 
     */
    public class OzCommandData : ILuaExt
    {
        ScriptOverride Override;
        private string ExtraMethods = "";
        
        public OzCommandData(ref OzLuaState luaState, string overrideMethodsScript, string extraMethodsScript)
        {
            Override = new ScriptOverride(ref luaState);
            Override.LoadOverriddenMethods(overrideMethodsScript);
            ExtraMethods = extraMethodsScript;
        }

        public void Construct(Dictionary<string, object> parameters)
        {
            //if this fails, then the script did NOT override this method, therefore nothing needs to happen
            Override.CallOverriddenMethod("Construct", parameters);
        }
        public void Initialize()
        {
            //if this fails, then the script did NOT override this method, therefore nothing needs to happen
            Override.CallOverriddenMethod("Initialize");
        }
        public void Execute()
        {
            //if this fails, then the script did NOT override this method, therefore nothing needs to happen
            Override.CallOverriddenMethod("Execute");
        }
        public bool IsFinished()
        {
            //this is the only method which returns a value, therefore its the only method we care what the output is
            object[] retVal;

            //try calling the method
            if(Override.CallOverriddenMethod("Execute", null, out retVal))
            {
                //make sure this method returned something
                if (null == retVal)
                    return true;    //to be SAFE make sure this ends
                if (retVal.Length < 1)
                    return true;    //to be SAFE make sure this ends

                //Parse the result to a boolean value
                bool retValBool;
                bool success = Boolean.TryParse(retVal[0].ToString(), out retValBool);
                if (!success)
                    return true;//if the return value CLEARLY was NOT a boolean, be safe and end

                //successful method call
                return retValBool;
            }

            return true;
        }
        public void End()
        {
            //if this fails, then the script did NOT override this method, therefore nothing needs to happen
            Override.CallOverriddenMethod("End");
        }
        public void Interrupted()
        {
            //if this fails, then the script did NOT override this method, therefore nothing needs to happen
            Override.CallOverriddenMethod("Interrupted");
        }

        public string GetFormattedExtScriptText()
        {
            return ExtraMethods;
        }
    }
}
