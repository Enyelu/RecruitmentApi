using RecruitmentDomain.Enums;

namespace RecruitmentDomain.Entities
{
    public class Workflow : BaseEntity
    {
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