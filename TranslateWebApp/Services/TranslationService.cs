using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace TranslateWebApp.Services
{
    public class TranslationService
    {
        private readonly HttpClient httpClient;
        private readonly string endpoint = "https://api.mymemory.translated.net/get";

        public TranslationService()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<string> TranslateAsync(string text, string fromLanguage, string toLanguage)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            // Önce doğrudan çeviri dene
            string? result = await TryTranslateAsync(text, fromLanguage, toLanguage);
            
            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }

            // Doğrudan çeviri başarısız olduysa ve İngilizce değilse, İngilizce üzerinden çevir
            if (fromLanguage != "en" && toLanguage != "en")
            {
                // Önce kaynak dil -> İngilizce
                string? englishText = await TryTranslateAsync(text, fromLanguage, "en");
                
                if (!string.IsNullOrEmpty(englishText))
                {
                    // Sonra İngilizce -> hedef dil
                    string? finalResult = await TryTranslateAsync(englishText, "en", toLanguage);
                    
                    if (!string.IsNullOrEmpty(finalResult))
                    {
                        return finalResult;
                    }
                }
            }

            throw new Exception("Çeviri yapılamadı. Lütfen tekrar deneyin.");
        }

        private async Task<string?> TryTranslateAsync(string text, string fromLanguage, string toLanguage)
        {
            try
            {
                // MyMemory Translation API - Ücretsiz, API anahtarı gerektirmez
                // Günlük limit: 10,000 kelime
                string encodedText = WebUtility.UrlEncode(text);
                string url = $"{endpoint}?q={encodedText}&langpair={fromLanguage}|{toLanguage}";

                HttpResponseMessage response = await httpClient.GetAsync(url).ConfigureAwait(false);
                string result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                // JSON yanıtını parse et
                var translationResult = JsonConvert.DeserializeObject<MyMemoryResponse>(result);
                
                if (translationResult != null && 
                    translationResult.ResponseStatus == 200 && 
                    !string.IsNullOrEmpty(translationResult.ResponseData?.TranslatedText))
                {
                    string translatedText = translationResult.ResponseData.TranslatedText.Trim();
                    
                    // Eğer çeviri sonucu kaynak metinle aynıysa, çeviri başarısız sayılır
                    if (translatedText.Equals(text, StringComparison.OrdinalIgnoreCase))
                    {
                        return null;
                    }
                    
                    return translatedText;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        private class MyMemoryResponse
        {
            [JsonProperty("responseStatus")]
            public int ResponseStatus { get; set; }

            [JsonProperty("responseData")]
            public MyMemoryData? ResponseData { get; set; }
        }

        private class MyMemoryData
        {
            [JsonProperty("translatedText")]
            public string TranslatedText { get; set; } = string.Empty;
        }
    }
}

