using domain.Models;
using DataBase.Models;

namespace DataBase.Converters;

public static class SpecializationConverter
{
    public static SpecializationDB ToDataBaseModel(this Specialization specialization)
    {
        return new SpecializationDB
        {
            Id = specialization.Id,
            Name = specialization.Name
        };
    }
    public static Specialization ToDomainModel(this SpecializationDB specializationDB)
    {
        return new Specialization
        {
            Id = specializationDB.Id,
            Name = specializationDB.Name
        };
    }
}
