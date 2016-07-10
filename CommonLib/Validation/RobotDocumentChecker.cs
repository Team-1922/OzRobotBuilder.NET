using CommonLib.Model;
using CommonLib.Model.CompositeTypes;
using CommonLib.Model.Documents;
using CommonLib.Model.PrimaryTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Validation
{
    /// <summary>
    /// Validates a robot document
    /// TODO: encapsulate individual operations better to clean up code
    /// </summary>
    public class RobotDocumentChecker : IObjectChecker<RobotDocument>
    {
        #region Subobject Checkers
        private SubsystemChecker SubsystemCheckerInst { get; } = new SubsystemChecker();
        private CommandChecker CommandCheckerInst { get; } = new CommandChecker();
        private JoystickChecker JoystickCheckerInst { get; } = new JoystickChecker();
        private TriggerChecker TriggerCheckerInst { get; } = new TriggerChecker();
        #endregion

        /// <summary>
        /// Takes a robot document and returns any issues the document has
        /// </summary>
        /// <param name="obj">the document to validate</param>
        /// <param name="settings">the types of validation issues to handle</param>
        /// <param name="workingPath">unused; this is for recursion of subobjects</param>
        /// <returns>a report of any issues <paramref name="obj"/> has</returns>
        public ValidationReport ValidateObject(RobotDocument obj, ValidationSettings settings, string workingPath = "")
        {
            ValidationReport ret = new ValidationReport(settings);

            if (null == obj)
            {
                ret.ValidationIssues.Add(new NullValueValidationIssue("RobotDocument"));
                return ret;//return early if obj is null
            }
            workingPath = obj.Name;
            ret.ValidationIssues.AddRange(ValidateSubsystems(obj, settings, workingPath).ValidationIssues);
            ret.ValidationIssues.AddRange(ValidateCommands(obj, settings, workingPath).ValidationIssues);
            ret.ValidationIssues.AddRange(ValidateJoysticks(obj, settings, workingPath).ValidationIssues);
            ret.ValidationIssues.AddRange(ValidateTriggers(obj, settings, workingPath).ValidationIssues);

            return ret;
        }

        #region Static Helper Methods
        /// <summary>
        /// Extends the working path with given path and next sublevel
        /// </summary>
        /// <param name="path">the path to extend</param>
        /// <param name="nextLocation">the name of the next sublevel to extend to</param>
        /// <returns></returns>
        protected static string ExtendWorkingPath(string path, string nextLocation)
        {
            return string.Format("{0}{1}{2}", path, Path.DirectorySeparatorChar, nextLocation);
        }
        /// <summary>
        /// Gets the names of the objects in <paramref name="items"/> with each id; used to get which id's have overlap
        /// </summary>
        /// <typeparam name="T">Adds generalization to this method so any type with a name and ID can be used</typeparam>
        /// <param name="items">the items to process</param>
        /// <param name="workingPath">the current working path to prepend each name in <paramref name="items"/> with in <paramref name="output"/></param>
        /// <param name="output">a map of each used ID and the path to which item is using it</param>
        protected static void GetNamesOfId<T>(List<T> items, string workingPath, ref Dictionary<uint, List<string>> output) where T : INamedClass, IIdentificationNumber
        {
            if (output == null)
                return;
            if (items == null)
                return;
            foreach (var item in items)
            {
                if (item == null)
                    continue;
                string workingItemPath = ExtendWorkingPath(workingPath, item.Name);
                if (output.ContainsKey(item.ID))
                    output[item.ID].Add(workingItemPath);
                else
                    output.Add(item.ID, new List<string>() { workingItemPath });
            }
        }
        /// <summary>
        /// Gets the number of different items with the same path
        /// </summary>
        /// <typeparam name="T">adds generalization to this method so any type with a name can be used</typeparam>
        /// <param name="items">the items to get the name from</param>
        /// <param name="workingPath">the current working path to prepend each name in <paramref name="items"/> with in <paramref name="output"/></param>
        /// <param name="output">a map of each name used and the number of times each is used</param>
        protected static void GetNamesCount<T>(List<T> items, string workingPath, ref Dictionary<string, int> output) where T : INamedClass
        {
            if (output == null)
                return;
            if (items == null)
                return;
            foreach(var item in items)
            {
                if (item == null)
                    continue;

                string workingItemPath = ExtendWorkingPath(workingPath, item.Name);
                if (output.ContainsKey(workingItemPath))
                    output[workingItemPath]++;
                else
                    output.Add(workingItemPath, 1);
            }
        }
        /// <summary>
        /// Converts the output of <see cref="GetNamesOfId{T}(List{T}, string, ref Dictionary{uint, List{string}})"/> to a list of <see cref="ReusedIdValidationIssue"/>
        /// </summary>
        /// <param name="map">the output to <see cref="GetNamesOfId{T}(List{T}, string, ref Dictionary{uint, List{string}})"/></param>
        /// <returns>a list of <see cref="ReusedIdValidationIssue"/></returns>
        protected static List<ReusedIdValidationIssue> GetIssuesFromIdMap(Dictionary<uint, List<string>> map)
        {
            List<ReusedIdValidationIssue> ret = new List<ReusedIdValidationIssue>();
            foreach(var mapItem in map)
            {
                if (mapItem.Value.Count > 1)
                    ret.Add(new ReusedIdValidationIssue(mapItem.Value[0], mapItem.Value.GetRange(1, mapItem.Value.Count - 1).ToArray()));
            }
            return ret;
        }
        #endregion

        #region Subobject Validation
        private ValidationReport ValidateSubsystems(RobotDocument obj, ValidationSettings settings, string workingPath)
        {
            ValidationReport ret = new ValidationReport(settings);
            workingPath = ExtendWorkingPath(workingPath, "Subsystems");

            #region Null Validation
            if (null == obj.Subsystems)//container null-check
            {
                ret.ValidationIssues.Add(new NullValueValidationIssue(workingPath));
                return ret;
            }
            else
                for (int i = 0; i < obj.Subsystems.Count; ++i)
                    if (null == obj.Subsystems[i])//individual null-check
                        ret.ValidationIssues.Add(new NullValueValidationIssue(string.Format("{0}[{1}]", workingPath, i)));
                    else
                        ret.ValidationIssues.AddRange(SubsystemCheckerInst.ValidateObject(obj.Subsystems[i], settings, workingPath).ValidationIssues);//individual validation
            #endregion

            #region Name Validation
            //group validation (name validation)
            if (settings.Contains(ValidationSetting.ReusedNames))
            {
                //a map for each name used
                var usedNames = new Dictionary<string, int>();

                //names for subsystems
                GetNamesCount(obj.Subsystems, workingPath, ref usedNames);

                //get the issues from these names
                foreach(var name in usedNames)
                {
                    if (name.Value > 1)
                        ret.ValidationIssues.Add(new ReusedNameValidationIssue(name.Key));
                }
            }
            #endregion

            #region Reused ID Validation
            if(settings.Contains(ValidationSetting.ReusedIds))
            {
                //a map for each id used
                var usedAnalogInputs = new Dictionary<uint, List<string>>();
                var usedPWMOutputs = new Dictionary<uint, List<string>>();
                var usedSRXOutputs = new Dictionary<uint, List<string>>();
                var usedDigitalInputs = new Dictionary<uint, List<string>>();

                //go through each subsystem once
                foreach (var subsystem in obj.Subsystems)
                {
                    if (subsystem == null)
                        continue;

                    string workingSubsystemPath = ExtendWorkingPath(workingPath, subsystem.Name);
                    
                    //analog inputs
                    GetNamesOfId(subsystem.AnalogInputDevices, workingSubsystemPath, ref usedAnalogInputs);

                    //PWM outputs
                    GetNamesOfId(subsystem.PWMMotorControllers, workingSubsystemPath, ref usedPWMOutputs);

                    //CAN Ids
                    GetNamesOfId(subsystem.TalonSRXs, workingSubsystemPath, ref usedSRXOutputs);

                    //digital inputs
                    GetNamesOfId(subsystem.DigitalInputs, workingSubsystemPath, ref usedDigitalInputs);

                    //special instance for quad encoders
                    foreach (var quadEncoder in subsystem.QuadEncoders)
                    {
                        if (quadEncoder == null)
                            continue;
                        string workingEncoderPath = ExtendWorkingPath(workingSubsystemPath, quadEncoder.Name);
                        if (usedDigitalInputs.ContainsKey(quadEncoder.ID))
                            usedDigitalInputs[quadEncoder.ID].Add(workingEncoderPath);
                        else
                            usedDigitalInputs.Add(quadEncoder.ID, new List<string>() { workingEncoderPath });

                        if (usedDigitalInputs.ContainsKey(quadEncoder.ID1))
                            usedDigitalInputs[quadEncoder.ID1].Add(workingEncoderPath);
                        else
                            usedDigitalInputs.Add(quadEncoder.ID1, new List<string>() { workingEncoderPath });
                    }

                }

                //now create the issues from any conflicts
                ret.ValidationIssues.AddRange(GetIssuesFromIdMap(usedAnalogInputs));
                ret.ValidationIssues.AddRange(GetIssuesFromIdMap(usedPWMOutputs));
                ret.ValidationIssues.AddRange(GetIssuesFromIdMap(usedSRXOutputs));
                ret.ValidationIssues.AddRange(GetIssuesFromIdMap(usedDigitalInputs));
            }
            #endregion

            //IllogicalValues, ReusedNames, and DefaultValues are handled by the individual handlers

            //TODO: UnusedElements

            return ret;
        }
        private ValidationReport ValidateCommands(RobotDocument obj, ValidationSettings settings, string workingPath)
        {
            ValidationReport ret = new ValidationReport(settings);
            workingPath = ExtendWorkingPath(workingPath, "Commands");

            if (null == obj.Commands)//container null-check
                ret.ValidationIssues.Add(new NullValueValidationIssue(workingPath));
            else
                for (int i = 0; i < obj.Commands.Count; ++i)
                    if (null == obj.Commands[i])//individual null-check
                        ret.ValidationIssues.Add(new NullValueValidationIssue(string.Format("{0}[{1}]", workingPath, i)));
                    else
                        ret.ValidationIssues.AddRange(CommandCheckerInst.ValidateObject(obj.Commands[i], settings, workingPath).ValidationIssues);//individual validation

            return ret;
        }
        private ValidationReport ValidateJoysticks(RobotDocument obj, ValidationSettings settings, string workingPath)
        {
            ValidationReport ret = new ValidationReport(settings);
            workingPath = ExtendWorkingPath(workingPath, "Joysticks");

            if (null == obj.Joysticks)//container null-check
                ret.ValidationIssues.Add(new NullValueValidationIssue(workingPath));
            else
                for (int i = 0; i < obj.Joysticks.Count; ++i)
                    if (null == obj.Joysticks[i])//individual null-check
                        ret.ValidationIssues.Add(new NullValueValidationIssue(string.Format("{0}[{1}]", workingPath, i)));
                    else
                        ret.ValidationIssues.AddRange(JoystickCheckerInst.ValidateObject(obj.Joysticks[i], settings, workingPath).ValidationIssues);//individual validation

            return ret;
        }
        private ValidationReport ValidateTriggers(RobotDocument obj, ValidationSettings settings, string workingPath)
        {
            ValidationReport ret = new ValidationReport(settings);
            workingPath = ExtendWorkingPath(workingPath, "Triggers");

            if (null == obj.Triggers)//container null-check
                ret.ValidationIssues.Add(new NullValueValidationIssue(workingPath));
            else
                for (int i = 0; i < obj.Triggers.Count; ++i)
                    if (null == obj.Triggers[i])//individual null-check
                        ret.ValidationIssues.Add(new NullValueValidationIssue(string.Format("{0}[{1}]", workingPath, i)));
                    else
                        ret.ValidationIssues.AddRange(TriggerCheckerInst.ValidateObject(obj.Triggers[i], settings, workingPath).ValidationIssues);//individual validation

            //group validation

            return ret;
        }
        #endregion
    }
}
