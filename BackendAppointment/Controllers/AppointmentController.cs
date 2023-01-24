using domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using domain.UseCases;

namespace BackendAppointment.Controllers
{
    [ApiController]
    [Route("appointment")]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentService _service;
        private readonly ScheduleService _serviceSched;
        public AppointmentController(AppointmentService service, ScheduleService scheduleService)
        {
            _service = service;
            _serviceSched = scheduleService;
        }

        [Authorize]
        [HttpPost("save")]
        public IActionResult SaveAppointment(int patient_id, int doctor_id, DateTime start_time, DateTime end_time, int schedule_id)
        {
            Appointment appointment = new(0, start_time, end_time, patient_id, doctor_id);
            var schedule = _serviceSched.GetSchedule(schedule_id);
            if (schedule.IsFailure)
                return Problem(statusCode: 404, detail: schedule.Error);

            var res = _service.SaveAppointment(appointment, schedule.Value);

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Error);

            return Ok(res.Value);
        }

        [HttpGet("get/spec")]
        public IActionResult GetFreeTime(int spec_)
        {
            Specialization spec = new(spec_, "");
            var res = _service.GetFreeTime(spec);

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Error);

            return Ok(res.Value);
        }

        [HttpGet("get/doctor")]
        public IActionResult GetFreeTime(Doctor doctor)
        {
            var schedule = _serviceSched.GetSchedule(doctor.Id);
            if (schedule.IsFailure)
                return Problem(statusCode: 404, detail: schedule.Error);
            var res = _service.GetFreeTime(doctor.Id);

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Error);

            return Ok(res.Value);
        }
    }
}