using Microsoft.AspNetCore.Mvc;

namespace RecruitmentApi.Controllers
{
    [Route("api/[controller]")]
    public class WorkflowController : ApiBaseController
    {
        public WorkflowController()
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
