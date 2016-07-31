using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;

namespace Team1922.MVVM.Contracts.Events
{
    public class ItemSelectEvent : EventArgs
    {
        public INotifyPropertyChanged SelectedElement { get; set; }
    }
}
