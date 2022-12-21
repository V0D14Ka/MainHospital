using Domain.Logic;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases
{
    public class DoctorService
    {
        private readonly IDoctorRepository _db;
        private readonly ISpecializationRepository _dbSpec;

        public DoctorService(IDoctorRepository db, ISpecializationRepository db2)
        {
            _db = db;
            _dbSpec = db2;
        }

        public Result<Doctor> CreateDoctor(Doctor doctor)
        {
            var result = doctor.IsValid();
            if (result.IsFailure)
                return Result.Fail<Doctor>("Invalid doctor: " + result.Error);

            if (_db.IsDoctorExist(doctor.Id))
                return Result.Fail<Doctor>("Doctor is already exists");

            return _db.Create(doctor) ? Result.Ok(doctor) : Result.Fail<Doctor>("Unable to create doctor");
        }

        public Result<Doctor> DeleteDoctor(Doctor doctor)
        {
            if (_db.IsDoctorExist(doctor.Id))
                return _db.Delete(doctor.Id) ? Result.Ok(doctor) : Result.Fail<Doctor>("Unable to delete doctor");

            return Result.Fail<Doctor>("Unable to delete doctor");
        }

        public Result<Doctor> GetDoctorByID(int id)
        {
            if (id < 0)
                return Result.Fail<Doctor>("Invalid id");

            var doctor = _db.GetDoctorById(id);

            return doctor != null ? Result.Ok(doctor) : Result.Fail<Doctor>("Unable to find doctor");
        }

        public Result<IEnumerable<Doctor?>> GetAllDoctors()
        {
            return Result.Ok(_db.GetAll());
        }

        public Result<IEnumerable<Doctor?>> GetDoctorsBySpecialization(int specid)
        {
            if (specid.ToString() == string.Empty)
                return Result.Fail<IEnumerable<Doctor?>>("Invalid specialization id");

            if (_dbSpec.IsSpecializationExistById(specid))
                return Result.Ok(_db.GetDoctorsBySpec(specid));

            return Result.Fail<IEnumerable<Doctor?>>("Specialization is not exist");
        }

        public Result<bool> IsDoctorExists(int id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
                return Result.Fail<bool>("Invalid login");

            return Result.Ok(_db.IsDoctorExist(id));
        }

        public void Save()
        {
            _db.Save();
        }
    }
}

