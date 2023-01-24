using domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using domain.UseCases;

namespace BackendAppointment.Controllers;

[ApiController]
[Route("doctor")]
public class DoctorController : ControllerBase
{
    private readonly DoctorService _service;
    public DoctorController(DoctorService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpPost("create")]
    public IActionResult CreateDoctor(string name, Specialization spec)
    {
        Doctor doctor = new(0, name, spec);
        var res = _service.CreateDoctor(doctor);

        if (res.IsFailure)
            return Problem(statusCode: 404, detail: res.Error);

        return Ok(res.Value);
    }

    [Authorize]
    [HttpDelete("delete")]
    public IActionResult DeleteDoctor(int id)
    {
        var res = _service.DeleteDoctor(id);

        if (res.IsFailure)
            return Problem(statusCode: 404, detail: res.Error);

        return Ok(res.Value);
    }

    [HttpGet("get_all")]
    public IActionResult GetAllDoctors()
    {
        var res = _service.GetAllDoctors();

        if (res.IsFailure)
            return Problem(statusCode: 404, detail: res.Error);

        return Ok(res.Value);
    }

    [HttpGet("find")]
    public IActionResult FindDoctor(int id)
    {
        var res = _service.GetDoctor(id);

        if (res.IsFailure)
            return Problem(statusCode: 404, detail: res.Error);

        return Ok(res.Value);
    }

    [HttpGet("get")]
    public IActionResult FindDoctors(int spec)
    {
        Specialization spec_ = new(spec, "a");
        var res = _service.GetDoctor(spec_);

        if (res.IsFailure)
            return Problem(statusCode: 404, detail: res.Error);

        return Ok(res.Value);
    }
}