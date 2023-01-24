using domain.Models;
using DataBase.Models;

namespace DataBase.Converters;

public static class ScheduleConverter
{
    public static ScheduleDB ToDataBaseModel(this Schedule schedule)
    {
        return new ScheduleDB
        {
            Id = schedule.Id,
            StartWorkingDay = schedule.StartWorkingDay,
            EndWorkingDay = schedule.EndWorkingDay,
            DoctorId = schedule.DoctorId
        };
    }
    public static Schedule ToDomainModel(this ScheduleDB scheduleDB)
    {
        return new Schedule
        {
            Id = scheduleDB.Id,
            StartWorkingDay = scheduleDB.StartWorkingDay,
            EndWorkingDay = scheduleDB.EndWorkingDay,
            DoctorId = scheduleDB.DoctorId
        };
    }
}
