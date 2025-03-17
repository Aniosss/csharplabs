using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AircraftApp.Models;
using AircraftApp.Helpers;

namespace AircraftApp.ViewModels
{

    public class AircraftViewModel : ViewModelBase
    {
        public Airplane Airplane { get; private set; }
        public Helicopter Helicopter { get; } = new Helicopter();

        public ICommand AirplaneTakeOffCommand { get; }
        public ICommand AirplaneLandCommand { get; }
        public ICommand HelicopterTakeOffCommand { get; }
        public ICommand HelicopterLandCommand { get; }

        public ObservableCollection<string> LogMessages { get; }

        private string _runwayLengthInput = string.Empty;
        public string RunwayLengthInput
        {
            get => _runwayLengthInput;
            set
            {
                if (_runwayLengthInput != value)
                {
                    _runwayLengthInput = value;
                    OnPropertyChanged();
                }
            }
        }

        public AircraftViewModel()
        {

            LogMessages = new ObservableCollection<string>();

            RunwayLengthInput = "600";
            Airplane = new Airplane(600);
            SubscribeAircraftEvents(Airplane);
            SubscribeAircraftEvents(Helicopter);

            AirplaneTakeOffCommand = new RelayCommand(AirplaneTakeOff);
            AirplaneLandCommand = new RelayCommand(AirplaneLand);
            HelicopterTakeOffCommand = new RelayCommand(HelicopterTakeOff);
            HelicopterLandCommand = new RelayCommand(HelicopterLand);
        }

        private void SubscribeAircraftEvents(Aircraft aircraft)
        {
            aircraft.TakeOffCompleted += (s, msg) => LogMessages.Add(msg);
            aircraft.LandingCompleted += (s, msg) => LogMessages.Add(msg);
        }

        private void AirplaneTakeOff()
        {
            if (double.TryParse(RunwayLengthInput, out double runwayLength))
            {
                Airplane = new Airplane(runwayLength);
                SubscribeAircraftEvents(Airplane);
                bool result = Airplane.TakeOff();
                LogMessages.Add("Самолёт, высота: " + Airplane.Altitude);
            }
            else
            {
                LogMessages.Add("Некорректное значение длины взлётной полосы.");
            }
        }

        private void AirplaneLand()
        {
            Airplane.Land();
            LogMessages.Add("Самолёт, высота после посадки: " + Airplane.Altitude);
        }

        private void HelicopterTakeOff()
        {
            bool result = Helicopter.TakeOff();
            LogMessages.Add("Вертолёт, высота: " + Helicopter.Altitude);
        }

        private void HelicopterLand()
        {
            Helicopter.Land();
            LogMessages.Add("Вертолёт, высота после посадки: " + Helicopter.Altitude);
        }
    }
}
