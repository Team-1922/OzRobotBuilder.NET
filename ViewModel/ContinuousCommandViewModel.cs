using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    public class ContinuousCommandViewModel : IContinuousCommandProvider
    {
        public IEventTargetProvider EventTarget
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void SetContinuousCommand(ContinuousCommand continuousCommand)
        {
            throw new NotImplementedException();
        }
    }
}