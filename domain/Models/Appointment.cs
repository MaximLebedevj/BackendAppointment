
using domain.UseCases;

namespace domain.Models
{
    public class Appointment
    {
        private DateTime start;
        private DateTime end;
        private int v1;
        private int v2;

        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public Appointment() : this(0, DateTime.MinValue, DateTime.MaxValue, 0, 0) { }
        public Appointment(int Id, DateTime startTime, DateTime endTime, int patientId, int doctorId)
        {
            this.Id = Id;
            StartTime = startTime;
            EndTime = endTime;
            PatientId = patientId;
            DoctorId = doctorId;
        }

        public Appointment(DateTime start, DateTime end, int v1, int v2)
        {
            this.start = start;
            this.end = end;
            this.v1 = v1;
            this.v2 = v2;
        }

        public Result IsValid()
        {
            if (EndTime < StartTime)
                return Result.Fail("Invalid Time");

            return Result.Ok();
        }
    }
}
