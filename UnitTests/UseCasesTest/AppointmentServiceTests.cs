using domain.Repository;
using Moq;
using domain.Models;

namespace UnitTests.UseCasesTest
{
    public class AppointmentServiceTests
    {
        private readonly AppointmentService _appointmentService;
        private readonly Mock<IAppointmentRepository> _appointmentRepositoryMock;

        public AppointmentServiceTests()
        {
            _appointmentRepositoryMock = new Mock<IAppointmentRepository>();
            _appointmentService = new AppointmentService(_appointmentRepositoryMock.Object);
        }

        [Fact]
        public void GetAppointmentBySpecialization_InvalidSpecialization_Fail()
        {
            Specialization invalidSpecialization = new Specialization(1, String.Empty);

            var result = _appointmentService.GetAppointmentBySpecialization(invalidSpecialization);

            Assert.True(result.IsFailure);
            Assert.Equal("Invalid Specialization (Invalid Name)", result.Error);
        }

        [Fact]
        public void GetAppointmentBySpecialization_Success()
        {
            DateTime start = new DateTime(1), end = new DateTime(2);
            List<Appointment> appointments = new()
            {
                new Appointment(start, end, 1, 2),
                new Appointment(start, end, 5, 6)
            };
            _appointmentRepositoryMock.Setup(repository => repository.GetAppointmentBySpecialization(It.IsAny<Specialization>()))
                .Returns(() => appointments);

            Specialization specialization = new Specialization(1, "...");

            var result = _appointmentService.GetAppointmentBySpecialization(specialization);

            Assert.True(result.Success);
        }

        [Fact]
        public void AddConcreteDoctorAppointment_InvalidAppointment_Fail()
        {
            DateTime start = new DateTime(10), end = new DateTime(1);
            Appointment invalidAppointment = new Appointment(start, end, 1, 2);

            var result = _appointmentService.AddConcreteDoctorAppointment(invalidAppointment);

            Assert.True(result.IsFailure);
            Assert.Equal("Invalid Appointment (Invalid Time)", result.Error);
        }

        [Fact]
        public void AddConcreteDoctorAppointment_FailedToAdd_Fail()
        {
            _appointmentRepositoryMock.Setup(repository => repository.AddAppointment(It.IsAny<Appointment>()))
                .Returns(() => false);

            DateTime start = new DateTime(1), end = new DateTime(10);
            Appointment validAppointment = new Appointment(start, end, 1, 2);

            var result = _appointmentService.AddConcreteDoctorAppointment(validAppointment);

            Assert.True(result.IsFailure);
            Assert.Equal("Appointment has not been added", result.Error);
        }

        [Fact]
        public void AddAnyAppointment_InvalidSpecialization_Fail()
        {
            Specialization invalidSpecialization = new Specialization(1, String.Empty);

            var result = _appointmentService.AddAnyAppointment(invalidSpecialization);

            Assert.True(result.IsFailure);
            Assert.Equal("Invalid Specialization (Invalid Name)", result.Error);
        }

        [Fact]
        public void AddConcreteDoctorAppointment_Success()
        {
            _appointmentRepositoryMock.Setup(repository => repository.AddAppointment(It.IsAny<Appointment>()))
                .Returns(() => true);

            DateTime start = new DateTime(1), end = new DateTime(10);
            Appointment validAppointment = new Appointment(start, end, 1, 2);

            var result = _appointmentService.AddConcreteDoctorAppointment(validAppointment);

            Assert.True(result.Success);
        }
    }
}
