using System;
using System.Collections.Generic;
using System.Text;

namespace VipaksAeroplane.Interfaces
{
    public delegate void DateTimeHandler(DateTime step);
    public interface IDispatcherTime
    {        
        public event DateTimeHandler OnChangeTime;

        void SetStep(int newStep);
        void Start(DateTime realTime);
        void Stop();

    }
}
