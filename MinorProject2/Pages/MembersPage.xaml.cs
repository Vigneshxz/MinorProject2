using System.Windows;
using System.Windows.Controls;
using MinorProject2.Data;
using MinorProject2.Data.Models;
using System.IO;
using System.Text.Json;

namespace MinorProject2.Pages;

public partial class MembersPage : Page
{
    private readonly MemberData memberData;

    // Keeps track of the member currently selected in the table
    private int selectedMemberID = 0;

    public MembersPage()
    {
        InitializeComponent();

        string settingsPath = "appsettings.json";

        string json = File.ReadAllText(settingsPath);
        AppSettings settings =
            JsonSerializer.Deserialize<AppSettings>(json)
            ?? new AppSettings();
        
        DatabaseConnection database =
            new DatabaseConnection(settings.ConnectionString);
        
        memberData = new MemberData(database);
        
        LoadMembers();
    }

    // READ - Load all members
    private void LoadMembers()
    {
        try
        {
            MembersGrid.ItemsSource = memberData.GetAllMembers();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Could not load members.\n\n" + ex.Message,
                "Database Error");
        }
    }

    // SEARCH - Search members
    private void SearchButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            string firstName = FirstNameSearchBox.Text;
            string lastName = LastNameSearchBox.Text;
            string email = EmailSearchBox.Text;

            MembersGrid.ItemsSource =
                memberData.SearchMembers(
                    firstName,
                    lastName,
                    email);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Search failed.\n\n" + ex.Message,
                "Database Error");
        }
    }

    // Show all members
    private void ShowAllButton_Click(object sender, RoutedEventArgs e)
    {
        FirstNameSearchBox.Clear();
        LastNameSearchBox.Clear();
        EmailSearchBox.Clear();

        LoadMembers();
    }

    // CREATE - Add a new member
    private void AddMemberButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(FirstNameBox.Text) ||
                string.IsNullOrWhiteSpace(LastNameBox.Text) ||
                string.IsNullOrWhiteSpace(EmailBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneBox.Text))
            {
                MessageBox.Show(
                    "Please fill in all member fields.",
                    "Missing Information");

                return;
            }

            Member newMember = new Member
            {
                FirstName = FirstNameBox.Text,
                LastName = LastNameBox.Text,
                Email = EmailBox.Text,
                Phone = PhoneBox.Text
            };

            memberData.AddMember(newMember);

            MessageBox.Show(
                "Member added successfully.",
                "Success");

            ClearMemberFields();

            LoadMembers();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Could not add member.\n\n" + ex.Message,
                "Database Error");
        }
    }

    // SELECT - Select a member from the table
    private void MembersGrid_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        if (MembersGrid.SelectedItem is Member selectedMember)
        {
            selectedMemberID = selectedMember.MemberID;

            FirstNameBox.Text = selectedMember.FirstName;
            LastNameBox.Text = selectedMember.LastName;
            EmailBox.Text = selectedMember.Email;
            PhoneBox.Text = selectedMember.Phone;
        }
    }

    // UPDATE - Update selected member
    private void UpdateMemberButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            if (selectedMemberID == 0)
            {
                MessageBox.Show(
                    "Please select a member from the table first.",
                    "No Member Selected");

                return;
            }

            if (string.IsNullOrWhiteSpace(FirstNameBox.Text) ||
                string.IsNullOrWhiteSpace(LastNameBox.Text) ||
                string.IsNullOrWhiteSpace(EmailBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneBox.Text))
            {
                MessageBox.Show(
                    "Please fill in all member fields.",
                    "Missing Information");

                return;
            }

            Member updatedMember = new Member
            {
                MemberID = selectedMemberID,
                FirstName = FirstNameBox.Text,
                LastName = LastNameBox.Text,
                Email = EmailBox.Text,
                Phone = PhoneBox.Text
            };

            memberData.UpdateMember(updatedMember);

            MessageBox.Show(
                "Member updated successfully.",
                "Success");

            ClearMemberFields();

            selectedMemberID = 0;

            LoadMembers();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Could not update member.\n\n" + ex.Message,
                "Database Error");
        }
    }

    // DELETE - Delete selected member
    private void DeleteMemberButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            if (selectedMemberID == 0)
            {
                MessageBox.Show(
                    "Please select a member from the table first.",
                    "No Member Selected");

                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to delete this member?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            memberData.DeleteMember(selectedMemberID);

            MessageBox.Show(
                "Member deleted successfully.",
                "Success");

            ClearMemberFields();

            selectedMemberID = 0;

            LoadMembers();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Could not delete member.\n\n" + ex.Message,
                "Database Error");
        }
    }

    // Clear the member form
    private void ClearMemberFields()
    {
        FirstNameBox.Clear();
        LastNameBox.Clear();
        EmailBox.Clear();
        PhoneBox.Clear();
    }
}