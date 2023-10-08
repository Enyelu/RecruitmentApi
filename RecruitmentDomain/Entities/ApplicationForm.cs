using RecruitmentDomain.Enums;

namespace RecruitmentDomain.Entities
{
    public class ApplicationForm : BaseEntity
    {
        public string ProgramDetailId { get; set; }
        public string Email { get; set; }
        public bool IsDraft { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string CoverImageUrl { get; set; }
        public Education Education { get; set; }
        public Experience Experience { get; set; }
        public InternalHideData DOB { get; set; }
        public InternalHideData Gender { get; set; }
        public InternalHideData IDNumber { get; set; }
        public InternalHideData PhoneNumber { get; set; }
        public InternalHideData Nationality { get; set; }
        public InternalHideData CurrentResidence {get; set;}
        public MandatoryHideData Resume { get; set; }
        public List<AdditionalObjectData> Additionals { get; set; }
    }

    public class Education
    {
        public string school { get; set; }
        public string Course { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MinQualification Degree { get; set; }
        public bool Hide { get; set; }
        public bool Mandatory { get; set; }
    }

    public class Experience
    {
        public string Company { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class InternalHideData
    {
        public string Value { get; set; }
        public bool Hide { get; set; }
        public bool Internal { get; set; }
    }

    public class MandatoryHideData
    {
        public string Value { get; set; }
        public bool Hide { get; set; }
        public bool Mandatory { get; set; }
    }

    public class AdditionalObjectData
    {
        public string Type { get; set; }
        public bool Question { get; set; }
        public List<string> Choices { get; set; }
        public bool EnableOtherOption { get; set; }
    }
}