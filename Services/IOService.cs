using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Services
{
    /// <summary>
    /// The means the global io can be accessed
    /// </summary>
    public static class IOService
    {
        private static IRobotIOService _instance;
        private static bool _clampValues;
        /// <summary>
        /// The global IO Service instance
        /// </summary>
        public static IRobotIOService Instance
        {
            get
            {
                if (null == _instance)
                    throw new NullReferenceException("IO Service Null! Call IOService.Init(IRobotIOService) before accessing IOService.Instance");
                return _instance;
            }
        }
        /// <summary>
        /// Clamps the attribute if clamping is enabled.
        /// </summary>
        /// <param name="attributeName">the attribute name of the value to clamp</param>
        /// <param name="value">the clamped value if clamping is enabled</param>
        /// <returns></returns>
        public static double Clamp(string attributeName, double value)
        {
            if (_clampValues)
                return TypeRestrictions.Clamp(attributeName, value);
            return value;
        }
        /// <summary>
        /// Called by the consuming program before accessing ANY IO
        /// </summary>
        /// <param name="mainIOService">The service to access the IO of the robot</param>
        /// <param name="clampValues">whether frequently accessed values should be clamped instead of exceptions thrown</param>
        public static void Init(IRobotIOService mainIOService, bool clampValues)
        {
            if (_instance != null)
                throw new Exception("IO Service Is Already Initialized!");
            _instance = mainIOService;
            _clampValues = clampValues;
        }
    }
}
