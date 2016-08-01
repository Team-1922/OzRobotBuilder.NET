using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// Nothing really special to add here
    /// <see cref="IOutputService.Value"/>  is a normalized value between -1 and 1 to abstract pulse widths away from the user
    /// </summary>
    public interface IPWMOutputIOService : IOutputService
    {
    }
}
