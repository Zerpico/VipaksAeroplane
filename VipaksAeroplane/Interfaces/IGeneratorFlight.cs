using System;
using System.Collections.Generic;
using System.Text;
using VipaksAeroplane.Model;

namespace VipaksAeroplane.Interfaces
{
    public interface IGeneratorFlight
    {
        IEnumerable<FlighInfo> GenerateFlight(int count = 100, double frequency = 1);
    }
}
