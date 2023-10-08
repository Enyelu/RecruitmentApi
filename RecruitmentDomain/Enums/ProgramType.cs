using System.ComponentModel;

namespace RecruitmentDomain.Enums
{
    public enum ProgramType
    {
        [Description("Full-Time")]
        FullTime,

        [Description("Part-Time")]
        PartTime,

        [Description("Contract")]
        Contract,
    }
}