﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    public interface IPWMOutputProvider
    {
        int ID { get; set; }
        string Name { get; set; }
        double Value { get; set; }

        void SetPWMOutput(PWMOutput pwmOutput);
    }
}
