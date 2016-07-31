using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.Services
{
    /// <summary>
    /// The means the global io can be accessed
    /// </summary>
    public static class IOService
    {
        private static IRobotIOService ioService;
        /// <summary>
        /// The global IO Service instance
        /// </summary>
        public static IRobotIOService Instance
        {
            get
            {
                if (null == ioService)
                    throw new NullReferenceException("IO Service Null! Call IOService.Init(IRobotIOService) before accessing IOService.Instance");
                return ioService;
            }
        }
        /// <summary>
        /// Called by the consuming program before accessing ANY IO
        /// </summary>
        /// <param name="mainIOService">The service to access the IO of the robot</param>
        public static void Init(IRobotIOService mainIOService)
        {
            ioService = mainIOService;
        }
    }
}
