using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecruitmentCore.Common;
using RecruitmentCore.Handlers;
using RecruitmentCore.Queries;
using RecruitmentDomain.Entities;
using RecruitmentDomain.Models;

namespace RecruitmentApi.Controllers
{
    [Route("api/[controller]")]
    public class ProgramDetailsController : ApiBaseController
    {
        private readonly IMapper _mapper;
        public ProgramDetailsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost("Upsert")]
        [Produces("application/json", Type = typeof(GenericResponse<string>))]
        public async Task<IActionResult> Create([FromBody]ProgramDetailsDto command)
        {
            if (!command.IsUpdate) 
            { 
                command.id = null; 
            }
            
            var detail = _mapper.Map<ProgramDetail>(command);
            detail.id = Guid.NewGuid().ToString();

            var respons = await Mediator.Send(new UpsertProgramDetail.Command(detail, command.IsUpdate));
            return Ok(respons);
        }

        [HttpGet()]
        [Produces("application/json", Type = typeof(GenericResponse<ProgramDetailsDto>))]
        public async Task<IActionResult> Fetch(string id)
        {
            var response = await Mediator.Send(new FetchProgramDetail.Query(id));
            return Ok(response);
        }

        [HttpGet("All")]
        [Produces("application/json", Type = typeof(GenericResponse<List<ProgramDetailsDto>>))]
        public async Task<IActionResult> FetchMany(DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.Now.AddDays(-30);
            endDate ??= DateTime.Now;
            var response = await Mediator.Send(new FetchProgramDetails.Query(startDate, endDate));
            return Ok(response);
        }
    }
}