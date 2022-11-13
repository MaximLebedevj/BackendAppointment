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
    }
}
