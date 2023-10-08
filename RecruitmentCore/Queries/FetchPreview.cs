using AutoMapper;
using MediatR;
using RecruitmentCore.Common;
using RecruitmentDomain.Entities;
using RecruitmentDomain.Models;
using RecruitmentInfrastructure.Data.Interface;

namespace RecruitmentCore.Queries
{
    public class FetchPreview
    {
        public record Query(string ProgramDetailId) : IRequest<GenericResponse<PreviewDto>>;

        public class ProgramDetailHandler : IRequestHandler<Query, GenericResponse<PreviewDto>>
        {
            private readonly IMapper _mapper;
            private readonly ICosmoDbService _dbService;
            public ProgramDetailHandler(IMapper mapper, ICosmoDbService dbService)
            {
                _mapper = mapper;
                _dbService = dbService;
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
