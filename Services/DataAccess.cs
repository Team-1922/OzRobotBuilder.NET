﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.Services
{
    /// <summary>
    /// Represents the global data instance (potentially plural in the future)
    /// NOTE: Eventually this will be ugraded with Import/Export
    /// </summary>
    public static class DataAccess
    {
        private static IDataAccessService _dataAccess;
        /// <summary>
        /// The global IDataAccessService instance
        /// </summary>
        public static IDataAccessService Instance
        {
            get
            {
                if (null == _dataAccess)
                    throw new NullReferenceException("DataAccess Service was Null! Call DataAccess.Init(IDataAccessService) before accessing DataAccess.Instance");
                return _dataAccess;
            }
        }
        /// <summary>
        /// Called by the consumer to register the one global data access class
        /// </summary>
        /// <param name="dataAccess">the global data instance</param>
        public static void Init(ref IHierarchialAccess dataAccess)
        {
            if (_dataAccess != null)
                throw new Exception("DataAccess Service Is Already Initialized!");
            _dataAccess = new DataAccessService() { DataInstance = dataAccess };
        }
    }
}
