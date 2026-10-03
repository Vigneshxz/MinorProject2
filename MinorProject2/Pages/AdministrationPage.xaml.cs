using System.Windows;
using System.Windows.Controls;

namespace MinorProject2.Pages;

public partial class AdministrationPage : Page
{
    public AdministrationPage()
    {
        InitializeComponent();
    }

    private void OpenTrainerManagementButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        NavigationService?.Navigate(new TrainersPage());
    }
}