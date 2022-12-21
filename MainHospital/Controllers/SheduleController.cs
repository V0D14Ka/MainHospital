using Domain.Models;
using Domain.UseCases;
using MainHospital.SearchModels;
using MainHospital.Views;
using Microsoft.AspNetCore.Mvc;

namespace MainHospital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SheduleController : ControllerBase
    {
        private readonly SheduleService _service;
        public SheduleController(SheduleService service)
        {
            _service = service;
        }

        [HttpGet("get")]
        public ActionResult GetAll()
        {

            var answer = _service.GetAll();
            if (answer.IsFailure)
                return Problem(statusCode: 404, detail: answer.Error);

            return Ok(answer.Value);
        }

        [HttpPost("getDD")]
        public ActionResult<SheduleSearchView> GetByDoctorAndDate([FromBody] SheduleSearch search)
        {
            
            var answer = _service.GetSheduleByDoctorAndDate(new Doctor
            {
                Id = search.doctor.Id,
                Name = search.doctor.Name,
                Specialization = search.doctor.Specialization
            }, search.date);
            if (answer.IsFailure)
                return Problem(statusCode: 404, detail: answer.Error);

            return Ok(new SheduleSearchView
            {
                Id = answer.Value.Id,
                DoctorId = answer.Value.DoctorId,
                StartWorking = answer.Value.StartWorking.ToLocalTime(),
                EndWorking = answer.Value.EndWorking.ToLocalTime()
            });
        }

        [HttpPost("reg")]
        public ActionResult Register([FromBody] Shedule shed)
        {

            if (shed.IsValid().IsFailure)
                return Problem(statusCode: 404, detail: shed.IsValid().Error);
            var userRes = _service.CreateShedule(shed);
            if (userRes.IsFailure)
                return Problem(statusCode: 404, detail: userRes.Error);
            _service.Save();

            return Ok(new { Success = true });
        }
    }
}
