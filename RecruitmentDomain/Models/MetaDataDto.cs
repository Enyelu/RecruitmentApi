namespace RecruitmentDomain.Models
{
    public class MetaDataDto
    {
        public List<MetaDataResponse> Skills { get; set; }
        public List<MetaDataResponse> ProgramType { get; set; }
        public List<MetaDataResponse> MaxDurationType { get; set; }
        public List<MetaDataResponse> MinQualification { get; set; }
        public List<MetaDataResponse> ProgramDuration { get; set; }
    }
    
    public class MetaDataResponse
    {
        public int Code { get; set; }
        public string Description { get; set; }
    }
}