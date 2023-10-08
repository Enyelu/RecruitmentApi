using RecruitmentDomain.Entities;

namespace RecruitmentDomain.Models
{
    public class ApplicationFormDto
    {
        public string ProgramDetailId { get; set; }
        public string? id { get; set; }
        public bool IsUpdate { get; set; }
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
        public InternalHideData CurrentResidence { get; set; }
        public MandatoryHideData Resume { get; set; }
        public List<AdditionalObjectData> Additionals { get; set; }
    }
}