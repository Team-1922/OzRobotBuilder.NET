using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Team1922.WebFramework
{
    public class CompoundRequestDelegator : RequestDelegatorWebHooks
    {
        public CompoundRequestDelegator(string pathRoot) : base(pathRoot)
        {
        }
        
        public void AddDelegator(IRequestDelegator delegator)
        {
            _delegatorsLock.EnterWriteLock();
            _delegators.Add(delegator.PathRoot, delegator);
            _delegatorsLock.ExitWriteLock();
        }

        public void RemoveDelegator(string pathRoot)
        {
            _delegatorsLock.EnterWriteLock();
            _delegators.Remove(pathRoot);
            _delegatorsLock.ExitWriteLock();
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
            _delegatorsLock.EnterReadLock();
            foreach (var delegator in _delegators)
            {
                if (IsOnBeginning(path, delegator.Key))
                {
                    chosenDelegator = delegator.Value;
                }
            }
            _delegatorsLock.ExitReadLock();


            //if none is found, then 404
            if (null == chosenDelegator)
            {
                return new BasicHttpResponse() { Body = "Could Not Find Compatable Request Delegator", StatusCode = 404 };
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
