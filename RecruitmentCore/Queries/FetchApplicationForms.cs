using AutoMapper;
using MediatR;
using RecruitmentCore.Common;
using RecruitmentDomain.Entities;
using RecruitmentDomain.Models;
using RecruitmentInfrastructure.Data.Interface;

namespace RecruitmentCore.Queries
{
    public class FetchApplicationForms
    {
        public record Query(DateTime? startDate, DateTime? endDate) : IRequest<GenericResponse<List<ApplicationFormDto>>>;

        public class FetchApplicationFormsHandler : IRequestHandler<Query, GenericResponse<List<ApplicationFormDto>>>
        {
            private readonly IMapper _mapper;
            private readonly ICosmoDbService _dbService;
            public FetchApplicationFormsHandler(IMapper mapper, ICosmoDbService dbService)
            {
                _mapper = mapper;
                _dbService = dbService;
            }

            public async Task<GenericResponse<List<ApplicationFormDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var items = await _dbService.GetManyAsync<ApplicationForm>($"SELECT * FROM c WHERE c.CreatedOn >= '{request.startDate?.ToString("yyyy-MM-dd")}' AND c.CreatedOn <= '{request.endDate?.ToString("yyyy-MM-dd")}' AND c.IsDeleted = false");

                if (items.Count() == 0)
                {
                    return GenericResponse<List<ApplicationFormDto>>.NotFound($"Application details list is empty.");
                }

                var response = _mapper.Map<List<ApplicationFormDto>>(items);

                return GenericResponse<List<ApplicationFormDto>>.Success(response, "Successful");
            }
        }
    }
}