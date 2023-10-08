using AutoMapper;
using MediatR;
using RecruitmentCore.Common;
using RecruitmentDomain.Entities;
using RecruitmentDomain.Models;
using RecruitmentInfrastructure.Data.Interface;

namespace RecruitmentCore.Queries
{
    public class FetchWorkflows
    {
        public record Query(DateTime? startDate, DateTime? endDate) : IRequest<GenericResponse<List<WorkflowDto>>>;

        public class FetchWorkflowHandler : IRequestHandler<Query, GenericResponse<List<WorkflowDto>>>
        {
            private readonly IMapper _mapper;
            private readonly ICosmoDbService _dbService;
            public FetchWorkflowHandler(IMapper mapper, ICosmoDbService dbService)
            {
                _mapper = mapper;
                _dbService = dbService;
            }

            public async Task<GenericResponse<List<WorkflowDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var items = await _dbService.GetManyAsync<Workflow>($"SELECT * FROM c WHERE c.CreatedOn >= '{request.startDate?.ToString("yyyy-MM-dd")}' AND c.CreatedOn <= '{request.endDate?.ToString("yyyy-MM-dd")}' AND c.IsDeleted = false");

                if (items.Count() == 0)
                {
                    return GenericResponse<List<WorkflowDto>>.NotFound($"Workflow details list is empty.");
                }

                var response = _mapper.Map<List<WorkflowDto>>(items);

                return GenericResponse<List<WorkflowDto>>.Success(response, "Successful");
            }
        }
    }
}
