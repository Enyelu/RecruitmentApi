using MediatR;
using RecruitmentCore.Common;
using RecruitmentDomain.Entities;
using RecruitmentInfrastructure.Data;

namespace RecruitmentCore.Handlers
{
    public class UpsertApplicationForm
    {
        public record Command(ApplicationForm detail, bool isUpdate) : IRequest<GenericResponse<string>>;

        public class ApplicationDetailHandler : IRequestHandler<Command, GenericResponse<string>>
        {
            private readonly CosmosDbService _dbService;
            public ApplicationDetailHandler()
            {
                string connectionString = "AccountEndpoint=https://localhost:8081;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==;";
                string databaseId = "RecruitmentApiDb";
                string containerId = "RecruitmentApiContainer";
                _dbService = new CosmosDbService(connectionString, databaseId, containerId);
            }
            public async Task<GenericResponse<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                var itemExist = await _dbService.GetManyAsync<ApplicationForm>($"SELECT * FROM c WHERE c.id = '{request.detail.id}' OR c.ProgramDetailId = '{request.detail.ProgramDetailId}'");

                if (itemExist.Count() > 0 && !request.isUpdate)
                {
                    return GenericResponse<string>.Fail($"Program detail for application with progamId {request.detail.ProgramDetailId} failed! Duplicate entry.");
                }

                var programDetail = await _dbService.GetManyAsync<ProgramDetail>($"SELECT * FROM c WHERE c.id = '{request.detail.ProgramDetailId}'");
                if (programDetail?.FirstOrDefault()?.id != request.detail.ProgramDetailId)
                {
                    return GenericResponse<string>.NotFound($"No Program detail with Id {request.detail.ProgramDetailId} exist.");
                }

                if (request.isUpdate)
                {
                    if (itemExist.Count() > 0)
                    {
                        var applicationToUpdate = itemExist.FirstOrDefault();

                        applicationToUpdate.Email = request.detail.Email;
                        applicationToUpdate.IsDraft = request.detail.IsDraft;
                        applicationToUpdate.LastName = request.detail.LastName;
                        applicationToUpdate.FirstName = request.detail.FirstName;
                        applicationToUpdate.CoverImageUrl = request.detail.CoverImageUrl;
                        applicationToUpdate.Education = request.detail.Education;
                        applicationToUpdate.Experience = request.detail.Experience;
                        applicationToUpdate.DOB = request.detail.DOB;
                        applicationToUpdate.Gender = request.detail.Gender;
                        applicationToUpdate.IDNumber = request.detail.IDNumber;
                        applicationToUpdate.PhoneNumber = request.detail.PhoneNumber;
                        applicationToUpdate.Nationality = request.detail.Nationality;
                        applicationToUpdate.CurrentResidence = request.detail.CurrentResidence;
                        applicationToUpdate.Resume = request.detail.Resume;
                        applicationToUpdate.Additionals = request.detail.Additionals;

                        await _dbService.UpdateAsync(applicationToUpdate);

                        return GenericResponse<string>.Success($"Application form with program detail Id: {request.detail.ProgramDetailId} was upserted successfully!", "Successful");
                    }
                    return GenericResponse<string>.Fail($"Application form with program detail Id: {request.detail.ProgramDetailId} failed to update!");
                }

                await _dbService.SaveAsync(request.detail);

                return GenericResponse<string>.Success($"Application form with program detail Id: {request.detail.ProgramDetailId} was upserted successfully!", "Successful");
            }
        }
    }
}
