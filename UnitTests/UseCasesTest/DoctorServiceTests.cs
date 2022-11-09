using domain.Repository;
using Moq;
using domain.Models;
using domain.UseCases;

public class DoctorServiceTests
{
    private readonly DoctorService _doctorService;
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock;

    public DoctorServiceTests()
    {
        _doctorRepositoryMock = new Mock<IDoctorRepository>();
        _doctorService = new DoctorService(_doctorRepositoryMock.Object);
    }

    [Fact]
    public void CreateDoctor_InvalidFullName_Fail()
    {
        Specialization validSpecialization = new Specialization(1, "Therapist");
        Doctor invalidDoctor = new Doctor(1, String.Empty, validSpecialization);

        var result = _doctorService.CreateDoctor(invalidDoctor);

        Assert.True(result.IsFailure);
        Assert.Equal("Invalid FullName", result.Error);
    }

    [Fact]
    public void CreateDoctor_InvalidSpecialization_Fail()
    {
        Specialization invalidSpecialization = new Specialization(1, String.Empty);
        Doctor validDoctor = new Doctor(1, "...", invalidSpecialization);

        var result = _doctorService.CreateDoctor(validDoctor);

        Assert.True(result.IsFailure);
        Assert.Equal("Invalid Specialization (Invalid Name)", result.Error);
    }

    [Fact]
    public void CreateDoctor_FailedToCreate_Fail()
    {
        _doctorRepositoryMock.Setup(repository => repository.CreateDoctor(It.IsAny<Doctor>()))
            .Returns(() => false);

        Specialization validSpecialization = new Specialization(1, "Therapist");
        Doctor validDoctor = new Doctor(1, "...", validSpecialization);

        var result = _doctorService.CreateDoctor(validDoctor);

        Assert.True(result.IsFailure);
    }

    [Fact]
    public void CreateDoctor_SuccessfullyCreated_Success()
    {
        _doctorRepositoryMock.Setup(repository => repository.CreateDoctor(It.IsAny<Doctor>()))
            .Returns(() => true);

        Specialization validSpecialization = new Specialization(1, "Therapist");
        Doctor validDoctor = new Doctor(1, "...", validSpecialization);

        var result = _doctorService.CreateDoctor(validDoctor);

        Assert.True(result.Success);
    }

    [Fact]
    public void DeleteDoctor_DoctorHasAppointments_Fail()
    {
        DateTime startTime = new DateTime();
        DateTime endTime = new DateTime();
        var patiendId = 1;
        var doctorId = 1;

        DateTime startTime1 = new DateTime();
        DateTime endTime1 = new DateTime();
        var patiendId1 = 2;
        var doctorId1 = 2;


        List<Appointment> appointments = new()
        {
            
            new Appointment(startTime, endTime, patiendId, doctorId),
            new Appointment(startTime1, endTime1, patiendId1, doctorId1 ) 
        };

        var result = _doctorService.DeleteDoctor(0, appointments);

        Assert.True(result.IsFailure);
        Assert.Equal("Cannot delete doctor with appointments", result.Error);

    }

    [Fact]
    public void DeleteDoctor_FaildToFind_Fail()
    {
        List<Appointment> appointments = new () {};

        var result = _doctorService.DeleteDoctor(0, appointments);

        Assert.True(result.IsFailure);
        Assert.Equal("Doctor not found", result.Error);
    }

    [Fact]
    public void DeleteDoctor_FailedToDelete_Fail()
    {
        _doctorRepositoryMock.Setup(repository => repository.DeleteDoctor(It.IsAny<int>()))
            .Returns(() => false);
        _doctorRepositoryMock.Setup(repository => repository.GetDoctor(It.IsAny<int>()))
            .Returns(() => new Doctor(1, "...", new Specialization(1, "...")));

        List<Appointment> appointments = new() { };
        var result = _doctorService.DeleteDoctor(1, appointments);

        Assert.True(result.IsFailure);
        Assert.Equal("User has not been deleted", result.Error);
    }

    [Fact]
    public void DeleteDoctor_SuccessfullyDeleted_Success()
    {
        _doctorRepositoryMock.Setup(repository => repository.DeleteDoctor(It.IsAny<int>()))
            .Returns(() => true);
        _doctorRepositoryMock.Setup(repository => repository.GetDoctor(It.IsAny<int>()))
            .Returns(() => new Doctor(1, "...", new Specialization(1, "...")));

        List<Appointment> appointments = new() { };
        var result = _doctorService.DeleteDoctor(1, appointments);

        Assert.True(result.Success);
    }


    [Fact]
    public void GetAllDoctors_Success()
    {
        List <Doctor> doctorsList = new() 
        { new Doctor(1, "...", new Specialization(1, "...")),
          new Doctor(2, "...", new Specialization(2, "...")),
          new Doctor(3, "...", new Specialization(3, "..."))
        };

        IEnumerable<Doctor> doctors = doctorsList;

        _doctorRepositoryMock.Setup(repository => repository.GetAllDoctors())
            .Returns(() => doctors);

        var result = _doctorService.GetAllDoctors();

        Assert.True(result.Success);
    }

    [Fact]
    public void GetDoctor_ById_NotFound_Fail()
    {
        _doctorRepositoryMock.Setup(repository => repository.GetDoctor(It.IsAny<int>()))
            .Returns(() => null);

        var result = _doctorService.GetDoctor(2);

        Assert.True(result.IsFailure);
        Assert.Equal("Doctor not found", result.Error);
    }

    [Fact]
    public void GetDoctor_ById_Success()
    {
        _doctorRepositoryMock.Setup(repository => repository.GetDoctor(It.IsAny<int>()))
            .Returns(() => new Doctor(1, "...", new Specialization(1, "...")));

        var result = _doctorService.GetDoctor(1);

        Assert.True(result.Success);
    }

    [Fact]
    public void GetDoctors_BySpecialization_Invalid_Fail()
    {


        Specialization invalidSpecialization = new Specialization(1, String.Empty);
        var result = _doctorService.GetDoctor(invalidSpecialization);

        Assert.True(result.IsFailure);
        Assert.Equal("Invalid Name", result.Error);
    }

    [Fact]
    public void GetDoctors_BySpecialization_NotFound_Fail()
    {
        _doctorRepositoryMock.Setup(repository => repository.GetDoctor(It.IsAny<int>()))
            .Returns(() => null);

        Specialization validSpecialization = new Specialization(1, "...");
        var result = _doctorService.GetDoctor(validSpecialization);

        Assert.True(result.IsFailure);
        Assert.Equal("Doctor not found", result.Error);
    }

    [Fact]
    public void GetDoctors_BySpecialization_Success()
    {
        Specialization specialization = new Specialization(1, "SomeSpecialization");
        _doctorRepositoryMock.Setup(repository => repository.GetDoctor(It.IsAny<Specialization>()))
            .Returns(() => new Doctor(1, "...", specialization));

        var result = _doctorService.GetDoctor(specialization);


        Assert.True(result.Success);
    }


}
