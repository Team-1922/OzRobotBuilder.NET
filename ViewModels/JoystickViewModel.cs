using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{

    internal class JoystickViewModel : ViewModelBase, IJoystickProvider
    {
        Joystick _joystickModel;

        public JoystickViewModel()
        {
            _axes.CollectionChanged += _axes_CollectionChanged;
            _buttons.CollectionChanged += _buttons_CollectionChanged;
        }

        private void _buttons_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e != null && e.Action == NotifyCollectionChangedAction.Replace)
            {
                OnPropertyChanged($"Buttons[{e.NewStartingIndex}]");
            }
        }

        private void _axes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e != null && e.Action == NotifyCollectionChangedAction.Replace)
            {
                OnPropertyChanged($"Axes[{e.NewStartingIndex}]");
            }
        }

        public IReadOnlyDictionary<uint, double> Axes
        {
            get
            {
                return _axes;
            }
        }

        public IReadOnlyDictionary<uint, bool> Buttons
        {
            get
            {
                return _buttons;
            }
        }

        public int ID
        {
            get
            {
                return _joystickModel.ID;
            }

            set
            {
                _joystickModel.ID = value;
            }
        }

        public string Name
        {
            get
            {
                return _joystickModel.Name;
            }

            set
            {
                _joystickModel.Name = value;
            }
        }

        public void SetJoystick(Joystick joystick)
        {
            _joystickModel = joystick;
        }

        public void UpdateInputValues()
        {
            var thisJoystickIOService = IOService.Instance.Joysticks[ID];
            for(uint i = 1; i <= 12; ++i)
            {
                _buttons[i] = thisJoystickIOService.Buttons[i];
            }
            for(uint i = 1; i <= 5; ++i)
            {
                _axes[i] = thisJoystickIOService.Axes[i];
            }
        }

        #region Private Fields
        ReadonlyObservableDictionary<uint, double> _axes = new ReadonlyObservableDictionary<uint, double>();
        ReadonlyObservableDictionary<uint, bool> _buttons = new ReadonlyObservableDictionary<uint, bool>();
        #endregion
    }
}