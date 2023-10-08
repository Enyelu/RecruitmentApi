using RecruitmentDomain.Entities;

namespace RecruitmentDomain.Models
{
    public class PreviewDto
    {
        public WorkflowDto Workflow { get; set; }
        public ProgramDetailsDto ProgramDetail { get; set; }
        public ApplicationFormDto ApplicationForm { get; set; }
    }

    public class PreviewListDto
    {
        public List<PreviewDto> PreviewList { get; set; }
    }
}