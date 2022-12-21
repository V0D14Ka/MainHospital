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
    public class SpecializationService
    {
        private readonly ISpecializationRepository _db;

        public SpecializationService(ISpecializationRepository db)
        {
            _db = db;
        }

        public Result<bool> IsSpecExists(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Fail<bool>("Invalid login");

            return Result.Ok(_db.IsSpecializationExist(name));
        }

        public Result<Specialization> Create(Specialization spec)
        {
            var result = spec.IsValid();
            if (result.IsFailure)
                return Result.Fail<Specialization>("Invalid spec: " + result.Error);

            if (_db.IsSpecializationExistById(spec.Id))
                return Result.Fail<Specialization>("Specialization is already exists");

            return _db.Create(spec) ? Result.Ok(spec) : Result.Fail<Specialization>("Unable to create specialization");
        }

        public Result<Specialization> Delete(Specialization spec)
        {
            if (_db.IsSpecializationExistById(spec.Id))
                return _db.Delete(spec.Id) ? Result.Ok(spec) : Result.Fail<Specialization>("Unable to delete specialization");

            return Result.Fail<Specialization>("Unable to delete specialization");
        }

        public Result<Specialization> GetSpecByName(string name)
        {
            if (name == string.Empty)
                return Result.Fail<Specialization>("Invalid name");

            var spec = _db.GetSpecialization(name);

            return spec != null ? Result.Ok(spec) : Result.Fail<Specialization>("Unable to find spec");
        }

        public Result<IEnumerable<Specialization?>> GetAllSpecializations()
        {
            return Result.Ok(_db.GetAll());
        }


        public void Save()
        {
            _db.Save();
        }
    }
}