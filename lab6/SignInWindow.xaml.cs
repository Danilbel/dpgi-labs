using System.Windows;
using System.Windows.Input;
using lab6.Context;

namespace lab6;

public partial class SignInWindow : Window
{
    public SignInWindow()
    {
        InitializeComponent();
    }

    private void SignInClick(object sender, RoutedEventArgs e)
    {
        var username = UsernameTextBox.Text;
        var password = PasswordBox.Password;
        using var context = new MyDbContext();
        var user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user is null)
        {
            MessageBox.Show("Invalid username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        var mainWindow = new MainWindow(user);
        mainWindow.Show();
        Close();
    }

    private void RegistrationMouseDown(object sender, MouseButtonEventArgs e)
    {
        var regWindow = new RegWindow();
        regWindow.Show();
        Close();
    }
}