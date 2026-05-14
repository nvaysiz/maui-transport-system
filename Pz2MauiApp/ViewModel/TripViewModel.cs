using Pz2MauiApp.Model;
using Pz2MauiApp.Repository;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Pz2MauiApp.ViewModel
{
    public class TripViewModel : INotifyPropertyChanged
    {
        private Vehicle? selectedVehicle;
        private DateTime tripDate;
        private double distance;
        private string cargoName = "";
        private string route = "";
        private double fuelAmount;
        private double totalCost;
        private Trip? selectedTrip;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public ObservableCollection<Vehicle> Vehicles { get; set; }
        public ObservableCollection<Trip> Trips { get; set; }

        public Vehicle? SelectedVehicle
        {
            get => selectedVehicle;
            set
            {
                selectedVehicle = value;
                OnPropertyChanged();
                RecalculateTrip();
            }
        }

        public DateTime TripDate
        {
            get => tripDate;
            set { tripDate = value; OnPropertyChanged(); }
        }

        public double Distance
        {
            get => distance;
            set
            {
                distance = value;
                OnPropertyChanged();
                RecalculateTrip();
            }
        }

        public string CargoName
        {
            get => cargoName;
            set { cargoName = value; OnPropertyChanged(); }
        }

        public string Route
        {
            get => route;
            set { route = value; OnPropertyChanged(); }
        }

        public double FuelAmount
        {
            get => fuelAmount;
            set { fuelAmount = value; OnPropertyChanged(); }
        }

        public double TotalCost
        {
            get => totalCost;
            set { totalCost = value; OnPropertyChanged(); }
        }

        public Trip? SelectedTrip
        {
            get => selectedTrip;
            set
            {
                selectedTrip = value;
                OnPropertyChanged();

                if (value != null)
                {
                    SelectedVehicle = value.Vehicle;
                    TripDate = value.TripDate;
                    Distance = value.Distance;
                    CargoName = value.CargoName;
                    Route = value.Route;
                    FuelAmount = value.FuelAmount;
                    TotalCost = value.TotalCost;
                }
            }
        }

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand UpdateCommand { get; }

        public TripViewModel()
        {
            Vehicles = ServiceDb.GetVehicles();
            Trips = ServiceDb.GetTrips();
            TripDate = DateTime.Now;

            AddCommand = new Command(AddTrip);
            RemoveCommand = new Command(RemoveTrip);
            UpdateCommand = new Command(UpdateTrip);
        }

        private void AddTrip()
        {
            Trip trip = new Trip
            {
                Vehicle = SelectedVehicle,
                TripDate = TripDate,
                Distance = Distance,
                CargoName = CargoName,
                Route = Route
            };

            bool result = ServiceDb.AddTrip(trip);

            if (result)
                ClearFields();
        }

        private void RemoveTrip()
        {
            if (SelectedTrip == null) return;

            Trips.Remove(SelectedTrip);
            ClearFields();
        }

        private void UpdateTrip()
        {
            if (SelectedTrip == null || SelectedVehicle == null) return;

            SelectedTrip.Vehicle = SelectedVehicle;
            SelectedTrip.TripDate = TripDate;
            SelectedTrip.Distance = Distance;
            SelectedTrip.CargoName = CargoName;
            SelectedTrip.Route = Route;
            SelectedTrip.FuelAmount = Distance * SelectedVehicle.FuelConsumption / 100.0;
            SelectedTrip.TotalCost = ServiceDb.CalculateTripCost(SelectedVehicle, Distance);

            int index = Trips.IndexOf(SelectedTrip);
            if (index >= 0)
                Trips[index] = SelectedTrip;

            ClearFields();
        }

        private void RecalculateTrip()
        {
            if (SelectedVehicle != null && Distance > 0)
            {
                FuelAmount = Distance * SelectedVehicle.FuelConsumption / 100.0;
                TotalCost = ServiceDb.CalculateTripCost(SelectedVehicle, Distance);
            }
            else
            {
                FuelAmount = 0;
                TotalCost = 0;
            }
        }

        private void ClearFields()
        {
            SelectedVehicle = null;
            TripDate = DateTime.Now;
            Distance = 0;
            CargoName = string.Empty;
            Route = string.Empty;
            FuelAmount = 0;
            TotalCost = 0;
            SelectedTrip = null;
        }
    }
}