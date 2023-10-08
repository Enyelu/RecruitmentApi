using AutoMapper;
using MediatR;
using RecruitmentCore.Common;
using RecruitmentDomain.Entities;
using RecruitmentDomain.Models;
using RecruitmentInfrastructure.Data;
using RecruitmentInfrastructure.Data.Interface;

namespace RecruitmentCore.Queries
{
    public class FetchProgramDetail
    {
        public record Query(string id) : IRequest<GenericResponse<List<ProgramDetailsDto>>>;

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
                var items = await _dbService.GetManyAsync<ProgramDetail>($"SELECT * FROM c WHERE c.id = '{request.id}' AND c.IsDeleted = false");

                if (items.Count() == 0 || items.Count() > 1)
                {
                    return GenericResponse<List<ProgramDetailsDto>>.NotFound($"Program detail for {request.id} not found.");
                }

                var response = _mapper.Map<List<ProgramDetailsDto>>(items);

                return GenericResponse<List<ProgramDetailsDto>>.Success(response, "Successful");
            }
        }
    }
}
