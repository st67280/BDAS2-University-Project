using BDAS2_University_Project.Models;
using BDAS2_University_Project.Repositories.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly string _connectionString;

        public CarRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Car> GetAll()
        {
            var cars = new List<Car>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT CAR_ID, SPZ, CAR_BRAND, SYMPTOMS, RESERVATION_RESERVATION_ID FROM CAR";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var car = new Car
                            {
                                CarId = Convert.ToInt32(reader["CAR_ID"]),
                                Spz = reader["SPZ"].ToString(),
                                CarBrand = reader["CAR_BRAND"].ToString(),
                                Symptoms = reader["SYMPTOMS"].ToString(),
                                ReservationReservationId = Convert.ToInt32(reader["RESERVATION_RESERVATION_ID"])
                            };
                            cars.Add(car);
                        }
                    }
                }
            }

            return cars;
        }

        public Car GetById(int id)
        {
            Car car = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT CAR_ID, SPZ, CAR_BRAND, SYMPTOMS, RESERVATION_RESERVATION_ID FROM CAR WHERE CAR_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            car = new Car
                            {
                                CarId = Convert.ToInt32(reader["CAR_ID"]),
                                Spz = reader["SPZ"].ToString(),
                                CarBrand = reader["CAR_BRAND"].ToString(),
                                Symptoms = reader["SYMPTOMS"].ToString(),
                                ReservationReservationId = Convert.ToInt32(reader["RESERVATION_RESERVATION_ID"])
                            };
                        }
                    }
                }
            }

            return car;
        }

        public void Add(Car car)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO CAR (SPZ, CAR_BRAND, SYMPTOMS, RESERVATION_RESERVATION_ID)
                                 VALUES (:Spz, :CarBrand, :Symptoms, :ReservationReservationId)";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Spz", car.Spz));
                    command.Parameters.Add(new OracleParameter("CarBrand", car.CarBrand));
                    command.Parameters.Add(new OracleParameter("Symptoms", car.Symptoms));
                    command.Parameters.Add(new OracleParameter("ReservationReservationId", car.ReservationReservationId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Car car)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE CAR SET SPZ = :Spz, CAR_BRAND = :CarBrand, SYMPTOMS = :Symptoms, RESERVATION_RESERVATION_ID = :ReservationReservationId
                                 WHERE CAR_ID = :CarId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Spz", car.Spz));
                    command.Parameters.Add(new OracleParameter("CarBrand", car.CarBrand));
                    command.Parameters.Add(new OracleParameter("Symptoms", car.Symptoms));
                    command.Parameters.Add(new OracleParameter("ReservationReservationId", car.ReservationReservationId));
                    command.Parameters.Add(new OracleParameter("CarId", car.CarId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM CAR WHERE CAR_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
