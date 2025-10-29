using ResumeAligner.Models;

namespace ResumeAligner.Services
{
    public class WorkspaceService
    {
        public WorkspaceModel Current { get; } = new WorkspaceModel();

        public void SetResume(string text)
        {
            Current.ResumeText = text;
        }

        public void SetFileName(string fileName)
        {
            Current.ResumeFileName = fileName;
        }

        public void SetJobDescription(string text)
        {
            Current.JobDescriptionText = text;
        }

        public void SetMatchScore(double score)
        {
            Current.MatchScore = score;
        }
    }
}