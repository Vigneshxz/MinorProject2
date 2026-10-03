using System.Windows;
using System.Windows.Controls;
using MinorProject2.Data;
using MinorProject2.Data.Models;

namespace MinorProject2.Pages;

public partial class TrainersPage : Page
{
    private readonly TrainerData trainerData;

    // Stores the TrainerID of the selected trainer
    private int selectedTrainerID = 0;

    public TrainersPage()
    {
        InitializeComponent();

        string settingsPath = "appsettings.json";

        string json = System.IO.File.ReadAllText(settingsPath);

        AppSettings settings =
            System.Text.Json.JsonSerializer.Deserialize<AppSettings>(json)
            ?? new AppSettings();

        DatabaseConnection database =
            new DatabaseConnection(settings.ConnectionString);

        trainerData = new TrainerData(database);

        LoadTrainers();
    }

    // READ - Load all trainers
    private void LoadTrainers()
    {
        try
        {
            TrainersGrid.ItemsSource =
                trainerData.GetAllTrainers();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Could not load trainers.\n\n" + ex.Message,
                "Database Error");
        }
    }

    // CREATE - Add trainer
    private void AddTrainerButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(FirstNameBox.Text) ||
                string.IsNullOrWhiteSpace(LastNameBox.Text) ||
                string.IsNullOrWhiteSpace(SpecialtyBox.Text) ||
                string.IsNullOrWhiteSpace(EmailBox.Text))
            {
                MessageBox.Show(
                    "Please fill in all trainer fields.",
                    "Missing Information");

                return;
            }

            Trainer newTrainer = new Trainer
            {
                FirstName = FirstNameBox.Text,
                LastName = LastNameBox.Text,
                Specialty = SpecialtyBox.Text,
                Email = EmailBox.Text
            };

            trainerData.AddTrainer(newTrainer);

            MessageBox.Show(
                "Trainer added successfully.",
                "Success");

            ClearTrainerFields();

            LoadTrainers();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Could not add trainer.\n\n" + ex.Message,
                "Database Error");
        }
    }

    // SELECT - Select trainer from table
    private void TrainersGrid_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        if (TrainersGrid.SelectedItem is Trainer selectedTrainer)
        {
            selectedTrainerID = selectedTrainer.TrainerID;

            FirstNameBox.Text = selectedTrainer.FirstName;
            LastNameBox.Text = selectedTrainer.LastName;
            SpecialtyBox.Text = selectedTrainer.Specialty;
            EmailBox.Text = selectedTrainer.Email;
        }
    }

    // UPDATE - Update selected trainer
    private void UpdateTrainerButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            if (selectedTrainerID == 0)
            {
                MessageBox.Show(
                    "Please select a trainer from the table first.",
                    "No Trainer Selected");

                return;
            }

            if (string.IsNullOrWhiteSpace(FirstNameBox.Text) ||
                string.IsNullOrWhiteSpace(LastNameBox.Text) ||
                string.IsNullOrWhiteSpace(SpecialtyBox.Text) ||
                string.IsNullOrWhiteSpace(EmailBox.Text))
            {
                MessageBox.Show(
                    "Please fill in all trainer fields.",
                    "Missing Information");

                return;
            }

            Trainer updatedTrainer = new Trainer
            {
                TrainerID = selectedTrainerID,
                FirstName = FirstNameBox.Text,
                LastName = LastNameBox.Text,
                Specialty = SpecialtyBox.Text,
                Email = EmailBox.Text
            };

            trainerData.UpdateTrainer(updatedTrainer);

            MessageBox.Show(
                "Trainer updated successfully.",
                "Success");

            selectedTrainerID = 0;

            ClearTrainerFields();

            LoadTrainers();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Could not update trainer.\n\n" + ex.Message,
                "Database Error");
        }
    }

    // DELETE - Delete selected trainer
    private void DeleteTrainerButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            if (selectedTrainerID == 0)
            {
                MessageBox.Show(
                    "Please select a trainer from the table first.",
                    "No Trainer Selected");

                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to delete this trainer?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            trainerData.DeleteTrainer(selectedTrainerID);

            MessageBox.Show(
                "Trainer deleted successfully.",
                "Success");

            selectedTrainerID = 0;

            ClearTrainerFields();

            LoadTrainers();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Could not delete trainer.\n\n" + ex.Message,
                "Database Error");
        }
    }

    // Clear trainer form
    private void ClearTrainerFields()
    {
        FirstNameBox.Clear();
        LastNameBox.Clear();
        SpecialtyBox.Clear();
        EmailBox.Clear();
    }
}