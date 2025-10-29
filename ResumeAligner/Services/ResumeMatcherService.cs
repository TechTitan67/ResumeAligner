using ResumeAligner.Entities;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace ResumeAligner.Services
{
    public partial class ResumeMatcherService
    {
        public double CalculateMatchScore(string resumeText, string jobDescriptionText)
        {
            var resumeWords = ExtractKeywords(resumeText);
            var jobWords = ExtractKeywords(jobDescriptionText);

            if (jobWords.Count == 0) return 0;

            int matchCount = jobWords.Count(word => resumeWords.Contains(word));
            return Math.Round((double)matchCount / jobWords.Count * 100, 2); // percentage
        }

        private static HashSet<string> ExtractKeywords(string text)
        {
            var words = GetKeywordRegex().Matches(text.ToLower())
                             .Select(m => m.Value)
                             .Where(w => w.Length > 2) // filter out short/common words
                             .ToHashSet();

            return words;
        }

        [GeneratedRegex(@"\b\w+\b", RegexOptions.Compiled)]
        private static partial Regex GetKeywordRegex();
    }
}
