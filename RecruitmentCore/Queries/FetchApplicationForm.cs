using AutoMapper;
using MediatR;
using RecruitmentCore.Common;
using RecruitmentDomain.Entities;
using RecruitmentDomain.Models;
using RecruitmentInfrastructure.Data.Interface;

namespace RecruitmentCore.Queries
{
    public class FetchApplicationForm
    {
        public record Query(string id) : IRequest<GenericResponse<List<ApplicationFormDto>>>;

        public class ProgramDetailHandler : IRequestHandler<Query, GenericResponse<List<ApplicationFormDto>>>
        {
            private readonly IMapper _mapper;
            private readonly ICosmoDbService _dbService;
            public ProgramDetailHandler(IMapper mapper, ICosmoDbService dbService)
            {
                _mapper = mapper;
                _dbService = dbService;
            }

            public async Task<GenericResponse<List<ApplicationFormDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var items = await _dbService.GetManyAsync<ApplicationForm>($"SELECT * FROM c WHERE c.id = '{request.id}' AND c.IsDeleted = false");

                if (items.Count() == 0)
                {
                    return GenericResponse<List<ApplicationFormDto>>.NotFound($"Application detail not found.");
                }

                var response = _mapper.Map<List<ApplicationFormDto>>(items);

                return GenericResponse<List<ApplicationFormDto>>.Success(response, "Successful");
            }
        }
    }
}