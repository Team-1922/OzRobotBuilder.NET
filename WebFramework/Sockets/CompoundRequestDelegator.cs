using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.WebFramework.Sockets
{
    public class CompoundRequestDelegator : IRequestDelegator
    {
        public void AddRequestDelegator(string pathRoot, IRequestDelegator requestDelegator)
        {
            _delegatorLock.EnterWriteLock();
            try
            {
                _requestDelegators.Add(pathRoot, requestDelegator);
            }
            finally
            {
                _delegatorLock.ExitWriteLock();
            }
        }

        #region IRequestDelegator
        public async Task<Response> ProcessRequestAsync(Request request)
        {
            return await GetDelegatorFromRequest(ref request)?.ProcessRequestAsync(request);
        }
        public IHierarchialAccessRoot Data
        {
            get
            {
                //TODO: something else?
                return null;
            }
        }
        #endregion

        #region Private Methods
        private IRequestDelegator GetDelegatorFromRequest(ref Request request)
        {
            var rootPath = request.Path.Split(':');
            if (rootPath.Length != 2)
                return null;

            IRequestDelegator ret = null;
            _delegatorLock.EnterReadLock();

            try
            {
                foreach(var delegator in _requestDelegators)
                {
                    if (rootPath[0] == delegator.Key)
                        ret = delegator.Value;
                }
            }
            finally
            {
                _delegatorLock.ExitReadLock();
            }
            request.Path = rootPath[1];
            return ret;
        }
        #endregion

        #region Private Fields
        ReaderWriterLockSlim _delegatorLock = new ReaderWriterLockSlim();
        Dictionary<string, IRequestDelegator> _requestDelegators = new Dictionary<string, IRequestDelegator>();
        #endregion
    }
}
