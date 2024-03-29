﻿using Domain.Models;
using Domain.UseCases;
using MainHospital.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainHospital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorService _service;
        public DoctorController(DoctorService service)
        {
            _service = service;
        }

        [HttpGet("get")]
        public ActionResult GetDoctors()
        {

            var answer = _service.GetAllDoctors();
            if (answer.IsFailure)
                return Problem(statusCode: 404, detail: answer.Error);

            return Ok(answer.Value);
        }

        [HttpGet("id/{id}")]
        public ActionResult GetDoctorByid(int id)
        {
            if (id.ToString() == string.Empty)
                return Problem(statusCode: 404, detail: "Не указан id");
            var answer = _service.GetDoctorByID(id);
            if (answer.IsFailure)
                return Problem(statusCode: 404, detail: answer.Error);

            return Ok(answer.Value);
        }

        [HttpGet("spec/{id}")]
        public ActionResult GetDoctorBySpec(int id)
        {
            if(id.ToString() == string.Empty)
                return Problem(statusCode: 404, detail: "Не указан id");
            var answer = _service.GetDoctorsBySpecialization(id);
            if (answer.IsFailure)
                return Problem(statusCode: 404, detail: answer.Error);

            return Ok(answer.Value);
        }

        [HttpGet("exist/{id}")]
        public ActionResult IsExists(int id)
        {
            if (id.ToString() == string.Empty)
                return Problem(statusCode: 404, detail: "Не указан id");
            var answer = _service.IsDoctorExists(id);
            if (answer.IsFailure)
                return Problem(statusCode: 404, detail: answer.Error);

            return Ok(new { IsExists = answer.Value });
        }

        [Authorize]
        [HttpPost("reg")]
        public ActionResult Register([FromBody] Doctor doctor)
        {
            var userRes = _service.CreateDoctor(doctor);
            if (userRes.IsFailure)
                return Problem(statusCode: 404, detail: userRes.Error);
            _service.Save();

            return Ok(new { Success = true });
        }
    }
}
