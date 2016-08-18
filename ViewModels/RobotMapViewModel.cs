using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Contracts.Events;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class RobotMapViewModel : ViewModelBase, IRobotMapProvider
    {
        private Robot _robotModel;

        public RobotMapViewModel(IHierarchialAccess topParent) : base(topParent)
        {
            EventAggregator<AddRobotMapEntryEvent>.Instance.Event += OnAddNewEntry;
        }

        private void OnAddNewEntry(object arg1, AddRobotMapEntryEvent arg2)
        {
            //update the key value list every time a new value is added to keep the datagrid up to date
            UpdateKeyValueList();
        }

        #region ViewModelBase
        public override string ModelTypeName
        {
            get
            {
                var brokenName = _robotModel.GetType().ToString().Split('.');
                return brokenName[brokenName.Length - 1];
            }
        }

        protected override string GetValue(string key)
        {
            foreach(var item in _robotModel.RobotMap)
            {
                if (item.Key == key)
                    return item.Value;
            }
            throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
        }

        protected override void SetValue(string key, string value)
        {
            for(int i = 0; i < _robotModel.RobotMap.Count; ++i)
            {
                if (_robotModel.RobotMap[i].Key == key)
                {
                    _robotModel.RobotMap[i].Value = value;
                    return;
                }
            }
            throw new ArgumentException($"\"{key}\" is Read-Only or Does Not Exist");
        }

        protected override List<string> GetOverrideKeys()
        {
            //be careful here; this may be called before construction completes/model instance set
            if (null == _robotModel)
                return new List<string>();

            List<string> ret = new List<string>();
            foreach(var item in _robotModel.RobotMap)
            {
                ret.Add(item.Key);
            }
            return ret;
        }
        #endregion

        #region IRobotMapProvider
        public string Name
        {
            get
            {
                return "RobotMap";
            }
        }

        public void SetRobot(Robot robot)
        {
            _robotModel = robot;
            UpdateKeyValueList();
        }
        #endregion

        #region Private Members

        #endregion
    }
}
