using RecruitmentDomain.Entities;
using RecruitmentDomain.Enums;

namespace RecruitmentDomain.Models
{
    public class ProgramDetails
    {
        public string? id { get; set; }
        public bool IsUpdate { get; set; }
        public string Title { get; set; }
        public List<Skills> Skills { get; set; }
        public string Benefits { get; set; }
        public string Criteria { get; set; }
        public string Location { get; set; }
        public bool IsRemote { get; set; }
        public int MaxApplicant { get; set; }
        public ProgramType Type { get; set; }
        public DateTime StartDate { get; set; }
        public ObjectData Summary { get; set; }
        public ObjectData Description { get; set; }
        public DateTime ApplicationOpen { get; set; }
        public DateTime ApplicationClose { get; set; }
        public ProgramDuration Duration { get; set; }
        public MinQualification MinQualification { get; set; }
    }
}