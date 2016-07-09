using CommonLib.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Validation
{
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
    public class ValidationIssue : IValidationIssue
    {
        /// <summary>
        /// Constructor for readonly class
        /// </summary>
        /// <param name="location"><see cref="IValidationIssue.Location"/></param>
        /// <param name="message"><see cref="IValidationIssue.Message"/></param>
        /// <param name="trigger"><see cref="IValidationIssue.SettingTrigger"/></param>
        public ValidationIssue(string location, string message, ValidationSetting trigger)
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
    public class NullValueValidationIssue : ValidationIssue
    {
        public NullValueValidationIssue(string location) : base(location, string.Format("{0} was null", location), ValidationSetting.NullValues)
        { }
    }
    public class UnusedElementValidationIssue : ValidationIssue
    {
        public UnusedElementValidationIssue(string location) : base(location, string.Format("{0} is never referenced", location), ValidationSetting.UnusedElements)
        { }
    }
    public class DefaultValueValidationIssue : ValidationIssue
    {
        public DefaultValueValidationIssue(string location) : base(location, string.Format("{0} contains its default value", location), ValidationSetting.DefaultValues)
        { }
    }
    /// <summary>
    /// TODO: at some point also include a way to identify WHAT kind of ID was reused
    /// </summary>
    public class ReusedIdValidationIssue : ValidationIssue
    {
        public string[] OtherIdUseLocations { get; }
        public ReusedIdValidationIssue(string location, string[] otherReferences) : base(location, string.Format("{0} contains an ID already used; Other References: {1}", location, string.Join(", ", otherReferences)), ValidationSetting.ReusedIds)
        {
            OtherIdUseLocations = otherReferences;
        }
    }
    public class ReusedNameValidationIssue : ValidationIssue
    {
        public ReusedNameValidationIssue(string location) : base(location, string.Format("{0} is used multiple times", location), ValidationSetting.ReusedNames)
        { }
    }
    public class IllogicalValueValidationIssue : ValidationIssue
    {
        public string IllogicalValue { get; }
        public IllogicalValueValidationIssue(string location, string value) : base(location, string.Format("{0} contains an illogical value; Value = \"{1}\"", location, value), ValidationSetting.IllogicalValues)
        {
            IllogicalValue = value;
        }
    }
}
