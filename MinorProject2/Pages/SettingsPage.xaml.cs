using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MinorProject2.Pages;

public partial class SettingsPage : Page
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    // Switch to Light Theme
    private void LightThemeButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        Application.Current.Resources["AppBackground"] =
            new SolidColorBrush(Colors.White);

        Application.Current.Resources["AppForeground"] =
            new SolidColorBrush(Colors.Black);

        Application.Current.Resources["ControlBackground"] =
            new SolidColorBrush(Color.FromRgb(242, 242, 242));

        Application.Current.Resources["ControlBorder"] =
            new SolidColorBrush(Color.FromRgb(204, 204, 204));

        ThemeStatusText.Text = "Current theme: Light";
    }

    // Switch to Dark Theme
    private void DarkThemeButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        Application.Current.Resources["AppBackground"] =
            new SolidColorBrush(Color.FromRgb(35, 35, 35));

        Application.Current.Resources["AppForeground"] =
            new SolidColorBrush(Colors.White);

        Application.Current.Resources["ControlBackground"] =
            new SolidColorBrush(Color.FromRgb(55, 55, 55));

        Application.Current.Resources["ControlBorder"] =
            new SolidColorBrush(Color.FromRgb(90, 90, 90));

        ThemeStatusText.Text = "Current theme: Dark";
    }
}