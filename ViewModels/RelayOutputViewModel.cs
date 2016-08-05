using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    internal class RelayOutputViewModel : IRelayOutputProvider
    {
        RelayOutput _relayOutputModel;

        public RelayDirection Direction
        {
            get
            {
                return _relayOutputModel.Direction;
            }

            set
            {
                _relayOutputModel.Direction = value;
            }
        }

        public int ID
        {
            get
            {
                return _relayOutputModel.ID;
            }

            set
            {
                _relayOutputModel.ID = value;
            }
        }

        public string Name
        {
            get
            {
                return _relayOutputModel.Name;
            }

            set
            {
                _relayOutputModel.Name = value;
            }
        }

        public RelayValue Value
        {
            get
            {
                return _relayOutputModel.Value;
            }

            set
            {
                var ioService = IOService.Instance.RelayOutputs[ID];
                switch (value)
                {
                    case RelayValue.Forward:
                        if (Direction == RelayDirection.ForwardOnly || Direction == RelayDirection.Both)
                        {
                            ioService.ValueAsBool = true;
                            ioService.ReverseValueAsBool = false;
                        }
                        break;
                    case RelayValue.Reverse:
                        if (Direction == RelayDirection.ReverseOnly || Direction == RelayDirection.Both)
                        {
                            ioService.ValueAsBool = false;
                            ioService.ReverseValueAsBool = true;
                        }
                        break;
                    case RelayValue.Off:
                        ioService.ValueAsBool = false;
                        ioService.ReverseValueAsBool = false;
                        break;
                    case RelayValue.On:
                        if (Direction == RelayDirection.ForwardOnly)
                        {
                            Value = RelayValue.Forward;
                        }
                        else if (Direction == RelayDirection.ReverseOnly)
                        {
                            Value = RelayValue.Reverse;
                        }
                        else
                        {
                            ioService.ValueAsBool = true;
                            ioService.ReverseValueAsBool = true;
                        }
                        break;
                }
                //this is added at the end, becuase the RelayValue.On case calls this setter with different parameters;
                _relayOutputModel.Value = value;
            }
        }

        public void SetRelayOutput(RelayOutput relayOutput)
        {
            _relayOutputModel = relayOutput;
        }
    }
}