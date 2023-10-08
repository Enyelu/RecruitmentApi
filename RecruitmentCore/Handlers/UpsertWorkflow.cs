using MediatR;
using RecruitmentCore.Common;
using RecruitmentDomain.Entities;
using RecruitmentInfrastructure.Data;

namespace RecruitmentCore.Handlers
{
    public class UpsertWorkflow
    {
        public record Command(Workflow detail, bool isUpdate) : IRequest<GenericResponse<string>>;

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
                var itemExist = await _dbService.GetManyAsync<Workflow>($"SELECT * FROM c WHERE c.id = '{request.detail.id}' OR c.ProgramDetailId = '{request.detail.ProgramDetailId}'");

                if (itemExist.Count() > 0 && !request.isUpdate)
                {
                    return GenericResponse<string>.Fail($"Program detail for workflow with progamId {request.detail.ProgramDetailId} failed! Duplicate entry.");
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
                        var workflorToUpdate = itemExist.FirstOrDefault();

                        workflorToUpdate.Applied = request.detail.Applied;
                        workflorToUpdate.Shortlisted = request.detail.Shortlisted;
                        workflorToUpdate.VideoInterview = request.detail.VideoInterview;
                        workflorToUpdate.OtherVideoInterview = request.detail.OtherVideoInterview;
                        workflorToUpdate.FirstZoomInterview = request.detail.FirstZoomInterview;
                        workflorToUpdate.InPersonMeeting = request.detail.InPersonMeeting;
                        workflorToUpdate.Placement = request.detail.Placement;
                        workflorToUpdate.Offered = request.detail.Offered;

                        workflorToUpdate.Others.Clear();
                        foreach (var item in request.detail.Others)
                        {
                            workflorToUpdate.Others.Add(item);
                        }

                        await _dbService.UpdateAsync(workflorToUpdate);

                        return GenericResponse<string>.Success($"Workflow with program detail Id: {request.detail.ProgramDetailId} was upserted successfully!", "Successful");
                    }
                    return GenericResponse<string>.Fail($"Workflow with program detail Id: {request.detail.ProgramDetailId} failed to update!");
                }

                await _dbService.SaveAsync(request.detail);

                return GenericResponse<string>.Success($"Workflow with program detail Id: {request.detail.ProgramDetailId} was upserted successfully!", "Successful");
            }
        }
    }
}