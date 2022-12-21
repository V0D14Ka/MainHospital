using Microsoft.AspNetCore.Mvc;
using MainHospital.Views;
using Domain.UseCases;
using Domain.Models;

namespace MainHospital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpGet("login/{login}")] 
        public ActionResult<UserSearchView> GetUserByLogin(string login)
        {

            if (login == string.Empty)
                return Problem(statusCode: 404, detail: "Не указан логин");

            var userRes = _service.GetUserByLogin(login);
            if (userRes.IsFailure)
                return Problem(statusCode: 404, detail: userRes.Error);

            return Ok(new UserSearchView
            {
                Id = userRes.Value.Id,
                Login = userRes.Value.UserName,
            });
        }
        
        [HttpGet("id/{id}")]
        public ActionResult<UserSearchView> GetUserById(int id)
        {

            if (id.ToString() == string.Empty)
                return Problem(statusCode: 404, detail: "Не указан id");

            var userRes = _service.GetUserById(id);
            if (userRes.IsFailure)
                return Problem(statusCode: 404, detail: userRes.Error);

            return Ok(new UserSearchView
            {
                Id = userRes.Value.Id,
                Login = userRes.Value.UserName,
            });
        }

        [HttpGet("exist/{login}")]
        public ActionResult IsExist(string login)
        {

            if (login == string.Empty)
                return Problem(statusCode: 404, detail: "Не указан login");

            var userRes = _service.IsUserExists(login);
            if (userRes.IsFailure)
                return Problem(statusCode: 404, detail: userRes.Error);

            return Ok(new { IsExists = userRes.Value }) ;
        }

        [HttpPost("reg")]
        public ActionResult Register([FromBody] User user)
        {
            var userRes = _service.Register(user);
            if (userRes.IsFailure)
                return Problem(statusCode: 404, detail: userRes.Error);
            _service.Save();

            return Ok(new { Success = true });
        }
    }
}
