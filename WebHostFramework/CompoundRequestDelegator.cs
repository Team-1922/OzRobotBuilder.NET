using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace Team1922.WebFramework
{
    public class CompoundRequestDelegator : RequestDelegatorWebHooks
    {
        public CompoundRequestDelegator(string pathRoot) : base(pathRoot)
        {
        }
        
        public void AddDelegator(IRequestDelegator delegator)
        {
            try
            {
                _delegatorsLock.EnterWriteLock();
                _delegators.Add(delegator.PathRoot, delegator);
            }
            finally
            {
                _delegatorsLock.ExitWriteLock();
            }
        }

        public void RemoveDelegator(string pathRoot)
        {
            try
            {
                _delegatorsLock.EnterWriteLock();
                _delegators.Remove(pathRoot);
            }
            finally
            {
                _delegatorsLock.ExitWriteLock();
            }
        }
        
        #region Private Methods
        private bool IsOnBeginning(string test, string pattern)
        {
            if (test.Length < pattern.Length)
                return false;
            var testSubStr = test.Substring(0, pattern.Length);
            if (testSubStr == pattern)
                return true;
            return false;
        }
        #endregion

        #region RequestDelegatorWebHooks
        protected override async Task<BasicHttpResponse> AggregateRequestAsync(string method, string path, string body)
        {
            //try to find a delegator which matches this path
            IRequestDelegator chosenDelegator = null;
            try
            {
                _delegatorsLock.EnterReadLock();
                foreach (var delegator in _delegators)
                {
                    if (IsOnBeginning(path, delegator.Key))
                    {
                        chosenDelegator = delegator.Value;
                    }
                }
            }
            finally
            {
                _delegatorsLock.ExitReadLock();
            }


            //if none is found, then HttpStatusCode.NotFound
            if (null == chosenDelegator)
            {
                return new BasicHttpResponse() { Body = "Could Not Find Compatable Request Delegator", StatusCode = HttpStatusCode.NotFound };
            }
            else
            {
                return await chosenDelegator.ProcessRequestAsync(method, path, body);
            }
        }
        #endregion

        #region Private Fields
        private ReaderWriterLockSlim _delegatorsLock = new ReaderWriterLockSlim();
        private Dictionary<string, IRequestDelegator> _delegators = new Dictionary<string, IRequestDelegator>();
        #endregion
    }
}
