using MediatR;
using RecruitmentCore.Common;
using RecruitmentDomain.Enums;
using RecruitmentDomain.Models;

namespace RecruitmentCore.Queries
{
    public class FetchMetaData
    {
        public record Query() : IRequest<GenericResponse<MetaDataDto>>;

        public class FetchMetaDataHandler : IRequestHandler<Query, GenericResponse<MetaDataDto>>
        {
            public async Task<GenericResponse<MetaDataDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var metaData = new MetaDataDto
                {
                    Skills = EnumExtensions.GetEnumList<Skills>().Data,
                    ProgramType = EnumExtensions.GetEnumList<ProgramType>().Data,
                    ProgramDuration = EnumExtensions.GetEnumList<ProgramDuration>().Data,
                    MaxDurationType = EnumExtensions.GetEnumList<MaxDurationType>().Data, 
                    MinQualification = EnumExtensions.GetEnumList<MinQualification>().Data
                };
                
                return GenericResponse<MetaDataDto>.Success(metaData, "Successful");
            }
        }

    }
}