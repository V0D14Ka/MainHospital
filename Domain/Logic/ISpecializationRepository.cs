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
        bool IsSpecializationExist(string name);
        bool IsSpecializationExistById(int id);
        Specialization? GetSpecialization(string name);
        bool IsSpecializationExist(Specialization spec);
    }
}
