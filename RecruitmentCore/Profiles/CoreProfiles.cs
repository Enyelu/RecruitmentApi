using AutoMapper;
using RecruitmentDomain.Entities;
using RecruitmentDomain.Models;

namespace RecruitmentCore.Profiles
{
    public class CoreProfiles : Profile
    {
        public CoreProfiles() 
        {
            CreateMap<ProgramDetailsDto, ProgramDetail>()
                   .ReverseMap();

            CreateMap<ApplicationFormDto, ApplicationForm>()
                   .ReverseMap();
        }
    }
}