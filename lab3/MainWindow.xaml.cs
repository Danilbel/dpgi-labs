using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;

namespace lab3;

public partial class MainWindow : Window
{
    private static readonly RoutedCommand OpenCommand = new RoutedCommand();
    private static readonly RoutedCommand ClearCommand = new RoutedCommand();
    private static readonly RoutedCommand SaveCommand = new RoutedCommand();

    private static readonly RoutedCommand EncryptCommand = new RoutedCommand();
    private static readonly RoutedCommand DecryptCommand = new RoutedCommand();

    public MainWindow()
    {
        InitializeComponent();

        var openCommandBinding = new CommandBinding(OpenCommand, OpenCommandExecuted, OpenCommandCanExecute);
        CommandBindings.Add(openCommandBinding);
        OpenButton.Command = OpenCommand;

        var clearCommandBinding = new CommandBinding(ClearCommand, ClearCommandExecuted, ClearCommandCanExecute);
        CommandBindings.Add(clearCommandBinding);
        ClearButton.Command = ClearCommand;

        var saveCommandBinding = new CommandBinding(SaveCommand, SaveCommandExecuted, SaveCommandCanExecute);
        CommandBindings.Add(saveCommandBinding);
        SaveButton.Command = SaveCommand;

        var encryptCommandBinding =
            new CommandBinding(EncryptCommand, EncryptCommandExecuted, CryptCommandCanExecute);
        CommandBindings.Add(encryptCommandBinding);
        EncryptButton.Command = EncryptCommand;

        var decryptCommandBinding =
            new CommandBinding(DecryptCommand, DecryptCommandExecuted, CryptCommandCanExecute);
        CommandBindings.Add(decryptCommandBinding);
        DecryptButton.Command = DecryptCommand;

        InputKeyTextBox.PreviewTextInput += InputKeyTextBoxPreviewTextInput;
    }

    private void EncryptCommandExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        OutputTextBox.Text = Crypt(InputTextBox.Text, int.Parse(InputKeyTextBox.Text));
    }

    private void DecryptCommandExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        OutputTextBox.Text = Crypt(InputTextBox.Text, -int.Parse(InputKeyTextBox.Text));
    }

    private void CryptCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = InputTextBox.Text.Trim().Length > 0 && InputKeyTextBox.Text.Trim().Length > 0;
    }

    private static string Crypt(string input, int key)
    {
        var output = new StringBuilder();
        foreach (var symbol in input)
        {
            output.Append((char)(symbol + key));
        }

        return output.ToString();
    }

    private void InputKeyTextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        var inputKeyTextBox = (TextBox)sender;
        // Ввід лише цифр та першого символу мінус
        e.Handled = !int.TryParse(e.Text, out _) && !(e.Text == "-" && inputKeyTextBox.Text.Length == 0);
    }

    private void SaveCommandExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        var saveFileDialog = new SaveFileDialog
        {
            Filter = "Text files (*.txt)|*.txt"
        };
        if (saveFileDialog.ShowDialog() == true)
        {
            System.IO.File.WriteAllText(saveFileDialog.FileName, OutputTextBox.Text);
        }
    }

    private void SaveCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = OutputTextBox.Text.Trim().Length > 0;
    }

    private void ClearCommandExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        InputTextBox.Text = string.Empty;
        OutputTextBox.Text = string.Empty;
    }

    private void ClearCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = InputTextBox.Text.Trim().Length > 0;
    }

    private void OpenCommandExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Text files (*.txt)|*.txt"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            InputTextBox.Text = System.IO.File.ReadAllText(openFileDialog.FileName);
        }
    }

    private static void OpenCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }
}