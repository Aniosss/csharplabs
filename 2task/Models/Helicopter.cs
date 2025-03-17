using System;

namespace AircraftApp.Models
{
    public class Helicopter : Aircraft
    {
        public Helicopter()
        {
        }

        public override bool TakeOff()
        {
            Altitude = 5000;
            OnTakeOffCompleted("Вертолёт успешно взлетел.");
            return true;
        }

        public override void Land()
        {
            Altitude = 0;
            OnLandingCompleted("Вертолёт успешно совершил посадку.");
        }
    }
}
