using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Team1922.MVVM.Framework
{
    public abstract class ViewModelBase : BindableBase
    {
        public IReadOnlyDictionary<string, string> Properties
        {
            get
            {
                return (from x in GetType().GetProperties()
                        where x.Name != "Properties"
                        select x)
                        .ToDictionary(x => x.Name, x => (x.GetGetMethod().Invoke(this, null)?.ToString() ?? ""));
            }
        }
    }
}
