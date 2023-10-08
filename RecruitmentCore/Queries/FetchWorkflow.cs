using AutoMapper;
using MediatR;
using RecruitmentCore.Common;
using RecruitmentDomain.Entities;
using RecruitmentDomain.Models;
using RecruitmentInfrastructure.Data;
using RecruitmentInfrastructure.Data.Interface;

namespace RecruitmentCore.Queries
{
    public class FetchWorkflow
    {
        public record Query(string id) : IRequest<GenericResponse<List<WorkflowDto>>>;

        public class ProgramDetailHandler : IRequestHandler<Query, GenericResponse<List<WorkflowDto>>>
        {
            private readonly IMapper _mapper;
            private readonly ICosmoDbService _dbService;
            public ProgramDetailHandler(IMapper mapper, ICosmoDbService dbService)
            {
                _mapper = mapper;
                _dbService = new CosmosDbService();
            }

            public async Task<GenericResponse<List<WorkflowDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var items = await _dbService.GetManyAsync<Workflow>($"SELECT * FROM c WHERE c.id = '{request.id}' AND c.IsDeleted = false");

                if (items.Count() == 0)
                {
                    return GenericResponse<List<WorkflowDto>>.NotFound($"Workflow not found.");
                }

                var response = _mapper.Map<List<WorkflowDto>>(items);

                return GenericResponse<List<WorkflowDto>>.Success(response, "Successful");
            }
        }
    }
}