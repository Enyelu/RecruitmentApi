using AutoMapper;
using RecruitmentDomain.Entities;
using RecruitmentDomain.Models;

namespace RecruitmentCore.Profiles
{
    public class CoreProfiles : Profile
    {
        public CoreProfiles() 
        {
            CreateMap<ProgramDetails, ProgramDetail>()
                   .ReverseMap();
        }
    }
}