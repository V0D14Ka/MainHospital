using Domain.Models;
using Domain.UseCases;
using MainHospital.Views;
using Microsoft.AspNetCore.Mvc;

namespace MainHospital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : Controller
    {
        private readonly DoctorService _service;
        public DoctorController(DoctorService service)
        {
            _service = service;
        }

        [HttpGet("login")]
        public ActionResult<UserSearchView> GetDoctors()
        {

            var answer = _service.GetAllDoctors();
            if (answer.IsFailure)
                return Problem(statusCode: 404, detail: answer.Error);

            return Ok(answer.Value);
        }

        [HttpGet("login/{id}")]
        public ActionResult<UserSearchView> GetDoctorByid(int id)
        {
            if (id.ToString() == string.Empty)
                return Problem(statusCode: 404, detail: "Не указан id");
            var answer = _service.GetDoctorByID(id);
            if (answer.IsFailure)
                return Problem(statusCode: 404, detail: answer.Error);

            return Ok(answer.Value);
        }

        [HttpGet("spec/{id}")]
        public ActionResult<UserSearchView> GetDoctorBySpec(int id)
        {
            if(id.ToString() == string.Empty)
                return Problem(statusCode: 404, detail: "Не указан id");
            var answer = _service.GetDoctorsBySpecialization(id);
            if (answer.IsFailure)
                return Problem(statusCode: 404, detail: answer.Error);

            return Ok(answer.Value);
        }

        [HttpPost("reg")]
        public ActionResult<UserSearchView> Register([FromBody] Doctor doctor)
        {

            if (doctor.IsValid().IsFailure)
                return Problem(statusCode: 404, detail: doctor.IsValid().Error);

            var userRes = _service.CreateDoctor(doctor);
            if (userRes.IsFailure)
                return Problem(statusCode: 404, detail: userRes.Error);
            _service.Save();

            return Ok(new { Success = true });
        }
    }
}
