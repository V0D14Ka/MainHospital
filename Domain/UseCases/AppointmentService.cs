using Domain.Logic;
using Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases
{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _db;
        private readonly IDoctorRepository _dbDoctor;
        private readonly IUserRepository _dbUser;
        private readonly ISpecializationRepository _dbSpec;

        public AppointmentService(IAppointmentRepository db, IDoctorRepository dbDoctor, IUserRepository dbUser, ISpecializationRepository dbSpec)
        {
            _db = db;
            _dbDoctor = dbDoctor;
            _dbUser = dbUser;
            _dbSpec = dbSpec;
        }

        public Result<Appointment> CreateAppointment(int patientId, int doctorId = -1, string specialization = "-")
        {
            if(doctorId == -1 && specialization == "-" || doctorId != -1 && specialization != "-")
                return Result.Fail<Appointment>("Need to give one parametr: doctorId or specialization");

            if (doctorId != -1 && _dbDoctor.GetDoctorById(doctorId) == null)
                return Result.Fail<Appointment>("Doctor is not exist");
            
            if(_dbUser.IsUserExistByID(patientId) == false)
                return Result.Fail<Appointment>("Patient is not exist");

            if(_dbUser.GetUserByID(patientId).Role != Role.Patient)
                return Result.Fail<Appointment>("User is not a patient");

            if(specialization != "-" && _dbSpec.IsSpecializationExists(specialization) == false)
                return Result.Fail<Appointment>("Specialization is not exist");

            var date = (doctorId == -1 ? _db.GetActualDates(specialization).First<Appointment>() : _db.GetActualDatesByDoctorID(doctorId).First<Appointment>());
            date.PatientId = patientId;
            
            if (date.IsValid().IsFailure)
            {
                return Result.Fail<Appointment>("Invalid appointment: " + date.IsValid().Error);
            }
            
            if (_db.IsAppointmentExist(date))
                return Result.Fail<Appointment>("Appointment is already exists");

            return _db.MakeNote(date) ? Result.Ok(date) : Result.Fail<Appointment>("Unable to create appointment");
        }

        public Result<IEnumerable<Appointment>> GetActualDates(string specialization)
        {
            return Result.Ok(_db.GetActualDates(specialization));
        }
    }
}
