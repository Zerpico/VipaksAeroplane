using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace VipaksAeroplane.Model
{
    public class FlighInfo : INotifyPropertyChanged
    {
        /// <summary>
        /// Тип Самолёта
        /// </summary>
        public Plane TypePlane { get; set; }

        /// <summary>
        /// Название рейса
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Аэропорт отправления
        /// </summary>
        public AirportInfo Departure { get; set; }

        /// <summary>
        /// Аэропорт назначения
        /// </summary>
        public AirportInfo Destination { get; set; }

        /// <summary>
        /// Время вылета
        /// </summary>
        public DateTime DateStart { get; set; }

        /// <summary>
        /// Время прилета
        /// </summary>
        public DateTime DateDestination { get; set; }

        /// <summary>
        /// Количество пассажиров на рейсе
        /// </summary>
        public int Passenger { get; set; }

        /// <summary>
        /// Статус рейса в текущий момент
        /// </summary>        
        public StatusFlight Status { get => status; set => Set(ref status, value); }
        private StatusFlight status;

        #region NotifyProperty
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
              
        protected bool Set<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion

        public override string ToString()
        {
            return String.Format("{0:HH:mm} {1:0.0}", this.DateStart, this.Passenger
                );
        }
    }

  
    public enum StatusFlight : int
    {
        [Description("Ожидается")]
        Waiting = 0,

        [Description("Регистрация")]
        Registration = 1,

        [Description("Вылетел")]
        Departure = 2,

        [Description("В пути")]
        Transit = 3,

        [Description("Идёт на посадку")]
        Landing = 4,

        [Description("Прилетел")]
        Arrival = 5,

        [Description("Закрыт")]
        Close = 6
    }
}
