using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase;

public class ApplicationContext : DbContext
{
    public DbSet<AppointmentDB> Appointments { get; set; }
    public DbSet<DoctorDB> Doctors { get; set; }
    public DbSet<ScheduleDB> Schedules { get; set; }
    public DbSet<SpecializationDB> specializations { get; set; }
    public DbSet<UserDB> Users { get; set; }
    public ApplicationContext(DbContextOptions options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserDB>().HasIndex(model => model.FullName);
    }


}
