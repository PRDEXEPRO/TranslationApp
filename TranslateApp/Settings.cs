using System;
using System.IO;
using System.Text;

namespace TranslateApp
{
    public static class Settings
    {
        private static readonly string SettingsFilePath = 
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
                        "TranslateApp", "settings.txt");

        public static string GetApiKey()
        {
            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    return File.ReadAllText(SettingsFilePath, Encoding.UTF8).Trim();
                }
            }
            catch
            {
                // Hata durumunda boş döndür
            }
            return string.Empty;
        }

        public static void SaveApiKey(string apiKey)
        {
            try
            {
                string? directory = Path.GetDirectoryName(SettingsFilePath);
                if (directory != null && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                File.WriteAllText(SettingsFilePath, apiKey, Encoding.UTF8);
            }
            catch
            {
                // Hata durumunda sessizce geç
            }
        }
    }
}

