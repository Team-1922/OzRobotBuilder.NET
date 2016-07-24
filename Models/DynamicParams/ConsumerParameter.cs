using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;

namespace Team1922.MVVM.Models.DynamicParams
{
    /// <summary>
    /// the data for a function parameter
    /// TODO: support type-safe parameters
    /// </summary>
    public class ConsumerParameter : BindableBase, INamedClass
    {
        /// <summary>
        /// the parameter name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        /// <summary>
        /// the parameter value
        /// </summary>
        public string Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }


        #region Private Fields
        private string _name = "Param";
        private string _value;
        #endregion
    }
}
