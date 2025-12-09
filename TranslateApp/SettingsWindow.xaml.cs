using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace TranslateApp
{
    public partial class SettingsWindow : Window
    {
        public string ApiKey { get; private set; } = string.Empty;

        public SettingsWindow(string currentApiKey = "")
        {
            InitializeComponent();
            ApiKeyPasswordBox.Password = currentApiKey;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ApiKey = ApiKeyPasswordBox.Password;
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            });
            e.Handled = true;
        }
    }
}

