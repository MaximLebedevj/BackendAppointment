
using domain.UseCases;

namespace domain.Models
{
    public class Schedule
    {
        public int DoctorId { get; set; }
        public DateTime StartWorkingDay { get; set; }
        public DateTime EndWorkingDay { get; set; }

        public Schedule (int doctorId, DateTime startWorkingDay, DateTime endWorkingDay)
        {
            DoctorId = doctorId;
            StartWorkingDay = startWorkingDay;
            EndWorkingDay = endWorkingDay;
        }

        public Result IsValid()
        {
            if (EndWorkingDay < StartWorkingDay)
                return Result.Fail("Invalid Time");

            return Result.Ok();
        }
    }
}
