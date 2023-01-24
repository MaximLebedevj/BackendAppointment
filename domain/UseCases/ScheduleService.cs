using domain.Models;
using domain.Repository;

namespace domain.UseCases
{
    public class ScheduleService
    {
        private readonly IScheduleRepository _db;

        public ScheduleService(IScheduleRepository db)
        {
            _db = db;
        }

        public Result<List<Schedule>> GetScheduleOfDoctor(Doctor doctor)
        {
            var validDoctor = doctor.IsValid();
            if (validDoctor.IsFailure)
                return Result.Fail<List<Schedule>>("Invalid Doctor (" + validDoctor.Error + ")");

            var result = _db.GetScheduleOfDoctor(doctor);
            return result.Count > 0 ? Result.Ok(result) : Result.Fail<List<Schedule>>("Failed to get Schedule");
        }

        public Result<Schedule> GetSchedule(int id)
        {
            if (id < 0)
                return Result.Fail<Schedule>("Invalid Doctor Id");
            Schedule schedule = _db.GetSchedule(id);
            if (schedule != null)
                return Result.Ok(schedule);
            return Result.Fail<Schedule>("Schedule Not Found");
        }

        public Result<bool> AddSchedule(Schedule schedule, Doctor doctor)
        {
            var validSchedule = schedule.IsValid();
            if (validSchedule.IsFailure)
                return Result.Fail<bool>("Invalid Schedule (" + validSchedule.Error + ")");

            var validDoctor = doctor.IsValid();
            if (validDoctor.IsFailure)
                return Result.Fail<bool>("Invalid Doctor (" + validDoctor.Error + ")");

            var success = _db.AddSchedule(schedule, doctor);
            return success ? Result.Ok(success) : Result.Fail<bool>("Failed to add Schedule");
        }

        public Result AddSchedule(Schedule schedule)
        {
            var result = schedule.IsValid();
            if (result.IsFailure)
                return Result.Fail("Invalid schedule: " + result.Error);

            if (_db.Create(schedule))
            {
                _db.Save();
                return Result.Ok();
            }
            return Result.Fail<Schedule>("Unable to add schedule");
        }

        public Result<bool> UpdateSchedule(Schedule schedule, Doctor doctor)
        {
            var validSchedule = schedule.IsValid();
            if (validSchedule.IsFailure)
                return Result.Fail<bool>("Invalid Schedule (" + validSchedule.Error + ")");

            var validDoctor = doctor.IsValid();
            if (validDoctor.IsFailure)
                return Result.Fail<bool>("Invalid Doctor (" + validDoctor.Error + ")");

            var success = _db.UpdateSchedule(schedule, doctor);
            return success ? Result.Ok(success) : Result.Fail<bool>("Failed to add Schedule");
        }

        public Result UpdateSchedule(Schedule schedule)
        {
            var result = schedule.IsValid();
            if (result.IsFailure)
                return Result.Fail("Invalid Schedule");
            if (schedule.DoctorId < 0)
                return Result.Fail("Invalid Doctor Id");
            _db.UpdateSchedule(schedule);
            return Result.Ok();
        }
    }
}
