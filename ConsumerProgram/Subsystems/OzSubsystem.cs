using ConsumerProgram.ExtLua;
using System;
using System.Collections.Generic;
using System.Reflection;
using WPILib.Commands;

namespace ConsumerProgram.Subsystems
{
    public class OzSubsystem : Subsystem, ILuaExt, IResData
    {
        ScriptOverride OverrideMethod;
        WPILib.CANTalon

        public OzSubsystem(ref OzLuaState luaState, IResData scriptData)
        {
            if (null == luaState)
                throw new ArgumentException("Null OzLuaState", "luaState");

            OverrideMethod = new ScriptOverride(ref luaState);
        }

        public void SetExtScript(string resPath)
        {
            WPILib.CANTalon
        }

        public string GetFormattedExtScriptText()
        {
            return "";
        }

        // Put methods for controlling this subsystem
        // here. Call these from Commands.

        protected override void InitDefaultCommand()
        {
            // Set the default command for a subsystem here.
            //SetDefaultCommand(new MySpecialCommand());
        }

        public string ExampleMethod(string exampleParam)
        {
            //load up the parameters into the dictionary
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["exampleParam"] = exampleParam;

            object[] retData;

            //attempt to call the overriden method
            if (OverrideMethod.CallOverriddenMethod(MethodBase.GetCurrentMethod().Name, param, out retData))
            {
                if (null == retData)
                    return "";
                if (retData.Length == 0)
                    return "";
                return retData[0].ToString();
            }

            //remaining code is if the method is NOT overriden
            return "TESTING";
        }

        public string GetHumanReadable()
        {
            //return something JSON-y
            throw new NotImplementedException();
        }

        public void CleanUp()
        {
            //destroy/unregister WPILib instances
            throw new NotImplementedException();
        }
    }
}
