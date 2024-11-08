using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using System.Data;

public sealed class DatabaseConnection
{
    // Static variable to hold the single instance of the DatabaseConnection
    private static readonly Lazy<DatabaseConnection> instance =
        new Lazy<DatabaseConnection>(() => new DatabaseConnection());

    // SqlConnection instance
    private Microsoft.Data.SqlClient.SqlConnection connection;

    // Private constructor to prevent direct instantiation
    private DatabaseConnection()
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagementDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;";
        connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString);
    }

    // Public static property to get the single instance of DatabaseConnection
    public static DatabaseConnection Instance
    {
        get
        {
            return instance.Value;
        }
    }

    // Method to get the SqlConnection instance
    public Microsoft.Data.SqlClient.SqlConnection GetConnection()
    {
        if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
        {
            connection.Open();
        }
        return connection;
    }

    // Method to close the connection
    public void CloseConnection()
    {
        if (connection != null && connection.State == System.Data.ConnectionState.Open)
        {
            connection.Close();
        }
    }
}
