namespace ResumeAligner.Entities
{
    public class JobDescription
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty; // Initialize with a default value
        public string DescriptionText { get; set; } = string.Empty; // Initialize with a default value
        public DateTime CreatedAt { get; set; }
        public ICollection<JobMatch> Matches { get; set; } = new List<JobMatch>(); // Initialize with an empty collection
    }
}
