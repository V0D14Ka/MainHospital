﻿using Domain.Logic;
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

        public Result<Appointment> CreateAppointment(User patient, Doctor doctor)
        {

            if (_dbDoctor.IsDoctorExist(doctor) == false)
                return Result.Fail<Appointment>("Doctor is not exist");
            
            if(_dbUser.IsUserExist(patient) == false)
                return Result.Fail<Appointment>("Patient is not exist");

            if(patient.Role != Role.Patient)
                return Result.Fail<Appointment>("User is not a patient");

            var date = _db.GetActualDates(doctor).First<Appointment>();
            date.PatientId = patient.Id;
            
            if (date.IsValid().IsFailure)
            {
                return Result.Fail<Appointment>("Invalid appointment: " + date.IsValid().Error);
            }
            
            if (_db.IsAppointmentExist(date))
                return Result.Fail<Appointment>("Appointment is already exists");

            return _db.MakeNote(date) ? Result.Ok(date) : Result.Fail<Appointment>("Unable to create appointment");
        }

        public Result<Appointment> CreateAppointment(User patient, Specialization spec)
        {

            if (_dbUser.IsUserExist(patient) == false)
                return Result.Fail<Appointment>("Patient is not exist");

            if (patient.Role != Role.Patient)
                return Result.Fail<Appointment>("User is not a patient");

            if (_dbSpec.IsSpecializationExists(spec) == false)
                return Result.Fail<Appointment>("Specialization is not exist");

            var date = _db.GetActualDates(spec).First<Appointment>();
            date.PatientId = patient.Id;

            if (date.IsValid().IsFailure)
            {
                return Result.Fail<Appointment>("Invalid appointment: " + date.IsValid().Error);
            }

            if (_db.IsAppointmentExist(date))
                return Result.Fail<Appointment>("Appointment is already exists");

            return _db.MakeNote(date) ? Result.Ok(date) : Result.Fail<Appointment>("Unable to create appointment");
        }

        public Result<IEnumerable<Appointment>> GetActualDates(Specialization spec)
        {
            return Result.Ok(_db.GetActualDates(spec));
        }
    }
}