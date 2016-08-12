using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.Services
{
    internal class DataAccessService : IDataAccessService
    {
        public IHierarchialAccess DataInstance { get; set; }
    }
}
