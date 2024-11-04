using System;
using System.Data.SqlClient;

namespace LibraryManagementSystem.Data
{
    public class DatabaseConnection
    {
        private static DatabaseConnection _instance;
        private static readonly object _lock = new object();

        private Microsoft.Data.SqlClient.SqlConnection _connection;
        private readonly string _connectionString;

        // Private constructor to prevent instantiation from outside
        private DatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
        }

        // Public static method to get the single instance of DatabaseConnection
        public static DatabaseConnection GetInstance(string connectionString)
        {
            // Double-checked locking to ensure thread safety
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DatabaseConnection(connectionString);
                    }
                }
            }
            return _instance;
        }

        // Method to open the connection if it's not already open
        public Microsoft.Data.SqlClient.SqlConnection GetConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }
            return _connection;
        }

        // Optional method to close the connection when done
        public void CloseConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
