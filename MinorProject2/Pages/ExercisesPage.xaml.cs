using System.Windows;
using System.Windows.Controls;
using MinorProject2.Data;
using MinorProject2.Data.Models;

namespace MinorProject2.Pages;

public partial class ExercisesPage : Page
{
    private readonly ExerciseData exerciseData;

    // Stores the ExerciseID of the selected exercise
    private int selectedExerciseID = 0;

    public ExercisesPage()
    {
        InitializeComponent();

        string settingsPath = "appsettings.json";

        string json = System.IO.File.ReadAllText(settingsPath);

        AppSettings settings =
            System.Text.Json.JsonSerializer.Deserialize<AppSettings>(json)
            ?? new AppSettings();

        DatabaseConnection database =
            new DatabaseConnection(settings.ConnectionString);

        exerciseData = new ExerciseData(database);

        LoadExercises();
    }

    // READ - Load all exercises
    private void LoadExercises()
    {
        try
        {
            ExercisesGrid.ItemsSource =
                exerciseData.GetAllExercises();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Could not load exercises.\n\n" + ex.Message,
                "Database Error");
        }
    }

    // CREATE - Add exercise
    private void AddExerciseButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(ExerciseNameBox.Text) ||
                string.IsNullOrWhiteSpace(MuscleGroupBox.Text) ||
                string.IsNullOrWhiteSpace(EquipmentBox.Text) ||
                string.IsNullOrWhiteSpace(DifficultyBox.Text))
            {
                MessageBox.Show(
                    "Please fill in all exercise fields.",
                    "Missing Information");

                return;
            }

            Exercise newExercise = new Exercise
            {
                ExerciseName = ExerciseNameBox.Text,
                MuscleGroup = MuscleGroupBox.Text,
                Equipment = EquipmentBox.Text,
                Difficulty = DifficultyBox.Text
            };

            exerciseData.AddExercise(newExercise);

            MessageBox.Show(
                "Exercise added successfully.",
                "Success");

            ClearExerciseFields();

            LoadExercises();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Could not add exercise.\n\n" + ex.Message,
                "Database Error");
        }
    }

    // SELECT - Select exercise from table
    private void ExercisesGrid_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        if (ExercisesGrid.SelectedItem is Exercise selectedExercise)
        {
            selectedExerciseID = selectedExercise.ExerciseID;

            ExerciseNameBox.Text = selectedExercise.ExerciseName;
            MuscleGroupBox.Text = selectedExercise.MuscleGroup;
            EquipmentBox.Text = selectedExercise.Equipment;
            DifficultyBox.Text = selectedExercise.Difficulty;
        }
    }

    // UPDATE - Update selected exercise
    private void UpdateExerciseButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            if (selectedExerciseID == 0)
            {
                MessageBox.Show(
                    "Please select an exercise from the table first.",
                    "No Exercise Selected");

                return;
            }

            if (string.IsNullOrWhiteSpace(ExerciseNameBox.Text) ||
                string.IsNullOrWhiteSpace(MuscleGroupBox.Text) ||
                string.IsNullOrWhiteSpace(EquipmentBox.Text) ||
                string.IsNullOrWhiteSpace(DifficultyBox.Text))
            {
                MessageBox.Show(
                    "Please fill in all exercise fields.",
                    "Missing Information");

                return;
            }

            Exercise updatedExercise = new Exercise
            {
                ExerciseID = selectedExerciseID,
                ExerciseName = ExerciseNameBox.Text,
                MuscleGroup = MuscleGroupBox.Text,
                Equipment = EquipmentBox.Text,
                Difficulty = DifficultyBox.Text
            };

            exerciseData.UpdateExercise(updatedExercise);

            MessageBox.Show(
                "Exercise updated successfully.",
                "Success");

            selectedExerciseID = 0;

            ClearExerciseFields();

            LoadExercises();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Could not update exercise.\n\n" + ex.Message,
                "Database Error");
        }
    }

    // DELETE - Delete selected exercise
    private void DeleteExerciseButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            if (selectedExerciseID == 0)
            {
                MessageBox.Show(
                    "Please select an exercise from the table first.",
                    "No Exercise Selected");

                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to delete this exercise?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            exerciseData.DeleteExercise(selectedExerciseID);

            MessageBox.Show(
                "Exercise deleted successfully.",
                "Success");

            selectedExerciseID = 0;

            ClearExerciseFields();

            LoadExercises();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Could not delete exercise.\n\n" + ex.Message,
                "Database Error");
        }
    }

    // Clear exercise form
    private void ClearExerciseFields()
    {
        ExerciseNameBox.Clear();
        MuscleGroupBox.Clear();
        EquipmentBox.Clear();
        DifficultyBox.Clear();
    }
}