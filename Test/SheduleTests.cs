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
        public void InvalidTimeCreate_ShouldFail()
        {
            Shedule shedule = new Shedule();
            var res = _sheduleService.CreateShedule(shedule);

            Assert.True(res.IsFailure);
            Assert.Equal("Invalid shedule: Invalid time", res.Error);
        }
        [Fact]
        public void InvalidNewSheduleUpdate_ShouldFail()
        {
            Shedule shedule = new()
            {
                DoctorId = -1
            };
            var res = _sheduleService.UpdateShedule(shedule);

            Assert.True(res.IsFailure);
            Assert.Equal("Invalid new shedule: Invalid id", res.Error);
        }

        [Fact]
        public void AbstractUpdate_ShouldFail()
        {
            Shedule shedule = new()
            {
                StartWorking = new DateTime(2000, 5, 1),
                EndWorking = new DateTime(2001,5,1)
            };
            var res = _sheduleService.UpdateShedule(shedule);

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
            _doctorRepositoryMock.Setup(repository => repository.IsDoctorExist(It.IsAny<int>()))
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
            _doctorRepositoryMock.Setup(repository => repository.IsDoctorExist(It.IsAny<int>()))
                .Returns(() => false);
            var res = _sheduleService.GetSheduleByDoctorAndDate(doctor, date);

            Assert.True(res.IsFailure);
            Assert.Equal("Doctor is not exist", res.Error);
        }
    }
}
