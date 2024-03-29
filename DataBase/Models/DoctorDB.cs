﻿using domain.Models;

namespace DataBase.Models;

public class DoctorDB
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public Specialization? Specialization { get; set; }
}
