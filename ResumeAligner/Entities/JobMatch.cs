namespace ResumeAligner.Entities
{
    public class JobMatch
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public Resume Resume { get; set; } = null!; // Non-nullable property initialized to avoid CS8618

        public int JobDescriptionId { get; set; }
        public JobDescription JobDescription { get; set; } = null!; // Non-nullable property initialized to avoid CS8618

        public double MatchScore { get; set; }
        public string Notes { get; set; } = string.Empty; // Initialize to avoid null issues
        public DateTime EvaluatedAt { get; set; }
    }
}
