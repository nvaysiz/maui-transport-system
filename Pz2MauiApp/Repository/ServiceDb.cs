using Pz2MauiApp.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pz2MauiApp.Repository
{
    internal class ServiceDb
    {
        // Фиктивное хранилище данных

        static ObservableCollection<Fuel> Fuels = new()
        {
            new Fuel
            {
                Id = 0,
                Name = "Дизель",
                PricePerLiter = 68.50
            },
            new Fuel
            {
                Id = 1,
                Name = "АИ-92",
                PricePerLiter = 54.20
            },
            new Fuel
            {
                Id = 2,
                Name = "АИ-95",
                PricePerLiter = 59.90
            }
        };

        static ObservableCollection<Vehicle> Vehicles = new()
        {
            new Vehicle
            {
                Id = 0,
                Number = "А123ВС78",
                Brand = "КАМАЗ",
                Model = "5320",
                DriverName = "Иванов Иван Иванович",
                FuelConsumption = 28.0,
                AmortizationPerKm = 15.0,
                FuelType = Fuels[0]
            },
            new Vehicle
            {
                Id = 1,
                Number = "В456ОР78",
                Brand = "ГАЗ",
                Model = "3302",
                DriverName = "Сидоров Сидор Сидорович",
                FuelConsumption = 16.0,
                AmortizationPerKm = 10.0,
                FuelType = Fuels[1]
            },
            new Vehicle
            {
                Id = 2,
                Number = "Е789КХ78",
                Brand = "MAN",
                Model = "TGS",
                DriverName = "Петров Петр Петрович",
                FuelConsumption = 24.0,
                AmortizationPerKm = 18.0,
                FuelType = Fuels[0]
            }
        };

        static ObservableCollection<Trip> Trips = new()
        {
            new Trip
            {
                Id = 0,
                Vehicle = Vehicles[0],
                Distance = 120,
                TripDate = new DateTime(2026, 4, 10),
                CargoName = "Строительные материалы",
                Route = "Склад - Объект 1",
                FuelAmount = 33.6,
                TotalCost = CalculateTripCostInternal(Vehicles[0], 120)
            },
            new Trip
            {
                Id = 1,
                Vehicle = Vehicles[1],
                Distance = 85,
                TripDate = new DateTime(2026, 4, 15),
                CargoName = "Оборудование",
                Route = "База - Клиент",
                FuelAmount = 13.6,
                TotalCost = CalculateTripCostInternal(Vehicles[1], 85)
            },
            new Trip
            {
                Id = 2,
                Vehicle = Vehicles[2],
                Distance = 210,
                TripDate = new DateTime(2026, 4, 18),
                CargoName = "Металлоконструкции",
                Route = "Завод - Стройплощадка",
                FuelAmount = 50.4,
                TotalCost = CalculateTripCostInternal(Vehicles[2], 210)
            }
        };

        public static ObservableCollection<Vehicle> GetVehicles()
        {
            return Vehicles;
        }

        public static ObservableCollection<Fuel> GetFuels()
        {
            return Fuels;
        }

        public static ObservableCollection<Trip> GetTrips()
        {
            return Trips;
        }

        public static bool AddVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
                return false;

            if (Vehicles.Any(v => v.Number == vehicle.Number))
                return false;

            vehicle.Id = Vehicles.Count > 0 ? Vehicles.Max(v => v.Id) + 1 : 0;
            Vehicles.Add(vehicle);
            return true;
        }

        public static bool AddFuel(Fuel fuel)
        {
            if (fuel == null)
                return false;

            if (Fuels.Any(f => f.Name == fuel.Name))
                return false;

            fuel.Id = Fuels.Count > 0 ? Fuels.Max(f => f.Id) + 1 : 0;
            Fuels.Add(fuel);
            return true;
        }

        public static bool AddTrip(Trip trip)
        {
            if (trip == null || trip.Vehicle == null)
                return false;

            trip.Id = Trips.Count > 0 ? Trips.Max(t => t.Id) + 1 : 0;  //терн. оператор - условие ? если_да : если_нет
            trip.FuelAmount = trip.Distance * trip.Vehicle.FuelConsumption / 100.0;
            trip.TotalCost = CalculateTripCostInternal(trip.Vehicle, trip.Distance);

            Trips.Add(trip);
            return true;
        }

        public static double CalculateTripCost(Vehicle vehicle, double distance)
        {
            if (vehicle == null || vehicle.FuelType == null || distance < 0)
                return 0;

            return CalculateTripCostInternal(vehicle, distance);
        }

        private static double CalculateTripCostInternal(Vehicle vehicle, double distance)
        {
            double fuelAmount = distance * vehicle.FuelConsumption / 100.0;
            double fuelCost = fuelAmount * vehicle.FuelType.PricePerLiter;
            double amortizationCost = distance * vehicle.AmortizationPerKm;

            return Math.Round(fuelCost + amortizationCost, 2);
        }
    }
}