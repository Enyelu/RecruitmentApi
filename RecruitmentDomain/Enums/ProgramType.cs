using System.ComponentModel;

namespace RecruitmentDomain.Enums
{
    public enum ProgramType
    {
        [Description("FullTime")]
        FullTime,

        [Description("PartTime")]
        PartTime,

        [Description("Contract")]
        Contract,
    }
}