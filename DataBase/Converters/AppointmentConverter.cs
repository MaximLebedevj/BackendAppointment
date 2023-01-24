using domain.Models;
using DataBase.Models;

namespace DataBase.Converters
{
    public static class AppointmentConverter
    {
        public static AppointmentDB ToDataBaseModel(this Appointment appointment)
        {
            return new AppointmentDB
            {
                Id = appointment.Id,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId
            };
        }
        public static Appointment ToDomainModel(this AppointmentDB appointmentDB)
        {
            return new Appointment
            {
                Id = appointmentDB.Id,
                StartTime = appointmentDB.StartTime,
                EndTime = appointmentDB.EndTime,
                PatientId = appointmentDB.PatientId,
                DoctorId = appointmentDB.DoctorId
            };
        }
    }
}
