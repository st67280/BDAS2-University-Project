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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly string _connectionString;

        public PaymentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Payment> GetAll()
        {
            var payments = new List<Payment>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT PAYMENT_ID, BILL_BILL_ID, CLIENT_CLIENT_ID, TYPE_PAYMENT FROM PAYMENT";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var payment = new Payment
                            {
                                PaymentId = Convert.ToInt32(reader["PAYMENT_ID"]),
                                BillBillId = Convert.ToInt32(reader["BILL_BILL_ID"]),
                                ClientClientId = Convert.ToInt32(reader["CLIENT_CLIENT_ID"]),
                                TypePayment = reader["TYPE_PAYMENT"].ToString()
                            };
                            payments.Add(payment);
                        }
                    }
                }
            }

            return payments;
        }

        public Payment GetById(int id)
        {
            Payment payment = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT PAYMENT_ID, BILL_BILL_ID, CLIENT_CLIENT_ID, TYPE_PAYMENT FROM PAYMENT WHERE PAYMENT_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            payment = new Payment
                            {
                                PaymentId = Convert.ToInt32(reader["PAYMENT_ID"]),
                                BillBillId = Convert.ToInt32(reader["BILL_BILL_ID"]),
                                ClientClientId = Convert.ToInt32(reader["CLIENT_CLIENT_ID"]),
                                TypePayment = reader["TYPE_PAYMENT"].ToString()
                            };
                        }
                    }
                }
            }

            return payment;
        }

        public void Add(Payment payment)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO PAYMENT (BILL_BILL_ID, CLIENT_CLIENT_ID, TYPE_PAYMENT)
                                 VALUES (:BillBillId, :ClientClientId, :TypePayment)";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("BillBillId", payment.BillBillId));
                    command.Parameters.Add(new OracleParameter("ClientClientId", payment.ClientClientId));
                    command.Parameters.Add(new OracleParameter("TypePayment", payment.TypePayment));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Payment payment)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE PAYMENT SET BILL_BILL_ID = :BillBillId, CLIENT_CLIENT_ID = :ClientClientId, TYPE_PAYMENT = :TypePayment
                                 WHERE PAYMENT_ID = :PaymentId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("BillBillId", payment.BillBillId));
                    command.Parameters.Add(new OracleParameter("ClientClientId", payment.ClientClientId));
                    command.Parameters.Add(new OracleParameter("TypePayment", payment.TypePayment));
                    command.Parameters.Add(new OracleParameter("PaymentId", payment.PaymentId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM PAYMENT WHERE PAYMENT_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
