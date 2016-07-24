using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models.DynamicParams;

namespace Team1922.MVVM.Models
{
    /// <summary>
    /// The base trigger data which contains information about the command which is called
    /// NOTE: this will likely be replaced with events in the future, leveraging the <see cref="Framework.BindableBase"/>
    /// </summary>
    public class Trigger : BindableBase, INamedClass
    {
        /// <summary>
        /// the name of this trigger
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        /// <summary>
        /// the command to run when the trigger condition is met
        /// </summary>
        public string CommandName
        {
            get { return _commandName; }
            set { SetProperty(ref _commandName, value); }
        }
        /// <summary>
        /// the parameters to the command name
        /// </summary>
        public ParameterConsumerCollection CommandParams { get; set; } = new ParameterConsumerCollection();

        #region Private Fields
        private string _name = "Trigger";
        private string _commandName;
        #endregion
    }
}
