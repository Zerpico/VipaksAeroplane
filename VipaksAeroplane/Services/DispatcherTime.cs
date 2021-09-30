using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;
using VipaksAeroplane.Interfaces;

namespace VipaksAeroplane.Services
{
    public class DispatcherTime : IDispatcherTime
    {        
        private readonly DispatcherTimer _countTimer;
        private double sumInterval;
        
        private DateTime upTime;       
        private double step;

        // Событие, возникающее при обновлении времени
        public event DateTimeHandler OnChangeTime;

        public DispatcherTime()
        {
            //инициализируем всё
            step = 1;
            _countTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100),
                IsEnabled = false
            };
            _countTimer.Tick += CountTimerTick;
        }

        /// <summary>
        /// Сменить шаг(ход) времени
        /// </summary>
        public void SetStep(int newStep)
        {
            step = newStep;
        }

        /// <summary>
        /// Запустить сервис
        /// </summary>
        public void Start(DateTime realTime)
        {
            upTime = realTime;
            _countTimer.Start();
        }

        /// <summary>
        /// Остановить сервис
        /// </summary>
        public void Stop()
        {
            _countTimer.Stop();
        }

        /// <summary>
        /// Обновление данных
        /// </summary>
        private void CountTimerTick(object sender, EventArgs e)
        {
            sumInterval += (sender as DispatcherTimer).Interval.TotalMilliseconds;
            if (sumInterval >= 300)
            {
                //обновляем время и событие
                upTime = upTime.AddMilliseconds(sumInterval * step);
                OnChangeTime(upTime);

                //обнуляем счётчик
                sumInterval = 0;
            }
        }
    }
}
