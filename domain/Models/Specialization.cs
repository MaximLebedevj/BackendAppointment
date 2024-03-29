﻿
using domain.UseCases;

namespace domain.Models
{
    public class Specialization
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Specialization()
        {
            Id = 0;
            Name = "NULL";
        }

        public Specialization(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Result IsValid()
        {
            if (string.IsNullOrEmpty(Name))
                return Result.Fail("Invalid Name");

            return Result.Ok();
        }
    }
}
