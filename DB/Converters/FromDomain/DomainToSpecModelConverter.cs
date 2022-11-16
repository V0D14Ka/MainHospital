using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Converters.FromDomain
{
    public static class DomainToSpecModelConverter
    {
        public static SpecializationModel? ToSpecModel(this Specialization model)
        {
            return new SpecializationModel
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}
