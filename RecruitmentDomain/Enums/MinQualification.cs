using System.ComponentModel;

namespace RecruitmentDomain.Enums
{
    public enum MinQualification
    {
        [Description("Primary")]
        Primary = 0,

        [Description("High School")]
        HighSchool,

        [Description("Certification")]
        Certification,

        [Description("Bachelor")]
        Bachelor,

        [Description("Master")]
        Master,

        [Description("Phd")]
        Phd
    }
}