using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.WebFramework
{
    public static class RobotRepository
    {
        private static IHierarchialAccessRoot _instance;
        public static IHierarchialAccessRoot Instance
        {
            get
            {
                if (null == _instance)
                    throw new NullReferenceException("Initialize the Data Access Instance before calling the web api");
                return _instance;
            }

            set
            {
                _instance = value;
            }
        }
    }
}
