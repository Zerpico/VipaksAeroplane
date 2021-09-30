using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using VipaksAeroplane.Interfaces;
using VipaksAeroplane.Model;

namespace VipaksAeroplane.Services
{
    public class GeneratorFlight : IGeneratorFlight
    {
        private List<AirportInfo> airports;
        private List<Plane> planes;        

        public GeneratorFlight(IDataRepository repository)
        {
            airports = repository.GetAirports().ToList();
            planes = repository.GetPlanes().ToList();
        }

        /// <summary>
        /// Получить сгенерировый список рейсов
        /// </summary>
        public IEnumerable<FlighInfo> GenerateFlight(int count = 100, double frequency = 1)
        {
            FlighInfo[] flights = new FlighInfo[count];
            var rnd = new Random();
            var startDate = DateTime.Now.AddHours(-3);
            startDate = GetRandomDate(startDate, rnd, true);

            for (int i = 0; i < count; i++)
            {
                var airport = GetRandomAirport(rnd);
                var plane = GetRandomPlane(rnd);
                var newFlight = new FlighInfo()
                {
                    Departure = airport,
                    Destination = GetRandomAirport(rnd, airport),
                    TypePlane = plane,
                    DateStart = startDate = GetRandomDate(startDate, rnd),
                    DateDestination = GetRandomDate(startDate, rnd, true),
                    Passenger = rnd.Next(plane.Capacity/2, plane.Capacity)
                };
                //генерируем имя на основе внутренего кода и времени в пути
                newFlight.Name = airport.CodeInternal + "-" + (int)(newFlight.DateDestination - newFlight.DateStart).TotalMinutes;
                flights[i] = newFlight;                
            }
            return flights.OrderBy(d => d.DateStart);
        }

        /// <summary>
        /// Получить случайный тип самолёта
        /// </summary>
        private Plane GetRandomPlane(Random rnd)
        {
            return planes[rnd.Next(0, planes.Count - 1)];
        }

        /// <summary>
        /// Получить случайный аэропорт, или случайный исключая выбранный
        /// </summary>
        private AirportInfo GetRandomAirport(Random rnd, AirportInfo exclude = null)
        {
            return exclude == null ?
                    airports[rnd.Next(0, airports.Count - 1)] :
                    airports.Where(a => a != exclude).ToList()[rnd.Next(0, airports.Count - 2)];
        }

        /// <summary>
        /// Получить случайную дату
        /// </summary>
        public static DateTime GetRandomDate(DateTime start, Random rnd, bool after = false)
        {
            DateTime result = start.AddMinutes(rnd.Next(after ? 30 : -60, after ? 360 : 180));

            return result;
        }
    }
}
