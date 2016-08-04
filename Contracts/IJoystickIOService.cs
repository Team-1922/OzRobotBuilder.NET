using System.Collections.Generic;

namespace Team1922.MVVM.Contracts
{
    public interface IJoystickIOService
    {
        IReadOnlyDictionary<uint, double> Axes { get; }
        IReadOnlyDictionary<uint, bool> Buttons { get; }
        int ID { get; }
    }
}