using AutoMapper;
using MediatR;
using RecruitmentCore.Common;
using RecruitmentDomain.Entities;
using RecruitmentDomain.Models;
using RecruitmentInfrastructure.Data;
using RecruitmentInfrastructure.Data.Interface;

namespace RecruitmentCore.Queries
{
    public class FetchProgramDetails
    {
        public record Query(DateTime? startDate, DateTime? endDate) : IRequest<GenericResponse<List<ProgramDetailsDto>>>;

        public class ProgramDetailHandler : IRequestHandler<Query, GenericResponse<List<ProgramDetailsDto>>>
        {
            private readonly IMapper _mapper;
            private readonly ICosmoDbService _dbService;
            public ProgramDetailHandler(IMapper mapper, ICosmoDbService dbService)
            {
                _mapper = mapper;
                _dbService = dbService;
            }

            public async Task<GenericResponse<List<ProgramDetailsDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var items = await _dbService.GetManyAsync<ProgramDetail>($"SELECT * FROM c WHERE c.CreatedOn >= '{request.startDate?.ToString("yyyy-MM-dd")}' AND c.CreatedOn <= '{request.endDate?.ToString("yyyy-MM-dd")}' AND c.IsDeleted = false");

                if (items.Count() == 0)
                {
                    return GenericResponse<List<ProgramDetailsDto>>.NotFound($"Program details list is empty.");
                }

                var response = _mapper.Map<List<ProgramDetailsDto>>(items);
                 
                return GenericResponse<List<ProgramDetailsDto>>.Success(response, "Successful");
            }
        }
    }
}