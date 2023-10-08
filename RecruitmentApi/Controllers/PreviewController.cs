using Microsoft.AspNetCore.Mvc;
using RecruitmentCore.Common;
using RecruitmentCore.Queries;
using RecruitmentDomain.Models;

namespace RecruitmentApi.Controllers
{
    [Route("api/[controller]")]
    public class PreviewController  : ApiBaseController
    {
        [HttpGet()]
        [Produces("application/json", Type = typeof(GenericResponse<PreviewDto>))]
        public async Task<IActionResult> Test(string id)
        {
            var response = await Mediator.Send(new FetchPreview.Query(id));
            return Ok(response);
        }

        [HttpGet("All")]
        [Produces("application/json", Type = typeof(GenericResponse<PreviewListDto>))]
        public async Task<IActionResult> FetchAll(DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.Now.AddDays(-30);
            endDate ??= DateTime.Now;
            var response = await Mediator.Send(new FetchPreviews.Query(startDate, endDate));
            return Ok(response);
        }
    }
}