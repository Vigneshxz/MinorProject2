namespace MinorProject2.Data.Models;

public class Exercise
{
    public int ExerciseID { get; set; }

    public string ExerciseName { get; set; } = "";

    public string MuscleGroup { get; set; } = "";

    public string Equipment { get; set; } = "";

    public string Difficulty { get; set; } = "";
}