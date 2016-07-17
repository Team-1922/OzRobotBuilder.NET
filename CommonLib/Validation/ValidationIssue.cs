using CommonLib.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Validation
{
    /// <summary>
    /// A base for validation issues
    /// </summary>
    public interface IValidationIssue
    {
        /// <summary>
        /// Where the validation issue occured
        /// </summary>
        string Location { get; }
        /// <summary>
        /// A description of the issue
        /// </summary>
        string Message { get; }
        /// <summary>
        /// Which setting triggered this issue
        /// </summary>
        ValidationSetting SettingTrigger { get; }
    }
    /// <summary>
    /// contians the bare implementation of the <see cref="IValidationIssue"/> interfacec
    /// </summary>
    public class ValidationIssue : IValidationIssue
    {
        /// <summary>
        /// Constructor for readonly class
        /// </summary>
        /// <param name="location"><see cref="IValidationIssue.Location"/></param>
        /// <param name="message"><see cref="IValidationIssue.Message"/></param>
        /// <param name="trigger"><see cref="IValidationIssue.SettingTrigger"/></param>
        protected ValidationIssue(string location, string message, ValidationSetting trigger)
        {
            Location = location;
            Message = message;
            SettingTrigger = trigger;
        }
        #region IValidationIssue Implementation
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Location { get; }
        public string Message { get; }
        public ValidationSetting SettingTrigger { get; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        #endregion
    }
    /// <summary>
    /// The validation issue for <see cref="ValidationSetting.NullValues"/>
    /// </summary>
    public class NullValueValidationIssue : ValidationIssue
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">a directory-style path to the object which is null</param>
        public NullValueValidationIssue(string location) : base(location, string.Format("{0} was null", location), ValidationSetting.NullValues)
        { }
    }
    /// <summary>
    /// The validation issue for <see cref="ValidationSetting.UnusedElements"/>
    /// </summary>
    public class UnusedElementValidationIssue : ValidationIssue
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">a directory-style path to the object which is unused</param>
        public UnusedElementValidationIssue(string location) : base(location, string.Format("{0} is never referenced", location), ValidationSetting.UnusedElements)
        { }
    }
    /// <summary>
    /// The validation issue for <see cref="ValidationSetting.DefaultValues"/>
    /// </summary>
    public class DefaultValueValidationIssue : ValidationIssue
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">a directory-style path to the object which is default</param>
        public DefaultValueValidationIssue(string location) : base(location, string.Format("{0} contains its default value", location), ValidationSetting.DefaultValues)
        { }
    }
    /// <summary>
    /// The validation issue for <see cref="ValidationSetting.ReusedIds"/>
    /// TODO: at some point also include a way to identify WHAT kind of ID was reused
    /// </summary>
    public class ReusedIdValidationIssue : ValidationIssue
    {
        /// <summary>
        /// a list of directory-style paths to the other objects which reuse this id
        /// </summary>
        public string[] OtherIdUseLocations { get; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">a directory-style path to the object which is reused</param>
        /// <param name="otherReferences">a list of directory-style paths to the other objects which reuse this id</param>
        public ReusedIdValidationIssue(string location, string[] otherReferences) : base(location, string.Format("{0} contains an ID already used; Other References: {1}", location, string.Join(", ", otherReferences)), ValidationSetting.ReusedIds)
        {
            OtherIdUseLocations = otherReferences;
        }
    }
    /// <summary>
    /// The validation issue for <see cref="ValidationSetting.ReusedNames"/>
    /// </summary>
    public class ReusedNameValidationIssue : ValidationIssue
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">a directory-style path to the object which is default</param>
        public ReusedNameValidationIssue(string location) : base(location, string.Format("{0} is used multiple times", location), ValidationSetting.ReusedNames)
        { }
    }
    /// <summary>
    /// The validation issue for <see cref="ValidationSetting.IllogicalValues"/>
    /// </summary>
    public class IllogicalValueValidationIssue : ValidationIssue
    {
        /// <summary>
        /// A string representation of the illogical value found at <see cref="ValidationIssue.Location"/>
        /// </summary>
        public string IllogicalValue { get; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">a directory-style path to the object which is default</param>
        /// <param name="value">the actual value of the object at <paramref name="location"/></param>
        public IllogicalValueValidationIssue(string location, string value) : base(location, string.Format("{0} contains an illogical value; Value = \"{1}\"", location, value), ValidationSetting.IllogicalValues)
        {
            IllogicalValue = value;
        }
    }
}
