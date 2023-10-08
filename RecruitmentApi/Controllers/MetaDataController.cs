using Microsoft.AspNetCore.Mvc;
using RecruitmentCore.Common;
using RecruitmentCore.Queries;
using RecruitmentDomain.Models;

namespace RecruitmentApi.Controllers
{
    public class MetaDataController : ApiBaseController
    {
        [HttpGet()]
        [Produces("application/json", Type = typeof(GenericResponse<MetaDataDto>))]
        public async Task<IActionResult> FetchAll()
        {
            var response = await Mediator.Send(new FetchMetaData.Query());
            return Ok(response);
        }
    }
}
