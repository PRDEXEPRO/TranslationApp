using Microsoft.AspNetCore.Mvc;
using TranslateWebApp.Services;

namespace TranslateWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranslationController : ControllerBase
    {

        [HttpPost("translate")]
        public async Task<IActionResult> Translate([FromBody] TranslateRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Text))
                {
                    return BadRequest(new { error = "Çevrilecek metin boş olamaz." });
                }

                if (string.IsNullOrWhiteSpace(request.FromLanguage) || string.IsNullOrWhiteSpace(request.ToLanguage))
                {
                    return BadRequest(new { error = "Kaynak ve hedef dil seçilmelidir." });
                }

                // API anahtarı gerektirmez - MyMemory Translation API kullanılıyor
                var translationService = new TranslationService();
                string translatedText = await translationService.TranslateAsync(
                    request.Text, 
                    request.FromLanguage, 
                    request.ToLanguage);

                return Ok(new { translatedText = translatedText });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }

    public class TranslateRequest
    {
        public string Text { get; set; } = string.Empty;
        public string FromLanguage { get; set; } = string.Empty;
        public string ToLanguage { get; set; } = string.Empty;
    }
}

