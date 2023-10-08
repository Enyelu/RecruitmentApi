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
    public class ApplicationFormController : ApiBaseController
    {
        private readonly IMapper _mapper;
        public ApplicationFormController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost("Upsert")]
        [Produces("application/json", Type = typeof(GenericResponse<string>))]
        public async Task<IActionResult> Create([FromBody] ApplicationFormDto command)
        {
            if (!command.IsUpdate)
            {
                command.id = null;
            }

            var detail = _mapper.Map<ApplicationForm>(command);
            detail.id = Guid.NewGuid().ToString();

            var respons = await Mediator.Send(new UpsertApplicationForm.Command(detail, command.IsUpdate));
            return Ok(respons);
        }

        [HttpGet()]
        [Produces("application/json", Type = typeof(GenericResponse<List<ApplicationFormDto>>))]
        public async Task<IActionResult> Test(string id)
        {
            var response = await Mediator.Send(new FetchApplicationForm.Query(id));
            return Ok(response);
        }

        [HttpGet("All")]
        [Produces("application/json", Type = typeof(GenericResponse<List<ApplicationFormDto>>))]
        public async Task<IActionResult> FetchAll(DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.Now.AddDays(-30);
            endDate ??= DateTime.Now;
            var response = await Mediator.Send(new FetchApplicationForms.Query(startDate, endDate));
            return Ok(response);
        }
    }
}
