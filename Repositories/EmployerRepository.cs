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
    public class EmployerRepository : IEmployerRepository
    {
        private readonly string _connectionString;

        public EmployerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Employer> GetAll()
        {
            var employers = new List<Employer>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT EMPLOYER_ID, SPECIALITY, NAME_EMPLOYEE, PHONE, OFFICE_OFFICE_ID, EMPLOYER_EMPLOYER_ID, ADDRESS_ADDRESS_ID FROM EMPLOYER";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var employer = new Employer
                            {
                                EmployerId = Convert.ToInt32(reader["EMPLOYER_ID"]),
                                Speciality = reader["SPECIALITY"].ToString(),
                                NameEmployee = reader["NAME_EMPLOYEE"].ToString(),
                                Phone = reader["PHONE"].ToString(),
                                OfficeOfficeId = Convert.ToInt32(reader["OFFICE_OFFICE_ID"]),
                                EmployerEmployerId = reader["EMPLOYER_EMPLOYER_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["EMPLOYER_EMPLOYER_ID"]),
                                AddressAddressId = Convert.ToInt32(reader["ADDRESS_ADDRESS_ID"])
                            };
                            employers.Add(employer);
                        }
                    }
                }
            }

            return employers;
        }

        public Employer GetById(int id)
        {
            Employer employer = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT EMPLOYER_ID, SPECIALITY, NAME_EMPLOYEE, PHONE, OFFICE_OFFICE_ID, EMPLOYER_EMPLOYER_ID, ADDRESS_ADDRESS_ID FROM EMPLOYER WHERE EMPLOYER_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            employer = new Employer
                            {
                                EmployerId = Convert.ToInt32(reader["EMPLOYER_ID"]),
                                Speciality = reader["SPECIALITY"].ToString(),
                                NameEmployee = reader["NAME_EMPLOYEE"].ToString(),
                                Phone = reader["PHONE"].ToString(),
                                OfficeOfficeId = Convert.ToInt32(reader["OFFICE_OFFICE_ID"]),
                                EmployerEmployerId = reader["EMPLOYER_EMPLOYER_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["EMPLOYER_EMPLOYER_ID"]),
                                AddressAddressId = Convert.ToInt32(reader["ADDRESS_ADDRESS_ID"])
                            };
                        }
                    }
                }
            }

            return employer;
        }

        public void Add(Employer employer)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO EMPLOYER (SPECIALITY, NAME_EMPLOYEE, PHONE, OFFICE_OFFICE_ID, EMPLOYER_EMPLOYER_ID, ADDRESS_ADDRESS_ID)
                                 VALUES (:Speciality, :NameEmployee, :Phone, :OfficeOfficeId, :EmployerEmployerId, :AddressAddressId)";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Speciality", employer.Speciality));
                    command.Parameters.Add(new OracleParameter("NameEmployee", employer.NameEmployee));
                    command.Parameters.Add(new OracleParameter("Phone", employer.Phone));
                    command.Parameters.Add(new OracleParameter("OfficeOfficeId", employer.OfficeOfficeId));
                    command.Parameters.Add(new OracleParameter("EmployerEmployerId", employer.EmployerEmployerId));
                    command.Parameters.Add(new OracleParameter("AddressAddressId", employer.AddressAddressId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Employer employer)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE EMPLOYER SET SPECIALITY = :Speciality, NAME_EMPLOYEE = :NameEmployee, PHONE = :Phone, 
                                 OFFICE_OFFICE_ID = :OfficeOfficeId, EMPLOYER_EMPLOYER_ID = :EmployerEmployerId, ADDRESS_ADDRESS_ID = :AddressAddressId
                                 WHERE EMPLOYER_ID = :EmployerId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Speciality", employer.Speciality));
                    command.Parameters.Add(new OracleParameter("NameEmployee", employer.NameEmployee));
                    command.Parameters.Add(new OracleParameter("Phone", employer.Phone));
                    command.Parameters.Add(new OracleParameter("OfficeOfficeId", employer.OfficeOfficeId));
                    command.Parameters.Add(new OracleParameter("EmployerEmployerId", employer.EmployerEmployerId));
                    command.Parameters.Add(new OracleParameter("AddressAddressId", employer.AddressAddressId));
                    command.Parameters.Add(new OracleParameter("EmployerId", employer.EmployerId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM EMPLOYER WHERE EMPLOYER_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
