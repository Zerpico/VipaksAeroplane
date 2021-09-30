using System;
using System.Collections.Generic;
using System.Text;

namespace VipaksAeroplane.Model
{
    /// <summary>
    /// Информация о аэропорте
    /// </summary>
    public class AirportInfo
    {
        /// <summary>
        /// Код ИАТА
        /// </summary>
        public string CodeIATA { get; set; }

        /// <summary>
        /// Код ИКАО
        /// </summary>
        public string CodeICAO { get; set; }

        /// <summary>
        /// Внутрений код
        /// </summary>
        public string CodeInternal { get; set; }

        /// <summary>
        /// Класс аэропорта
        /// </summary>
        public string AirportClass { get; set; }

        /// <summary>
        /// Название аэропорта
        /// </summary>
        public string AirportName { get; set; }

        /// <summary>
        /// Название города, где находится аэропорт
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Название региона, где находится город
        /// </summary>
        public string RegionName { get; set; }
    }
}
