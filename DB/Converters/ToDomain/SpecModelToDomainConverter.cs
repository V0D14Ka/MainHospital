using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Converters.ToDomain
{
    public static class SpecModelToDomainConverter
    {
        public static Specialization? ToDomain(this SpecializationModel model)
        {
            return new Specialization
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}
