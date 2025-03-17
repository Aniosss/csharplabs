using System;

namespace AircraftApp.Models
{

    public abstract class Aircraft
    {
        public double Altitude { get; protected set; } = 0;

        public event EventHandler<string>? TakeOffCompleted;
        public event EventHandler<string>? LandingCompleted;

        public abstract bool TakeOff();

        public abstract void Land();

        protected void OnTakeOffCompleted(string message)
        {
            TakeOffCompleted?.Invoke(this, message);
        }

        protected void OnLandingCompleted(string message)
        {
            LandingCompleted?.Invoke(this, message);
        }
    }
}
