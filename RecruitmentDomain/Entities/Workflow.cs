using RecruitmentDomain.Enums;

namespace RecruitmentDomain.Entities
{
    public class Workflow : BaseEntity
    {
        public string ProgramDetailId { get; set; }
        public WorkflowItem Appiled { get; set; }
        public WorkflowItem Shortlisted { get; set; }
        public WorkflowVideoItem VideoInterview { get; set; }
        public List<WorkflowVideoItem> OtherVideoInterview { get; set; }
        public WorkflowItem FirstZoomInterview { get; set; }
        public WorkflowItem InPersonMeeting { get; set; }
        public WorkflowItem Placement { get; set; }
        public WorkflowItem Offered { get; set; }
        public List<WorkflowItem> Others { get;}
    }

    public class WorkflowItem
    {
        public bool Show { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Position { get; set; }
        
    }

    public class WorkflowVideoItem
    {
        public bool Show { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Position { get; set; }
        public string Question { get; set; }
        public int MaxDuration { get; set; }
        public MaxDurationType MaxDurationType { get; set; }
        public string AdditionalInformation { get; set; }
        public int SubmissionDurationInDays { get; set; }
    }
}