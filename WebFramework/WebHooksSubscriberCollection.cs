using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;

namespace Team1922.WebFramework
{
    internal class WebHooksSubscriberCollection : IEnumerable<Uri>
    {
        internal struct Enumerator : IEnumerator<Uri>, IEnumerator
        {
            private WebHooksSubscriberCollection _collection;
            private List<Uri>.Enumerator _enumerator;
            public Enumerator(WebHooksSubscriberCollection collection)
            {
                _collection = collection.GetDeepCopy();
                _enumerator = _collection._subscriberList.GetEnumerator();
            }

            public object Current
            {
                get
                {
                    return Current;
                }
            }

            Uri IEnumerator<Uri>.Current
            {
                get
                {
                    return _enumerator.Current;
                }
            }

            public void Dispose()
            {
                _enumerator.Dispose();
            }

            public bool MoveNext()
            {
                return _enumerator.MoveNext();
            }

            public void Reset()
            {
                //TODO: what do here?
            }
        }

        public bool Add(Uri uri)
        {
            try
            {
                _listLock.ExitWriteLock();

                if (!Contains(uri))
                {
                    _subscriberList.Add(uri);
                    return true;
                }
                return false;
            }
            finally
            {
                _listLock.ExitWriteLock();
            }
        }

        public bool Contains(Uri uri)
        {
            try
            {
                _listLock.EnterReadLock();

                string subString = uri.ToString();
                foreach (var subscriber in _subscriberList)
                {
                    if (subscriber.ToString() == subString)
                        return true;
                }
                return false;
            }
            finally
            {
                _listLock.ExitReadLock();
            }
        }

        public bool Remove(Uri uri)
        {
            try
            {
                _listLock.EnterWriteLock();

                string subString = uri.ToString();
                for (int i = 0; i < _subscriberList.Count; ++i)
                {
                    if (_subscriberList[i].ToString() == subString)
                    {
                        _subscriberList.RemoveAt(i);
                        return true;
                    }
                }
                return false;
            }
            finally
            {
                _listLock.ExitWriteLock();
            }
        }

        public async Task<string> GetJsonAsync()
        {
            try
            {
                _listLock.EnterReadLock();

                return await Task.Run(() => { return JsonConvert.SerializeObject((from uri in _subscriberList select uri.ToString()).ToList(), Formatting.Indented, BindableBase.Settings); });
            }
            finally
            {
                _listLock.ExitReadLock();
            }
        }

        private WebHooksSubscriberCollection GetDeepCopy()
        {
            WebHooksSubscriberCollection ret = new WebHooksSubscriberCollection();
            try
            {
                _listLock.EnterReadLock();
                Uri[] copy = new Uri[_subscriberList.Count];
                _subscriberList.CopyTo(copy);
                ret._subscriberList = copy.ToList();
            }
            finally
            {
                _listLock.ExitReadLock();
            }
            return ret;
        }

        #region IEnumerable
        public IEnumerator<Uri> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Private Fields
        ReaderWriterLockSlim _listLock = new ReaderWriterLockSlim();
        List<Uri> _subscriberList = new List<Uri>();
        #endregion
    }
}
