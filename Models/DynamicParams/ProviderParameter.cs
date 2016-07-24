using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;

namespace Team1922.MVVM.Models.DynamicParams
{
    /// <summary>
    /// The requirements for a function parameter
    /// TODO: support type-safe parameters
    /// </summary>
    public class ProviderParameter : BindableBase, INamedClass
    {
        /// <summary>
        /// the parameter name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        

        #region Private Fields
        private string _name = "Param";
        #endregion
    }
}
