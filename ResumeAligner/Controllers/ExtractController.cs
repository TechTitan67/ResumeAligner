using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ResumeAligner.Services;

namespace ResumeAligner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExtractController : ControllerBase
    {
        private readonly TextExtractionService _extractor;

        public ExtractController(TextExtractionService extractor)
        {
            _extractor = extractor;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] IFormFile file)
        {
            if (file is null) return BadRequest("File required");

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            await using var stream = file.OpenReadStream();

            string preview = ext switch
            {
                ".pdf" => await _extractor.ExtractFromPdfAsync(stream),
                ".docx" => await _extractor.ExtractFromDocxAsync(stream),
                _ => string.Empty
            };

            return Ok(new { preview, fileName = file.FileName });
        }
    }
}