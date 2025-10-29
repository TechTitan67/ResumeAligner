namespace ResumeAligner.Entities
{
    public class Resume
    {
        public int Id { get; set; }
        public string OriginalText { get; set; } = string.Empty; // Default value added
        public string OptimizedText { get; set; } = string.Empty; // Default value added
        public DateTime CreatedAt { get; set; }
        public ICollection<JobMatch> Matches { get; set; } = new List<JobMatch>(); // Default value added
    }
}
