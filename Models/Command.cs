using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models.DynamicParams;

namespace Team1922.MVVM.Models.Deprecated
{
    /// <summary>
    /// The data to create a dynamically loaded command
    /// This is currently closely following the provided command system, however it will 
    ///     likely change to something that works better with the event system
    /// </summary>
    public class Command : BindableBase, INamedClass
    {
        /// <summary>
        /// The parameters to be passed to this command
        /// </summary>
        public ParameterProviderCollection Params { get; set; } = new ParameterProviderCollection();
        /// <summary>
        /// The name of the command
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        /// <summary>
        /// The script for the command constructor
        /// </summary>
        public string Construct
        {
            get { return _construct; }
            set { SetProperty(ref _construct, value); }
        }
        /// <summary>
        /// The script for the command Init
        /// </summary>
        public string Init
        {
            get { return _init; }
            set { SetProperty(ref _init, value); }
        }
        /// <summary>
        /// The script for the command Execute
        /// </summary>
        public string Execute
        {
            get { return _execute; }
            set { SetProperty(ref _execute, value); }
        }
        /// <summary>
        /// The script for the command IsFinished
        /// </summary>
        public string IsFinished
        {
            get { return _isFinished; }
            set { SetProperty(ref _isFinished, value); }
        }
        /// <summary>
        /// The script for the command End
        /// </summary>
        public string End
        {
            get { return _end; }
            set { SetProperty(ref _end, value); }
        }
        /// <summary>
        /// The script for the command Interrupted
        /// </summary>
        public string Interrupted
        {
            get { return _interrupted; }
            set { SetProperty(ref _interrupted, value); }
        }

        #region Private Fields
        private string _name = "Command";
        private string _construct = "";
        private string _init = "";
        private string _execute = "";
        private string _isFinished = "";
        private string _end = "";
        private string _interrupted = "";
        #endregion
    }
}
