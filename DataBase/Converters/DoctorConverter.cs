using domain.Models;
using DataBase.Models;

namespace DataBase.Converters;

public static class DoctorConverter
{
    public static DoctorDB ToDataBaseModel(this Doctor doctor)
    {
        return new DoctorDB
        {
            Id = doctor.Id,
            FullName = doctor.FullName,
            Specialization = doctor.Specialization
        };
    }
    public static Doctor ToDomainModel(this DoctorDB doctorDB)
    {
        return new Doctor
        {
            Id = doctorDB.Id,
            FullName = doctorDB.FullName,
            Specialization = doctorDB.Specialization
        };
    }
}
