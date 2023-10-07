using Microsoft.AspNetCore.Mvc;

namespace RecruitmentApi.Controllers
{
    [Route("api/[controller]")]
    public class ApplicationFormController : ApiBaseController
    {
        public ApplicationFormController()
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
