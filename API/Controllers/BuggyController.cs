using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        [HttpGet("unauthorized")]
        public ActionResult GetUnauthorized()
        {
            return Unauthorized("This is an unauthorized request.");
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest("Not a good request.");
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFound()
        {
            return NotFound("Resource not found.");
        }

        [HttpGet("internalerror")]
        public ActionResult GetInternalError()
        {
            throw new Exception("This is an internal server error.");
        }

        [HttpPost("validationerror")]
        public ActionResult GetValidationError(Product product)
        {
            return Ok();
        }
    }
}
