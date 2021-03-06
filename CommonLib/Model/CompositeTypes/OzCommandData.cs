﻿using CommonLib.Interfaces;
using CommonLib.Model.PrimaryTypes;
using CommonLib.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.CompositeTypes
{
    /// <summary>
    /// The data to create a dynamically loaded command
    /// </summary>
    public class OzCommandData : INamedClass, IValidatable
    {
        /// <summary>
        /// The name of the command
        /// </summary>
        public string Name { get; set; } = "OzCommandData";
        /// <summary>
        /// The lua script for the command constructor
        /// </summary>
        public string Construct { get; set; } = "";
        /// <summary>
        /// The lua script for the command Init
        /// </summary>
        public string Init { get; set; } = "";
        /// <summary>
        /// The lua script for the command Execute
        /// </summary>
        public string Execute { get; set; } = "";
        /// <summary>
        /// The lua script for the command IsFinished
        /// </summary>
        public string IsFinished { get; set; } = "";
        /// <summary>
        /// The lua script for the command End
        /// </summary>
        public string End { get; set; } = "";
        /// <summary>
        /// The lua script for the command Interrupted
        /// </summary>
        public string Interrupted { get; set; } = "";
        /// <summary>
        /// The lua script for the commands additional methods not specified above
        /// </summary>
        public string AddedMethods { get; set; } = "";
        /// <summary>
        /// Goes through each validation setting and member 
        /// </summary>
        /// <param name="settings">the active settings for validation</param>
        /// <param name="workingPath">the path for instance; used for traversal of hierarchial data types</param>
        /// <returns>a report of the validation issues</returns>
        public ValidationReport Validate(ValidationSettings settings, string workingPath)
        {
            ValidationReport ret = new ValidationReport(settings);
            workingPath = ValidationUtils.ExtendWorkingPath(workingPath, Name);

            if (settings.Contains(ValidationSetting.IllogicalValues))
            {
                if (!ValidationUtils.CheckName(Name))
                    ret.ValidationIssues.Add(new IllogicalValueValidationIssue(ValidationUtils.ExtendWorkingPath(workingPath, "Name"), Name));
                // TODO: check the different commands to make sure they meet all requirements
            }

            return ret;
        }
        //TODO: should we be using this?
        //public ScriptExtensableData ExtensableScriptData; 
    }

    /*
    /// <summary>
    /// The data to create a dynamically loaded command.
    /// Any extra member variables the script needs as part of the command should be stored in the <see cref="Name"/>_ext table.
    /// This also contains the methods of using the command.  This goes beyond just the raw data, however it does not include any robot-specific code so it is fine.
    /// </summary>
    public class OzCommand : ILuaExt
    {
        /// <summary>
        /// The data for the script override
        /// </summary>
        ScriptOverride Override;
        /// <summary>
        /// The data for the extra methods added to this command
        /// </summary>
        private string ExtraMethods = "";
        /// <summary>
        /// The name of this command
        /// </summary>
        public string Name = "OzCommandData";
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="luaState"></param>
        /// <param name="overrideMethodsScript"></param>
        /// <param name="extraMethodsScript"></param>
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
    }*/
}
