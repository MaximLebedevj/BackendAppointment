namespace DataBase.Models;

public class ScheduleDB
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public DateTime StartWorkingDay { get; set; }
    public DateTime EndWorkingDay { get; set; }
}
