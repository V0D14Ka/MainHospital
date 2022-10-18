using Domain.Models;
using Domain.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class AppointmentTests
    {
        private readonly AppointmentService _appointmentService;
        private readonly Mock<IAppointmentRepository> _appointmentRepositoryMock;
        private readonly Mock<IDoctorRepository> _doctorRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ISpecializationRepository> _specializationRepositoryMock;

        public AppointmentTests()
        {
            _appointmentRepositoryMock = new Mock<IAppointmentRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _doctorRepositoryMock = new Mock<IDoctorRepository>();
            _specializationRepositoryMock = new Mock<ISpecializationRepository>();
            _appointmentService = new AppointmentService(_appointmentRepositoryMock.Object, _doctorRepositoryMock.Object, _userRepositoryMock.Object, _specializationRepositoryMock.Object );
        }

        [Fact]
        public void TwoParametrsCreate_ShouldFail()
        {
            var res = _appointmentService.CreateAppointment(1,2,"dantist");

            Assert.True(res.IsFailure);
            Assert.Equal("Need to give one parametr: doctorId or specialization", res.Error);
        }

        [Fact]
        public void DoctorIsNotExist_ShouldFail()
        {
            _doctorRepositoryMock.Setup(repository => repository.GetDoctorById(It.IsAny<int>()))
                .Returns(() => null);
            var res = _appointmentService.CreateAppointment(1, 2);

            Assert.True(res.IsFailure);
            Assert.Equal("Doctor is not exist", res.Error);
        }

        [Fact]
        public void UserIsNotExist_ShouldFail()
        {
            _userRepositoryMock.Setup(repository => repository.IsUserExistByID(It.IsAny<int>()))
                .Returns(() => false);
            _doctorRepositoryMock.Setup(repository => repository.GetDoctorById(It.IsAny<int>()))
                .Returns(() => new Doctor());
            var res = _appointmentService.CreateAppointment(1, 2);

            Assert.True(res.IsFailure);
            Assert.Equal("Patient is not exist", res.Error);
        }

        [Fact]
        public void UserIsNotPatient_ShouldFail()
        {
            _userRepositoryMock.Setup(repository => repository.IsUserExistByID(It.IsAny<int>()))
                .Returns(() => true);
            _userRepositoryMock.Setup(repository => repository.GetUserByID(It.IsAny<int>()))
                .Returns(() => new User(1,"8","Ivan",Role.Administrator,"as","123"));
            _doctorRepositoryMock.Setup(repository => repository.GetDoctorById(It.IsAny<int>()))
                .Returns(() => new Doctor());
            var res = _appointmentService.CreateAppointment(1, 2);

            Assert.True(res.IsFailure);
            Assert.Equal("User is not a patient", res.Error);
        }

        [Fact]
        public void AppointmentAlreadyExist_ShouldFail()
        {
            List<Appointment> list = new();
            list.Add(new Appointment());
            IEnumerable<Appointment> en = list;

            _userRepositoryMock.Setup(repository => repository.IsUserExistByID(It.IsAny<int>()))
                .Returns(() => true);
            _userRepositoryMock.Setup(repository => repository.GetUserByID(It.IsAny<int>()))
                .Returns(() => new User());
            _appointmentRepositoryMock.Setup(repository => repository.IsAppointmentExist(It.IsAny<Appointment>()))
                .Returns(() => true);
            _appointmentRepositoryMock.Setup(repository => repository.GetActualDates(It.IsAny<string>()))
                .Returns(() => en);
            _appointmentRepositoryMock.Setup(repository => repository.GetActualDatesByDoctorID(It.IsAny<int>()))
                .Returns(() => en);
            _doctorRepositoryMock.Setup(repository => repository.GetDoctorById(It.IsAny<int>()))
                .Returns(() => new Doctor());
            var res = _appointmentService.CreateAppointment(1, 2);

            Assert.True(res.IsFailure);
            Assert.Equal("Appointment is already exists", res.Error);
        }

        [Fact]
        public void AbstractMakeNote_ShouldFail()
        {
            List<Appointment> list = new();
            list.Add(new Appointment());
            IEnumerable<Appointment> en = list;

            _userRepositoryMock.Setup(repository => repository.IsUserExistByID(It.IsAny<int>()))
                .Returns(() => true);
            _userRepositoryMock.Setup(repository => repository.GetUserByID(It.IsAny<int>()))
                .Returns(() => new User());
            _appointmentRepositoryMock.Setup(repository => repository.IsAppointmentExist(It.IsAny<Appointment>()))
                .Returns(() => false);
            _appointmentRepositoryMock.Setup(repository => repository.GetActualDates(It.IsAny<string>()))
                .Returns(() => en);
            _appointmentRepositoryMock.Setup(repository => repository.GetActualDatesByDoctorID(It.IsAny<int>()))
                .Returns(() => en);
            _doctorRepositoryMock.Setup(repository => repository.GetDoctorById(It.IsAny<int>()))
                .Returns(() => new Doctor());
            var res = _appointmentService.CreateAppointment(1, 2);

            Assert.True(res.IsFailure);
            Assert.Equal("Unable to create appointment", res.Error);
        }

        [Fact]
        public void SpecIsNotExist_ShouldFail()
        {
            List<Appointment> list = new();
            list.Add(new Appointment());
            IEnumerable<Appointment> en = list;

            _userRepositoryMock.Setup(repository => repository.IsUserExistByID(It.IsAny<int>()))
                .Returns(() => true);
            _userRepositoryMock.Setup(repository => repository.GetUserByID(It.IsAny<int>()))
                .Returns(() => new User());
            _appointmentRepositoryMock.Setup(repository => repository.IsAppointmentExist(It.IsAny<Appointment>()))
                .Returns(() => true);
            _appointmentRepositoryMock.Setup(repository => repository.GetActualDates(It.IsAny<string>()))
                .Returns(() => en);
            _appointmentRepositoryMock.Setup(repository => repository.GetActualDatesByDoctorID(It.IsAny<int>()))
                .Returns(() => en);
            _doctorRepositoryMock.Setup(repository => repository.GetDoctorById(It.IsAny<int>()))
                .Returns(() => new Doctor());
            var res = _appointmentService.CreateAppointment(1,-1,"spec");

            Assert.True(res.IsFailure);
            Assert.Equal("Specialization is not exist", res.Error);
        }
    }
}
