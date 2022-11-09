using domain.Repository;
using Moq;
using domain.Models;

namespace UnitTests.UseCasesTest
{
    public class ScheduleServiceTests
    {
        private readonly ScheduleService _scheduleService;
        private readonly Mock<IScheduleRepository> _scheduleRepositoryMock;

        public ScheduleServiceTests()
        {
            _scheduleRepositoryMock = new Mock<IScheduleRepository>();
            _scheduleService = new ScheduleService(_scheduleRepositoryMock.Object);
        }

        [Fact]
        public void GetScheduleOfDoctor_InvalidDoctor_Fail()
        {
            Doctor invalidDoctor = new Doctor(1, String.Empty, new Specialization(1, "..."));

            var result = _scheduleService.GetScheduleOfDoctor(invalidDoctor);

            Assert.True(result.IsFailure);
            Assert.Equal("Invalid Doctor (Invalid FullName)", result.Error);
        }

        [Fact]
        public void GetScheduleOfDoctor_Success()
        {
            DateTime start = new DateTime(), end = new DateTime();
            List<Schedule> schedules = new()
            {
                new Schedule(1, start, end),
                new Schedule(2, start, end)
            };

            _scheduleRepositoryMock.Setup(repository => repository.GetScheduleOfDoctor(It.IsAny<Doctor>()))
                .Returns(() => schedules);

            Doctor doctor = new Doctor(1, "...", new Specialization(1, "..."));

            var result = _scheduleService.GetScheduleOfDoctor(doctor);

            Assert.True(result.Success);

        }

        [Fact]
        public void AddSchedule_InvalidSchedule_Fail()
        {
            DateTime start = new DateTime(10), end = new DateTime(1);
            Schedule invalidSchedule = new Schedule(1, start, end);

            Doctor validDoctor = new Doctor(1, "...", new Specialization(1, "..."));

            var result = _scheduleService.AddSchedule(invalidSchedule, validDoctor);

            Assert.True(result.IsFailure);
            Assert.Equal("Invalid Schedule (Invalid Time)", result.Error);
        }

        [Fact]
        public void AddSchedule_InvalidDoctor_Fail()
        {
            DateTime start = new DateTime(1), end = new DateTime(10);
            Schedule validSchedule = new Schedule(1, start, end);

            Doctor invalidDoctor = new Doctor(1, String.Empty, new Specialization(1, "..."));

            var result = _scheduleService.AddSchedule(validSchedule, invalidDoctor);

            Assert.True(result.IsFailure);
            Assert.Equal("Invalid Doctor (Invalid FullName)", result.Error);
        }

        [Fact]
        public void AddSchedule_FailedToAdd_Fail()
        {
            _scheduleRepositoryMock.Setup(repository => repository.AddSchedule(It.IsAny<Schedule>(), It.IsAny<Doctor>()))
                .Returns(() => false);

            DateTime start = new DateTime(1), end = new DateTime(10);
            Schedule validSchedule = new Schedule(1, start, end);

            Doctor validDoctor = new Doctor(1, "...", new Specialization(1, "..."));

            var result = _scheduleService.AddSchedule(validSchedule, validDoctor);

            Assert.True(result.IsFailure);
            Assert.Equal("Failed to add Schedule", result.Error);
        }

        [Fact]
        public void AddSchedule_Success()
        {
            _scheduleRepositoryMock.Setup(repository => repository.AddSchedule(It.IsAny<Schedule>(), It.IsAny<Doctor>()))
                .Returns(() => true);

            DateTime start = new DateTime(1), end = new DateTime(10);
            Schedule validSchedule = new Schedule(1, start, end);

            Doctor validDoctor = new Doctor(1, "...", new Specialization(1, "..."));

            var result = _scheduleService.AddSchedule(validSchedule, validDoctor);

            Assert.True(result.Success);
        }

        [Fact]
        public void UpdateSchedule_InvalidSchedule_Fail()
        {
            DateTime start = new DateTime(10), end = new DateTime(1);
            Schedule invalidSchedule = new Schedule(1, start, end);

            Doctor validDoctor = new Doctor(1, "...", new Specialization(1, "..."));

            var result = _scheduleService.UpdateSchedule(invalidSchedule, validDoctor);

            Assert.True(result.IsFailure);
            Assert.Equal("Invalid Schedule (Invalid Time)", result.Error);
        }

        [Fact]
        public void UpdateSchedule_InvalidDoctor_Fail()
        {
            DateTime start = new DateTime(1), end = new DateTime(10);
            Schedule validSchedule = new Schedule(1, start, end);

            Doctor invalidDoctor = new Doctor(1, String.Empty, new Specialization(1, "..."));

            var result = _scheduleService.UpdateSchedule(validSchedule, invalidDoctor);

            Assert.True(result.IsFailure);
            Assert.Equal("Invalid Doctor (Invalid FullName)", result.Error);
        }

        [Fact]
        public void UpdateSchedule_FailedToAdd_Fail()
        {
            _scheduleRepositoryMock.Setup(repository => repository.UpdateSchedule(It.IsAny<Schedule>(), It.IsAny<Doctor>()))
                .Returns(() => false);

            DateTime start = new DateTime(1), end = new DateTime(10);
            Schedule validSchedule = new Schedule(1, start, end);

            Doctor validDoctor = new Doctor(1, "...", new Specialization(1, "..."));

            var result = _scheduleService.UpdateSchedule(validSchedule, validDoctor);

            Assert.True(result.IsFailure);
            Assert.Equal("Failed to add Schedule", result.Error);
        }

        [Fact]
        public void UpdateSchedule_Success()
        {
            _scheduleRepositoryMock.Setup(repository => repository.UpdateSchedule(It.IsAny<Schedule>(), It.IsAny<Doctor>()))
                .Returns(() => true);

            DateTime start = new DateTime(1), end = new DateTime(10);
            Schedule validSchedule = new Schedule(1, start, end);

            Doctor validDoctor = new Doctor(1, "...", new Specialization(1, "..."));

            var result = _scheduleService.UpdateSchedule(validSchedule, validDoctor);

            Assert.True(result.Success);
        }

    }
}
