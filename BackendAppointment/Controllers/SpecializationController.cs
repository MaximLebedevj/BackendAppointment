using domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using domain.Repository;

namespace BackendAppointment.Controllers
{
    [ApiController]
    [Route("Spec")]
    public class SpecController : ControllerBase
    {
        private readonly ISpecializationRepository _rep;
        public SpecController(ISpecializationRepository rep)
        {
            _rep = rep;
        }

        [Authorize]
        [HttpPost("add")]
        public IActionResult AddSpec(string name)
        {
            Specialization Spec = new(0, name);
            var res = Spec.IsValid();
            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Error);

            if (_rep.Create(Spec))
            {
                _rep.Save();
                return Ok(_rep.GetByName(name));
            }
            return Problem(statusCode: 404, detail: "Error while creating");
        }

        [Authorize]
        [HttpDelete("delete")]
        public IActionResult DeleteSpec(int id)
        {
            if (_rep.Delete(id))
            {
                _rep.Save();
                return Ok();
            }
            return Problem(statusCode: 404, detail: "Error while deleting");

        }

        [HttpGet("get_all")]
        public IActionResult GetAll()
        {
            return Ok(_rep.GetAll());
        }
    }
}