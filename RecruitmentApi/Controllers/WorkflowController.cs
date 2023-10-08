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
    public class WorkflowController : ApiBaseController
    {
        private readonly IMapper _mapper;
        public WorkflowController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost("Upsert")]
        [Produces("application/json", Type = typeof(GenericResponse<string>))]
        public async Task<IActionResult> Create([FromBody] WorkflowDto command)
        {
            if (!command.IsUpdate)
            {
                command.id = null;
            }

            var detail = _mapper.Map<Workflow>(command);
            detail.id = Guid.NewGuid().ToString();

            var respons = await Mediator.Send(new UpsertWorkflow.Command(detail, command.IsUpdate));
            return Ok(respons);
        }

        [HttpGet()]
        [Produces("application/json", Type = typeof(GenericResponse<List<WorkflowDto>>))]
        public async Task<IActionResult> Test(string id)
        {
            var response = await Mediator.Send(new FetchWorkflow.Query(id));
            return Ok(response);
        }

        [HttpGet("All")]
        [Produces("application/json", Type = typeof(GenericResponse<List<WorkflowDto>>))]
        public async Task<IActionResult> FetchAll(DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.Now.AddDays(-30);
            endDate ??= DateTime.Now;
            var response = await Mediator.Send(new FetchWorkflows.Query(startDate, endDate));
            return Ok(response);
        }
    }
}
