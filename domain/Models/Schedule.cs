
using domain.UseCases;

namespace domain.Models
{
    public class Schedule
    {
        private int v;
        private DateTime start;
        private DateTime end;

        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DateTime StartWorkingDay { get; set; }
        public DateTime EndWorkingDay { get; set; }
        public Schedule() : this(0, 0, DateTime.MinValue, DateTime.MaxValue) { }
        public Schedule (int Id, int doctorId, DateTime startWorkingDay, DateTime endWorkingDay)
        {
            this.Id = Id;
            DoctorId = doctorId;
            StartWorkingDay = startWorkingDay;
            EndWorkingDay = endWorkingDay;
        }

        public Schedule(int v, DateTime start, DateTime end)
        {
            this.v = v;
            this.start = start;
            this.end = end;
        }

        public Result IsValid()
        {
            if (EndWorkingDay < StartWorkingDay)
                return Result.Fail("Invalid Time");

            return Result.Ok();
        }
    }
}
