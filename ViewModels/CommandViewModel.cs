using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class CommandViewModel : ViewModelBase<Command>, ICommandProvider
    {
        public CommandViewModel(IProvider parent) : base(parent)
        {
        }

        #region ICommandProvider
        public string IsFinished
        {
            get
            {
                return ModelReference.IsFinished;
            }

            set
            {
                var temp = ModelReference.IsFinished;
                SetProperty(ref temp, value);
                ModelReference.IsFinished = temp;
            }
        }

        public string OnEnd
        {
            get
            {
                return ModelReference.OnEnd;
            }

            set
            {
                var temp = ModelReference.OnEnd;
                SetProperty(ref temp, value);
                ModelReference.OnEnd = temp;
            }
        }

        public string OnStart
        {
            get
            {
                return ModelReference.OnStart;
            }

            set
            {
                var temp = ModelReference.OnStart;
                SetProperty(ref temp, value);
                ModelReference.OnStart = temp;
            }
        }

        public string OnUpdate
        {
            get
            {
                return ModelReference.OnUpdate;
            }

            set
            {
                var temp = ModelReference.OnUpdate;
                SetProperty(ref temp, value);
                ModelReference.OnUpdate = temp;
            }
        }

        public string ParamNames
        {
            get
            {
                return ModelReference.ParamNames;
            }

            set
            {
                var temp = ModelReference.ParamNames;
                SetProperty(ref temp, value);
                ModelReference.ParamNames = temp;
            }
        }
        #endregion

        #region ViewModelBase
        public override string Name
        {
            get
            {
                return ModelReference.Name;
            }

            set
            {
                var temp = ModelReference.Name;
                SetProperty(ref temp, value);
                ModelReference.Name = temp;
            }
        }

        protected override string GetValue(string key)
        {
            throw new NotImplementedException();
        }

        protected override void SetValue(string key, string value)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
