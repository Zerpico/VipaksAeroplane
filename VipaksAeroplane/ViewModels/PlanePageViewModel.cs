using Autofac.SmartNavigation.Base;
using Autofac.SmartNavigation.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using VipaksAeroplane.Interfaces;
using VipaksAeroplane.Model;
using System.Windows.Data;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System.Windows.Media;

namespace VipaksAeroplane.ViewModels
{
    public class PlanePageViewModel : BaseVM
    {
        private IGeneratorFlight _generatorFlight;
        private IDispatcherTime _dtimer;
        public PlanePageViewModel(INavigationService navigator, IGeneratorFlight generatorFlight, IDispatcherTime dtimer)
        {
            _generatorFlight = generatorFlight;
            _dtimer = dtimer;
            _dtimer.OnChangeTime += dtimer_OnChangeTime;
            Flighs = new ObservableCollection<FlighInfo>(_generatorFlight.GenerateFlight());

            DepartureVoyage = new CollectionViewSource() { Source = Flighs };
            DestinationVoyage = new CollectionViewSource() { Source = Flighs };
            DepartureVoyage.Filter += new FilterEventHandler(departFilter);
            DestinationVoyage.Filter += new FilterEventHandler(destFilter);

            LastFligh = Flighs[0];
            InitGraphSeries();

            _dtimer.Start(DateTime.Now);
        }

        private void InitGraphSeries()
        {
            DepartureSeries = new ChartValues<FlightGraph>();
            DestinationSeries = new ChartValues<FlightGraph>();
            var dayConfig = Mappers.Xy<FlightGraph>()
                  .X(dateModel => dateModel.Dt.Ticks / TimeSpan.FromHours(1).Ticks)
                  .Y(dateModel => dateModel.Value);

            Series = new SeriesCollection(dayConfig)
            {
                new ColumnSeries
                {
                    Values = DepartureSeries,
                    Title = "Вылет",
                    MaxColumnWidth = 15
                },
                new ColumnSeries
                {
                    Values = DestinationSeries,
                    Title = "Прилёт",
                    MaxColumnWidth = 15
                }
            };
            Formatter = value => new DateTime((long)(value * TimeSpan.FromHours(1).Ticks)).ToString("dd.MM  hh:mm");
        }


        #region Fields

        public ObservableCollection<FlighInfo> Flighs { get; set; }
        public FlighInfo LastFligh
        {
            get => lastFligh; set => SetProperty(ref lastFligh, value);
        }
        private FlighInfo lastFligh;
        public CollectionViewSource DepartureVoyage { get; set; }
        public CollectionViewSource DestinationVoyage { get; set; }

        public int DepartureInProgress
        {
            get => departureInProgress; set => SetProperty(ref departureInProgress, value);
        }
        private int departureInProgress;

        public int DestinationInProgress
        {
            get => destinationInProgress; set => SetProperty(ref destinationInProgress, value);
        }
        private int destinationInProgress;

        public Func<double, string> Formatter { get; set; }
        public ChartValues<FlightGraph> DepartureSeries { get; set; }
        public ChartValues<FlightGraph> DestinationSeries { get; set; }

        public SeriesCollection Series { get; set; }

        private int stepTimer;
        public int StepTimer
        {
            get { return stepTimer; }
            set { stepTimer = value; SetProperty(ref stepTimer, value); _dtimer.SetStep(value); }
        }
        public DateTime RealDateTime { get; set; }
        #endregion

        #region Methods
        private void dtimer_OnChangeTime(DateTime step)
        {
            RealDateTime = step;
            foreach (var fligh in Flighs)
            {
                if (SetStatus(fligh))
                    LastFligh = fligh;
            }
            DepartureVoyage.View.Refresh();
            DestinationVoyage.View.Refresh();
            DepartureInProgress = Flighs.Where(d => (int)d.Status >= 2).Count();
            DestinationInProgress = Flighs.Where(d => (int)d.Status >= 5).Count();
            OnPropertyChanged(nameof(RealDateTime));
        }

        //фильтр для вылета
        private void departFilter(object sender, FilterEventArgs e)
        {
            var voyage = e.Item as FlighInfo;
            if (voyage != null)
            {
                //статус 0(в ожидании)
                if (voyage.Status >= 0 && (int)voyage.Status < 3)
                    e.Accepted = true;
                else
                    e.Accepted = false;
            }
        }

        //фильтр для прилета
        private void destFilter(object sender, FilterEventArgs e)
        {
            var voyage = e.Item as FlighInfo;
            if (voyage != null)
            {
                //статус 3(в пути)
                if ((int)voyage.Status >= 3 && (int)voyage.Status < 6)
                    e.Accepted = true;
                else
                    e.Accepted = false;
            }
        }

        public bool SetStatus(FlighInfo flight)
        {
            //полпути
            var halfPath = (flight.DateDestination - flight.DateStart).TotalMinutes / 2;
            StatusFlight beforeStatus = flight.Status; //статус до изменения

            if (RealDateTime < flight.DateStart.AddMinutes(-90))
            {
                flight.Status = 0;
            }
            else if (RealDateTime >= flight.DateStart.AddMinutes(-90) && RealDateTime < flight.DateStart)
            {
                flight.Status = (StatusFlight)1;
            }
            else if (RealDateTime >= flight.DateStart && RealDateTime < flight.DateDestination.AddMinutes(-(halfPath + halfPath / 2)))
            {
                flight.Status = (StatusFlight)2;           
                DepartureSeries.Add(new FlightGraph() { Dt = flight.DateStart, Value = flight.Passenger });
                OnPropertyChanged(nameof(Series));
                //DepartureInProgress++;
            }
            else if (RealDateTime >= flight.DateStart.AddMinutes(halfPath / 2) && RealDateTime < flight.DateDestination.AddMinutes(-(halfPath / 2)))
            {
                flight.Status = (StatusFlight)3;
            }
            else if (RealDateTime >= flight.DateDestination.AddMinutes(-(halfPath / 2)) && RealDateTime < flight.DateDestination)
            {
                flight.Status = (StatusFlight)4;              
            }
            else if (RealDateTime >= flight.DateDestination && RealDateTime < flight.DateDestination.AddMinutes(60))
            {
                flight.Status = (StatusFlight)5;
                DestinationSeries.Add(new FlightGraph() { Dt = flight.DateDestination, Value = flight.Passenger });
                OnPropertyChanged(nameof(Series));
                //DestinationInProgress++;
            }
            else flight.Status = (StatusFlight)6;

            return beforeStatus != flight.Status;
        }

        #endregion
    }
}
