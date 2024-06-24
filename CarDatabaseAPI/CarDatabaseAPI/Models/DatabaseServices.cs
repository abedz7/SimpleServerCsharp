using System;
using System.Collections.Generic;
using System.Linq;

namespace CarDatabaseAPI.Models
{
    public static class DatabaseServices
    {
        private static readonly DBmock _dbMock = new DBmock();

        public static List<Car> GetAllCars()
        {
            return _dbMock.cars;
        }

        public static Car GetCarById(int id)
        {
            return _dbMock.cars.FirstOrDefault(car => car.id == id);
        }

        public static int InsertCar(Car car)
        {
            if (car == null)
                throw new ArgumentNullException(nameof(car));

            car.id = _dbMock.cars.Max(c => c.id) + 1;
            _dbMock.cars.Add(car);
            return car.id;
        }

        public static int UpdateCar(Car car)
        {
            if (car == null)
                throw new ArgumentNullException(nameof(car));

            var existingCar = _dbMock.cars.FirstOrDefault(c => c.id == car.id);
            if (existingCar == null)
                return 0;

            existingCar.Company = car.Company;
            existingCar.Model = car.Model;
            existingCar.Car_Number = car.Car_Number;
            return 1;
        }

        public static bool DeleteCar(int id)
        {
            var car = _dbMock.cars.FirstOrDefault(c => c.id == id);
            if (car == null)
                return false;

            _dbMock.cars.Remove(car);
            return true;
        }
    }
}
