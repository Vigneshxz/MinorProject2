using System.Windows;
using MinorProject2.Pages;

namespace MinorProject2;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // Open Members page when the application starts
        MainFrame.Navigate(new MembersPage());
    }

    private void MembersButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        MainFrame.Navigate(new MembersPage());
    }

    private void TrainersButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        MainFrame.Navigate(new TrainersPage());
    }

    private void ExercisesButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        MainFrame.Navigate(new ExercisesPage());
    }

    private void SettingsButton_Click(
    object sender,
    RoutedEventArgs e)
    {
        MainFrame.Navigate(new SettingsPage());
    }

    private void AdministrationButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        MainFrame.Navigate(new AdministrationPage());
    }

}