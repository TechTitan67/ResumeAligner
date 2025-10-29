namespace ResumeAligner.Models
{
    public class WorkspaceModel
    {
        public string ResumeText { get; set; } = string.Empty;
        public string ResumeFileName { get; set; } = string.Empty;
        public string JobDescriptionText { get; set; } = string.Empty;
        public double MatchScore { get; set; } = 0.0;
    }
}