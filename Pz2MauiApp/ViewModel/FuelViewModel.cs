using Pz2MauiApp.Model;
using Pz2MauiApp.Repository;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Pz2MauiApp.ViewModel
{
    public class FuelViewModel : INotifyPropertyChanged
    {
        private string name;
        private double pricePerLiter;
        private Fuel? selectedFuel;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public ObservableCollection<Fuel> Fuels { get; set; }

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        public double PricePerLiter
        {
            get => pricePerLiter;
            set { pricePerLiter = value; OnPropertyChanged(); }
        }

        public Fuel? SelectedFuel
        {
            get => selectedFuel;
            set
            {
                selectedFuel = value;
                OnPropertyChanged();

                if (value != null)
                {
                    Name = value.Name;
                    PricePerLiter = value.PricePerLiter;
                }
            }
        }

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand UpdateCommand { get; }

        public FuelViewModel()
        {
            Fuels = ServiceDb.GetFuels();

            AddCommand = new Command(AddFuel);
            RemoveCommand = new Command(RemoveFuel);
            UpdateCommand = new Command(UpdateFuel);
        }

        private void AddFuel()
        {
            Fuel fuel = new Fuel
            {
                Name = Name,
                PricePerLiter = PricePerLiter
            };

            bool result = ServiceDb.AddFuel(fuel);

            if (result)
                ClearFields();
        }

        private void RemoveFuel()
        {
            if (SelectedFuel == null) return;

            Fuels.Remove(SelectedFuel);
            ClearFields();
        }

        private void UpdateFuel()
        {
            if (SelectedFuel == null) return;

            SelectedFuel.Name = Name;
            SelectedFuel.PricePerLiter = PricePerLiter;

            int index = Fuels.IndexOf(SelectedFuel);
            if (index >= 0)
                Fuels[index] = SelectedFuel; //замена элемента в колекции обсервбл уже с новой информацией

            ClearFields();
        }

        private void ClearFields()
        {
            Name = string.Empty;
            PricePerLiter = 0;
            SelectedFuel = null;
        }
    }
}