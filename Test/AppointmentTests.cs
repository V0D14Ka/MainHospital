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
        public void DoctorIsNotExist_ShouldFail()
        {
            _doctorRepositoryMock.Setup(repository => repository.IsDoctorExists(It.IsAny<Doctor>()))
                .Returns(() => false);

            var res = _appointmentService.CreateAppointment(new User(), new Doctor());

            Assert.True(res.IsFailure);
            Assert.Equal("Doctor is not exist", res.Error);
        }

        [Fact]
        public void UserIsNotExist_ShouldFail()
        {
            _userRepositoryMock.Setup(repository => repository.IsUserExists(It.IsAny<User>()))
                .Returns(() => false);

            _doctorRepositoryMock.Setup(repository => repository.IsDoctorExists(It.IsAny<Doctor>()))
                .Returns(() => true);

            var res = _appointmentService.CreateAppointment(new User(), new Doctor());

            Assert.True(res.IsFailure);
            Assert.Equal("Patient is not exist", res.Error);
        }

        [Fact]
        public void UserIsNotPatient_ShouldFail()
        {
            _userRepositoryMock.Setup(repository => repository.IsUserExists(It.IsAny<User>()))
                .Returns(() => true);

            _doctorRepositoryMock.Setup(repository => repository.IsDoctorExists(It.IsAny<Doctor>()))
                .Returns(() => true);

            var res = _appointmentService.CreateAppointment(new User() { Role = Role.Administrator}, new Doctor());

            Assert.True(res.IsFailure);
            Assert.Equal("User is not a patient", res.Error);
        }

        [Fact]
        public void AppointmentAlreadyExist_ShouldFail()
        {
            List<Appointment> list = new()
            {
                new Appointment()
                {
                    StartTime = new DateTime(2000, 5, 1),
                    EndTime = new DateTime(2001, 5, 1)
                }
            };
            IEnumerable<Appointment> en = list;

            _userRepositoryMock.Setup(repository => repository.IsUserExists(It.IsAny<User>()))
                .Returns(() => true);

            _doctorRepositoryMock.Setup(repository => repository.IsDoctorExists(It.IsAny<Doctor>()))
                .Returns(() => true);

            _appointmentRepositoryMock.Setup(repository => repository.IsAppointmentExist(It.IsAny<Appointment>()))
                .Returns(() => true);

            _appointmentRepositoryMock.Setup(repository => repository.GetActualDates(It.IsAny<Doctor>()))
                .Returns(() => en);

            var res = _appointmentService.CreateAppointment(new User(), new Doctor());

            Assert.True(res.IsFailure);
            Assert.Equal("Appointment is already exists", res.Error);
        }

        [Fact]
        public void InvalidTimeMakeNote_ShouldFail()
        {
            List<Appointment> list = new();
            list.Add(new Appointment());
            IEnumerable<Appointment> en = list;

            _userRepositoryMock.Setup(repository => repository.IsUserExists(It.IsAny<User>()))
                .Returns(() => true);

            _doctorRepositoryMock.Setup(repository => repository.IsDoctorExists(It.IsAny<Doctor>()))
                .Returns(() => true);

            _appointmentRepositoryMock.Setup(repository => repository.IsAppointmentExist(It.IsAny<Appointment>()))
                .Returns(() => false);

            _appointmentRepositoryMock.Setup(repository => repository.GetActualDates(It.IsAny<Doctor>()))
                .Returns(() => en);

            var res = _appointmentService.CreateAppointment(new User(), new Doctor());

            Assert.True(res.IsFailure);
            Assert.Equal("Invalid appointment: Invalid time", res.Error);
        }

        [Fact]
        public void SpecIsNotExist_ShouldFail()
        {
            List<Appointment> list = new()
            {
                new Appointment()
                {
                    StartTime = new DateTime(2000, 5, 1),
                    EndTime = new DateTime(2001, 5, 1)
                }
            };
            IEnumerable<Appointment> en = list;

            _userRepositoryMock.Setup(repository => repository.IsUserExists(It.IsAny<User>()))
                .Returns(() => true);

            _doctorRepositoryMock.Setup(repository => repository.IsDoctorExists(It.IsAny<Doctor>()))
                .Returns(() => true);

            _appointmentRepositoryMock.Setup(repository => repository.IsAppointmentExist(It.IsAny<Appointment>()))
                .Returns(() => false);

            _appointmentRepositoryMock.Setup(repository => repository.GetActualDates(It.IsAny<Specialization>()))
                .Returns(() => en);

            _specializationRepositoryMock.Setup(repository => repository.IsSpecializationExists(It.IsAny<Specialization>()))
                .Returns(() => false);

            var res = _appointmentService.CreateAppointment(new User(), new Specialization());

            Assert.True(res.IsFailure);
            Assert.Equal("Specialization is not exist", res.Error);
        }
    }
}
