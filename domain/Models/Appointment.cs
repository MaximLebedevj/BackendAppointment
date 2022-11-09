
using domain.UseCases;

namespace domain.Models
{
    public class Appointment
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public Appointment(DateTime startTime, DateTime endTime, int patientId, int doctorId)
        {
            StartTime = startTime;
            EndTime = endTime;
            PatientId = patientId;
            DoctorId = doctorId;
        }

        public Result IsValid()
        {
            if (EndTime < StartTime)
                return Result.Fail("Invalid Time");

            return Result.Ok();
        }
    }
}
