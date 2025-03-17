using System;

namespace AircraftApp.Models
{
    public class Airplane : Aircraft
    {
        public double RunwayLength { get; }

        public Airplane(double runwayLength)
        {
            RunwayLength = runwayLength;
        }

        public override bool TakeOff()
        {
            if (RunwayLength >= 500)
            {
                Altitude = 10000;
                OnTakeOffCompleted("Самолёт успешно взлетел.");
                return true;
            }
            else
            {
                OnTakeOffCompleted("Самолёт не смог взлететь: недостаточная длина взлётной полосы.");
                return false;
            }
        }

        public override void Land()
        {
            Altitude = 0;
            OnLandingCompleted("Самолёт успешно совершил посадку.");
        }
    }
}
