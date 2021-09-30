using System;
using System.Collections.Generic;
using System.Text;

namespace VipaksAeroplane.Model
{
    /// <summary>
    /// Информация о типе самолета
    /// </summary>
    public class Plane
    {
        /// <summary>
        /// Название модели самолета
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Груподъемность самолета
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Средняя скорость самолета
        /// </summary>
        public int Speed { get; set; }
    }
}
