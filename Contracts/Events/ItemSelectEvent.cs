using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts.Events
{
    public class ItemSelectEvent : EventArgs
    {
        public IProvider SelectedElement { get; set; }
    }
}
