using MediatR;
using RecruitmentCore.Common;
using RecruitmentDomain.Entities;
using RecruitmentInfrastructure.Data;

namespace RecruitmentCore.Handlers
{
    public class UpsertProgramDetail
    {
        public record Command(ProgramDetail detail) : IRequest<GenericResponse<string>>;

        public class ProgramDetailHandler : IRequestHandler<Command, GenericResponse<string>>
        {
            private readonly CosmosDbService _dbService;
            public ProgramDetailHandler()
            {
                string connectionString = "AccountEndpoint=https://localhost:8081;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==;";
                string databaseId = "RecruitmentApiDb";
                string containerId = "RecruitmentApiContainer";
                _dbService = new CosmosDbService(connectionString, databaseId, containerId);
            }
            public async Task<GenericResponse<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                var itemExist = await _dbService.GetManyAsync<ProgramDetail>($"SELECT * FROM c WHERE c.id = '{request.detail.Title}' OR c.id = '{request.detail.id}'");

                if (itemExist.Count() > 0)
                {
                    return GenericResponse<string>.Fail($"Program detail for {request.detail.Title} failed! Duplicate entry.");
                }

                await _dbService.SaveAsync(request.detail);

                return GenericResponse<string>.Success($"Program detail for {request.detail.Title} was upserted successfully!", "Successful");
            }
        }
    }
}