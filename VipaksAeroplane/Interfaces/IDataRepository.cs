using System;
using System.Collections.Generic;
using System.Text;
using VipaksAeroplane.Model;

namespace VipaksAeroplane.Interfaces
{
    public interface IDataRepository
    {
        /// <summary>
        /// Получить список аэропортов
        /// </summary>
        IEnumerable<AirportInfo> GetAirports();

        IEnumerable<Plane> GetPlanes();
    }
}
