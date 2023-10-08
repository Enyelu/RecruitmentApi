using RecruitmentDomain.Entities;

namespace RecruitmentDomain.Models
{
    public class WorkflowDto
    {
        public string ProgramDetailId { get; set; }
        public string? id { get; set; }
        public bool IsUpdate { get; set; }
        public WorkflowItem Applied { get; set; }
        public WorkflowItem Shortlisted { get; set; }
        public WorkflowVideoItem VideoInterview { get; set; }
        public List<WorkflowVideoItem>? OtherVideoInterview { get; set; }
        public WorkflowItem FirstZoomInterview { get; set; }
        public WorkflowItem InPersonMeeting { get; set; }
        public WorkflowItem Placement { get; set; }
        public WorkflowItem Offered { get; set; }
        public List<WorkflowItem>? Others { get; }
    }
}
