
using domain.UseCases;

namespace domain.Models
{
    public class Doctor
    {
        public int Id { get; set; } 
        public string FullName { get; set; }
        public Specialization Specialization { get; set; }

        public Doctor()
        {
            Id = 0;
            FullName = "NULL";
            Specialization = new Specialization();
        }

        public Doctor(int id, string fullName, Specialization specialization)
        {
            Id = id;
            FullName = fullName;
            Specialization = specialization;
        }

        public Result IsValid()
        {
            if (string.IsNullOrEmpty(FullName))
                return Result.Fail("Invalid FullName");

            if (Specialization.IsValid().IsFailure)
                return Result.Fail("Invalid Specialization (" + Specialization.IsValid().Error + ")");

            return Result.Ok();
        }
    }
}
