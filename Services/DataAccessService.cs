using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Services
{
    /// <summary>
    /// Represents the global data instance (potentially plural in the future)
    /// NOTE: Eventually this will be ugraded with Import/Export
    /// </summary>
    public static class DataAccessService
    {
        private static IDataAccessService _instance;
        /// <summary>
        /// The global IDataAccessService instance
        /// </summary>
        public static IDataAccessService Instance
        {
            get
            {
                if (null == _instance)
                    throw new NullReferenceException("DataAccess Service was Null! Call DataAccess.Init(IDataAccessService) before accessing DataAccess.Instance");
                return _instance;
            }
        }
        /// <summary>
        /// Called by the consumer to register the one global data access class
        /// </summary>
        /// <param name="dataAccess">the global data instance</param>
        public static void Init(IHierarchialAccess dataAccess)
        {
            if (_instance != null)
                throw new Exception("DataAccess Service Is Already Initialized!");
            _instance = new DataAccess() { DataInstance = dataAccess };
        }
        /// <summary>
        /// Clamps the attribute if clamping is enabled.
        /// </summary>
        /// <param name="attributeName">the attribute name of the value to clamp</param>
        /// <param name="value">the clamped value if clamping is enabled</param>
        /// <returns></returns>
        public static double Clamp(string attributeName, double value)
        {
            if (Instance.ClampingValues)
                return TypeRestrictions.Clamp(attributeName, value);
            return value;
        }
        /// <summary>
        /// Throws an exception stating the issues with <paramref name="value"/> if enabled
        /// </summary>
        /// <param name="attributeName">the name of the attribute to check</param>
        /// <param name="value">the value to check</param>
        public static void Validate(string attributeName, object value)
        {
            TypeRestrictions.Validate(attributeName, value);
        }
        /// <summary>
        /// Returns the string representation of what is wrong with a given value
        /// </summary>
        /// <param name="attributeName">the name of the attribute to check</param>
        /// <param name="value">the value to check</param>
        /// <returns>the string representation of what is wrong with <paramref name="value"/>"/></returns>
        public static string DataErrorString(string attributeName, object value)
        {
            return TypeRestrictions.DataErrorString(attributeName, value);
        }
    }
}