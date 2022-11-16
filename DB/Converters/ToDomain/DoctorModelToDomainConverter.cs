using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Converters.ToDomain
{
    public static class DoctorModelToDomainConverter
    {
        public static Doctor? ToDomain(this DoctorModel model)
        {
            return new Doctor
            {
                Id = model.Id,
                Name = model.Name,
                Specialization = model.Specialization.ToDomain()
            };
        }
    }
}
