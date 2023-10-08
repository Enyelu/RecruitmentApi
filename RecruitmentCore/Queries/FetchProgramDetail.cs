﻿using AutoMapper;
using MediatR;
using RecruitmentCore.Common;
using RecruitmentDomain.Entities;
using RecruitmentDomain.Models;
using RecruitmentInfrastructure.Data;

namespace RecruitmentCore.Queries
{
    public class FetchProgramDetail
    {
        public record Query(string id) : IRequest<GenericResponse<List<ProgramDetailsDto>>>;

        public class ProgramDetailHandler : IRequestHandler<Query, GenericResponse<List<ProgramDetailsDto>>>
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

            public async Task<GenericResponse<List<ProgramDetailsDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var items = await _dbService.GetManyAsync<ProgramDetail>($"SELECT * FROM c WHERE c.id = '{request.id}' AND c.IsDeleted = false");

                if (items.Count() == 0 || items.Count() > 1)
                {
                    return GenericResponse<List<ProgramDetailsDto>>.NotFound($"Program detail for {request.id} not found.");
                }

                var response = _mapper.Map<List<ProgramDetailsDto>>(items);

                return GenericResponse<List<ProgramDetailsDto>>.Success(response, "Successful");
            }
        }
    }
}
