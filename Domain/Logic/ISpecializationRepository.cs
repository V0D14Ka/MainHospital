using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logic
{
    public interface ISpecializationRepository : IRepository<Specialization>
    {
        bool IsSpecializationExists(string name);
        bool IsSpecializationExistsById(int id);
        Specialization? GetSpecialization(string name);
        Specialization? GetSpecializationById(int id);
        IEnumerable<Specialization> GetAllSpecializations();
    }
}
