using Pz2MauiApp.Model;
using Pz2MauiApp.Repository;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Pz2MauiApp.ViewModel
{
    internal class VehicleViewModel : INotifyPropertyChanged
    {
        private string number = "";
        private string brand = "";
        private string model = "";
        private string driverName = "";
        private double fuelConsumption;
        private double amortizationPerKm;
        private Fuel? selectedFuel;
        private Vehicle? selectedVehicle;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public ObservableCollection<Vehicle> Vehicles { get; set; }
        public ObservableCollection<Fuel> Fuels { get; set; }

        public string Number
        {
            get => number;
            set { number = value; OnPropertyChanged(); }
        }

        public string Brand
        {
            get => brand;
            set { brand = value; OnPropertyChanged(); }
        }

        public string Model
        {
            get => model;
            set { model = value; OnPropertyChanged(); }
        }

        public string DriverName
        {
            get => driverName;
            set { driverName = value; OnPropertyChanged(); }
        }

        public double FuelConsumption
        {
            get => fuelConsumption;
            set { fuelConsumption = value; OnPropertyChanged(); }
        }

        public double AmortizationPerKm
        {
            get => amortizationPerKm;
            set { amortizationPerKm = value; OnPropertyChanged(); }
        }

        public Fuel? SelectedFuel
        {
            get => selectedFuel;
            set { selectedFuel = value; OnPropertyChanged(); }
        }

        public Vehicle? SelectedVehicle
        {
            get => selectedVehicle;
            set
            {
                selectedVehicle = value;
                OnPropertyChanged();

                if (value != null)
                {
                    Number = value.Number;
                    Brand = value.Brand;
                    Model = value.Model;
                    DriverName = value.DriverName;
                    FuelConsumption = value.FuelConsumption;
                    AmortizationPerKm = value.AmortizationPerKm;
                    SelectedFuel = value.FuelType;
                }
            }
        }

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand UpdateCommand { get; }

        public VehicleViewModel()
        {
            Vehicles = ServiceDb.GetVehicles();
            Fuels = ServiceDb.GetFuels();

            AddCommand = new Command(AddVehicle);
            RemoveCommand = new Command(RemoveVehicle);
            UpdateCommand = new Command(UpdateVehicle);
        }

        private void AddVehicle()
        {
            Vehicle vehicle = new Vehicle
            {
                Number = Number,
                Brand = Brand,
                Model = Model,
                DriverName = DriverName,
                FuelConsumption = FuelConsumption,
                AmortizationPerKm = AmortizationPerKm,
                FuelType = SelectedFuel
            };

            bool result = ServiceDb.AddVehicle(vehicle);

            if (result)
                ClearFields();
        }

        private void RemoveVehicle()
        {
            if (SelectedVehicle == null) return;

            Vehicles.Remove(SelectedVehicle);
            ClearFields();
        }

        private void UpdateVehicle()
        {
            if (SelectedVehicle == null) return;

            SelectedVehicle.Number = Number;
            SelectedVehicle.Brand = Brand;
            SelectedVehicle.Model = Model;
            SelectedVehicle.DriverName = DriverName;
            SelectedVehicle.FuelConsumption = FuelConsumption;
            SelectedVehicle.AmortizationPerKm = AmortizationPerKm;
            SelectedVehicle.FuelType = SelectedFuel;

            int index = Vehicles.IndexOf(SelectedVehicle);
            if (index >= 0)
                Vehicles[index] = SelectedVehicle;

            ClearFields();
        }

        private void ClearFields()
        {
            Number = string.Empty;
            Brand = string.Empty;
            Model = string.Empty;
            DriverName = string.Empty;
            FuelConsumption = 0;
            AmortizationPerKm = 0;
            SelectedFuel = null;
            SelectedVehicle = null;
        }
    }
}