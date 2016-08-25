using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;

namespace Team1922.WebFramework
{
    internal class WebHooksSubscriberCollection : IEnumerable<Uri>
    {
        public bool Add(Uri uri)
        {
            if (!Contains(uri))
            {
                _subscriberList.Add(uri);
                return true;
            }
            return false;
        }

        public bool Contains(Uri uri)
        {
            string subString = uri.ToString();
            foreach(var subscriber in _subscriberList)
            {
                if (subscriber.ToString() == subString)
                    return true;
            }
            return false;
        }

        public bool Remove(Uri uri)
        {
            string subString = uri.ToString();
            for(int i = 0; i < _subscriberList.Count; ++i)
            {
                if (_subscriberList[i].ToString() == subString)
                {
                    _subscriberList.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public async Task<string> GetJsonAsync()
        {
            return await Task.Run(() => { return JsonConvert.SerializeObject((from uri in _subscriberList select uri.ToString()).ToList(), Formatting.Indented, BindableBase.Settings); });
        }

        #region IEnumerable
        public IEnumerator<Uri> GetEnumerator()
        {
            return _subscriberList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Private Fields
        List<Uri> _subscriberList = new List<Uri>();
        #endregion
    }
}
