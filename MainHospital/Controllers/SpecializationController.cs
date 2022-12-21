using Domain.Models;
using Domain.UseCases;
using MainHospital.Views;
using Microsoft.AspNetCore.Mvc;

namespace MainHospital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecializationController : Controller
    {
        private readonly SpecializationService _service;
        public SpecializationController(SpecializationService service)
        {
            _service = service;
        }

        [HttpGet("get")]
        public ActionResult GetSpecs()
        {

            var answer = _service.GetAllSpecializations();
            if (answer.IsFailure)
                return Problem(statusCode: 404, detail: answer.Error);

            return Ok(answer.Value);
        }

        [HttpGet("get/{name}")]
        public ActionResult GetSpecByName(string name)
        {
            if (name == string.Empty)
                return Problem(statusCode: 404, detail: "Не указан id");
            var answer = _service.GetSpecByName(name);
            if (answer.IsFailure)
                return Problem(statusCode: 404, detail: answer.Error);

            return Ok(answer.Value);
        }

        [HttpGet("exist/{name}")]
        public ActionResult IsExists(string name)
        {
            if (name == string.Empty)
                return Problem(statusCode: 404, detail: "Не указан id");
            var answer = _service.IsSpecExists(name);
            if (answer.IsFailure)
                return Problem(statusCode: 404, detail: answer.Error);

            return Ok(new { IsExists = answer.Value });
        }


        [HttpPost("reg")]
        public ActionResult Register([FromBody] Specialization spec)
        {
            var userRes = _service.Create(spec);
            if (userRes.IsFailure)
                return Problem(statusCode: 404, detail: userRes.Error);
            _service.Save();

            return Ok(new { Success = true });
        }
    }
}
