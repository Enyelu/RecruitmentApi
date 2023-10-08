﻿using MediatR;
using RecruitmentCore.Common;
using RecruitmentDomain.Entities;
using RecruitmentInfrastructure.Data;

namespace RecruitmentCore.Handlers
{
    public class UpsertProgramDetail
    {
        public record Command(ProgramDetail detail, bool isUpdate) : IRequest<GenericResponse<string>>;

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
                var itemExist = await _dbService.GetManyAsync<ProgramDetail>($"SELECT * FROM c WHERE c.Title = '{request.detail.Title}' OR c.id = '{request.detail.id}'");

                if (itemExist.Count() > 0 && !request.isUpdate)
                {
                    return GenericResponse<string>.Fail($"Upserting program detail for {request.detail.Title} failed! Duplicate entry.");
                }

                if (request.isUpdate)
                {
                    if (itemExist.Count() > 0)
                    {
                        var programToUpdate = itemExist.FirstOrDefault();

                        programToUpdate.Title = request.detail.Title;
                        programToUpdate.Skills = request.detail.Skills;
                        programToUpdate.Criteria = request.detail.Criteria;
                        programToUpdate.Location = request.detail.Location;
                        programToUpdate.IsRemote = request.detail.IsRemote;
                        programToUpdate.MaxApplicant = request.detail.MaxApplicant;
                        programToUpdate.StartDate = request.detail.StartDate;
                        programToUpdate.Summary = request.detail.Summary;
                        programToUpdate.Description = request.detail.Description;
                        programToUpdate.ApplicationOpen = request.detail.ApplicationOpen;
                        programToUpdate.ApplicationClose = request.detail.ApplicationClose;
                        programToUpdate.Duration = request.detail.Duration;
                        programToUpdate.MinQualification = request.detail.MinQualification;

                        await _dbService.UpdateAsync(programToUpdate);

                        return GenericResponse<string>.Success($"Program detail with TItle: {request.detail.Title} was updated successfully!", "Successful");
                    }
                    return GenericResponse<string>.Fail($"Program detail with TItle: {request.detail.Title} failed to update!");
                }

                await _dbService.SaveAsync(request.detail);

                return GenericResponse<string>.Success($"Program detail for {request.detail.Title} was upserted successfully!", "Successful");
            }
        }
    }
}