using Domain.Models;
using Domain.UseCases;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace Test
{
    public class DoctorTests
    {
        private readonly DoctorService _doctorService;
        private readonly Mock<IDoctorRepository> _doctorRepositoryMock;

        public DoctorTests()
        {
            _doctorRepositoryMock = new Mock<IDoctorRepository>();
            _doctorService = new DoctorService(_doctorRepositoryMock.Object);
        }
        [Fact]
        public void AbstractGetAll()
        {
            var res = _doctorService.GetAllDoctors();

            Assert.True(!res.IsFailure);
        }

        [Fact]
        public void AbstractGetDoctorsBySpec()
        {
            var res = _doctorService.GetDoctorsBySpecialization(new Specialization());

            Assert.True(!res.IsFailure);
        }

        [Fact]
        public void AbstractCreate_ShouldFail()
        {
            Doctor doctor = new Doctor(1, "Ivan", new Specialization());
            var res = _doctorService.CreateDoctor(doctor);

            Assert.True(res.IsFailure);
            Assert.Equal("Unable to create doctor", res.Error);
        }
        [Fact]
        public void AbstractDelete_ShouldFail()
        {
            Doctor doctor = new Doctor(1, "Ivan", new Specialization());
            var res = _doctorService.DeleteDoctor(doctor);

            Assert.True(res.IsFailure);
            Assert.Equal("Unable to delete doctor", res.Error);
        }
        [Fact]
        public void IdIsEmptyOrNull_ShouldFail()
        {
            var res = _doctorService.GetDoctorByID(-1);

            Assert.True(res.IsFailure);
            Assert.Equal("Invalid id", res.Error);
        }
    }
}