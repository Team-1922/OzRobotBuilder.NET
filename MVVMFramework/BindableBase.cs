using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using System.Linq;

namespace Team1922.MVVM.Framework
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        private event PropertyChangedEventHandler _propertyChangedNoDuplicates;
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                //prevent duplicates
                if (null == _propertyChangedNoDuplicates || !_propertyChangedNoDuplicates.GetInvocationList().Contains(value))
                    _propertyChangedNoDuplicates += value;
            }

            remove
            {
                _propertyChangedNoDuplicates -= value;
            }
        }

        /// <summary>
        /// This can be hugely leveraged on the robot in addition to the editor.  This just has to be very carefully serialized
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            _propertyChangedNoDuplicates?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool SetProperty<T>(ref T item, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(item, value)) return false;
            item = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public static JsonSerializerSettings Settings { get; } = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
    }
}
