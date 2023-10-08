using AutoMapper;
using MediatR;
using RecruitmentCore.Common;
using RecruitmentDomain.Entities;
using RecruitmentDomain.Models;
using RecruitmentInfrastructure.Data.Interface;

namespace RecruitmentCore.Queries
{
    public class FetchPreviews
    {
        public record Query(DateTime? startDate, DateTime? endDate) : IRequest<GenericResponse<PreviewListDto>>;

        public class FetchApplicationFormsHandler : IRequestHandler<Query, GenericResponse<PreviewListDto>>
        {
            private readonly IMapper _mapper;
            private readonly ICosmoDbService _dbService;
            public FetchApplicationFormsHandler(IMapper mapper, ICosmoDbService cosmoDbService)
            {
                _dbService = cosmoDbService;
                _mapper = mapper;
            }

            public async Task<GenericResponse<PreviewListDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var programDetailDto = new List<ProgramDetailsDto>();
                var applicationFormDto = new List<ApplicationFormDto>();
                var workflowDto = new List<WorkflowDto>();

                var programDetail = await _dbService.GetManyAsync<ProgramDetail>($"SELECT * FROM c WHERE c.CreatedOn >= '{request.startDate?.ToString("yyyy-MM-dd")}' AND c.CreatedOn <= '{request.endDate?.ToString("yyyy-MM-dd")}' AND c.IsDeleted = false");

                if (programDetail.Count() > 0)
                {
                    programDetailDto = _mapper.Map<List<ProgramDetailsDto>>(programDetail);
                }

                var applicationForm = await _dbService.GetManyAsync<ApplicationForm>($"SELECT * FROM c WHERE c.CreatedOn >= '{request.startDate?.ToString("yyyy-MM-dd")}' AND c.CreatedOn <= '{request.endDate?.ToString("yyyy-MM-dd")}' AND c.IsDeleted = false");
                if (applicationForm.Count() > 0)
                {
                    applicationFormDto = _mapper.Map<List<ApplicationFormDto>>(applicationForm);
                }

                var Workflow = await _dbService.GetManyAsync<Workflow>($"SELECT * FROM c WHERE c.CreatedOn >= '{request.startDate?.ToString("yyyy-MM-dd")}' AND c.CreatedOn <= '{request.endDate?.ToString("yyyy-MM-dd")}' AND c.IsDeleted = false");
                if (Workflow.Count() > 0)
                {
                    workflowDto = _mapper.Map<List<WorkflowDto>>(Workflow);
                }

                var response = new PreviewListDto();
                var PreviewDto = new List<PreviewDto>();

                foreach (var progDetail in programDetailDto)
                {
                    var appForm = applicationFormDto.Where(x => x.ProgramDetailId == progDetail.id)?.FirstOrDefault();
                    var workFlw = workflowDto.Where(x => x.ProgramDetailId == progDetail.id)?.FirstOrDefault();
                    
                    var responseDto = new PreviewDto
                    {
                        ProgramDetail = progDetail,
                        ApplicationForm = appForm,
                        Workflow = workFlw
                    };
                    PreviewDto.Add(responseDto);
                }

                response.PreviewList = PreviewDto;
                return GenericResponse<PreviewListDto>.Success(response, "Successful");
            }
        }
    }
}