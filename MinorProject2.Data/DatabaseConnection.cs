using MySql.Data.MySqlClient;

namespace MinorProject2.Data;

public class DatabaseConnection
{
    private readonly string connectionString;

    public DatabaseConnection(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }
}