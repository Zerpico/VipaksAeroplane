using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using VipaksAeroplane.Interfaces;
using VipaksAeroplane.Model;

namespace VipaksAeroplane.Services
{
    public class FileRepository : IDataRepository
    {
        AirportInfo[] airportInfos;
        public FileRepository()
        {

        }

        public IEnumerable<AirportInfo> GetAirports() => LoadDataFromJSON();
        
        

        /// <summary>
        /// Получить список из xml файл
        /// </summary>
        private IEnumerable<AirportInfo> LoadDataFromXML()
        {
            using (FileStream fs = new FileStream("airports.xml", FileMode.Open))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(AirportInfo[]));
                this.airportInfos = (AirportInfo[])formatter.Deserialize(fs);
            }
            return airportInfos;
        }

        /// <summary>
        /// Получить список из json файл
        /// </summary>
        private IEnumerable<AirportInfo> LoadDataFromJSON()
        {            
            using (FileStream fs = new FileStream("airports.json", FileMode.Open))
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);

                ReadOnlySpan<byte> stream = new ReadOnlySpan<byte>(buffer);

                var utf8Reader = new Utf8JsonReader(stream);
                this.airportInfos = JsonSerializer.Deserialize<AirportInfo[]>(ref utf8Reader);

            }
            return airportInfos;
        }

        public IEnumerable<Plane> GetPlanes()
        {
            //хардкодим да, хотя можно было тоже получать из файла
            return new Model.Plane[]
            {
                 new Plane() { Name="Ту-204", Capacity = 300 },
                 new Plane() { Name="ТУ-204-300", Capacity = 300 },
                 new Plane() { Name="Superjet 100", Capacity = 87 },
                 new Plane() { Name="МС-21", Capacity = 180 },
                 new Plane() { Name="Ан-148",Capacity = 83 },
                 new Plane() { Name="Ил-96",Capacity = 300 }
            };
        }
    }
}
