/////////////////////////////////////////////////////////
/// Парсер списка аэропортов с городами
/// Информация будем брать отсюда https://ru.wikipedia.org/wiki/Список_аэропортов_России
/// Необходимо извлечь список аэропортов с названием населенного пункта, класса и код
/// Сохраняет список в XML/JSON файл для дальнейшего использования в генераторе рейсов в VipaksAeroplane.Services
////////////////////////////////////////////////////////

using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Xml.Serialization;

namespace AirportParser
{
    class Program
    {
        const string url = @"https://ru.wikipedia.org/wiki/Список_аэропортов_России";
        static void Main(string[] args)
        {
            Console.WriteLine("Parser airport starter!");

            var webClient = new WebClient();
            var domParser = new HtmlParser();

            var htmlDoc = webClient.DownloadString(url);
            var document = domParser.ParseDocument(htmlDoc);

            //отбираем элементы из таблицы
            var tableElement = document.QuerySelector("table.standard");
            var rows = tableElement.QuerySelectorAll("tr").Skip(1);                        
           
            var airplaneList = rows.Select(row => row.QuerySelectorAll("td, th").Select(t=>t.TextContent.Trim()).ToArray());

            // мапим в наш класс
            List<AirportInfo> airportInfos = new List<AirportInfo>();
            foreach (var airplane in airplaneList)
            {
                if (airplane.Length < 7)
                    continue;

                airportInfos.Add(
                    new AirportInfo()
                    {
                        CodeIATA = airplane[0],
                        CodeICAO = airplane[1],
                        CodeInternal = airplane[2],
                        AirportClass = airplane[4],
                        AirportName = airplane[5],
                        CityName = airplane[6].Substring(0,airplane[6].IndexOf(',')), 
                        RegionName = airplane[7]
                    });
            }

            var fileName = SaveAirportsToXML(airportInfos.ToArray());
            Console.WriteLine("Done!\tSave to "+ fileName);

            fileName = SaveAirportsToJSON(airportInfos.ToArray());
            Console.WriteLine("Done!\tSave to " + fileName);
        }

        /// <summary>
        /// Сохранить список аэропортов в XML файл
        /// </summary>
        /// <param name="airports"></param>
        static string SaveAirportsToXML(AirportInfo[] airports)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(AirportInfo[]));

            using (FileStream fs = new FileStream("airports.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, airports);
            }

            return "airports.xml";
        }

        /// <summary>
        /// Сохранить список аэропортов в JSON файл
        /// </summary>
        /// <param name="airports"></param>
        static string SaveAirportsToJSON(AirportInfo[] airports)
        {
            using (FileStream fs = new FileStream("airports.json", FileMode.OpenOrCreate))
            {
                var buffer = JsonSerializer.SerializeToUtf8Bytes<AirportInfo[]>(airports);
                fs.Write(buffer, 0, buffer.Length);
            }

            return "airports.json";
        }
    }

    [Serializable]
    public class AirportInfo
    {
        public AirportInfo() { }
        public string CodeIATA { get; set; }
        public string CodeICAO{ get; set; }
        public string CodeInternal { get; set; }
        public string AirportClass { get; set; }
        public string AirportName { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
    }
}
