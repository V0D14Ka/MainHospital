using Domain.Logic;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases
{
    public class DoctorService
    {
        private readonly IDoctorRepository _db;

        public DoctorService(IDoctorRepository db)
        {
            _db = db;
        }

        public Result<Doctor> CreateDoctor(Doctor doctor)
        {
            var result = doctor.IsValid();
            if (result.IsFailure)
                return Result.Fail<Doctor>("Invalid doctor: " + result.Error);

            if (_db.IsDoctorExist(doctor.Id))
                return Result.Fail<Doctor>("Doctor is already exists");

            return _db.CreateDoctor(doctor) ? Result.Ok(doctor) : Result.Fail<Doctor>("Unable to create doctor");
        }

        public Result<Doctor> DeleteDoctor(Doctor doctor)
        {
            if (_db.IsDoctorExist(doctor.Id))
                return _db.DeleteDoctor(doctor) ? Result.Ok(doctor) : Result.Fail<Doctor>("Unable to delete doctor");

            return Result.Fail<Doctor>("Unable to delete doctor");
        }

        public Result<Doctor> GetDoctorByID(int id)
        {
            if (id < 0)
                return Result.Fail<Doctor>("Invalid id");

            var doctor = _db.GetDoctorById(id);

            return doctor != null ? Result.Ok(doctor) : Result.Fail<Doctor>("Unable to find doctor");
        }

        public Result<IEnumerable<Doctor>> GetAllDoctors()
        {
            return Result.Ok(_db.GetAllDoctors());
        }

        public Result<IEnumerable<Doctor>> GetDoctorsBySpecialization(Specialization spec)
        {
            return Result.Ok(_db.GetDoctorsBySpec(spec));
        }
    }
}

