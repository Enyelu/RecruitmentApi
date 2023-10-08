using AutoMapper;
using MediatR;
using RecruitmentCore.Common;
using RecruitmentDomain.Entities;
using RecruitmentDomain.Models;
using RecruitmentInfrastructure.Data;

namespace RecruitmentCore.Queries
{
    public class FetchProgramDetails
    {
        public record Query(DateTime? startDate, DateTime? endDate) : IRequest<GenericResponse<List<ProgramDetailsDto>>>;

        public class ProgramDetailHandler : IRequestHandler<Query, GenericResponse<List<ProgramDetailsDto>>>
        {
            private readonly IMapper _mapper;
            private readonly CosmosDbService _dbService;
            public ProgramDetailHandler(IMapper mapper)
            {
                _mapper = mapper;
                string connectionString = "AccountEndpoint=https://localhost:8081;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==;";
                string databaseId = "RecruitmentApiDb";
                string containerId = "RecruitmentApiContainer";
                _dbService = new CosmosDbService(connectionString, databaseId, containerId);
            }

            public async Task<GenericResponse<List<ProgramDetailsDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var items = await _dbService.GetManyAsync<ProgramDetail>($"SELECT * FROM c WHERE c.CreatedOn >= '{request.startDate?.ToString("yyyy-MM-dd")}' AND c.CreatedOn <= '{request.endDate?.ToString("yyyy-MM-dd")}'");

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