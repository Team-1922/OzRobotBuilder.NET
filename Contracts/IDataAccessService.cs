using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// This is a holding class for a data-access class
    /// </summary>
    public interface IDataAccessService
    {
        /// <summary>
        /// The instace of the data to access
        /// </summary>
        IHierarchialAccess DataInstance { get; set; }
    }
}
