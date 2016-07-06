using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    /// <summary>
    /// The data needed to have a script-extensable class
    /// </summary>
    public class ScriptExtensableData
    {
        /// <summary>
        /// The additional methods added to this object
        /// </summary>
        public string ScriptExtension { get; set; }
        /// <summary>
        /// The existing methods which are overidden 
        /// TODO: get a good example of this
        /// </summary>
        public string OverriddenMethods { get; set; }
    }
}
