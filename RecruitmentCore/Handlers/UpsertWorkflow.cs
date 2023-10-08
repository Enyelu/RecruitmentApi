using MediatR;
using RecruitmentCore.Common;
using RecruitmentDomain.Entities;
using RecruitmentInfrastructure.Data.Interface;

namespace RecruitmentCore.Handlers
{
    public class UpsertWorkflow
    {
        public record Command(Workflow detail, bool isUpdate) : IRequest<GenericResponse<string>>;

        public class ApplicationDetailHandler : IRequestHandler<Command, GenericResponse<string>>
        {
            private readonly ICosmoDbService _dbService;
            public ApplicationDetailHandler(ICosmoDbService dbService)
            {
                _dbService = dbService;
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