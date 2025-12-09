using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace TranslateApp
{
    public partial class MainWindow : Window
    {
        private TranslationService? translationService;
        private Dictionary<string, string>? languages;

        public MainWindow()
        {
            InitializeComponent();
            InitializeLanguages();
            InitializeTranslationService();
        }

        private void InitializeLanguages()
        {
            languages = new Dictionary<string, string>
            {
                { "Türkçe", "tr" },
                { "İngilizce", "en" },
                { "Almanca", "de" },
                { "Fransızca", "fr" },
                { "İtalyanca", "it" },
                { "İspanyolca", "es" }
            };

            SourceLanguageComboBox.ItemsSource = new List<string>(languages.Keys);
            TargetLanguageComboBox.ItemsSource = new List<string>(languages.Keys);

            // Varsayılan olarak Türkçe -> İngilizce
            SourceLanguageComboBox.SelectedItem = "Türkçe";
            TargetLanguageComboBox.SelectedItem = "İngilizce";
        }

        private void InitializeTranslationService()
        {
            // API anahtarı gerektirmez - MyMemory Translation API kullanılıyor
            translationService = new TranslationService();
        }

        private void ShowSettingsDialog()
        {
            // Artık API anahtarı gerektirmediği için ayarlar penceresi gerekli değil
            MessageBox.Show(
                "Bu uygulama MyMemory Translation API kullanmaktadır.\n\n" +
                "✅ API anahtarı gerektirmez\n" +
                "✅ Tamamen ücretsizdir\n" +
                "✅ Günlük limit: 10,000 kelime\n\n" +
                "Uygulama kullanıma hazırdır!",
                "Bilgi",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private async void TranslateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SourceTextBox.Text))
            {
                MessageBox.Show("Lütfen çevrilecek metni girin.", "Uyarı", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SourceLanguageComboBox.SelectedItem == null || 
                TargetLanguageComboBox.SelectedItem == null)
            {
                MessageBox.Show("Lütfen kaynak ve hedef dilleri seçin.", "Uyarı", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string? selectedSource = SourceLanguageComboBox.SelectedItem?.ToString();
            string? selectedTarget = TargetLanguageComboBox.SelectedItem?.ToString();
            
            if (selectedSource == null || selectedTarget == null || languages == null)
            {
                MessageBox.Show("Lütfen kaynak ve hedef dilleri seçin.", "Uyarı", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            string sourceLang = languages[selectedSource];
            string targetLang = languages[selectedTarget];

            if (sourceLang == targetLang)
            {
                MessageBox.Show("Kaynak ve hedef dil aynı olamaz.", "Uyarı", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TranslateButton.IsEnabled = false;
            StatusTextBlock.Text = "Çeviriliyor...";

            try
            {
                if (translationService == null)
                {
                    MessageBox.Show("Çeviri servisi başlatılamadı. Lütfen API anahtarınızı kontrol edin.", "Hata", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                string translatedText = await translationService.TranslateAsync(
                    SourceTextBox.Text, sourceLang, targetLang);

                TargetTextBox.Text = translatedText;
                StatusTextBlock.Text = "Çeviri tamamlandı.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Çeviri hatası: {ex.Message}", "Hata", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                StatusTextBlock.Text = "Çeviri başarısız.";
            }
            finally
            {
                TranslateButton.IsEnabled = true;
            }
        }

        private void SourceLanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Otomatik olarak hedef dili ayarla
            if (SourceLanguageComboBox.SelectedItem?.ToString() == "Türkçe")
            {
                // Türkçe seçildiyse, hedef dil İngilizce olsun
                if (TargetLanguageComboBox.SelectedItem?.ToString() == "Türkçe")
                {
                    TargetLanguageComboBox.SelectedItem = "İngilizce";
                }
            }
            else
            {
                // Diğer diller seçildiyse, hedef dil Türkçe olsun
                if (TargetLanguageComboBox.SelectedItem?.ToString() != "Türkçe")
                {
                    TargetLanguageComboBox.SelectedItem = "Türkçe";
                }
            }
        }

        private void TargetLanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Kaynak ve hedef dil aynıysa uyar
            if (SourceLanguageComboBox.SelectedItem != null && 
                TargetLanguageComboBox.SelectedItem != null &&
                SourceLanguageComboBox.SelectedItem == TargetLanguageComboBox.SelectedItem)
            {
                StatusTextBlock.Text = "Kaynak ve hedef dil aynı olamaz!";
            }
            else
            {
                StatusTextBlock.Text = "Hazır";
            }
        }

        private void SwapButton_Click(object sender, RoutedEventArgs e)
        {
            // Kaynak ve hedef dilleri değiştir
            if (SourceLanguageComboBox.SelectedItem != null && 
                TargetLanguageComboBox.SelectedItem != null)
            {
                string? sourceLang = SourceLanguageComboBox.SelectedItem.ToString();
                string? targetLang = TargetLanguageComboBox.SelectedItem.ToString();
                
                if (sourceLang == null || targetLang == null)
                    return;

                // Dilleri değiştir
                SourceLanguageComboBox.SelectedItem = targetLang;
                TargetLanguageComboBox.SelectedItem = sourceLang;

                // Metinleri değiştir (çeviriyi tersine çevir)
                string tempText = SourceTextBox.Text;
                SourceTextBox.Text = TargetTextBox.Text;
                TargetTextBox.Text = tempText;

                StatusTextBlock.Text = "Diller değiştirildi.";
            }
        }

        private void SettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ShowSettingsDialog();
        }
    }
}

