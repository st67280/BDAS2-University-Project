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
    public class ReservationRepository : IReservationRepository
    {
        private readonly string _connectionString;

        public ReservationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Reservation> GetAll()
        {
            var reservations = new List<Reservation>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT RESERVATION_ID, DATE_RESERVACE, OFFICE_OFFICE_ID, CLIENT_CLIENT_ID FROM RESERVATION";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var reservation = new Reservation
                            {
                                ReservationId = Convert.ToInt32(reader["RESERVATION_ID"]),
                                DateReservace = Convert.ToDateTime(reader["DATE_RESERVACE"]),
                                OfficeOfficeId = Convert.ToInt32(reader["OFFICE_OFFICE_ID"]),
                                ClientClientId = Convert.ToInt32(reader["CLIENT_CLIENT_ID"])
                            };
                            reservations.Add(reservation);
                        }
                    }
                }
            }

            return reservations;
        }

        public Reservation GetById(int id)
        {
            Reservation reservation = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT RESERVATION_ID, DATE_RESERVACE, OFFICE_OFFICE_ID, CLIENT_CLIENT_ID FROM RESERVATION WHERE RESERVATION_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            reservation = new Reservation
                            {
                                ReservationId = Convert.ToInt32(reader["RESERVATION_ID"]),
                                DateReservace = Convert.ToDateTime(reader["DATE_RESERVACE"]),
                                OfficeOfficeId = Convert.ToInt32(reader["OFFICE_OFFICE_ID"]),
                                ClientClientId = Convert.ToInt32(reader["CLIENT_CLIENT_ID"])
                            };
                        }
                    }
                }
            }

            return reservation;
        }

        public void Add(Reservation reservation)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO RESERVATION (DATE_RESERVACE, OFFICE_OFFICE_ID, CLIENT_CLIENT_ID)
                                 VALUES (:DateReservace, :OfficeOfficeId, :ClientClientId)";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("DateReservace", reservation.DateReservace));
                    command.Parameters.Add(new OracleParameter("OfficeOfficeId", reservation.OfficeOfficeId));
                    command.Parameters.Add(new OracleParameter("ClientClientId", reservation.ClientClientId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Reservation reservation)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE RESERVATION SET DATE_RESERVACE = :DateReservace, OFFICE_OFFICE_ID = :OfficeOfficeId, CLIENT_CLIENT_ID = :ClientClientId
                                 WHERE RESERVATION_ID = :ReservationId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("DateReservace", reservation.DateReservace));
                    command.Parameters.Add(new OracleParameter("OfficeOfficeId", reservation.OfficeOfficeId));
                    command.Parameters.Add(new OracleParameter("ClientClientId", reservation.ClientClientId));
                    command.Parameters.Add(new OracleParameter("ReservationId", reservation.ReservationId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM RESERVATION WHERE RESERVATION_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
