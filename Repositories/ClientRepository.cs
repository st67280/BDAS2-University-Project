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
    public class ClientRepository : IClientRepository
    {
        private readonly string _connectionString;

        public ClientRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Client> GetAll()
        {
            var clients = new List<Client>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT CLIENT_ID, NAME_CUSTOMER, ADDRESS_ADDRESS_ID, PHONE FROM CLIENT";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var client = new Client
                            {
                                ClientId = Convert.ToInt32(reader["CLIENT_ID"]),
                                NameCustomer = reader["NAME_CUSTOMER"].ToString(),
                                AddressAddressId = Convert.ToInt32(reader["ADDRESS_ADDRESS_ID"]),
                                Phone = reader["PHONE"].ToString()
                            };
                            clients.Add(client);
                        }
                    }
                }
            }

            return clients;
        }

        public Client GetById(int id)
        {
            Client client = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT CLIENT_ID, NAME_CUSTOMER, ADDRESS_ADDRESS_ID, PHONE FROM CLIENT WHERE CLIENT_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            client = new Client
                            {
                                ClientId = Convert.ToInt32(reader["CLIENT_ID"]),
                                NameCustomer = reader["NAME_CUSTOMER"].ToString(),
                                AddressAddressId = Convert.ToInt32(reader["ADDRESS_ADDRESS_ID"]),
                                Phone = reader["PHONE"].ToString()
                            };
                        }
                    }
                }
            }

            return client;
        }

        public void Add(Client client)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO CLIENT (NAME_CUSTOMER, ADDRESS_ADDRESS_ID, PHONE)
                                 VALUES (:NameCustomer, :AddressAddressId, :Phone)";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("NameCustomer", client.NameCustomer));
                    command.Parameters.Add(new OracleParameter("AddressAddressId", client.AddressAddressId));
                    command.Parameters.Add(new OracleParameter("Phone", client.Phone));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Client client)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE CLIENT SET NAME_CUSTOMER = :NameCustomer, ADDRESS_ADDRESS_ID = :AddressAddressId, PHONE = :Phone
                                 WHERE CLIENT_ID = :ClientId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("NameCustomer", client.NameCustomer));
                    command.Parameters.Add(new OracleParameter("AddressAddressId", client.AddressAddressId));
                    command.Parameters.Add(new OracleParameter("Phone", client.Phone));
                    command.Parameters.Add(new OracleParameter("ClientId", client.ClientId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM CLIENT WHERE CLIENT_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
