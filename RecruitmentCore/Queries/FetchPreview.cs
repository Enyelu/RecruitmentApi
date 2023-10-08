using AutoMapper;
using MediatR;
using RecruitmentCore.Common;
using RecruitmentDomain.Entities;
using RecruitmentDomain.Models;
using RecruitmentInfrastructure.Data;

namespace RecruitmentCore.Queries
{
    public class FetchPreview
    {
        public record Query(string ProgramDetailId) : IRequest<GenericResponse<PreviewDto>>;

        public class ProgramDetailHandler : IRequestHandler<Query, GenericResponse<PreviewDto>>
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

            public async Task<GenericResponse<PreviewDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var programDetailDto = new ProgramDetailsDto();
                var applicationFormDto = new ApplicationFormDto();
                var workflowDto = new WorkflowDto();

                var programDetail = await _dbService.GetManyAsync<ProgramDetail>($"SELECT * FROM c WHERE c.ProgramDetailId = '{request.ProgramDetailId}' AND c.IsDeleted = false");

                if (programDetail.Count() > 0)
                {
                    programDetailDto = _mapper.Map<ProgramDetailsDto>(programDetail?.FirstOrDefault());
                }

                var applicationForm = await _dbService.GetManyAsync<ApplicationForm>($"SELECT * FROM c WHERE c.ProgramDetailId = '{request.ProgramDetailId}' AND c.IsDeleted = false");
                if (applicationForm.Count() > 0)
                {
                    applicationFormDto = _mapper.Map<ApplicationFormDto>(applicationForm?.FirstOrDefault());
                }

                var Workflow = await _dbService.GetManyAsync<Workflow>($"SELECT * FROM c WHERE c.ProgramDetailId = '{request.ProgramDetailId}' AND c.IsDeleted = false");
                if (Workflow.Count() > 0)
                {
                    workflowDto = _mapper.Map<WorkflowDto>(Workflow?.FirstOrDefault());
                }

                var response = new PreviewDto
                {
                    ProgramDetail = programDetailDto,
                    ApplicationForm = applicationFormDto,
                    Workflow = workflowDto
                };

                return GenericResponse<PreviewDto>.Success(response, "Successful");
            }
        }
    }
}
