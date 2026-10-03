using MySql.Data.MySqlClient;
using MinorProject2.Data.Models;

namespace MinorProject2.Data;

public class ExerciseData
{
    private readonly DatabaseConnection database;

    public ExerciseData(DatabaseConnection database)
    {
        this.database = database;
    }

    // READ - Get all exercises
    public List<Exercise> GetAllExercises()
    {
        List<Exercise> exercises = new List<Exercise>();

        using MySqlConnection connection = database.GetConnection();

        connection.Open();

        string query = """
            SELECT ExerciseID, ExerciseName, MuscleGroup, Equipment, Difficulty
            FROM Exercises
            """;

        using MySqlCommand command =
            new MySqlCommand(query, connection);

        using MySqlDataReader reader =
            command.ExecuteReader();

        while (reader.Read())
        {
            Exercise exercise = new Exercise
            {
                ExerciseID = reader.GetInt32("ExerciseID"),
                ExerciseName = reader.GetString("ExerciseName"),
                MuscleGroup = reader.GetString("MuscleGroup"),
                Equipment = reader.GetString("Equipment"),
                Difficulty = reader.GetString("Difficulty")
            };

            exercises.Add(exercise);
        }

        return exercises;
    }

    // CREATE - Add a new exercise
    public void AddExercise(Exercise exercise)
    {
        using MySqlConnection connection = database.GetConnection();

        connection.Open();

        string query = """
            INSERT INTO Exercises
                (ExerciseName, MuscleGroup, Equipment, Difficulty)
            VALUES
                (@ExerciseName, @MuscleGroup, @Equipment, @Difficulty)
            """;

        using MySqlCommand command =
            new MySqlCommand(query, connection);

        command.Parameters.AddWithValue(
            "@ExerciseName",
            exercise.ExerciseName);

        command.Parameters.AddWithValue(
            "@MuscleGroup",
            exercise.MuscleGroup);

        command.Parameters.AddWithValue(
            "@Equipment",
            exercise.Equipment);

        command.Parameters.AddWithValue(
            "@Difficulty",
            exercise.Difficulty);

        command.ExecuteNonQuery();
    }

    // UPDATE - Update an existing exercise
    public void UpdateExercise(Exercise exercise)
    {
        using MySqlConnection connection = database.GetConnection();

        connection.Open();

        string query = """
            UPDATE Exercises
            SET ExerciseName = @ExerciseName,
                MuscleGroup = @MuscleGroup,
                Equipment = @Equipment,
                Difficulty = @Difficulty
            WHERE ExerciseID = @ExerciseID
            """;

        using MySqlCommand command =
            new MySqlCommand(query, connection);

        command.Parameters.AddWithValue(
            "@ExerciseID",
            exercise.ExerciseID);

        command.Parameters.AddWithValue(
            "@ExerciseName",
            exercise.ExerciseName);

        command.Parameters.AddWithValue(
            "@MuscleGroup",
            exercise.MuscleGroup);

        command.Parameters.AddWithValue(
            "@Equipment",
            exercise.Equipment);

        command.Parameters.AddWithValue(
            "@Difficulty",
            exercise.Difficulty);

        command.ExecuteNonQuery();
    }

    // DELETE - Delete an exercise
    public void DeleteExercise(int exerciseID)
    {
        using MySqlConnection connection = database.GetConnection();

        connection.Open();

        string query = """
            DELETE FROM Exercises
            WHERE ExerciseID = @ExerciseID
            """;

        using MySqlCommand command =
            new MySqlCommand(query, connection);

        command.Parameters.AddWithValue(
            "@ExerciseID",
            exerciseID);

        command.ExecuteNonQuery();
    }
}