using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class SheduleTests
    {
        private readonly SheduleService _sheduleService;
        private readonly Mock<ISheduleRepository> _sheduleRepositoryMock;
        private readonly Mock<IDoctorRepository> _doctorRepositoryMock;

        public SheduleTests()
        {
            _sheduleRepositoryMock = new Mock<ISheduleRepository>();
            _doctorRepositoryMock = new Mock<IDoctorRepository>();
            _sheduleService = new SheduleService(_sheduleRepositoryMock.Object, _doctorRepositoryMock.Object);
        }

        [Fact]
        public void AbstractCreate_ShouldFail()
        {
            Shedule shedule = new Shedule();
            var res = _sheduleService.CreateShedule(shedule);

            Assert.True(res.IsFailure);
            Assert.Equal("Unable to create shedule", res.Error);
        }
        [Fact]
        public void InvalidNewSheduleUpdate_ShouldFail()
        {
            Shedule shedule = new();
            Shedule updatedshedule = new()
            {
                DoctorId = -1
            };
            var res = _sheduleService.UpdateShedule(shedule, updatedshedule);

            Assert.True(res.IsFailure);
            Assert.Equal("Invalid new shedule: Invalid id", res.Error);
        }

        [Fact]
        public void AbstractUpdate_ShouldFail()
        {
            Shedule shedule = new();
            Shedule updatedshedule = new();
            var res = _sheduleService.UpdateShedule(shedule, updatedshedule);

            Assert.True(res.IsFailure);
            Assert.Equal("Unable to update shedule", res.Error);
        }

        [Fact]
        public void GetSheduleNotFound_ShouldFail()
        {
            Doctor doctor = new();
            DateTime date = new();
            _sheduleRepositoryMock.Setup(repository => repository.GetSheduleByDoctorAndDate(It.IsAny<Doctor>(), It.IsAny<DateTime>()))
                .Returns(() => null);
            _doctorRepositoryMock.Setup(repository => repository.IsDoctorExists(It.IsAny<string>()))
                .Returns(() => true);
            var res = _sheduleService.GetSheduleByDoctorAndDate(doctor,date);

            Assert.True(res.IsFailure);
            Assert.Equal("Unable to find shedule", res.Error);
        }

        [Fact]
        public void GetDoctorIsNotExist_ShouldFail()
        {
            Doctor doctor = new();
            DateTime date = new();
            _sheduleRepositoryMock.Setup(repository => repository.GetSheduleByDoctorAndDate(It.IsAny<Doctor>(), It.IsAny<DateTime>()))
                .Returns(() => null);
            _doctorRepositoryMock.Setup(repository => repository.IsDoctorExists(It.IsAny<string>()))
                .Returns(() => false);
            var res = _sheduleService.GetSheduleByDoctorAndDate(doctor, date);

            Assert.True(res.IsFailure);
            Assert.Equal("Doctor is not exist", res.Error);
        }
    }
}
