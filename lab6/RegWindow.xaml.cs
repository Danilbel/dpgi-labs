using System.Windows;
using System.Windows.Input;
using lab6.Context;
using lab6.Entities;

namespace lab6;

public partial class RegWindow : Window
{
    public RegWindow()
    {
        InitializeComponent();
    }

    private void RegClick(object sender, RoutedEventArgs e)
    {
        var username = UsernameTextBox.Text;
        var password = PasswordBox.Password;
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            MessageBox.Show("Username and password must not be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        var user = new User
        {
            Username = username,
            Password = password
        };
        using var context = new MyDbContext();
        if (context.Users.Any(u => u.Username == username))
        {
            MessageBox.Show("User with this username already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        context.Users.Add(user);
        context.SaveChanges();
        MessageBox.Show("User successfully registered", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        var mainWindow = new MainWindow(user);
        mainWindow.Show();
        Close();
    }

    private void SignInClick(object sender, MouseButtonEventArgs e)
    {
        var signInWindow = new SignInWindow();
        signInWindow.Show();
        Close();
    }
}