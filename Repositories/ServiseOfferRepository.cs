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
    public class ServiseOfferRepository : IServiseOfferRepository
    {
        private readonly string _connectionString;

        public ServiseOfferRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<ServiseOffer> GetAll()
        {
            var serviseOffers = new List<ServiseOffer>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"SELECT OFFER_ID, PRICE_PER_HOUR, DATE_OFFER, EMPLOYER_EMPLOYER_ID, 
                                 CAR_CAR_ID, TYP, WORKING_HOURS FROM SERVISE_OFFER";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var serviseOffer = new ServiseOffer
                            {
                                OfferId = Convert.ToInt32(reader["OFFER_ID"]),
                                PricePerHour = Convert.ToDecimal(reader["PRICE_PER_HOUR"]),
                                DateOffer = Convert.ToDateTime(reader["DATE_OFFER"]),
                                EmployerEmployerId = Convert.ToInt32(reader["EMPLOYER_EMPLOYER_ID"]),
                                CarCarId = Convert.ToInt32(reader["CAR_CAR_ID"]),
                                Typ = reader["TYP"].ToString(),
                                WorkingHours = Convert.ToInt32(reader["WORKING_HOURS"])
                            };
                            serviseOffers.Add(serviseOffer);
                        }
                    }
                }
            }

            return serviseOffers;
        }

        public ServiseOffer GetById(int id)
        {
            ServiseOffer serviseOffer = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"SELECT OFFER_ID, PRICE_PER_HOUR, DATE_OFFER, EMPLOYER_EMPLOYER_ID, 
                                 CAR_CAR_ID, TYP, WORKING_HOURS FROM SERVISE_OFFER WHERE OFFER_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            serviseOffer = new ServiseOffer
                            {
                                OfferId = Convert.ToInt32(reader["OFFER_ID"]),
                                PricePerHour = Convert.ToDecimal(reader["PRICE_PER_HOUR"]),
                                DateOffer = Convert.ToDateTime(reader["DATE_OFFER"]),
                                EmployerEmployerId = Convert.ToInt32(reader["EMPLOYER_EMPLOYER_ID"]),
                                CarCarId = Convert.ToInt32(reader["CAR_CAR_ID"]),
                                Typ = reader["TYP"].ToString(),
                                WorkingHours = Convert.ToInt32(reader["WORKING_HOURS"])
                            };
                        }
                    }
                }
            }

            return serviseOffer;
        }

        public void Add(ServiseOffer serviseOffer)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO SERVISE_OFFER (PRICE_PER_HOUR, DATE_OFFER, EMPLOYER_EMPLOYER_ID, 
                                     CAR_CAR_ID, TYP, WORKING_HOURS)
                                 VALUES (:PricePerHour, :DateOffer, :EmployerEmployerId, :CarCarId, :Typ, :WorkingHours)";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("PricePerHour", serviseOffer.PricePerHour));
                    command.Parameters.Add(new OracleParameter("DateOffer", serviseOffer.DateOffer));
                    command.Parameters.Add(new OracleParameter("EmployerEmployerId", serviseOffer.EmployerEmployerId));
                    command.Parameters.Add(new OracleParameter("CarCarId", serviseOffer.CarCarId));
                    command.Parameters.Add(new OracleParameter("Typ", serviseOffer.Typ));
                    command.Parameters.Add(new OracleParameter("WorkingHours", serviseOffer.WorkingHours));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(ServiseOffer serviseOffer)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE SERVISE_OFFER SET PRICE_PER_HOUR = :PricePerHour, DATE_OFFER = :DateOffer, 
                                 EMPLOYER_EMPLOYER_ID = :EmployerEmployerId, CAR_CAR_ID = :CarCarId, TYP = :Typ, 
                                 WORKING_HOURS = :WorkingHours WHERE OFFER_ID = :OfferId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("PricePerHour", serviseOffer.PricePerHour));
                    command.Parameters.Add(new OracleParameter("DateOffer", serviseOffer.DateOffer));
                    command.Parameters.Add(new OracleParameter("EmployerEmployerId", serviseOffer.EmployerEmployerId));
                    command.Parameters.Add(new OracleParameter("CarCarId", serviseOffer.CarCarId));
                    command.Parameters.Add(new OracleParameter("Typ", serviseOffer.Typ));
                    command.Parameters.Add(new OracleParameter("WorkingHours", serviseOffer.WorkingHours));
                    command.Parameters.Add(new OracleParameter("OfferId", serviseOffer.OfferId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM SERVISE_OFFER WHERE OFFER_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
