using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Framework
{
    public delegate TResult AsyncPropertyGetMethod<out TResult>();
    public delegate void AsyncPropertySetMethod<in TResult>(TResult value);

    public class AsyncProperty<TResult> : BindableBase
    {
        public AsyncProperty(AsyncPropertyGetMethod<TResult> get, AsyncPropertySetMethod<TResult> set)
        {
            _getMethod = get;
            _setMethod = set;
        }

        public TResult Result
        {
            get
            {
                return (_getTask.Status == TaskStatus.RanToCompletion) ? _getTask.Result : tempValue;
            }

            set
            {
                tempValue = value;
            }
        }

        #region Private Fields
        private Task<TResult> _getTask;
        private Task _setTask;
        private AsyncPropertyGetMethod<TResult> _getMethod;
        private AsyncPropertySetMethod<TResult> _setMethod;
        private TResult tempValue = default(TResult);
        #endregion
    }
}
