﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    public interface IDigitalInputProvider : IInputProvider
    {
        int ID { get; set; }
        string Name { get; set; }
        bool Value { get; set; }

        void SetDigitalInput(DigitalInput digitalInput);
    }
}