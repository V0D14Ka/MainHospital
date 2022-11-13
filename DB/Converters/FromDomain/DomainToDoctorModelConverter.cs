using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Converters.FromDomain
{
    public static class DomainToDoctorModelConverter
    {
        public static DoctorModel? ToDoctorModel(this Doctor model)
        {
            return new DoctorModel
            {
                Id = model.Id,
                Name = model.Name,
                Specialization = model.Specialization.ToSpecModel()
            };
        }
    }
}
