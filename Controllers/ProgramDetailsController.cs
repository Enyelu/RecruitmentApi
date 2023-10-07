using Microsoft.AspNetCore.Mvc;

namespace RecruitmentApi.Controllers
{
    [Route("api/[controller]")]
    public class ProgramDetailsController : ApiBaseController
    {
        public ProgramDetailsController()
        {
        }

        [HttpGet()]
        //[Produces("application/json", Type = typeof())]
        public async Task<IActionResult> Test()
        {
            return Ok("TESTED");
        }
    }
}