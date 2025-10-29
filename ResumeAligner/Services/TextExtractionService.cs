using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Ensure you have installed the DocumentFormat.OpenXml NuGet package
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using UglyToad.PdfPig;
using WordprocessingDocument = DocumentFormat.OpenXml.Packaging.WordprocessingDocument;

namespace ResumeAligner.Services
{
    public class TextExtractionService
    {
        public async Task<string> ExtractFromDocxAsync(Stream input)
        {
            using var mem = new MemoryStream();
            await input.CopyToAsync(mem).ConfigureAwait(false);
            mem.Position = 0;

            using var word = WordprocessingDocument.Open(mem, false);
            var body = word.MainDocumentPart?.Document?.Body;
            if (body is null) return string.Empty;

            var paragraphs = body.Descendants<Paragraph>().Select(p => p.InnerText);
            return string.Join("\n", paragraphs);
        }

        public async Task<string> ExtractFromPdfAsync(Stream input)
        {
            using var mem = new MemoryStream();
            await input.CopyToAsync(mem).ConfigureAwait(false);
            mem.Position = 0;

            var sb = new StringBuilder();
            using var pdf = PdfDocument.Open(mem);
            foreach (var page in pdf.GetPages())
            {
                sb.AppendLine(page.Text);
            }

            return sb.ToString();
        }
    }
}
