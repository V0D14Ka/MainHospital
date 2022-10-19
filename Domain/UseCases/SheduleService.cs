using Domain.Logic;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases
{
    public class SheduleService
    {
        private readonly ISheduleRepository _db;
        private readonly IDoctorRepository _dbDoctor;

        public SheduleService(ISheduleRepository db, IDoctorRepository db2 )
        {
            _db = db;
            _dbDoctor = db2;
        }

        public Result<Shedule> CreateShedule(Shedule shedule)
        {
            var result = shedule.IsValid();
            if (result.IsFailure)
                return Result.Fail<Shedule>("Invalid shedule: " + result.Error);

            return _db.CreateShedule(shedule) ? Result.Ok(shedule) : Result.Fail<Shedule>("Unable to create shedule");
        }

        public Result<Shedule> UpdateShedule(Shedule shedule, Shedule updatedShedule)
        {
            var result = updatedShedule.IsValid();
            if (result.IsFailure)
                return Result.Fail<Shedule>("Invalid new shedule: " + result.Error);

            return _db.UpdateShedule(shedule, updatedShedule) ? Result.Ok(updatedShedule) : Result.Fail<Shedule>("Unable to update shedule");
        }

        public Result<Shedule> GetSheduleByDoctorAndDate(Doctor doc, DateTime date)
        {
            if (!_dbDoctor.IsDoctorExist(doc.Id))
                return Result.Fail<Shedule>("Doctor is not exist");
            var shed = _db.GetSheduleByDoctorAndDate(doc,date);

            return shed != null ? Result.Ok(shed) : Result.Fail<Shedule>("Unable to find shedule");
        }
    }
}
