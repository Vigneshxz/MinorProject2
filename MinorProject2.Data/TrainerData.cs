using MySql.Data.MySqlClient;
using MinorProject2.Data.Models;

namespace MinorProject2.Data;

public class TrainerData
{
    private readonly DatabaseConnection database;

    public TrainerData(DatabaseConnection database)
    {
        this.database = database;
    }

    // READ - Get all trainers
    public List<Trainer> GetAllTrainers()
    {
        List<Trainer> trainers = new List<Trainer>();

        using MySqlConnection connection = database.GetConnection();

        connection.Open();

        string query = """
            SELECT TrainerID, FirstName, LastName, Specialty, Email
            FROM Trainers
            """;

        using MySqlCommand command =
            new MySqlCommand(query, connection);

        using MySqlDataReader reader =
            command.ExecuteReader();

        while (reader.Read())
        {
            Trainer trainer = new Trainer
            {
                TrainerID = reader.GetInt32("TrainerID"),
                FirstName = reader.GetString("FirstName"),
                LastName = reader.GetString("LastName"),
                Specialty = reader.GetString("Specialty"),
                Email = reader.GetString("Email")
            };

            trainers.Add(trainer);
        }

        return trainers;
    }

    // CREATE - Add a new trainer
    public void AddTrainer(Trainer trainer)
    {
        using MySqlConnection connection = database.GetConnection();

        connection.Open();

        string query = """
            INSERT INTO Trainers
                (FirstName, LastName, Specialty, Email)
            VALUES
                (@FirstName, @LastName, @Specialty, @Email)
            """;

        using MySqlCommand command =
            new MySqlCommand(query, connection);

        command.Parameters.AddWithValue(
            "@FirstName",
            trainer.FirstName);

        command.Parameters.AddWithValue(
            "@LastName",
            trainer.LastName);

        command.Parameters.AddWithValue(
            "@Specialty",
            trainer.Specialty);

        command.Parameters.AddWithValue(
            "@Email",
            trainer.Email);

        command.ExecuteNonQuery();
    }

    // UPDATE - Update an existing trainer
    public void UpdateTrainer(Trainer trainer)
    {
        using MySqlConnection connection = database.GetConnection();

        connection.Open();

        string query = """
            UPDATE Trainers
            SET FirstName = @FirstName,
                LastName = @LastName,
                Specialty = @Specialty,
                Email = @Email
            WHERE TrainerID = @TrainerID
            """;

        using MySqlCommand command =
            new MySqlCommand(query, connection);

        command.Parameters.AddWithValue(
            "@TrainerID",
            trainer.TrainerID);

        command.Parameters.AddWithValue(
            "@FirstName",
            trainer.FirstName);

        command.Parameters.AddWithValue(
            "@LastName",
            trainer.LastName);

        command.Parameters.AddWithValue(
            "@Specialty",
            trainer.Specialty);

        command.Parameters.AddWithValue(
            "@Email",
            trainer.Email);

        command.ExecuteNonQuery();
    }

    // DELETE - Delete a trainer
    public void DeleteTrainer(int trainerID)
    {
        using MySqlConnection connection = database.GetConnection();

        connection.Open();

        string query = """
            DELETE FROM Trainers
            WHERE TrainerID = @TrainerID
            """;

        using MySqlCommand command =
            new MySqlCommand(query, connection);

        command.Parameters.AddWithValue(
            "@TrainerID",
            trainerID);

        command.ExecuteNonQuery();
    }
}