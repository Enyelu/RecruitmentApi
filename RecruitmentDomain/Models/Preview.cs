using RecruitmentDomain.Entities;

namespace RecruitmentDomain.Models
{
    public class Preview
    {
        public Workflow Workflow { get; set; }
        public ProgramDetail ProgramDetail { get; set; }
        public ApplicationForm ApplicationForm { get; set; }
    }
}