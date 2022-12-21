using Domain.Models;
using Domain.UseCases;
using MainHospital.ExtraModels;
using MainHospital.Views;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace MainHospital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentService _service;
        private readonly UserService _serviceU;
        private readonly DoctorService _serviceD;
        public AppointmentController(AppointmentService service, UserService service1, DoctorService service2)
        {
            _service = service;
            _serviceU = service1;
            _serviceD = service2;
        }

        [HttpPost("actual")]
        public ActionResult GetActualDates([FromBody] Specialization spec)
        {

            var answer = _service.GetActualDates(spec);
            if (answer.IsFailure)
                return Problem(statusCode: 404, detail: answer.Error);

            return Ok(answer.Value);
        }

        [HttpPost("reg")]
        public ActionResult Register([FromBody] AppointmentCreate app)
        {
            var answer = _service.CreateAppointment(app.patient, app.doctor);
            if (answer.IsFailure)
                return Problem(statusCode: 404, detail: answer.Error);

            return Ok(answer.Value);
        }
    }
}
